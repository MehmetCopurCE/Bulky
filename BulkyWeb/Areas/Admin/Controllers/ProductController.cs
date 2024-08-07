using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }   
   
    public IActionResult Index()
    {
        List<Product> productList = _unitOfWork._productRepository.GetAll().ToList();
        return View(productList);
    }
    
    public IActionResult Upsert(int? id)  //create => Update - Insert = Upsert
    {
        // IEnumerable<SelectListItem> CategoryList = _unitOfWork._categoryRepository.
        //     GetAll().Select(u => new SelectListItem
        //     {
        //         Text = u.Name,
        //         Value = u.Id.ToString()
        //     });
        
        //ViewBag.CategoryList = CategoryList;
        //ViewData["CategoryList"] = CategoryList;

        ProductVM productVm = new()
        {
            CategoryList = _unitOfWork._categoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            Product = new Product()
        };
        if(id == null || id == 0)
        {
            //create
            return View(productVm);
        }
        else
        {
            //update
            productVm.Product = _unitOfWork._productRepository.Get(u => u.Id == id);
            return View(productVm);

        }
    }
    
    [HttpPost]
    public IActionResult Upsert(ProductVM productVm, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork._productRepository.Add(productVm.Product);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index","Product");  
        }
        else
        {
            productVm.CategoryList = _unitOfWork._categoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(productVm);
        }
    }
    
    /*public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Product product = _unitOfWork._productRepository.Get(u => u.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork._productRepository.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfully";
            return RedirectToAction("Index","Product");  
        }
        return View();  
    }*/
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Product product = _unitOfWork._productRepository.Get(u => u.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    
    [HttpPost]
    public IActionResult Delete(Product product)
    {
        _unitOfWork._productRepository.Remove(product);
        _unitOfWork.Save();
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index","Product");  
    }
   
}