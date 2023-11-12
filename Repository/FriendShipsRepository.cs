using Data;
using Data.Entities;
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

        public Friendships Find(string toId, string fromId)
        {
            return _manageAppDbContext.Friendships.FirstOrDefault(f =>
                (f.UserID1 == fromId && f.UserID2 == toId || f.UserID2 == fromId && f.UserID1 == toId) && f.Status == "ban be");
        }

        public List<Friendships> GetAll(string Id)
        {
            return _manageAppDbContext.Friendships.Where(f => f.UserID1 == Id && f.Status == "ban be").Include(f => f.User).ToList();
        }
    }
}
