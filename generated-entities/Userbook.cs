using System;
using System.Collections.Generic;

namespace PersonalLibrary.Server
{
    public partial class Userbook
    {
        public int Userbook1 { get; set; }
        public int? Userid { get; set; }
        public int? Bookid { get; set; }
        public int? Rate { get; set; }
        public string Note { get; set; }
        public bool? Readdone { get; set; }
        public string Place { get; set; }

        public Book Book { get; set; }
        public Useraccess User { get; set; }
    }
}
