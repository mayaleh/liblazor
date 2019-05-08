namespace MyLibraryOverview.Server.Models.Entities
{
    public partial class UserBook
    {
        public int UserbookId { get; set; }
        public int BookId { get; set; }
        public int? Rate { get; set; }
        public string Note { get; set; }
        public bool? Readdone { get; set; }
        public string Place { get; set; }
        public string UserId { get; set; }

        public Book Book { get; set; }
        public UserAppIdentity User { get; set; }
    }
}
