using SocialNetworking.Models;

namespace SocialNetworking.Services
{
    public interface IFriendShipService
    {
        List<UserViewModel> GetById(string Id);

        void Delete(string id,string toId);
    }
}
