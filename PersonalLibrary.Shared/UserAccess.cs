﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.Shared
{
    public partial class UserAccess
    {
        public UserAccess()
        {
            Userbook = new HashSet<Userbook>();
        }

        [Key]
        public int Userid { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }


        public ICollection<Userbook> Userbook { get; set; }
    }
}
