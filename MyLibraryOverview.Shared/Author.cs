using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyLibraryOverview.Shared
{

    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        public int Authorid { get; set; }
        [Required]
        public string Name { get; set; }
        public string About { get; set; }

        public ICollection<Book> Book { get; set; }
        //public List<Book> Book { get; set; }
    }
}
