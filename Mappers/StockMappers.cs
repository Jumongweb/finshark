using finshark_api.Dtos.Stock;
using finshark_api.models;

namespace finshark_api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(comment => comment.ToDto()).ToList()
            };
        }

        public static Stock ToStock(this CreateStockRequest createStockRequest)
        {
            return new Stock
            {
                Symbol = createStockRequest.Symbol,
                CompanyName = createStockRequest.CompanyName,
                Purchase = createStockRequest.Purchase,
                LastDiv = createStockRequest.LastDiv,
                Industry = createStockRequest.Industry,
                MarketCap = createStockRequest.MarketCap
            };
        }

        

    }
}