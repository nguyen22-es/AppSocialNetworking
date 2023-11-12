using Data.Entities;

namespace SocialNetworking.Repository
{
    public interface IFriendShipsDbRepository
    {
        void CreateFriend(Friendships friendships);
        List<Friendships> GetAll(string Id); 
        void DeleteFriend(Guid id); 

        Friendships Find(string ToId,string FromId);
    
    }
}
