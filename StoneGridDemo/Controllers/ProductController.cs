using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoneGridDemo.Data;
using StoneGridDemo.Models;
using StoneGridDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StoneGridDemo.Controllers
{

    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> catItem = _db.Products;
            foreach(var item in catItem)
            {
                item.Category = _db.Categories.FirstOrDefault(u => u.Id == item.Id);
            }
            return View(catItem);
        }

        // GET - UPSERT
        public IActionResult Upsert(int? id)
        {
            //passing data from controller to view using viewBag
            /*  IEnumerable<SelectListItem> CategoryDropDown = _db.Categories.Select(i => new SelectListItem
              {
                  Text = i.Name,
                  Value = i.Id.ToString()
              });

              ViewBag.CategoryropDown = CategoryDropDown;


              Product product = new Product();*/

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };


            if(id == null)
            {
                // for creating product
                return View(productVM);   
            }
            else
            {
                // editing product
                productVM.Product = _db.Products.Find(id);
                if(productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
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
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(productVM.Product.Id == 0)
                {
                    // creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension),FileMode.Create ))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;
                    _db.Products.Add(productVM.Product);
                }
                else
                {
                    // updating
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        // GET -Delete
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

