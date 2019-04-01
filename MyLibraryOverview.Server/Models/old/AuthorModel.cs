using Microsoft.EntityFrameworkCore;
using MyLibraryOverview.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibraryOverview.Server.Models
{
    public class AuthorModel
    {
        AuthorContext db = new AuthorContext();

        //To Get all Authors
        public List<Author> GetAllAuthors()
        {
            try
            {
                return db.Author
                    //.Include(d => d.Book)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }


        //To Get all Authors
        public IEnumerable<Author> GetAllAuthorsBooks()
        {
            try
            {
                return db.Author
                    .Include(Book => Book)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Get all Authors


        //To Add or Edit   
        public void SaveAuthor(Author author)
        {
            try
            {
               
                if (author.Authorid != 0)
                { //edit
                    db.Entry(author).State = EntityState.Modified;
                }
                else
                { //add
                    Author authorCheck = db.Author
                            .Where(t => t.Name.ToUpper() == author.Name.ToUpper())
                            .FirstOrDefault(); // SingleOrDefault - when expect only one or zero. Without "OrDefault" will catch throw Exeption, couse zero is not allowed


                    if (authorCheck == null)
                    { // if doesnt exist add
                        db.Author.Add(author);
                    }
                    else
                    { //else exist
                        return;
                    }
                }
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //Get the one Author by ID    
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

        //To Delete author - Dont allow 
        public void DeleteAuthor(int id)
        {
            return;
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
    }
}
