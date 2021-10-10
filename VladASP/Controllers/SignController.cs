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
using VladASP.Validation;
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
        public async Task<IActionResult> Index(Client client)
        {
            rightClient(client);
            if (!nullErr(client))
            {
                if (!lenghtErr(client))

                {
                    Client user = db.clients.FirstOrDefault(u => u.Login == client.Login && u.Password == client.Password);

                    if (user != null)
                    {

                        await Authenticate(client.Login);
                        return RedirectToAction("favourite", "fly", new { @id = user.Id });
                    }
                    ViewBag.ErrExist = "Asa utilizator nu exista";

                }
            }
            return View();
        }
        public void rightClient(Client client)
        {
            client.Name = "1234";
            client.Mobile = 1234;
            client.Email = "1234";
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Client client)
        {
            if (!nullErr(client)) { 
                if (!lenghtErr(client))
                {
                    {
                        Client person = new Client();
                        person = db.clients.FirstOrDefault(u => u.Login == client.Login && u.Password == client.Password);
                        if (person == null)
                        {
                            {
                                db.Add(client);
                                db.SaveChanges();
                                return RedirectToAction("Index", "Sign");
                            }
                        }
                        ViewBag.ErrSame = "Introdu alta combinatie login-parola";
                    }


                } }
            return View();
        }

        public bool nullErr(Client client)
        {
            NullErr nullErr = new NullErr();
            string k = "";
            k = nullErr.autentificate(client);
            if (k.Contains("1"))
                ViewBag.ErrName = nullErr.errMess;
            if (k.Contains("2"))
                ViewBag.ErrEmail = nullErr.errMess;
            if (k.Contains("3"))
                ViewBag.ErrMobile = nullErr.errMess;
            if (k.Contains("4"))
                ViewBag.ErrLogin = nullErr.errMess;
            if (k.Contains("5"))
                ViewBag.ErrPassword = nullErr.errMess;
            if (k=="")
                return false;
            return true;
        }
        public bool lenghtErr(Client client)
        {
            LenghtErr nullErr = new LenghtErr();
            string k = "";
            k = nullErr.autentificate(client);
            if (k.Contains("1"))
                ViewBag.ErrName = nullErr.errMess;
            if (k.Contains("2"))
                ViewBag.ErrEmail = nullErr.errMess;
            if (k.Contains("3"))
                ViewBag.ErrMobile = nullErr.errMess;
            if (k.Contains("4"))
                ViewBag.ErrLogin = nullErr.errMess;
            if (k.Contains("5"))
                ViewBag.ErrPassword = nullErr.errMess;
            if (k == "")
                return false;
            return true;
        }

    }
}
