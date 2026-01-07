using finshark_api.Dtos.Stock;
using finshark_api.models;

namespace finshark_api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> getAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest updatedStockRequest);
        Task<bool> DeleteAsync(int id);   

        Task<bool> StockExists(int id);
    }
}