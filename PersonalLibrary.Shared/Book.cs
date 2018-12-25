﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLibrary.Shared
{
    public partial class Book
    {
        public int Bookid { get; set; }
        public int? Authorid { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Place { get; set; }

        public Author Author { get; set; }
    }
}
