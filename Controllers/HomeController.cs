using ai.Data;
using ai.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ai.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        // Dependency Injection ile veritabaný baðlamýný alýyoruz
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Ana sayfadaki butona basýlýnca çalýþacak yeni metod
        public IActionResult StartDetox()
        {
            // Arka planda otomatik bir misafir kullanýcý oluþturuyoruz
            var guestUser = new User
            {
                Name = "Misafir",
                TargetScreenTime = 2.0 // Varsayýlan bir hedef süre atadýk
            };

            _context.Users.Add(guestUser);
            _context.SaveChanges(); // ID'si otomatik oluþtu

            // Oluþan bu yeni ID ile doðrudan Usage(Ekran Süresi) sayfasýna yönlendiriyoruz
            return RedirectToAction("Create", "Usage", new { userId = guestUser.Id });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}