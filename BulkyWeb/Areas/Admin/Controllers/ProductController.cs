using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        List<Product> productList = _unitOfWork._productRepository.GetAll(includeProperties:"Category").ToList();
        return View(productList);
    }

    public IActionResult Upsert(int? id) //create => Update - Insert = Upsert
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
        if (id == null || id == 0)
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
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, "images", "product");
                if (!Directory.Exists(productPath))
                {
                    Directory.CreateDirectory(productPath);
                }
                if(!string.IsNullOrEmpty(productVm.Product.ImageUrl))
                {
                    //delete old image
                    string oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                
                using(var stream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                
                productVm.Product.ImageUrl = Path.Combine("images", "product", fileName).Replace("\\", "/");
            }

            if (productVm.Product.Id == 0)
            {
                _unitOfWork._productRepository.Add(productVm.Product);
            }else
            {
                _unitOfWork._productRepository.Update(productVm.Product);
            }
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index", "Product");
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

    /*public IActionResult Delete(int? id)
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
        return RedirectToAction("Index", "Product");
    }*/

   
    
    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<Product> allObj = _unitOfWork._productRepository.GetAll(includeProperties: "Category");
        return Json(new { data = allObj });
    }
    
    public IActionResult Delete(int? id)
    {
        var productToBeDeleted = _unitOfWork._productRepository.Get(u=> u.Id == id);
        if (productToBeDeleted == null)
        {
            return Json(new {succes = false, message = "Error while deleting"});
        }
        
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        string imagePath = Path.Combine(wwwRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }
        
        _unitOfWork._productRepository.Remove(productToBeDeleted);
        _unitOfWork.Save();
        return Json( new {success = true, message = "Delete successful"}); 
    }
    #endregion
}