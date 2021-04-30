using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VladASP.JoinModels;
using VladASP.Models;
using VladASP.ViewModels;

namespace VladASP.Controllers
{
    public class FlyController : Controller
    {
        public static int person;
   
        public FlyContext db;
        
        public FlyController (FlyContext context)
        {
            db = context;
            
       
        }
        
        public IActionResult OrderBuy(int id)
        {
            Order order = new Order();
            order.FlyghtId = id;
            Client user = new Client();
            user = db.clients.FirstOrDefault(u => u.Login == User.Identity.Name);
            order.ClientId = user.Id;
            person = user.Id;

            db.orders.Add(order);
            db.SaveChanges();
            return View();
        }
        [Authorize]
        public IActionResult Index(string searchString)
        {
            ViewModel obj = new ViewModel();
            List<WithCityname> join1 = new List<WithCityname>();

            if (!String.IsNullOrEmpty(searchString))
            {
                obj.flights = db.flyghts.Where(s => s.Cod.Contains(searchString)).ToList();
                join1 = Join(obj);

               return View(join1);
            }
 
            obj.flights = db.flyghts.ToList();
            join1 = Join(obj);

            return View(join1);
        }


        public IActionResult MyOrder()
        {
            ViewModel obj = new ViewModel();
            obj.orders = db.orders.Where(u => u.ClientId == person).ToList();

            obj.flights = obj.orders.Join(db.flyghts,
                u => u.FlyghtId,
                p => p.Id,
                (u, p) => new Flyght
                {
                    Cod = p.Cod,
                    Id = p.Id,
                    SourceId = p.SourceId,
                    DataDest = p.DataDest,
                    DateSource = p.DateSource,
                    Price = p.Price,
                    DestinationId = p.DestinationId,
                    Favorite = p.Favorite
                }).ToList();


            return View(obj.flights);
        }
        [Authorize]
        public IActionResult Favourite(int id)
        { Client user = new Client();
            user = db.clients.FirstOrDefault(u => u.Login == User.Identity.Name);
            person = user.Id;
            
            return View();
        }
        public IActionResult AboutUs()
        {  
            return View();
        }
        [Authorize]
        public IActionResult FavouriteProd (int id)
        {
           
            ViewModel obj = new ViewModel();

            

            obj.flights = db.flyghts.Where(u => u.SourceId == id).ToList();

            List<WithCityname> join = Join(obj);

            return View(join);
        }

        public List<WithCityname> Join(ViewModel obj)
        {
           
            ViewModel source = new ViewModel();
            ViewModel destination = new ViewModel();
            destination.destinCities = db.destinations.ToList();
            source.sourceCities = db.sources.ToList();
            
            List<WithCityname> join1 = new List<WithCityname>();
            List<WithSource> join2 = new List<WithSource>();
            join2 = obj.flights.Join(
                source.sourceCities,
                u => u.SourceId,
                p => p.Id,
                (u, p) => new WithSource
                {
                    Id = u.Id,
                    Cod = u.Cod,
                    Favorite = u.Favorite,
                    DateSource = u.DateSource,
                    DataDest = u.DataDest,
                    Price = u.Price,
                    Source = p.Name,
                    DestinationId = u.DestinationId
                }).ToList();

            join1 = join2.Join(destination.destinCities,
                 u => u.DestinationId,
                 p => p.Id,
                 (u, p) => new WithCityname
                 {
                     Id = u.Id,
                     Cod = u.Cod,
                     Favorite = u.Favorite,
                     DateSource = u.DateSource,
                     DataDest = u.DataDest,
                     Price = u.Price,
                     Source = u.Source,
                     Destination = p.Name
                 }

             ).ToList();
            return join1;
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Sign");
        }
    }
}
