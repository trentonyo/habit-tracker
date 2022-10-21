using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Habit_Tracker___Doveloop.Data;
using Habit_Tracker___Doveloop.Models;

namespace Habit_Tracker___Doveloop.Controllers
{
    public class EntriesController : Controller
    {
        private readonly HabitTrackerContext _context;

        public EntriesController(HabitTrackerContext context)
        {
            _context = context;
        }

        // GET: Entries
        public async Task<IActionResult> Index()
        {
              return View(await _context.HabitEntry.Where(e => e.User == HttpContext.User.Identity.Name).ToListAsync());
        }

        // GET: Entries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HabitEntry == null)
            {
                return NotFound();
            }

            var entry = await _context.HabitEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null || entry.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            return View(entry);
        }

        // GET: Entries/Create
        public IActionResult Create()
        {
            HabitEntry entry = new HabitEntry();
            entry.User = HttpContext.User.Identity.Name;
            return View(entry);
        }

        // POST: Entries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EntryTime,HabitId,User")] HabitEntry entry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entry);
        }

        // GET: Entries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HabitEntry == null)
            {
                return NotFound();
            }

            var entry = await _context.HabitEntry.FindAsync(id);
            if (entry == null || entry.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }
            return View(entry);
        }

        // POST: Entries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EntryTime,HabitId,User")] HabitEntry entry)
        {
            if (id != entry.Id || entry.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntryExists(entry.Id))
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
            return View(entry);
        }

        // GET: Entries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HabitEntry == null)
            {
                return NotFound();
            }

            var entry = await _context.HabitEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null || entry.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: Entries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HabitEntry == null)
            {
                return Problem("Entity set 'HabitTrackerContext.Entry'  is null.");
            }
            var entry = await _context.HabitEntry.FindAsync(id);
            if (entry != null && entry.User == HttpContext.User.Identity.Name)
            {
                _context.HabitEntry.Remove(entry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntryExists(int id)
        {
          return _context.HabitEntry.Any(e => e.Id == id);
        }
    }
}
