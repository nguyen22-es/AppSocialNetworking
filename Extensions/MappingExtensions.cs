
using Data.Entities;
using SocialNetworking.Models;



namespace SocialNetworking.Models
{
    public static class MappingExtensions
    {
        public static List<FriendShipViewModel> ToFriendShipsViewModel(this List<Friendships> friendships)
        {
            var items = new List<FriendShipViewModel>();
            foreach (var Friend in friendships)
            {
                var item = Friend.ToFriendShipViewModel();
                items.Add(item);
            }


            return items;
        }

        public static FriendShipViewModel ToFriendShipViewModel(this Friendships friendships)
        {
            return new FriendShipViewModel
            {
                FriendshipID = friendships.FriendshipID,
                UserID1 = friendships.User.toUserViewModel(),
                UserID2 = friendships.User.toUserViewModel(),
                Status = friendships.Status,               
            };
        }
        public static List<MessageViewModel> ToMessagesViewModel(this List<Messages>  messages)
        {
            var items = new List<MessageViewModel>();
            foreach (var mess in messages)
            {
                var item = mess.ToMessageViewModel();
                items.Add(item);
            }


            return items;
        }
        public static MessageViewModel ToMessageViewModel(this Messages  messages)
        {
            return new MessageViewModel
            {
                MessageID = messages.MessageID,
                Content = messages.Content,
                TimeSend = messages.TimeSend,
                ReceiverUser = messages.FromUser.toUserViewModel(),
                SenderUser = messages.FromUser.toUserViewModel(),
               
            };
        }

        public static List<PostViewModel> ToPostsViewModel(this List<Posts>  posts)
        {
            var items = new List<PostViewModel>();
            foreach (var post in posts)
            {
                var item = post.ToPostViewModel();
               items.Add(item);
            }

           
            return items;
        }
        public static PostViewModel ToPostViewModel(this Posts  posts)
        {
            return new PostViewModel
            {
                PostID = posts.PostID,
                Content = posts.Content,
                TimePost = posts.TimePost,
                FromUserViewModel = posts.FromUser.toUserViewModel(),
                commentsViewModels = posts.Comments.ToList().TocommentsViewModels()
            };
        }

        public static List<CommentsViewModel> TocommentsViewModels(this List<Comments>  comments)
        {
            var comment = new List<CommentsViewModel>();
            foreach (var item in comments)
            {
                var c = item.ToCommentViewModel();
                comment.Add(c);
            }
            return comment;
        }

        public static CommentsViewModel ToCommentViewModel(this Comments  comments)
        {
            return new CommentsViewModel
            {
                CommentID = comments.CommentID,
                Content = comments.Content,
                TimeComment = comments.TimeComment,
                userViewModel = comments.UserComment.toUserViewModel(),
               
            };
        }

      
        public static UserViewModel toUserViewModel(this ManagerUser user)
        {

            return new UserViewModel
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Avatar = user.Avatar,              
                FullName = user.LastName + user.FistName,
                Gender = user.Gender,
                Birthdate = user.Birthdate,
                Username = user.UserName,                               
            };
        }

        public static ManagerUser ToUserInfo(this UserViewModel userViewModel)
        {
            var path = userViewModel.Avatar;
            if (userViewModel.File != null)
            {
                path = "/images/" + userViewModel.File.FileName;
            }
            return new ManagerUser
            {
                Id = userViewModel.Id,
                PhoneNumber = userViewModel.PhoneNumber,
                Email = userViewModel.Email,
                FistName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Avatar = path,
                Gender = userViewModel.Gender,
                Birthdate = userViewModel.Birthdate,
  
                                          
            };
        }
    }
}
