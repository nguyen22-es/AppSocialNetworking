using DataAccess.Entities;

namespace SocialNetworking.Repository
{
    public interface IPostDbRepository
    {
        void CreatePosts(Posts posts);
        List<Posts> GetAll(string Id);
        void DeletePosts(Guid id);
        void UpdatePosts(Posts  posts); // sửa post

    }
}
