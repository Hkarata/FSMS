using FSMS.Data;

namespace FSMS.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public FuelType Fuel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Validity { get; set; }
    }
}
