using Data.Entities;

namespace SocialNetworking.Models
{
    public class PostViewModel
    {
        public Guid PostID { get; set; }
        public UserViewModel FromUserViewModel { get; set; }
        public string Content { get; set; }
        public DateTime TimePost { get; set; }
        public ICollection<CommentsViewModel>   commentsViewModels { get; set; }
    }
}
