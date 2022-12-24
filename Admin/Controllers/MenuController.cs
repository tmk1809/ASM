﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Models;
using Admin.Data;

namespace Admin.Controllers
{
    public class MenuController : Controller
    {
        private readonly FPTBookStore _context;
        public MenuController(FPTBookStore context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(String searchString)
        {
            var accounts = from a in _context.Accounts select a;
            if(!String.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(x => x.Name.Contains(searchString));
            }
            return View(await accounts.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name,Role,Username,Password,ProfilePicture")] Account account)
        {
            if(ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null || _context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if(account == null)
            {
                return NotFound();
            }
            return View(account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Role,Username,Password,ProfilePicture")] Account account)
        {
            if(id != account.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!AccountExists(account.Id))
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
            return View(account);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            if(account == null)
            {
                return NotFound();
            }
            return View(account);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            if(_context.Accounts == null)
            {
                return Problem("Entity set 'FPTBookStore.Account' is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if(account != null)
            {
                _context.Accounts.Remove(account);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
