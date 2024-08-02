using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        List<Category> objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("", "The name and display order can not match!");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index","Category");  
        }
        return View();  
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? ctgFromDb = _db.Categories.Find(id);
        // Category? ctgFromDb2 = _db.Categories.FirstOrDefault(id);
        // Category? ctgFromDb3 = _db.Categories.Find(id);

        if (ctgFromDb == null)
        {
            return NotFound();
        }

        return View(ctgFromDb);

    }
    
    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
        return View();  
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? ctgFromDb = _db.Categories.Find(id);
        // Category? ctgFromDb2 = _db.Categories.FirstOrDefault(u => u.id == id);
        // Category? ctgFromDb3 = _db.Categories.where(u => u.id == id).FirstOrDefault();

        if (ctgFromDb == null)
        {
            return NotFound();
        }

        return View(ctgFromDb);

    }
    
    [HttpPost][ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? ctgFromDb = _db.Categories.Find(id);
        if (ctgFromDb == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(ctgFromDb);
        _db.SaveChanges();
        return RedirectToAction("Index","Category");  
    }
}
