using ai.Data;
using ai.Models;
using ai.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ai.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                Name = model.Name,
                TargetScreenTime = model.TargetScreenTime
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Create", "Usage", new { userId = user.Id });
        }
    }
}