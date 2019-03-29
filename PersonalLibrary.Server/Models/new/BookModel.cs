using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Server.Models.Entities;

namespace PersonalLibrary.Server.Models.New
{
    public class BookModel
    {
        private ApplicationDBContext DBContext { get; }

        public BookModel(ApplicationDBContext dBContext)
        {
            DBContext = dBContext;
        }

        #region Get Data

        #region Public
            //To Get all Books (Public data)
            public List<Book> GetAllBooks()
            {
                try
                {
                    //var v = from p in db.Book join a in db.Author on p.Authorid equals a.Authorid;

                    return DBContext.Book
                        .Include(d => d.Author) // works  - error on Client site
                        .OrderBy(d => d.Name)
                        .ToList(); 
                    
                        //.ToList(); // neni nutne
                }
                catch
                {
                    throw;
                }
            }

            //Get authors and theirs books
            public List<Author> GetAllBooksByAuthor()
            {
                try
                {
                    return DBContext.Author
                        .Include(p => p.Book) // Error on client site
                        .ToList();
                }
                catch
                {
                    throw;
                }
            }
        
            //Get the one Book by ID    
            public Book GetBook(int bookId)
            {
                try
                {
                    Book book = DBContext.Book
                        .Where(b => b.Bookid == bookId)
                        .Include(d => d.Author)
                        .SingleOrDefault(); // Expect exactly one row. Better use SingleOrDefaut for accept null result
                        //.Find(bookId);
                        //.Include(d => d.Author);
                    return book;
                }
                catch
                {
                    throw;
                }
            }

            public Author GetAuthor(int authorId)
            {
                try
                {
                    Author author = DBContext.Author.Find(authorId);
                    return author;
                }
                catch
                {
                    throw;
                }
            }

        public Book FindBook(Book book)
        {
            return DBContext.Book
                .Where(b => (b.Name.ToUpper() == book.Name.ToUpper() && b.Authorid == book.Authorid))
                .SingleOrDefault();
        }
        #endregion

        #region Protected by Identity

        public List<UserBook> GetUsersBooks(string userId)
            {
                var result = DBContext.UserBook
                    .Where(b => b.UserId == userId) //TODO doesnot work???
                    .Include(d => d.Book)
                    .Include(d => d.Book.Author)
                    .ToList();
                return result;
            }

        public UserBook GetOneUserBook(string userId, int bookId)
        {
            return DBContext.UserBook
                .Where(ub => (ub.BookId == bookId && ub.UserId == userId))
                .FirstOrDefault();
        }

        #endregion

        #endregion

        #region Save actions


        /// <summary>
        /// On call action Save by loged in user
        /// </summary>
        public void SaveBook(UserBook userBook)
        {
            //var book = userBook.Book;
            var book = FindBook(userBook.Book) ?? _addNewBook(userBook.Book);
            userBook.BookId = book.Bookid;
            var author = book.Author;
        
            var ubExist = GetOneUserBook(userBook.UserId, userBook.BookId);
            try
            {
                if(book.Bookid != 0)
                {
                    if (ubExist == null)
                    {
                        this._addReferenceUserBook(userBook);
                    }
                    else
                    {
                        this._updateReferenceUserBook(userBook);
                    }
                }
                    
            }
            catch(Exception e)
            {
                
            }
        }

        /// <summary>
        /// Add new record to table Book.
        /// If Author doesnt exist, add new record to table Author and get reference for Book table
        /// </summary>
        private Book _addNewBook(Book book)
        {
            int? _authorId = null;
            if (!string.IsNullOrEmpty(book.Author.Name))
            {
                //find author if doesnt exist insert them and get his id
                Author author = DBContext.Author
                    .Where(t => t.Name.ToUpper() == book.Author.Name.ToUpper())
                    .FirstOrDefault(); // SingleOrDefault - when expect only one or zero. Without "OrDefault" will catch throw Exeption, couse zero is not allowed
                if (author == null)
                {
                    Author newAuthor = new Author()
                    {
                        Name = book.Author.Name
                    };
                    DBContext.Author.Add(newAuthor);
                    DBContext.SaveChanges();
                    _authorId = (int?)newAuthor.Authorid; // last inserted Id
                }
                else
                {
                    _authorId = (int?)author.Authorid;
                }
            }
            book.Authorid = _authorId;

            if (book.Bookid != 0)
            {
                DBContext.Entry(book).State = EntityState.Modified;
            }
            else
            {
                DBContext.Book.Add(book);
            }
            DBContext.SaveChanges();
            return book;
        }

        /// <summary>
        /// Add new record to table usersbook if Book record exist. (references table [user <=> book] many has many).
        /// </summary>
        private void _addReferenceUserBook(UserBook userbook)
        {
            if (userbook.BookId != 0 && !string.IsNullOrEmpty(userbook.UserId))
            {
                DBContext.UserBook.Add(userbook);
                DBContext.SaveChanges();
            }

        }

        /// <summary>
        /// Update record to table usersbook if reference UserBook record exist. (references table [user <=> book] many has many).
        /// </summary>
        private void _updateReferenceUserBook(UserBook userBook)
        {
            try
            {
                if (userBook.UserbookId != 0)
                {
                    DBContext.Entry(userBook).State = EntityState.Modified;
                    DBContext.SaveChanges();
                }
            }
            catch (DbUpdateException ue)
            {

            }
            catch (Exception ex)
            { }
        }
        #endregion

        //To Delete book  
        /*
        public void DeleteBook(int id)
        {
            try
            {
                Book book = db.Book
                    .Where(b => b.Bookid == id)
                    .Single(); // db.Book.Find(id);
                db.Book.Remove(book);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        */
    }
}
