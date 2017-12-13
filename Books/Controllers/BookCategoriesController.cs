using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Data;
using Books.Models;

namespace Books.Controllers
{
    public class BookCategoriesController : Controller
    {
        private readonly BooksDbContext _context;

        public BookCategoriesController(BooksDbContext context)
        {
            _context = context;    
        }

        // GET: BookCategories
        public async Task<IActionResult> Index()
        {
            var booksDbContext = _context.BooksCategories.Include(b => b.Book).Include(b => b.Category);
            return View(await booksDbContext.ToListAsync());
        }

        // GET: BookCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BooksCategories
                .Include(b => b.Book)
                .Include(b => b.Category)
                .SingleOrDefaultAsync(m => m.BookId == id);
            if (bookCategory == null)
            {
                return NotFound();
            }

            return View(bookCategory);
        }

        // GET: BookCategories/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: BookCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,CategoryId")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookCategory.BookId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", bookCategory.CategoryId);
            return View(bookCategory);
        }

        // GET: BookCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BooksCategories.SingleOrDefaultAsync(m => m.BookId == id);
            if (bookCategory == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookCategory.BookId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", bookCategory.CategoryId);
            return View(bookCategory);
        }

        // POST: BookCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,CategoryId")] BookCategory bookCategory)
        {
            if (id != bookCategory.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCategoryExists(bookCategory.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookCategory.BookId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", bookCategory.CategoryId);
            return View(bookCategory);
        }

        // GET: BookCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BooksCategories
                .Include(b => b.Book)
                .Include(b => b.Category)
                .SingleOrDefaultAsync(m => m.BookId == id);
            if (bookCategory == null)
            {
                return NotFound();
            }

            return View(bookCategory);
        }

        // POST: BookCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookCategory = await _context.BooksCategories.SingleOrDefaultAsync(m => m.BookId == id);
            _context.BooksCategories.Remove(bookCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BookCategoryExists(int id)
        {
            return _context.BooksCategories.Any(e => e.BookId == id);
        }
    }
}
