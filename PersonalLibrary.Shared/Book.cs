using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalLibrary.Shared
{
    public partial class Book
    {
        public Book()
        {
            Userbook = new HashSet<Userbook>();
        }

        public int Bookid { get; set; }
        public int? Authorid { get; set; }
        [Required]
        public string Name { get; set; }
        public string About { get; set; }

        //[ForeignKey("Authorid")]
        [ForeignKey(nameof(Authorid))]
        public Author Author { get; set; }
        public ICollection<Userbook> Userbook { get; set; }
    }
}
