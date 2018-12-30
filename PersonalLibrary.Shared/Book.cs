﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalLibrary.Shared
{
    public partial class Book
    {
        public int Bookid { get; set; }
        public int? Authorid { get; set; }
        [Required]
        public string Name { get; set; }
        public string About { get; set; }
        public string Place { get; set; }

        //[ForeignKey("Authorid")]
        [ForeignKey(nameof(Authorid))]
        public Author Author { get; set; }
    }
}