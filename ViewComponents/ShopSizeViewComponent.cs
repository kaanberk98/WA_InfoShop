using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WA_InfoShop.Models;

namespace WA_InfoShop.ViewComponents;

public class ShopSizeViewComponent : ViewComponent
{
    private readonly ApplicationContext _context;

    public ShopSizeViewComponent(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var data = await _context.ProductProperties.Include(p => p.Property).Where(p => p.PropertyId == 2).ToListAsync();

        return View(data);
    }
}
