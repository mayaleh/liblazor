using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLibrary.Shared
{
    public class Book
    {
        private int _authorId { get; set; }
        public string Name { get; set; }
        public Author Author { get; set; }
        public string About { get; set; }
        public string Place { get; set; }
    }
}
