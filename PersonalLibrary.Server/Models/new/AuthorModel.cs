using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Server.Models.Entities;

namespace PersonalLibrary.Server.Models.New
{
    public class AuthorModel
    {
        private readonly ApplicationDBContext DBContext;

        public AuthorModel(ApplicationDBContext dBContext)
        {
            DBContext = dBContext;
        }


        public Author GetAuthorById(int id)
        {
            return DBContext.Author
                .Where(a => a.Authorid == id)
                .FirstOrDefault();
        }

        public Author FindByName(string name)
        {
            return DBContext.Author
                .Where(a => a.Name.ToUpper() == name.ToUpper())
                .FirstOrDefault();
        }

        public void SearchOn()
        {

        }

        public Author SaveAuthor(Author author)
        {
            if (author.Authorid > 0)
            {
                var foundAuthor = GetAuthorById(author.Authorid);
                if (foundAuthor != null)
                {
                    return foundAuthor; // dont update
                    //return this._updateAuthor(author);
                }
                else
                {
                    throw new KeyNotFoundException("Author with ID: " + author.Authorid.ToString() + " not found to update");
                }
            }
            else
            {
                var foundAuthor = FindByName(author.Name);

                if (foundAuthor != null)
                {
                    return foundAuthor; //dont update
                    //return this._updateAuthor(author);
                }
                else
                {
                    return this._createAuthor(author);
                }

            }
        }


        private Author _createAuthor(Author author)
        {
            DBContext.Author.Add(author);
            DBContext.SaveChanges();
            int _authorId = (int)author.Authorid;
            return GetAuthorById(_authorId);
        }

        private Author _updateAuthor(Author author)
        {
            //TODO
            DBContext.Entry(author).State = EntityState.Modified;
            DBContext.SaveChanges();
            return author;
        }


    }
}
