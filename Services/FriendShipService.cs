using SocialNetworking.Models;
using SocialNetworking.Repository;

namespace SocialNetworking.Services
{
    public class FriendShipService : IFriendShipService
    {
        private readonly IFriendShipsDbRepository _friendShipsDbRepository;
        public FriendShipService(IFriendShipsDbRepository friendShipsDbRepository) 
        {
            _friendShipsDbRepository = friendShipsDbRepository;
        }

        public void Delete(string ToId, string FromId )
        {
          var friend =  _friendShipsDbRepository.Find(ToId, FromId);

            _friendShipsDbRepository.DeleteFriend(friend.FriendshipID);
        }

        public List<UserViewModel> GetById(string Id)
        {
            var friendships = _friendShipsDbRepository.GetAll(Id).ToFriendShipsViewModel();

            var friends = friendships
                .Where(friendship => friendship.UserID1.Id == Id || friendship.UserID2.Id == Id)
                .Select(friendship => friendship.UserID1.Id == Id ? friendship.UserID2 : friendship.UserID1)
                .ToList();

            return friends;
        }
    }
}
