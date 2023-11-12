using Data.Entities;

namespace SocialNetworking.Repository
{
    public interface IMessageDbRepository
    {

        void CreateMessages(Messages messages);// tạo tin nhắn
        List<Messages> GetAll(string FromUserId,string ToUserId); // hiển thị tin nhắn
        void DeleteMessages(Guid id); // xóa tin nhắn 
        void UpdateMessages(Messages messages); // update tín nhắn
    }
}
