using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace SocialNetworking.Repository
{
    public class FriendShipsRepository:IFriendShipsDbRepository
    {
        private readonly ManageAppDbContext _manageAppDbContext;
        public FriendShipsRepository(ManageAppDbContext manageAppDbContext)
        {

            this._manageAppDbContext = manageAppDbContext;
        }



        public void CreateFriend(Friendships friendships)
        {
            _manageAppDbContext.Friendships.Add(friendships);
            _manageAppDbContext.SaveChanges();
        }

        public void DeleteFriend(Guid id)
        {
         var friend =   _manageAppDbContext.Friendships.FirstOrDefault(f => f.FriendshipID == id);
            _manageAppDbContext.Remove(friend);
            _manageAppDbContext.SaveChanges();
        }

        public List<Friendships> GetAll(string Id)
        {
            return _manageAppDbContext.Friendships.Include(f => f.User).ToList();
        }
    }
}
