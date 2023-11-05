using System.Collections.ObjectModel;

namespace DataAccess.Entities
{
    public class Posts
    {
        public Guid PostID { get; set; }
        public string UserID { get; set; }
        public string Content { get; set; }

       public DateTime TimePost { get; set; }
        public ManagerUser FromUser { get; set; }
       public ICollection<Comments> Comments { get; set; }
    }
}
