using Data.Entities;
using NuGet.Protocol.Plugins;
using SocialNetworking.Models;
using SocialNetworking.Repository;
using System.Text.RegularExpressions;

namespace SocialNetworking.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessageDbRepository _messageDbRepository;
          public MessagesService(IMessageDbRepository messageDbRepository)
        { 
            _messageDbRepository = messageDbRepository;
        }

        public Messages create(MessageViewModel messageViewModel)
        {
            var message = new Messages()
            {
                MessageID = new Guid(),
                ReceiverUserID = messageViewModel.ReceiverUser.Id,
                SenderUserID = messageViewModel.SenderUser.Id,
                Content = Regex.Replace(messageViewModel.Content, @"<.*?>", string.Empty),
                TimeSend = DateTime.Now

            };


            _messageDbRepository.CreateMessages(message);
            return message;
        }

        public void Delete(Guid id)
        {
            _messageDbRepository.DeleteMessages(id);
        }

        public List<MessageViewModel> Getmessage(string FromUserId, string ToUserId)
        {
            var Message = _messageDbRepository.GetAll(FromUserId, ToUserId).ToMessagesViewModel();


            return Message;
        }
    }
}
