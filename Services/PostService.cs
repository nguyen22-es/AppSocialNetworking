using Data.Entities;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging;
using SocialNetworking.Models;
using SocialNetworking.Repository;

namespace SocialNetworking.Services
{
    public class PostService : IPostService
    {
        private readonly IPostDbRepository _postDbRepository;
       public PostService(IPostDbRepository postDbRepository)
        {
            _postDbRepository = postDbRepository;
        }

        public Posts CreatePost(PostViewModel post,string Id)
        {
            var postSame = new Posts()
            {
                TimePost = DateTime.Now,
                PostID = Guid.NewGuid(),
                Content = post.Content,
                UserID = Id
            };

            _postDbRepository.CreatePosts(postSame);

            return postSame;
        }

        public void Delete(string id)
        {
            if (Guid.TryParse(id, out Guid postId))
            {               
                _postDbRepository.DeletePosts(postId);
            }
           
        }

        public List<PostViewModel> GetById(string userId)
        {
           return  _postDbRepository.GetAll(userId).ToPostsViewModel();
        }

        public List<PostViewModel> GetByIdFriend(List<string> Id)
        {
            var PostList = new List<PostViewModel>();

            foreach (var item in Id)
            {
                var post = _postDbRepository.GetAllFriend(item).ToPostsViewModel();
                PostList.AddRange(post);
            }

            return PostList;
        }

        public void Update(PostViewModel post, string id)
        {
            if (Guid.TryParse(id, out Guid postId))
            {
                var postSame = new Posts()
                {
                    TimePost = DateTime.Now,
                    PostID = postId,
                    Content = post.Content,
                    UserID = post.FromUserViewModel.Id
                };
                _postDbRepository.UpdatePosts(postSame);
            }
        }
    }
}
