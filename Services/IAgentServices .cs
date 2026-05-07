using ai.Models;

namespace ai.Services
{
    public interface IAgentService
    {
        Plan GeneratePlan(User user, List<UsageRecord> usageRecords);
    }
}