using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyLibraryOverview.Shared
{
    public class UserLogin
    {
        [Required]
        [JsonRequired]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [Required]
        [JsonRequired]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
