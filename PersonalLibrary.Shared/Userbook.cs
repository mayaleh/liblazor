using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.Shared
{
    public partial class Userbook
    {
        public int Userbookid { get; set; }

        [Required]
        public int? Userid { get; set; }
        [Required]
        public int? Bookid { get; set; }
        public int? Rate { get; set; }
        public string Note { get; set; }
        public bool? Readdone { get; set; }
        public string Place { get; set; }

        public Book Book { get; set; }
        public UserAccess User { get; set; }
    }
}
