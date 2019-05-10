using Microsoft.EntityFrameworkCore;
using MyLibraryOverview.Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibraryOverview.Server.Models.New
{
    using importResult = Library.Rop.Result<int, Exception>;
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
        public importResult SaveBook(UserBook userBook)
        {
            //var book = userBook.Book;
            var book = FindBook(userBook.Book) ?? AddNewBook(userBook.Book);
            userBook.BookId = book.Bookid;
            var author = book.Author;

            var ubExist = GetOneUserBook(userBook.UserId, userBook.BookId);
            try
            {
                if (book.Bookid != 0)
                {
                    if (ubExist == null)
                    {
                        return this.AddReferenceUserBook(userBook);
                    }
                    else
                    {
                        ubExist.Readdone = userBook.Readdone;
                        ubExist.Note = userBook.Note;
                        ubExist.Place = userBook.Place;
                        ubExist.Rate = userBook.Rate;
                        return this.UpdateReferenceUserBook(ubExist);
                    }
                }
                else
                {
                    return importResult.Failed(new Exception("Could not create or update data. Foreign key do not relate to any book key!"));
                }

            }
            catch (Exception e)
            {
                return importResult.Failed(e);
            }
        }

        /// <summary>
        /// Add new record to table Book.
        /// If Author doesnt exist, add new record to table Author and get reference for Book table
        /// </summary>
        private Book AddNewBook(Book book)
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
        private importResult AddReferenceUserBook(UserBook userbook)
        {
            if (userbook.BookId != 0 && !string.IsNullOrEmpty(userbook.UserId))
            {
                DBContext.UserBook.Add(userbook);
                DBContext.SaveChanges();
                return importResult.Succeeded(1);
            }
            return importResult.Failed(new Exception("Foreign key do not relate to any book key!"));

        }

        /// <summary>
        /// Update record to table usersbook if reference UserBook record exist. (references table [user <=> book] many has many).
        /// </summary>
        private importResult UpdateReferenceUserBook(UserBook userBook)
        {
            try
            {
                if (userBook.UserbookId != 0)
                {
                    DBContext.Entry(userBook).State = EntityState.Modified;
                    DBContext.SaveChanges();
                    return importResult.Succeeded(1);
                }
                return importResult.Failed(new Exception("Id of User Book is empty!"));
            }
            catch (DbUpdateException ue)
            {
                return importResult.Failed(ue);
            }
            catch (Exception ex)
            {
                return importResult.Failed(ex);
            }
        }



        /// <summary>
        /// Delete record from table usersbook if reference UserBook record exist.
        /// </summary>
        public importResult DeleteBook(UserBook userBook)
        {
            try
            {
                UserBook uBook = DBContext.UserBook
                    .Where(b => b.UserId == userBook.UserId && b.BookId == userBook.BookId)
                    .Single(); // db.Book.Find(id);
                DBContext.UserBook.Remove(uBook);
                DBContext.SaveChanges();
                return importResult.Succeeded(1);
            }
            catch (Exception e)
            {
                return importResult.Failed(e);
            }
        }
        #endregion


    }
}
