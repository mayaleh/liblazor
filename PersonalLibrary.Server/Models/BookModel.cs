using PersonalLibrary.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PersonalLibrary.Server.Models
{
    public class BookModel
    {
        BookContext db = new BookContext();

        //To Get all Books
        public List<Book> GetAllBooks()
        {
            try
            {
                //var v = from p in db.Book join a in db.Author on p.Authorid equals a.Authorid;

                return db.Book
                    .Include(d => d.Author) // works  - error on Client site
                    .OrderBy(d => d.Name)
                    .ToList<Book>(); 
                    
                    //.ToList(); // neni nutne
            }
            catch
            {
                throw;
            }
        }

        public List<Author> GetAllBooksByAuthor()
        {
            try
            {
                return db.Author
                    .Include(p => p.Book) // Error on client site
                    .ToList<Author>();
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

        public Author GetAuthor(int authorId)
        {
            try
            {
                Author author = db.Author.Find(authorId);
                return author;
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
                int? _authorId = null;
                if (!string.IsNullOrEmpty(book.Author.Name))
                {
                    //find author if doesnt exist insert them and get his id
                    Author author = db.Author.First(t => t.Name.ToUpper() == book.Author.Name.ToUpper());
                    if (author == null)
                    {
                        Author newAuthor = new Author()
                        {
                            Name = book.Author.Name
                        };
                        db.Author.Add(newAuthor);
                        db.SaveChanges();
                        _authorId = newAuthor.Authorid; // last inserted Id
                    }
                    else
                    {
                        _authorId = author.Authorid;
                    }
                }
                book.Authorid = _authorId;
                db.Book.Add(book);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
