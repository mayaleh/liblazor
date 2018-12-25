using PersonalLibrary.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalLibrary.Server.Models
{
    public class BookModel
    {
        BookContext db = new BookContext();
        public BookModel()
        {
            try
            {
                //db.
            }
            catch (Exception)
            {

                throw;
            }
        }

        //To Get all Books
        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                return db.Book.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new Book     
        public void AddBook(Book book)
        {
            try
            {
                db.Book.Add(book);
                db.SaveChanges();
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
                Book book = db.Book.Find(bookId);
                return book;
            }
            catch
            {
                throw;
            }
        }
    }
}
