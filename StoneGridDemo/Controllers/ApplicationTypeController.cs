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
            IEnumerable<ApplicationType> appObj = _db.ApplicationTypes;
            return View(appObj);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        } 

        //Post - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType appObj)
        {
            _db.ApplicationTypes.Add(appObj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
