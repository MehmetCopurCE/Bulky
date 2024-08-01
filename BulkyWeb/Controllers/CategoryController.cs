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
            //Key for the validation type int the create page. 
            ModelState.AddModelError("name", "The display order can not mach with name");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Add(category);
            _db.SaveChanges(); //Update the db
            //return RedirectToAction("Index", "Category"); we can give category name as second parameter
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
        Category? categoryFromDb = _db.Categories.Find(id);
        // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(c => c.Id == id);
        // Category? categoryFromDb2 = _db.Categories.Where(c => c.Id == id).FirstOrDefault();
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _db.Categories.Update(category);
            _db.SaveChanges(); //Update the db
            //return RedirectToAction("Index", "Category"); we can give category name as second parameter
            return RedirectToAction("Index");
        }
        return View();
    }
}