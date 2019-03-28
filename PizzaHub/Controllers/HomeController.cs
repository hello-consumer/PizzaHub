using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaHub.Data;
using PizzaHub.Models;

namespace PizzaHub.Controllers
{
    public class HomeController : Controller
    {
        private PizzaDbContext _context;

        public HomeController(PizzaDbContext context)
        {
            this._context = context;
        }

        public IActionResult City(int id)
        {
            return Content("Welcome to city " + id);
        }

        public IActionResult Index()
        {
            ViewData["cities"] = _context.City.ToDictionary(k => k.Id, e => e.Name);
            return View();
        }

        public IActionResult GetLocation(decimal latitude, decimal longitude)
        {
            var cities = _context.City.ToArray();
            City nearestCity = null;
            decimal nearestDistance = decimal.MaxValue;
            foreach (City c in cities)
            {
                decimal distance = (decimal)Math.Sqrt(Math.Pow((double)((c.Latitude ?? 0) - latitude), 2) + Math.Pow((double)((c.Longitude ?? 0) - longitude), 2));
                if (distance < nearestDistance)
                {
                    nearestCity = c;
                    nearestDistance = distance;
                }
            }
            return Content(nearestCity.Id.ToString());

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