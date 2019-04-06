using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyLibraryOverview.Shared
{
    public partial class Book
    {
        public Book()
        {
            Author = new Author();
        }

        public int Bookid { get; set; }

        [Required]
        public string Name { get; set; }

        public string About { get; set; }

        public int Rate { get; set; }

        public string Note { get; set; }

        public bool Readdone { get; set; }

        public string Place { get; set; }

        public Author Author { get; set; }

        public int Authorid { get; set; }

        public string AuthorName { get; set; }

        public string AuthorAbout { get; set; }
    }
}
