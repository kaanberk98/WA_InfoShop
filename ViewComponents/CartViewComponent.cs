using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security.Claims;
using WA_InfoShop.Models;

namespace WA_InfoShop.ViewComponents;

public class CartViewComponent : ViewComponent
{
    private readonly ApplicationContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public CartViewComponent(ApplicationContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        //var userId = await User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Giriş Yapmış Kullanıcıyı Bulmamızı Sağlar.
        //var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
        
        var data = await _context.Carts.Where(c => c.UserId == "2").ToListAsync();

        return View(data);
    }
}
