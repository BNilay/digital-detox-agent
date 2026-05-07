namespace ai.Models
{
    public class UsageRecord
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public double ScreenTime { get; set; }

        public string MostUsedApp { get; set; } = string.Empty;

        public string UsagePeriod { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;
    }
}
