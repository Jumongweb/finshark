using finshark_api.Data;
using finshark_api.Dtos.Stock;
using finshark_api.Interfaces;
using finshark_api.models;
using Microsoft.EntityFrameworkCore;

namespace finshark_api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger<StockRepository> _logger;

        public StockRepository(ApplicationDBContext dBContext, ILogger<StockRepository> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }

        public async Task<Stock> createAsync(Stock stock)
        {
            _logger.LogInformation("============> Creating a new stock in the repository");
            await _dbContext.Stocks.AddAsync(stock);
            await _dbContext.SaveChangesAsync();
            return stock;
        }

        public Task<Stock> CreateAsync(Stock stock)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("============> Deleting a stock from the repository");
            var stock = await _dbContext.Stocks.FindAsync(id);
            if (stock == null)
            {
                return false;
            }

            _dbContext.Stocks.Remove(stock);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Stock>> getAllAsync()
        {
            _logger.LogInformation("============> Fetching all stocks from the repository");
            return await _dbContext.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            _logger.LogInformation("============> Fetching a stock by ID from the repository");
            return await _dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<bool> StockExists(int id)
        {
            _logger.LogInformation("============> Checking if a stock exists in the repository");
            return _dbContext.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequest updatedStockRequest)
        {
            _logger.LogInformation("============> Updating a stock in the repository");
            var existingStock = await _dbContext.Stocks.FindAsync(id);
            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = updatedStockRequest.Symbol ?? existingStock.Symbol;
            existingStock.CompanyName = updatedStockRequest.CompanyName ?? existingStock.CompanyName;
            existingStock.Purchase = updatedStockRequest.Purchase ?? existingStock.Purchase;
            existingStock.LastDiv = updatedStockRequest.LastDiv ?? existingStock.LastDiv;
            existingStock.Industry = updatedStockRequest.Industry ?? existingStock.Industry;
            existingStock.MarketCap = updatedStockRequest.MarketCap ?? existingStock.MarketCap;

            await _dbContext.SaveChangesAsync();
            return existingStock;
        }

    }
}