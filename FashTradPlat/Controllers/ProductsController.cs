using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FashTradPlat.Areas.Identity.Data;
using FashTradPlat.Models;
using Microsoft.AspNetCore.Authorization;

namespace FashTradPlat.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var products = from s in _context.Products
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Product_name.Contains(searchString)
                                       || s.Product_description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Product_name);
                    break;
                default:
                    products = products.OrderBy(s => s.Product_name);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Products/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Category_ID", "Category_ID");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,CategoryID,Image,Product_name,Product_description,Product_request,ImageFile")] Product product)
        {
            if (!ModelState.IsValid)
            {
                // Saves image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                product.ImageName=fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                // Insert record
                    _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Category_ID", "Category_ID", product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Category_ID", "Category_ID", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,CategoryID,Image,Product_name,Product_description,Product_request,ImageFile,ImageName")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing product entity without tracking
                    var existingProduct = await _context.Products
                        .FirstOrDefaultAsync(e => e.ProductID == id);

                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Update specific properties
                    existingProduct.CategoryID = product.CategoryID;
                    existingProduct.Product_name = product.Product_name;
                    existingProduct.Product_description = product.Product_description;
                    existingProduct.Product_request = product.Product_request;
                    existingProduct.ImageFile = product.ImageFile;

                    // updates image file if a new file is uploaded
                    if (product.ImageFile != null && product.ImageFile.Length > 0)
                    {


                        // Generate a unique file name for the new image
                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(fileStream);
                        }
                        existingProduct.ImageName = uniqueFileName;
                    }
                    else if (string.IsNullOrEmpty(product.ImageName))
                    {
                        // If no new ImageFile is provided and Name is null
                        // set it to the existing ImageName to maintain the current image
                        existingProduct.ImageName = existingProduct.ImageName;
                    }

                    _context.Update(existingProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Category_ID", "Category_ID", product.CategoryID);
            return View(product);
        }

        // Deletes old image file if a new one is uploaded
        private void DeleteOldImage(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        // GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            // Deletes image from wwwroot/images upon its record deletion
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath,"Image",product.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
