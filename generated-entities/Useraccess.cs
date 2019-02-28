using System;
using System.Collections.Generic;

namespace PersonalLibrary.Server
{
    public partial class Useraccess
    {
        public Useraccess()
        {
            Userbook = new HashSet<Userbook>();
        }

        public int Userid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public ICollection<Userbook> Userbook { get; set; }
    }
}
