using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLibrary.Shared.Model
{
    public class UserState
    {
        public bool IsLoggedIn { get; set; }

        public string FullName { get; set; }

        public string LoginName { get; set; }

        public string Email { get; set; }
    }
}
