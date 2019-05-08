using Newtonsoft.Json;

namespace MyLibraryOverview.Shared
{
    public class RegistrationResult
    {

        [JsonProperty("code")]
        public string Code { get; set; }


        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
