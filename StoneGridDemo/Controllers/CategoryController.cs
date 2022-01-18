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
        public IActionResult CreateView()
        {
            return View();
        }

        // create -POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category item)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Add(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
           
        }

        // GET -EDIT
        public IActionResult Edit(int? id)
        {
            if(id == null  || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        // create -POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category item)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);

        }

        // GET -EDIT
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        // create -POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var item = _db.Categories.Find(id);
           
            if (item == null)
            {
                return NotFound();
            }
                _db.Categories.Remove(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            

        }
    }
}

