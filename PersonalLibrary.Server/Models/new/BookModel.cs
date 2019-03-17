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
                        .Single(); // Expect exactly one row. Better use SingleOrDefaut for accept null result
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

        #endregion

        #endregion

        #region Save actions

        //todo Get user Id
        //TODO accept Object Userbook with sets Book And UserAccess for saving all references
        /// <summary>
        /// On call action Save by loged in user
        /// </summary>
        public void SaveBook(UserBook userBook)
        {
            var book = userBook.Book;

            var author = book.Author;

            //TODO find author if exist
            //TODO find book if exist
            //TODO find userBook if book and userBook and author exist
            try
            {
                //if book is set and exist, create only reference to user and book
                if (book.Bookid != 0)
                {
                    //this._addReferenceUserBook();
                }
                //else first create the new book, then create reference
                else
                {
                    //Book newRecord = this._addNewBook(book);
                    //this._addReferenceUserBook();
                }
                    
            }
            catch
            {
                throw;
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
            //int bookId = userbook.Bookid.GetValueOrDefault();
            //int userId = userbook.Userid.GetValueOrDefault();

            /*
            Book existingBook = db.Book.Where(b => b.Bookid == bookId).FirstOrDefault();

            if (existingBook == null || userId == 0)
            {
                return;
            }
            */

            //TODO save


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
