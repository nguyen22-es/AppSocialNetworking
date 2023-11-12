using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SocialNetworking.Hubs;
using SocialNetworking.Models;
using SocialNetworking.Services;

namespace SocialNetworking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private readonly UserManager<ManagerUser> _userManager;
        private readonly IFriendShipService _friendShipService;
        private readonly IHubContext<ChatHub> _hubContext;
        public FriendshipsController(UserManager<ManagerUser> userManager, IFriendShipService friendShipService, IHubContext<ChatHub> hubContext)
        {
            _friendShipService = friendShipService;
            _userManager = userManager;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendShipViewModel>>> Get() // danh sách bạn bè
        {

            var Friend = _friendShipService.GetById(_userManager.GetUserId(User));

            return Ok(Friend);
        }

       /* [HttpPost]
        public async Task<ActionResult<FriendShipViewModel>> Create(FriendShipViewModel  friendShipViewModel)
        {
            

            await _hubContext.Clients.All.SendAsync("addFriend", new { id = room.Id, name = room.Name });

            return CreatedAtAction(nameof(Get), new { id = room.Id }, new { id = room.Id, name = room.Name });
        }*/

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var Friend = _friendShipService.GetById(_userManager.GetUserId(User));

            var friendship =    Friend.FirstOrDefault(f => f.Id == id);

            return NoContent();
        }

    }
}
