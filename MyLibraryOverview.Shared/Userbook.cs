using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace MyLibraryOverview.Shared
{
    public partial class UserBook
    {
        [JsonProperty("userbookId")]
        public int UserbookId { get; set; }

        //[Required]
        //public int? Userid { get; set; }

        // user ID from AspNetUser table.
        [JsonProperty("ownerId")]
        public string OwnerID { get; set; }

        [JsonProperty("bookId")]
        public int? BookId { get; set; }

        [JsonProperty("rate")]
        public int? Rate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("readdone")]
        public bool? Readdone { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("book")]
        public Book Book { get; set; }
    }
}
