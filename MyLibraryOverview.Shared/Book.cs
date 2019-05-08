using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyLibraryOverview.Shared
{
    public partial class Book
    {
        public Book()
        {
            //Author = new Author();
        }

        [JsonProperty("bookid")]
        public int Bookid { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("readdone")]
        public bool Readdone { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("authorid")]
        public int Authorid { get; set; }
        [Required]

        [JsonProperty("authorName")]
        public string AuthorName { get; set; }

        [JsonProperty("authorAbout")]
        public string AuthorAbout { get; set; }
    }
}
