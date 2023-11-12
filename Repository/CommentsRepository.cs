using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace SocialNetworking.Repository
{
    public class CommentsRepository:ICommentsDbRepository
    {
        private readonly ManageAppDbContext _manageAppDbContext;

        public CommentsRepository(ManageAppDbContext manageAppDbContext)
        {
            _manageAppDbContext = manageAppDbContext;
        }

        public async Task CreateCommentAsync(Comments comment)
        {
            _manageAppDbContext.Comments.Add(comment);
            await _manageAppDbContext.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var Comment = await _manageAppDbContext.Comments.FirstOrDefaultAsync(c => c.CommentID == id);
            _manageAppDbContext.Comments.Remove(Comment);

            await _manageAppDbContext.SaveChangesAsync();
        }

        public List<Comments> GetAllComments(Guid postId)
        {
            var Comment =  _manageAppDbContext.Comments.Where(c => c.PostID == postId).ToList();
            return Comment;
        }

        public Task<Comments> GetCommentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCommentAsync(Comments comment)
        {
            var comments = await _manageAppDbContext.Comments.FirstOrDefaultAsync(c => c.CommentID == comment.CommentID);

            comments.TimeComment = comment.TimeComment;
            comments.Content = comment.Content;
            await _manageAppDbContext.SaveChangesAsync();

        }
    }
}
