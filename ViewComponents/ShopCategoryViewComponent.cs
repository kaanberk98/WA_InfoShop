using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WA_InfoShop.Models;

namespace WA_InfoShop.ViewComponents;

public class ShopCategoryViewComponent : ViewComponent
{
    private readonly ApplicationContext _context;

    public ShopCategoryViewComponent(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var data = _context.Categories.Where(p => p.SupCategoryId == 1).Where(p => p.DeletedDate == null).ToList();

        return View(data);
}
}
