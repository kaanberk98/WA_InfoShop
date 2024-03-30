using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WA_InfoShop.Models;

namespace WA_InfoShop.Areas.Management.Controllers
{
    [Area("Management")]
    public class ProductPropertiesController : Controller
    {
        private readonly ApplicationContext _context;

        public ProductPropertiesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Management/ProductProperties
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.ProductProperties.Include(p => p.Product).Include(p => p.Property);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Management/ProductProperties/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name");
            return View();
        }

        // POST: Management/ProductProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,PropertyId,Name,Id,CreatedDate,UpdatedDate,DeletedDate")] ProductProperty productProperty)
        {
            if (ModelState.IsValid)
            {
                productProperty.CreatedDate = DateTime.UtcNow;
                _context.Add(productProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productProperty.ProductId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", productProperty.PropertyId);
            return View(productProperty);
        }

        // GET: Management/ProductProperties/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProperty = await _context.ProductProperties.FindAsync(id);
            if (productProperty == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productProperty.ProductId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", productProperty.PropertyId);
            return View(productProperty);
        }

        // POST: Management/ProductProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,PropertyId,Name,Id,CreatedDate,UpdatedDate,DeletedDate")] ProductProperty productProperty)
        {
            if (id != productProperty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                productProperty.UpdatedDate = DateTime.UtcNow;
                _context.Update(productProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productProperty.ProductId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", productProperty.PropertyId);
            return View(productProperty);
        }

        // GET: Management/ProductProperties/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProperty = await _context.ProductProperties
                .Include(p => p.Product)
                .Include(p => p.Property)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productProperty == null)
            {
                return NotFound();
            }

            return View(productProperty);
        }

        // POST: Management/ProductProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productProperty = await _context.ProductProperties.FindAsync(id);
            if (productProperty != null)
            {
                _context.ProductProperties.Remove(productProperty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPropertyExists(int id)
        {
            return _context.ProductProperties.Any(e => e.Id == id);
        }
    }
}
