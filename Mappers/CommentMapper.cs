using finshark_api.Dtos.Comment;
using finshark_api.models;

namespace finshark_api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.title,
                Content = comment.content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }
        public static Comment ToModel(this CreateCommentRequest createCommentRequest, int stockId)
        {
            return new Comment
            {
                title = createCommentRequest.Title,
                content = createCommentRequest.Content,
                StockId = stockId
            };
        }

        public static Comment ToModel(this UpdateCommentRequest updateCommentRequest)
        {
            return new Comment
            {
                title = updateCommentRequest.Title,
                content = updateCommentRequest.Content
            };
        }

    }
}