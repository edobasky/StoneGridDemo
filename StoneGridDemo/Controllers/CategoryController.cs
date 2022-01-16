using Microsoft.AspNetCore.Mvc;
using StoneGridDemo.Data;
using StoneGridDemo.Models;
using System.Collections.Generic;

namespace StoneGridDemo.Controllers
{

    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> catItem = _db.Categories;
            return View(catItem);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }
    }
}
