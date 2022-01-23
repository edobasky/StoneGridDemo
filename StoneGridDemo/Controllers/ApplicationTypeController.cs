using Microsoft.AspNetCore.Mvc;
using StoneGridDemo.Data;
using StoneGridDemo.Models;
using System.Collections.Generic;

namespace StoneGridDemo.Controllers
{
   
     public class ApplicationTypeController : Controller
    {
        private readonly AppDbContext _db;

        public ApplicationTypeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> catItem = _db.ApplicationTypes;
            return View(catItem);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        // create -POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType item)
        {
            if(ModelState.IsValid)
            {
                _db.ApplicationTypes.Add(item);
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
        public IActionResult Edit(ApplicationType item)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationTypes.Update(item);
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
            var obj = _db.ApplicationTypes.Find(id);

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
            var item = _db.ApplicationTypes.Find(id);
           
            if (item == null)
            {
                return NotFound();
            }
                _db.ApplicationTypes.Remove(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            

        }
    }
}
