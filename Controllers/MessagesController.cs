using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SocialNetworking.Data;
using SocialNetworking.Hubs;
using SocialNetworking.Models;
using SocialNetworking.Services;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace SocialNetworking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly UserManager<ManagerUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMessagesService messagesService;
        public MessagesController(UserManager<ManagerUser> userManager, IHubContext<ChatHub> hubContext,IMessagesService messagesService)
        {
            _userManager = userManager;
         this.messagesService = messagesService;
            _hubContext = hubContext;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<List<MessageViewModel>>> Get(string id)
        {
            
            var message = messagesService.Getmessage(_userManager.GetUserId(User),id);
            if (message == null)
                return NotFound();

            return Ok(message);
        }



        [HttpPost]
        public async Task<ActionResult<MessageViewModel>> Create(MessageViewModel messageViewModel)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            messageViewModel.SenderUser = user.toUserViewModel();
         var createdMessage =   messagesService.create(messageViewModel);



            await _hubContext.Clients.Users(messageViewModel.ReceiverUser.Id).SendAsync("newMessage", createdMessage);

            return CreatedAtAction(nameof(Get), new { id = createdMessage.MessageID }, createdMessage);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            messagesService.Delete(id);
            return NoContent();
        }
    }
}
