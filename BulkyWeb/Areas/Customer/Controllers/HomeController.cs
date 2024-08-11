using System.Diagnostics;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models;

namespace BulkyWeb.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    

    public IActionResult Index() 
    {
        
        IEnumerable<Product> productList = _unitOfWork._productRepository.GetAll(includeProperties:"Category").ToList();
        
        //If we do not give a name for view it takes action
        //names and search in the view folder as controller name 
        return View(productList);

        //If I give a name to view, it takes view which has that name
        //in the view folder which named as tha same as controller name
        //return View("Privacy");
    }
    
    public IActionResult Details(int productId)
    {
        var  product = _unitOfWork._productRepository.Get(u => u.Id == productId, includeProperties:"Category");
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}