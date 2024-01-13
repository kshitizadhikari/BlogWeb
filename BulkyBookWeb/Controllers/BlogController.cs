using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Blog> blogList = _db.Blogs;
            return View(blogList);
        }

        //GET
        public IActionResult Create()
        {
            List<string> categoryList = new List<string>() {
                "Technology",
                "Travel",
                "Food",
                "Fashion",
                "Health",
                "Lifestyle"};
            ViewBag.categoryList = categoryList;
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog obj)
        {
            if((!ModelState.IsValid))
            {
                return View(obj);
            }

            if(obj.Title == obj.Category)
            {
                ModelState.AddModelError("Title", "The title and category cannot be same");
                return View(obj);
            }

            _db.Blogs.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            Blog? obj = _db.Blogs.Find(id);

            if(obj==null)
            {
                return NotFound();
            }

            List<string> categoryList = new List<string>() {
                "Technology",
                "Travel",
                "Food",
                "Fashion",
                "Health",
                "Lifestyle"};

            ViewBag.categoryList = categoryList;
            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog obj)
        {
            if ((!ModelState.IsValid))
            {
                return View(obj);
            }

            if (obj.Title == obj.Category)
            {
                ModelState.AddModelError("Title", "The title and category cannot be same");
                return View(obj);
            }

            _db.Blogs.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
           
            Blog? obj = _db.Blogs.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Blogs.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
