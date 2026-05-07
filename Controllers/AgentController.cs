using ai.Data;
using ai.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ai.Controllers
{
    public class AgentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAgentService _agentService;

        public AgentController(AppDbContext context, IAgentService agentService)
        {
            _context = context;
            _agentService = agentService;
        }

        [HttpGet]
        public IActionResult Analyze(int userId)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var usageRecords = _context.UsageRecords
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ToList();

            var plan = _agentService.GeneratePlan(user, usageRecords);

            _context.Plans.Add(plan);
            _context.SaveChanges();

            return View("Result", plan);
        }

        
    }
}