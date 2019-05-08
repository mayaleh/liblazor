using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyLibraryOverview.Shared
{
    public class UserRegistration
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }
        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
        [Required]
        [JsonProperty("realName")]
        public string RealName { get; set; }
        [Required]
        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}
