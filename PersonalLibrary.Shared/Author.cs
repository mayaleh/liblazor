﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLibrary.Shared
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
