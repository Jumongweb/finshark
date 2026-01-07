using finshark_api.Data;
using finshark_api.Interfaces;
using finshark_api.models;
using Microsoft.EntityFrameworkCore;

namespace finshark_api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger<CommentRepository> _logger;

        public CommentRepository(ApplicationDBContext dbContext, ILogger<CommentRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public async Task<Comment> CreateAsync(Comment comment)
        {
            _logger.LogInformation("============> Creating a new comment in the repository");
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }   
   

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("============> Deleting a comment from the repository");
            var comment = await _dbContext.Comments.FindAsync(id);
            if (comment == null)
            {
                return false;
            }

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            _logger.LogInformation("============> Fetching all comments from the repository");
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            _logger.LogInformation("============> Fetching a comment by ID from the repository");
            return await _dbContext.Comments.FindAsync(id);
        }
        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            _logger.LogInformation("============> Updating a comment in the repository");
            var existingComment = await _dbContext.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.title = comment.title;
            existingComment.content = comment.content;
            await _dbContext.SaveChangesAsync();
            return existingComment;
        }
    }
}