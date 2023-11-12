namespace Data.Entities
{
    public class Comments
    {
        public Guid CommentID { get;set; }
        public Guid PostID { get;set; }
        public string UserID { get;set; }
        public string Content { get;set; }
        public DateTime TimeComment { get;set; }

        public Posts  Post { get; set; }
        public ManagerUser UserComment { get; set; }
    }
}
