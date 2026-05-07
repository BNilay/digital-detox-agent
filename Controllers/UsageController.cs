using ai.Models;
using ai.Data;
using ai.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ai.Controllers
{
    public class UsageController : Controller
    {
        private readonly AppDbContext _context;

        public UsageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var model = new CreateUsageRecordViewModel
            {
                UserId = userId,
            };

            ViewBag.UserName = user.Name;

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateUsageRecordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var usageRecord = new UsageRecord
            {
                UserId = model.UserId,
                ScreenTime = model.ScreenTime,
                MostUsedApp = model.MostUsedApp,
                UsagePeriod = model.UsagePeriod,
                Reason = model.Reason
            };

            _context.UsageRecords.Add(usageRecord);
            _context.SaveChanges();

            return RedirectToAction("Analyze", "Agent", new { userId = model.UserId });


        }

        [HttpGet]
        public IActionResult History(int userId)
        {
            var records = _context.UsageRecords
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ToList();

            ViewBag.UserId = userId;

            return View(records);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
