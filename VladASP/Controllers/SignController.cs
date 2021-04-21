using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VladASP.Models;

namespace VladASP.Controllers
{
    public class SignController : Controller
    {
        public FlyContext db;

        public SignController(FlyContext context)
        {
            db = context;
                  }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Client client)
        {  if (client != null)
            {
                Client person = new Client();
               
                person = db.clients.FirstOrDefault(u => u.Login == client.Login);
                if (person == client)
                {
                    return RedirectToAction("Index", "Fly");
                }
            }

            return RedirectToAction("Index", "Sign");
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
