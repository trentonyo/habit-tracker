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
    public class LabelsController : Controller
    {
        private readonly HabitTrackerContext _context;

        public LabelsController(HabitTrackerContext context)
        {
            _context = context;
        }

        // GET: Labels
        public async Task<IActionResult> Index()
        {
              return View(await _context.Label.Where(l => l.User == HttpContext.User.Identity.Name).ToListAsync());
        }

        // GET: Labels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Label == null)
            {
                return NotFound();
            }

            var label = await _context.Label
                .FirstOrDefaultAsync(m => m.Id == id);
            if (label == null || label.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            return View(label);
        }

        // GET: Labels/Create
        public IActionResult Create()
        {
            Label label = new Label();
            label.User = HttpContext.User.Identity.Name;
            return View(label);
        }

        // POST: Labels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,User")] Label label)
        {
            //model state isnt valid
            //if (ModelState.IsValid)
            //{
                _context.Add(label);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(label);
        }

        // GET: Labels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Label == null)
            {
                return NotFound();
            }

            var label = await _context.Label.FindAsync(id);
            if (label == null || label.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }
            return View(label);
        }

        // POST: Labels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,User")] Label label)
        {
            if (id != label.Id || label.Name != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            //model state isnt valid
            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(label);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabelExists(label.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //return View(label);
        }

        // GET: Labels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Label == null)
            {
                return NotFound();
            }

            var label = await _context.Label
                .FirstOrDefaultAsync(m => m.Id == id);
            if (label == null || label.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            return View(label);
        }

        // POST: Labels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Label == null)
            {
                return Problem("Entity set 'HabitTrackerContext.Label'  is null.");
            }
            var label = await _context.Label.FindAsync(id);
            if (label != null && label.User == HttpContext.User.Identity.Name)
            {
                _context.Label.Remove(label);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabelExists(int id)
        {
          return _context.Label.Any(e => e.Id == id);
        }
    }
}
