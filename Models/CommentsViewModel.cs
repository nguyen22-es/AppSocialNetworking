namespace SocialNetworking.Models
{
    public class CommentsViewModel
    {
        public Guid CommentID { get; set; }
        public UserViewModel userViewModel { get; set; }
        public string Content { get; set; }
        public DateTime TimeComment { get; set; }
    }
}
