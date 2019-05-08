using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyLibraryOverview.Shared
{

    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        [JsonProperty("authorid")]
        public int Authorid { get; set; }
        //[Required]

        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("book")]
        public ICollection<Book> Book { get; set; }
        //public List<Book> Book { get; set; }
    }
}
