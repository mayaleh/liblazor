using System;
using System.Collections.Generic;

namespace PersonalLibrary.Server
{
    public partial class Book
    {
        public Book()
        {
            Userbook = new HashSet<Userbook>();
        }

        public int Bookid { get; set; }
        public int? Authorid { get; set; }
        public string Name { get; set; }
        public string About { get; set; }

        public Author Author { get; set; }
        public ICollection<Userbook> Userbook { get; set; }
    }
}
