using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WA_InfoShop.Models;

namespace WA_InfoShop.Controlers;

public class ShopController : Controller
{
    private readonly ApplicationContext _context;

    public ShopController(ApplicationContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Products.Include(p => p.Category).Include(p => p.Brand).Include(p=>p.ProductProperties).ToList());
    }

    public IActionResult Detail(int id)
    {
        return View(_context.Products.Include(p => p.Category).Include(p => p.Brand).Include(p => p.ProductProperties).Where(p=> p.Id == id).ToList());
    }
}
