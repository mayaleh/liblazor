using System;
using System.Collections.Generic;

namespace MyLibraryOverview.Server.Models.Entities
{
    public partial class Book
    {
        public Book()
        {
            UserBook = new HashSet<UserBook>();
        }

        public int Bookid { get; set; }
        public int? Authorid { get; set; }
        public string Name { get; set; }
        public string About { get; set; }

        public Author Author { get; set; }
        public ICollection<UserBook> UserBook { get; set; }
    }
}
