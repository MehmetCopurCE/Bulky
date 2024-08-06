
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // GET
    public IActionResult Index()
    {
        List<Category> objCategoryList = _unitOfWork._categoryRepository.GetAll().ToList();
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
            // _db.Categories.Add(category);
            // _db.SaveChanges();
            
            // _categoryRepository.Add(category);
            // _categoryRepository.Save();
            
            _unitOfWork._categoryRepository.Add(category);
            _unitOfWork.Save();
            
            TempData["success"] = "Category created successfully";
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

        // Category? ctgFromDb = _db.Categories.Find(id);
        // Category? ctgFromDb2 = _db.Categories.FirstOrDefault(id);
        // Category? ctgFromDb3 = _db.Categories.Find(id);

        Category? ctgFromDb = _unitOfWork._categoryRepository.Get(u => u.Id == id);

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
            // _db.Categories.Update(category);
            // _db.SaveChanges();
            
            _unitOfWork._categoryRepository.Update(category);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";

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

        Category? ctgFromDb = _unitOfWork._categoryRepository.Get(u => u.Id == id);
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
        Category? ctgFromDb = _unitOfWork._categoryRepository.Get(u => u.Id == id);
        if (ctgFromDb == null)
        {
            return NotFound();
        }

        // _db.Categories.Remove(ctgFromDb);
        // _db.SaveChanges();
        
        _unitOfWork._categoryRepository.Remove(ctgFromDb);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index","Category");  
    }
}
