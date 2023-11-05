namespace DataAccess.Entities
{
    public class Friendships
    {
        public Guid FriendshipID { get; set; }

        public string UserID1 { get; set; }

        public string UserID2 { get; set; }

        public string Status { get; set; }
        public ManagerUser User { get; set; }

    }
}
