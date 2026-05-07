namespace ai.ViewModels
{
    public class CreateUsageRecordViewModel
    {
        public int UserId { get; set; }

        public double ScreenTime { get; set; }

        public string MostUsedApp { get; set; } = string.Empty;

        public string UsagePeriod { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;
    }
}