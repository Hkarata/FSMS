using FSMS.Data;

namespace FSMS.Contracts.Request
{
    public record CreatePriceDto
    {
        public decimal Value { get; set; }
        public FuelType Fuel { get; set; }
    }
}
