using Data.Entities;
using SocialNetworking.Models;

namespace SocialNetworking.Services
{
    public interface IMessagesService
    {
        Messages create(MessageViewModel  messageViewModel);
        List<MessageViewModel>  Getmessage(string FromUserId, string ToUserId);
        void Delete(Guid id);
    }
}
