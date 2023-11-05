using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace SocialNetworking.Repository
{
    public class MessageRepository:IMessageDbRepository
    {
        private readonly ManageAppDbContext _manageAppDbContext;
        public MessageRepository(ManageAppDbContext manageAppDbContext)
        {

            this._manageAppDbContext = manageAppDbContext;
        }


        public void CreateMessages(Messages messages)
        {
            _manageAppDbContext.Messages.Add(messages);
            _manageAppDbContext.SaveChanges();
        }

        public void DeleteMessages(Guid id)
        {
            var message = _manageAppDbContext.Messages.FirstOrDefault(m => m.MessageID == id);

            _manageAppDbContext.Messages.Remove(message);
            _manageAppDbContext.SaveChanges();
            
        }

        public List<Messages> GetAll(string FromUserId, string ToUserId)
        {
            var messages = _manageAppDbContext.Messages.Where(m => m.SenderUserID == FromUserId && m.ReceiverUserID == ToUserId)
               .Include(m => m.FromUser)
               .Include(m => m.ToUser)
               .OrderByDescending(m => m.TimeSend)
               .Take(20)
               .AsEnumerable()
               .Reverse()
               .ToList();
            return messages;
        }

        public void UpdateMessages(Messages messages)
        {
            var message = _manageAppDbContext.Messages.FirstOrDefault(m => m.MessageID == messages.MessageID);
            message.Content = messages.Content;
            _manageAppDbContext.SaveChanges();
        }
    }
}
