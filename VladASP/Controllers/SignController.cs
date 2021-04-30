using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VladASP.Models;
using VladASP.ViewModels;

namespace VladASP.Controllers
{
    public class SignController : Controller
    {
        public FlyContext db;

        public SignController(FlyContext context)
        {
            db = context;
                  }
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(Client client)
        {
            if (ModelState.IsValid)
            {
                Client user =  db.clients.FirstOrDefault(u => u.Login == client.Login && u.Password == client.Password);
                if (user != null)
                {
                    await Authenticate(client.Login);
                    return RedirectToAction("favourite", "fly", new { @id = user.Id });
                }

            }
            return RedirectToAction("Index", "sign");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Client client)
        {
            if (client != null)
            {
                Client person = new Client();
                person = db.clients.FirstOrDefault(u => u.Login == client.Login && u.Password == client.Password);
                if (person!=client)
                {
                    db.Add(client);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Sign");
        }


    }
}
