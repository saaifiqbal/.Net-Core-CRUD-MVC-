using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoryController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order And Category Name Can't Be Same");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Name Can not Named Test");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = db.Categories.Find(id);
            Category? categoryFromDb1 = db.Categories.FirstOrDefault(u=>u.Id==id);
            Category? categoryFromDb2 = db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if(categoryFromDb == null) { return NotFound(); }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj) {
            if(ModelState.IsValid)
            {
                db.Categories.Update(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int? id) {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = db.Categories.Find(id);
            db.Categories.Remove(categoryFromDb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
