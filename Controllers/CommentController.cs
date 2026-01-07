using finshark_api.Dtos.Comment;
using finshark_api.Interfaces;
using finshark_api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace finshark_api.Controllers
{
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentController> _logger;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, ILogger<CommentController> logger, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _logger = logger;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogInformation("============> Fetching all comments");
            var comments = await _commentRepository.GetAllAsync();
            var commentTDto = comments.Select(comment => comment.ToDto());
            return Ok(commentTDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogInformation("============> Fetching comment with ID: {Id}", id);
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogInformation("============> Deleting comment with ID: {Id}", id);
            var result = await _commentRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Comment does not exist");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] int stockId, CreateCommentRequest createCommentRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogInformation("============> Creating a new comment"); 
            if(!await _stockRepository.StockExists(stockId))
            {
                return BadRequest($"Stock with ID {stockId} does not exist.");
            }
            var comment = createCommentRequest.ToModel(stockId);
            await _commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest updateCommentRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogInformation("============> Updating comment with ID: {Id}", id);
            var comment = await _commentRepository.UpdateAsync(id, updateCommentRequest.ToModel());
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToDto());
        }

    }
}