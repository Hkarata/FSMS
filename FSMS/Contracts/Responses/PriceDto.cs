using FSMS.Data;

namespace FSMS.Contracts.Responses
{
    public record PriceDto
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public FuelType Fuel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Validity { get; set; }
    }
}
