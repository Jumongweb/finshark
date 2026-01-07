using finshark_api.Data;
using finshark_api.Dtos.Stock;
using finshark_api.Interfaces;
using finshark_api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace finshark_api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger<StockController> _logger;
        private readonly IStockRepository _stockRepository;

        public StockController(ILogger<StockController> logger, ApplicationDBContext dbContext, IStockRepository stockRepository)
        {
            _logger = logger;
            _dbContext = dbContext;
            _stockRepository = stockRepository;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            _logger.LogInformation("============> Fetching all stocks");
            var stocks = await _stockRepository.getAllAsync();
            var stockDto = stocks.Select(stock => stock.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogInformation("============> Fetching stock with ID: {Id}", id);
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {      
                return NotFound();
            }
            return Ok(stock.ToStockDto());   
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequest createStockRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogInformation("============> Creating a new stock with symbol: {Symbol}", createStockRequest.Symbol);
            var stockModel = createStockRequest.ToStock();
            await _stockRepository.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateStockRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogInformation("============> Updating stock with ID: {Id}", id);
            var updatedStock = await _stockRepository.UpdateAsync(id, request);
            if (updatedStock == null)
            {
                return NotFound();
            }
            return Ok(updatedStock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogInformation("============> Deleting stock with ID: {Id}", id);
            var result =  await _stockRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Comment does not exist");
            }

            return NoContent();
        }
    }
}