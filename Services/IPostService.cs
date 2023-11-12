using Data.Entities;
using SocialNetworking.Models;

namespace SocialNetworking.Services
{
    public interface IPostService
    {
        List<PostViewModel> GetById(string Id);
        List<PostViewModel> GetByIdFriend(List<string> FriendId);
        Posts CreatePost(PostViewModel post,string Id);

        void Update (PostViewModel post,string id);

        void Delete (string Id);
    }
}
