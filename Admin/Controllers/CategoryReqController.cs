using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Admin.Models;
using Admin.Data;

namespace Admin.Controllers
{
    public class CategoryReqController : Controller
    {
        private readonly FPTBookStore _context;
        public CategoryReqController(FPTBookStore context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoryReqs.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Req")] CategoryReq categoryreq)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryreq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryreq);
        }
    }
}
