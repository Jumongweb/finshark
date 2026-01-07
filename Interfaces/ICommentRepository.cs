using finshark_api.models;

namespace finshark_api.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> GetByIdAsync(int id);
        Task<List<Comment>> GetAllAsync();
        Task<Comment> CreateAsync(Comment comment);
        Task<bool> DeleteAsync(int id);
        Task<Comment?> UpdateAsync(int id, Comment comment);
    }
}