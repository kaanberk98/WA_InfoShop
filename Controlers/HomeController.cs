using Microsoft.AspNetCore.Mvc;
using WA_InfoShop.Models;

namespace WA_InfoShop.Controlers;

public class HomeController : Controller
{
    private readonly ApplicationContext _context;

    public HomeController(ApplicationContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Products.ToList());
    }
}
