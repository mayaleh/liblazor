using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Priv = MyLibraryOverview.Server.Models.Entities;
using Publ = MyLibraryOverview.Shared;

namespace MyLibraryOverview.Server.Services
{
    public class EntitiyTranslator
    {
        public Publ.Book ToClientBook(Priv.Book book)
        {
            return new Shared.Book()
            {
                Bookid = book.Bookid,
                Authorid = book.Authorid.GetValueOrDefault(),
                Name = book.Name,
                About = book.About,
                Author = new Shared.Author()
                {
                    Authorid = book.Author.Authorid,
                    Name = book.Author.Name,
                    About = book.Author.About,
                },
                AuthorName = book.Author.Name,
                AuthorAbout = book.Author.About,
            };
        }

        //TODO 
        public Priv.Book ToServertBook(Publ.Book book)
        {
            return new Priv.Book()
            {
                Bookid = book.Bookid,
                Authorid = book.Authorid,
                Name = book.Name,
                About = book.About,
                Author = new Priv.Author()
                {
                    Authorid = book.Authorid,
                    Name = book.AuthorName,
                    About = book.AuthorAbout,
                },
            };
        }

        public Publ.Book ToClientBookUser(Priv.UserBook ub)
        {
            return new Shared.Book()
            {
                Bookid = ub.BookId,
                Authorid = ub.Book.Authorid.GetValueOrDefault(),
                Name = ub.Book.Name,
                About = ub.Book.About,
                Note = ub.Note,
                Place = ub.Place,
                Rate = ub.Rate.GetValueOrDefault(),
                Readdone = ub.Readdone.GetValueOrDefault(),
                Author = new Shared.Author()
                {
                    Authorid = ub.Book.Author.Authorid,
                    Name = ub.Book.Author.Name,
                    About = ub.Book.Author.About,
                },
                AuthorName = ub.Book.Author.Name,
                AuthorAbout = ub.Book.Author.About,
            };
        }


        public Priv.UserBook ToServerUserBook(Publ.Book book)
        {
            return new Priv.UserBook()
            {
                BookId = book.Bookid,
                Note = book.Note,
                Place = book.Place,
                Rate = book.Rate,
                Readdone = book.Readdone,
                Book = ToServertBook(book),
            };
        }

    }
}
