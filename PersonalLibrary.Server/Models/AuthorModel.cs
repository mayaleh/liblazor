using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalLibrary.Server.Models
{
    public class AuthorModel
    {
        AuthorContext db = new AuthorContext();

        //To Get all Authors
        public IEnumerable<Author> GetAllAuthors()
        {
            try
            {
                return db.Author.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new Author     
        public void AddAuthor(Author author)
        {
            try
            {
                db.Author.Add(author);
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
    }
}
