namespace FSMS.Entities
{
    public class Dispenser
    {
        public Guid Id { get; init; }
        public string Identifier { get; set; } = string.Empty;
        public List<Allocation>? Allocations { get; set; }
    }
}
