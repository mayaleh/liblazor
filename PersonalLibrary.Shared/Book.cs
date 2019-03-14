using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalLibrary.Shared
{
    public partial class Book
    {
        /*public Book()
        {
            Userbook = new HashSet<UserBook>();
        }

        public int Bookid { get; set; }
        public int? Authorid { get; set; }
        [Required]
        public string Name { get; set; }
        public string About { get; set; }

        //[ForeignKey("Authorid")]
        [ForeignKey(nameof(Authorid))]
        public Author Author { get; set; }
        public ICollection<UserBook> Userbook { get; set; }
        */
        public int Bookid { get; set; }
        public int? Authorid { get; set; }

        [Required]
        public string Name { get; set; }

        public string About { get; set; }

        public int Rate { get; set; }

        public string Note { get; set; }

        public bool Readdone { get; set; }

        public string Place { get; set; }

        public Author Author { get; set; }
    }
}
