using Appmrkt5.Data;
using Appmrkt5.Data.Entities;
using Appmrkt5.Helpers;
using Appmrkt5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appmrkt5.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public ProductsController(DataContext context, ICombosHelper combosHelper, IConverterHelper
            converterHelper, IImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
               .Include(p => p.Category)
               .Include(p => p.ProductImages)
               .ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Products
                .Include(c => c.Category)
                .Include(c => c.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ProductViewModel model = new ProductViewModel
            {
                Categories = _combosHelper.GetComboCategories(),
                IsActive = true
            };
            return View(model);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string path = string.Empty;
                    Product product = await _converterHelper.ToProductAsync(model, true, path);


                    if (model.ImageFile != null)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile);
                        product.ProductImages = new List<ProductImage>
                        {
                            new ProductImage { ImageUrl = path }
                        };
                    }

                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un producto con ese nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Products
                 .Include(p => p.Category)
                 .Include(p => p.ProductImages)
                 .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ProductViewModel model = _converterHelper.ToProductViewModel(product);
            return View(model);
        }

        // POST: Products/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( ProductViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    string path = string.Empty;
                    Product product = await _converterHelper.ToProductAsync(model, false, path);

                    if (model.ImageFile != null)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile);
                        if (product.ProductImages == null)
                        {
                            product.ProductImages = new List<ProductImage>();
                        }

                        product.ProductImages.Add(new ProductImage { ImageUrl = path });
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un producto con ese nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Categories = _combosHelper.GetComboCategories();
            return View(model);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }


            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            AddProductImageViewModel model = new AddProductImageViewModel { ProductId = product.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(AddProductImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = await _context.Products
                    .Include(p => p.ProductImages)
                    .FirstOrDefaultAsync(p => p.Id == model.ProductId);
                if (product == null)
                {
                    return NotFound();
                }

                try
                {
                    string path = string.Empty;
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);
                    if (product.ProductImages == null)
                    {
                        product.ProductImages = new List<ProductImage>();
                    }

                    product.ProductImages.Add(new ProductImage { ImageUrl = path });
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = product.Id });

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductImage productImage = await _context.ProductImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductImages.FirstOrDefault(pi => pi.Id == productImage.Id) != null);
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = product.Id });
        }
    }
}
