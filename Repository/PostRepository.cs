using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace SocialNetworking.Repository
{
    public class PostRepository:IPostDbRepository
    {
        private readonly ManageAppDbContext _manageAppDbContext;
        public PostRepository(ManageAppDbContext manageAppDbContext)
        {

            this._manageAppDbContext = manageAppDbContext;
        }

  

        public void CreatePosts(Posts posts)
        {
            _manageAppDbContext.Posts.Add(posts);
             _manageAppDbContext.SaveChanges();
        }

        public void DeletePosts(Guid id)
        {
            var posts = _manageAppDbContext.Posts.FirstOrDefault(m => m.PostID == id);

            _manageAppDbContext.Posts.Remove(posts);
            _manageAppDbContext.SaveChanges();
        }

        public List<Posts> GetAll(string Id)
        {
            var posts = _manageAppDbContext.Posts.Where(m => m.UserID == Id).OrderByDescending(m => m.TimePost).Include(p => p.FromUser).Include(p => p.Comments).ToList();



            return posts;
        }

        public List<Posts> GetAllFriend(string Id)
        {
            var posts = _manageAppDbContext.Posts.Where(m => m.UserID == Id).Include(p => p.FromUser).Include(p => p.Comments).Take(2).AsEnumerable().ToList();
            return posts;
        }

        public void UpdatePosts(Posts post)
        {
            var posts = _manageAppDbContext.Posts.FirstOrDefault(m => m.PostID == post.PostID);
            post.Content = posts.Content;
            _manageAppDbContext.SaveChanges();
        }
    }
}
