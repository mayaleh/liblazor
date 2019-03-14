using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.Server.Models;
using PersonalLibrary.Server.Models.Entities;
using Shared = PersonalLibrary.Shared;
using Model = PersonalLibrary.Server.Models.New;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace PersonalLibrary.Server.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : AppBaseController
    {
        private readonly Model.BookModel bookModel;
        //private BookModel _book = new BookModel();

        public BookController
            (
                Model.BookModel bookModel,
                SignInManager<UserAppIdentity> signInManager,
                UserManager<UserAppIdentity> userManager
            ) : base(signInManager, userManager)
        {
            this.bookModel = bookModel;
        }

        
        

        #region Public API methods
        [HttpGet("[action]")]
        public List<Book> GetAll()
        {
            List<Shared.Book> result = new List<Shared.Book>();
            List<Book> data = bookModel.GetAllBooks();
            foreach (Book item in data)
            {
                result.Add(
                        new Shared.Book()
                        {
                            Bookid = item.Bookid,
                            Authorid = item.Authorid,
                            Name = item.Name,
                            About = item.About,
                            Author = new Shared.Author()
                            {
                                Authorid = item.Author.Authorid,
                                Name = item.Author.Name,
                            },
                        }
                        );
            }
            return data;
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
            foreach(var item in data)
            {
                result.Add(
                         new Shared.Book()
                         {
                             Bookid = item.BookId,
                             Authorid = item.Book.Authorid,
                             Name = item.Book.Name,
                             About = item.Book.About,
                             Note = item.Note,
                             Place = item.Place,
                             Rate = item.Rate.GetValueOrDefault(),
                             Readdone = item.Readdone.GetValueOrDefault(),
                             Author = new Shared.Author()
                             {
                                 Authorid = item.Book.Author.Authorid,
                                 Name = item.Book.Author.Name,
                             },
                         }
                    );
            }
            return result;
        }

        #endregion

    }


}