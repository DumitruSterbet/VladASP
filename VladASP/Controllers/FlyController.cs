using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VladASP.JoinModels;
using VladASP.Models;
using VladASP.ViewModels;

namespace VladASP.Controllers
{
    public class FlyController : Controller
    {
        public static int person = 1;
        public FlyContext db;

        public FlyController (FlyContext context)
        {
            db = context;
        }
        public IActionResult Index(string searchString)
        {
            ViewModel obj = new ViewModel();
           

            if (!String.IsNullOrEmpty(searchString))
            {
                obj.flights = db.flyghts.Where(s => s.Cod.Contains(searchString)).ToList();
                return View(obj.flights);
            }

            return View(db.flyghts.ToList());
        }


        public IActionResult MyOrder()
        {  
            return View(db.orders.Where(u=>u.ClientId==person).ToList());
        }
        public IActionResult Favourite()
        {
           
            
            return View();
        }
        public IActionResult AboutUs()
        {  
            return View();
        }
        public IActionResult FavouriteProd (int id)
        {
            id = 2;
            ViewModel obj = new ViewModel();
            ViewModel source = new ViewModel();
            ViewModel destination = new ViewModel();
            destination.destinCities = db.destinations.ToList();
            source.sourceCities = db.sources.ToList();

            obj.flights = db.flyghts.Where(u => u.SourceId == id).ToList();

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
                    DataDest=u.DataDest,
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
                     DataDest=u.DataDest,
                     Price = u.Price,
                     Source = u.Source,
                     Destination = p.Name
                 }

             ).ToList();

            return View(join1);
        }
    }
}
