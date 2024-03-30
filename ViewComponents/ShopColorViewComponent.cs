using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WA_InfoShop.Models;

namespace WA_InfoShop.ViewComponents;

public class ShopColorViewComponent : ViewComponent
{
    private readonly ApplicationContext _context;

    public ShopColorViewComponent(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var data = await _context.ProductProperties.Include(p => p.Property).Where(p => p.PropertyId == 1).ToListAsync();

        return View(data);
    }
}
