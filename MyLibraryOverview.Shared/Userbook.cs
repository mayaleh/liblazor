using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace MyLibraryOverview.Shared
{
    public partial class UserBook
    {
        public int UserbookId { get; set; }

        //[Required]
        //public int? Userid { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        [Required]
        public int? BookId { get; set; }
        public int? Rate { get; set; }
        public string Note { get; set; }
        public bool? Readdone { get; set; }
        public string Place { get; set; }
        
        public Book Book { get; set; }
    }
}
