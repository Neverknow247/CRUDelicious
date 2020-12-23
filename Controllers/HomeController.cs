using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }
        private readonly ILogger<HomeController> _logger;

        // public HomeController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("")]
        public IActionResult Main()
        {
            List<Dishes> AllDishes = _context.Dishes.OrderByDescending(i => i.CreatedAt).ToList();
            return View(AllDishes);
        }
        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost("AddDish")]
        public IActionResult AddDish(Dishes dish)
        {
            if(ModelState.IsValid)
            {
                _context.Add(dish);
                _context.SaveChanges();
                return RedirectToAction("Main");
            }
            else
            {
                return View("Add");
            }
        }
        [HttpGet("View/{id}")]
        public IActionResult View(int id)
        {
            Dishes dish = _context.Dishes.FirstOrDefault(i => i.DishId == id);
            ViewBag.dish = dish;
            return View("View",dish);
        }
        [HttpGet("View/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Dishes dish = _context.Dishes.SingleOrDefault(i => i.DishId == id);
            _context.Dishes.Remove(dish);
            _context.SaveChanges();
            return RedirectToAction("Main");
        }
        [HttpGet("View/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            Dishes dish = _context.Dishes.FirstOrDefault(i => i.DishId == id);
            return View("Edit", dish);
        }
        [HttpPost("UpdateDish/{id}")]
        public IActionResult UpdateDish(int id, Dishes dish)
        {
            if(ModelState.IsValid)
            {
                Dishes RetrievedDish = _context.Dishes.FirstOrDefault(i => i.DishId == id);
                RetrievedDish.Chef = dish.Chef;
                RetrievedDish.Name = dish.Name;
                RetrievedDish.Calories = dish.Calories;
                RetrievedDish.Tastiness = dish.Tastiness;
                RetrievedDish.Description = dish.Description;
                RetrievedDish.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return Redirect($"/View/{id}");
                // return RedirectToAction("Main");
            }
            else
            {
                return View("Edit", dish);
            }
        }
    }
}
