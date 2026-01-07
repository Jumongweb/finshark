using System.ComponentModel.DataAnnotations;

namespace finshark_api.Dtos.Stock
{
    public class CreateStockRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "Symbol must not be less than 5 characters.")]
        [MaxLength(100, ErrorMessage = "Symbol must not exceed 100 characters.")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Company Name must not be less than 5 characters.")]
        [MaxLength(100, ErrorMessage = "Company Name must not exceed 100 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000000, ErrorMessage = "Purchase must be between 1 and 1,000,000,000.")]

        public decimal Purchase { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Purchase must be between 1 and 100.")]
        public decimal LastPrice { get; set; }
        [Required]
        [Range(0.001, 100, ErrorMessage = "Last Div must be between 0.001 and 100.")]
        public decimal LastDiv { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Industry must not be less than 3 characters.")]
        [MaxLength(10, ErrorMessage = "Industry must not exceed 10 characters.")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000000, ErrorMessage = "Market Cap must be between 1 and 5,000,000,000,000.")]
        public long MarketCap { get; set; }
    }
}