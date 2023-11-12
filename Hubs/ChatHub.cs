using AutoMapper;
using Data;
using Data.Entities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SocialNetworking.Models;
using SocialNetworking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace SocialNetworking.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public readonly static List<UserViewModel> _Connections = new List<UserViewModel>();

        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
        private readonly UserManager<ManagerUser> _userManager;
        private readonly IPostService _postService;
        private readonly ManageAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFriendShipService _friendShipService;
        public ChatHub(UserManager<ManagerUser> userManager, IMapper mapper, IPostService postService, IFriendShipService friendShipService)
        {
            _postService = postService;
            _userManager = userManager;
            _mapper = mapper;
            _friendShipService = friendShipService;
        }

        public async Task ReceiveNotification(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var Friend = _friendShipService.GetById(user.Id);
            foreach (var item in Friend)
            {
                if (_ConnectionsMap.TryGetValue(item.Id, out string userId))
                {
                        await Clients.Client(userId).SendAsync("ReceiveNotification", message);
                      
                         
                }
                }
               
            }
            
       

    public async Task SendMessageToUser(string userId, MessageViewModel message)
    {
        await Clients.Client(userId).SendAsync("newMessage", message);
    }
        /* public async Task SendPrivate(string receiverName, string message)
         {
             if (_ConnectionsMap.TryGetValue(receiverName, out string userId))
             {
                 // Who is the sender;
                 var sender = _Connections.Where(u => u.Username == IdentityName).First();

                 if (!string.IsNullOrEmpty(message.Trim()))
                 {
                     // Build the message
                     var messageViewModel = new MessageViewModel()
                     {
                         Content = Regex.Replace(message, @"<.*?>", string.Empty), // match bất kì kí tự nào
                         From = sender.FullName,
                         Avatar = sender.Avatar,
                         Room = "",
                         Timestamp = DateTime.Now.ToLongTimeString()
                     };

                     // Send the message
                     await Clients.Client(userId).SendAsync("newMessage", messageViewModel);
                     await Clients.Caller.SendAsync("newMessage", messageViewModel);
                 }
             }
         }*/

        /* public async Task Join(string roomName)
         {
             try
             {
                 var user = _Connections.Where(u => u.Username == IdentityName).FirstOrDefault();
                 if (user != null && user.CurrentRoom != roomName)
                 {
                     // Remove user from others list
                     if (!string.IsNullOrEmpty(user.CurrentRoom))
                         await Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                     // Join to new chat room
                     await Leave(user.CurrentRoom);
                     await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                     user.CurrentRoom = roomName;

                     // Tell others to update their list of users
                     await Clients.OthersInGroup(roomName).SendAsync("addUser", user);
                 }
             }
             catch (Exception ex)
             {
                 await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
             }
         }*/
        public async Task Leave(string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
    }




     public override async Task OnConnectedAsync()
     {
         try
         {
                var user = await _userManager.GetUserAsync(Context.User);

                var userViewModel = user.toUserViewModel();

             userViewModel.Device = GetDevice();
           

             if (!_Connections.Any(u => u.Username == user.UserName))
             {
                 _Connections.Add(userViewModel);
                 _ConnectionsMap.Add(user.UserName, Context.ConnectionId);
             }

             Clients.Caller.SendAsync("getProfileInfo", user.FistName+user.LastName, user.Avatar);
         }
         catch (Exception ex)
         {
             Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
         }
            await base.OnConnectedAsync();
     }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        try
        {
                var userr = await _userManager.GetUserAsync(Context.User);
                var user = _Connections.Where(u => u.Username == userr.UserName).First();
                _Connections.Remove(user);
           

            // Remove mapping
            _ConnectionsMap.Remove(user.Username);
        }
        catch (Exception ex)
        {
            Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
        }

            await base.OnDisconnectedAsync(exception);
    }




     private string GetDevice()
         {
             var device = Context.GetHttpContext().Request.Headers["Device"].ToString();
             if (!string.IsNullOrEmpty(device) && (device.Equals("Desktop") || device.Equals("Mobile")))
                 return device;

             return "Web";
         }

     }
}
