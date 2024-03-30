using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text.Json;
using WA_InfoShop.Models;

namespace WA_InfoShop.Controlers;

public class CartController : Controller
{
    private readonly ApplicationContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    public CartController(ApplicationContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    

    public IActionResult List() 
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        return View(_context.Carts.Where(c => c.UserId == userId).Include(c => c.Product).OrderBy(c => c.CreatedDate).ToList());
    }

    public IActionResult Add(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var cartItem = _context.Carts.Where(c => c.UserId == userId & c.ProductId == id).FirstOrDefault();

        if (cartItem == null) 
        {
            _context.Carts.Add(new()
            {
                ProductId = id,
                UserId = userId,
                Quantity = 1,
                CreatedDate = DateTime.UtcNow
            });
        }
        else
        {
            cartItem.Quantity += 1;
        }

        _context.SaveChanges();

        return RedirectToAction("List", "Cart");
    }

    public IActionResult AddToSession(int id) 
    {
        var product = _context.Products.Find(id);

        if(product == null) 
        {
            var sessionCart = HttpContext.Session.GetString("Cart");

            if (sessionCart != null) 
            {
                List<SessionCartModel> cartList = JsonSerializer.Deserialize<List<SessionCartModel>>(sessionCart);

                var cartItem = cartList.Find(p => p.ProductId == id);

                if(cartItem != null) 
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    cartList.Add(new SessionCartModel
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = 1
                    });
                }

                string modifiedCart = JsonSerializer.Serialize(cartList);

                HttpContext.Session.SetString("Cart",modifiedCart);
            }
            else
            {
                List<SessionCartModel> newCartList = new();

                newCartList.Add(new()
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });

                string newSessionCart = JsonSerializer.Serialize(newCartList);

                HttpContext.Session.SetString("Cart",newSessionCart);
            }
        }

        return RedirectToAction("Index", "Home");
    }


    public IActionResult Delete(int id)
    {
        Cart? crt = _context.Carts.Find(id);

        if (crt != null)
        {
            _context.Carts.Remove(crt);

            _context.SaveChanges();
        }

        return RedirectToAction("List", "Cart");
    }

    public IActionResult QuantityAdd(int id)
    {
        Cart? crt = _context.Carts.Find(id);

        if (crt != null)
        {
            crt.Quantity += 1;

            _context.SaveChanges();
        }

        return RedirectToAction("List", "Cart");
    }

    public IActionResult QuantityRemove(int id)
    {
        Cart? crt = _context.Carts.Find(id);

        if (crt != null)
        {
            if (crt.Quantity > 1)
            {
                crt.Quantity -= 1;
            }
            else
            {
                _context.Carts.Remove(crt);
            }

            _context.SaveChanges();
        }

        return RedirectToAction("List", "Cart");
    }
}
