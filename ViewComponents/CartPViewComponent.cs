using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WA_InfoShop.Models;

namespace WA_InfoShop.ViewComponents;

public class CartPViewComponent : ViewComponent
{
    private readonly ApplicationContext _context;

    public CartPViewComponent(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var data = await _context.ProductProperties.Include(p => p.Property).Where(p => p.ProductId == id).ToListAsync();

        return View(data);
    }
}
