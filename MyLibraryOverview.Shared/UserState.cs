using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibraryOverview.Shared
{
    public class UserState
    {
        public bool IsLoggedIn { get; set; }

        public string FullName { get; set; }

        public string LoginName { get; set; }

        public string Email { get; set; }
    }
}
