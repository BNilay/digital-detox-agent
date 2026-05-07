namespace ai.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double TargetScreenTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<UsageRecord> UsageRecords { get; set; } = new();

        public List<Plan> Plans { get; set; } = new();
    }
}