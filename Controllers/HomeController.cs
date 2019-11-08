using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CRUDelicious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dishes> AllDishes = dbContext.Dishes.ToList();
            return View(AllDishes);
        }

        [HttpGet("/{DishID}")]
        public IActionResult DishDeetsPage(int DishID)
        {
            Dishes OneDish = dbContext.Dishes.FirstOrDefault(DishDeets => DishDeets.DishID == DishID);
            return View(OneDish);
        }

        [HttpGet("/new")]
        public IActionResult AddDishPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDish(Dishes newDish)
        {
            dbContext.Add(newDish);
            dbContext.SaveChanges();
            // Console.WriteLine("WHASSUP FOOOOOOOOOOOOOOOOOOOOOOOOOO");
            return RedirectToAction("Index");
        }

        [HttpPost("/{DishID}")]
        public IActionResult DeleteDish(int DishID)
        {
            Dishes RetreivedDish = dbContext.Dishes.SingleOrDefault(dish => dish.DishID == DishID);
            dbContext.Dishes.Remove(RetreivedDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/edit/{DishID}")]
        public IActionResult EditDishPage(int DishID)
        {
            Dishes thisDish = dbContext.Dishes.SingleOrDefault(dish => dish.DishID == DishID);
            
            return View(thisDish);
        }
        [HttpPost]
        public IActionResult EditDish(Dishes Dish)
        {
            Console.WriteLine($"The edit dish method is running");
            dbContext.Dishes.Update(Dish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}