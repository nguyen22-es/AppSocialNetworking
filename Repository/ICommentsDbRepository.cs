using DataAccess.Entities;

namespace SocialNetworking.Repository
{
    public interface ICommentsDbRepository
    {
        Task<Comments> GetCommentAsync(Guid id); // Lấy một comment bằng id
        Task CreateCommentAsync(Comments comment); // Thêm một comment
        List<Comments> GetAllComments(Guid postId); // Lấy danh sách các comment trong một bài post
        Task DeleteCommentAsync(Guid id); // Xóa một comment bằng id
        Task UpdateCommentAsync(Comments comment); // Cập nhật thông tin của một comment
    }
}
