using FSMS.Data;

namespace FSMS.Entities
{
    public class Tank
    {
        public Guid Id { get; init; }
        public string Identifier { get; set; } = string.Empty;
        public FuelType Fuel { get; set; }
        public double Capacity { get; set; }
    }
}
