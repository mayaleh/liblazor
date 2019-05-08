using Newtonsoft.Json;

namespace MyLibraryOverview.Shared
{
    public class UserState
    {

        [JsonProperty("isLoggedIn")]
        public bool IsLoggedIn { get; set; }


        [JsonProperty("fullName")]
        public string FullName { get; set; }


        [JsonProperty("loginName")]
        public string LoginName { get; set; }


        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
