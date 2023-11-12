namespace SocialNetworking.Models
{
    public class FriendShipViewModel
    {
        public Guid FriendshipID { get; set; }

        public UserViewModel UserID1 { get; set; }

        public UserViewModel UserID2 { get; set; }

        public string Status { get; set; }
    }
}
