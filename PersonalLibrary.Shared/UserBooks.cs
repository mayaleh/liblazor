using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*
 * https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/existing-database
 * https://docs.microsoft.com/cs-cz/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application
 */
namespace PersonalLibrary.Shared
{
    class UserBooks
    {
        [Key]
        public int UserBooksId { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int Bookid { get; set; }

        [Required]
        [ForeignKey("UserAccess")]
        public int Userid { get; set; }


        [ForeignKey(nameof(Bookid))]
        public List<Book> Book { get; set; }

        [ForeignKey(nameof(Userid))]
        public List<UserAccess> UserAccesses { get; set; }


        public string Note { get; set; }

        public bool Read { get; set; }
    }
}
