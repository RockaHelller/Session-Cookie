using Microsoft.AspNetCore.Mvc;
using Session_Cookie.Models;
using Session_Cookie.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Session_Cookie.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM model)
        {
            List<User> dbUsers = GetAll();

            var findUserByEmail = dbUsers.FirstOrDefault(m=>m.Email == model.Email);

            if (findUserByEmail == null)
            {
                ViewBag.error = "Email or password is wrong";
                return View();
            }

            if(findUserByEmail.Password != model.Password)
            {
                ViewBag.error = "Email or password is wrong";
                return View();
            }

            HttpContext.Session.SetString("user", JsonSerializer.Serialize(findUserByEmail));

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }


        private List<User> GetAll()
        {
            User user1 = new()
            {
                Id = 1,
                UserName = "resul123",
                Email = "resul@gmail.com",
                Password = "Resul123"
            };

            User user2 = new()
            {
                Id = 1,
                UserName = "gultac123",
                Email = "gultac@gmail.com",
                Password = "Gultacl123"
            };

            User user3 = new()
            {
                Id = 1,
                UserName = "novreste123",
                Email = "novreste@gmail.com",
                Password = "Novreste123"
            };

            List<User> users = new() { user1, user2, user3 };

            return users;
        }
        
    }
}
