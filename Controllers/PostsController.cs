using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SocialNetworking.Hubs;
using SocialNetworking.Models;
using SocialNetworking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SocialNetworking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly UserManager<ManagerUser> _userManager;
        private readonly IPostService _postService;
        private readonly IFriendShipService _friendShipService;
        private readonly IHubContext<ChatHub> _hubContext;
        public PostsController(UserManager<ManagerUser> userManager, IFriendShipService friendShipService,
         IPostService postService, IHubContext<ChatHub> hubContext)
        {
            _friendShipService = friendShipService;
            _userManager = userManager;
            _postService = postService;
            _hubContext = hubContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> Get() // lấy thông tin bài post người dùng hiện tại
        {

            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));         

            var PostViewModel = _postService.GetById(user.Id);

            return Ok(PostViewModel);
        }

        [HttpGet("friend-posts")]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPost() // hiển thị những bài post trong newsfeed gồm những bài post bạn bè của mình
        {
            var Friend = _friendShipService.GetById(_userManager.GetUserId(User));
            var list = new List<string>();
            foreach (var item in Friend)
            {
                list.Add(item.Id);
            }

            var PostsViewModel = _postService.GetByIdFriend(list);

            if (PostsViewModel == null || PostsViewModel.Count == 0)
            {
                // Nếu danh sách rỗng hoặc null, trả về danh sách rỗng
                return new List<PostViewModel>();
            }
            else
            {
                // Trả về danh sách bài đăng từ bạn bè
                return Ok(PostsViewModel);
            }
        }



        [HttpPost]
        public async Task<ActionResult<PostViewModel>> Create(PostViewModel  postViewModel)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var Friend = _friendShipService.GetById(user.Id);
          var p =   _postService.CreatePost(postViewModel, user.Id);

            foreach (var friend in Friend)
            {
                // Gửi thông báo đến bạn bè thông qua SignalR
                _hubContext.Clients.User(friend.Id).SendAsync("ReceiveNotification", "Bạn bè của bạn đã đăng một bài đăng mới!");
            }
          

            return CreatedAtAction(nameof(Get), new { id = p.PostID }, p);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, PostViewModel postViewModel)
        {
            _postService.Update(postViewModel, id);
           
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            
            _postService.Delete(id);

            return NoContent();
        }

    }
}
