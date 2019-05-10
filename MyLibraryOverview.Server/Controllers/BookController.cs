using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLibraryOverview.Server.Models.Entities;
using MyLibraryOverview.Server.Services;
using System;
using System.Collections.Generic;
using Model = MyLibraryOverview.Server.Models.New;

namespace MyLibraryOverview.Server.Controllers
{
    using importerResult = MyLibraryOverview.Shared.Library.Rop.Result<object, Exception>;

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : AppBaseController
    {
        private readonly Model.BookModel bookModel;
        private readonly Model.AuthorModel authorModel;
        private readonly EntitiyTranslator etranslator;


        public BookController
            (
                Model.BookModel bookModel,
                Model.AuthorModel authorModel,
                EntitiyTranslator etranslator,
                SignInManager<UserAppIdentity> signInManager,
                UserManager<UserAppIdentity> userManager
            ) : base(signInManager, userManager)
        {
            this.bookModel = bookModel;
            this.authorModel = authorModel;
            this.etranslator = etranslator;
        }


        #region Public API methods
        [HttpGet("[action]")]
        public List<Shared.Book> GetAll()
        {
            List<Shared.Book> result = new List<Shared.Book>();
            List<Book> data = bookModel.GetAllBooks();
            foreach (Book item in data)
            {
                result.Add(etranslator.ToClientBook(item));
            }
            return result;
        }

        #endregion

        #region Protected API Methods by user identity

        [Authorize]
        [HttpGet("[action]")]
        public List<Shared.Book> GetUserBooks()
        {
            var userId = GetUserId().Result;
            List<UserBook> data = bookModel.GetUsersBooks(userId);
            List<Shared.Book> result = new List<Shared.Book>();
            foreach (var item in data)
            {
                result.Add(
                    etranslator.ToClientBookUser(item)
                    //new Shared.Book()
                    //{
                    //    Bookid = item.BookId,
                    //    Authorid = item.Book.Authorid,
                    //    Name = item.Book.Name,
                    //    About = item.Book.About,
                    //    Note = item.Note,
                    //    Place = item.Place,
                    //    Rate = item.Rate.GetValueOrDefault(),
                    //    Readdone = item.Readdone.GetValueOrDefault(),
                    //    Author = new Shared.Author()
                    //    {
                    //        Authorid = item.Book.Author.Authorid,
                    //        Name = item.Book.Author.Name,
                    //    },
                    //}
                    );
            }
            return result;
        }



        [Authorize]
        [HttpPost("[action]")]
        public object AddBook([FromBody] Shared.Book book)
        {
            if (ModelState.IsValid)
            {
                var userBook = etranslator.ToServerUserBook(book);
                userBook.UserId = GetUserId().Result;
                var serverBook = userBook.Book;
                var author = serverBook.Author;


                var updatedAuthor = authorModel.SaveAuthor(author);
                serverBook.Authorid = updatedAuthor.Authorid;
                serverBook.Author = updatedAuthor;
                userBook.Book = serverBook;

                var operationResult = bookModel.SaveBook(userBook);
                if (operationResult.IsFailure) return importerResult.Failed(operationResult.Failure);
                return importerResult.Succeeded(etranslator.ToClientBook(userBook.Book));
            }

            return importerResult.Failed(new Exception("Model is invalid!"));
        }


        [Authorize]
        [HttpPost("[action]")]
        public object EditBook([FromBody] Shared.Book book)
        {
            if (ModelState.IsValid)
            {
                var userBook = etranslator.ToServerUserBook(book);
                userBook.UserId = GetUserId().Result;
                var serverBook = userBook.Book;
                userBook.Book = serverBook;

                var operationResult = bookModel.SaveBook(userBook);
                if (operationResult.IsFailure) return importerResult.Failed(operationResult.Failure);
                return importerResult.Succeeded(etranslator.ToClientBook(userBook.Book));
            }

            return importerResult.Failed(new Exception("Model is invalid!"));
        }


        [Authorize]
        [HttpPost("[action]")]
        public object DeleteBook([FromBody] Shared.Book book)
        {
            if (ModelState.IsValid)
            {
                var userBook = etranslator.ToServerUserBook(book);
                userBook.UserId = GetUserId().Result;

                var operationResult = bookModel.DeleteBook(userBook);
                if (operationResult.IsFailure) return importerResult.Failed(operationResult.Failure);
                return importerResult.Succeeded(book);
            }

            return importerResult.Failed(new Exception("Model is invalid!"));
        }


        #endregion

    }


}