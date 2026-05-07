namespace ai.Models
{
    public class Plan
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public string RiskLevel { get; set; } = string.Empty;

        public string WeeklyGoal { get; set; } = string.Empty;

        public string DailySuggestion { get; set; } = string.Empty;

        public string AlternativeActivity { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}