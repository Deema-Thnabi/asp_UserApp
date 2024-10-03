using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using UserApp.Data;
using UserApp.Models;

namespace UserApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Users.AsNoTracking().ToList());
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(LogIn));
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(User user)
        {
            var CheckUser = _context.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).ToList();
            if (CheckUser.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Error = "Invalid Data";
            return View(user);
        }

        public IActionResult changeUserState(Guid id)
        {
            var user = _context.Users.Find(id);
            user.IsActive = !user.IsActive;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
