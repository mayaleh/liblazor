﻿using System.Collections.Generic;

namespace MyLibraryOverview.Server.Models.Entities
{
    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        public int Authorid { get; set; }
        public string Name { get; set; }
        public string About { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}
