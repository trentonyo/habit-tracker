using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Habit_Tracker___Doveloop.Data;
using Habit_Tracker___Doveloop.Models;
using Habit_Tracker___Doveloop.Models.ViewModels;

namespace Habit_Tracker___Doveloop.Controllers
{
    public class HabitsController : Controller
    {
        private readonly HabitTrackerContext _context;

        public HabitsController(HabitTrackerContext context)
        {
            _context = context;
        }

        private async Task<Habit?> GetHabit(int? id)
        {
            return await _context.Habit
                .Include(h => h.Labels.Where(hl => hl.HabitId == id))
                .Include(h => h.Days.Where(hd => hd.HabitId == id))
                .Include(h => h.HabitEntries.Where(he => he.HabitId == id))
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        private HabitViewModel CreateHabitView(Habit habit)
        {
            HabitViewModel viewModel = new HabitViewModel();

            List<Label> labels = new List<Label>();
            List<Day> days = new List<Day>();

            viewModel.Habit = habit;
            //labels
            foreach (HabitLabel habitLabel in habit.Labels)
                _context.Entry(habitLabel).Reference(hl => hl.Label).Load();
            habit.Labels.ForEach(hl => labels.Add(hl.Label));
            viewModel.Labels = labels;
            //days
            foreach (HabitDay habitDay in habit.Days)
                _context.Entry(habitDay).Reference(hd => hd.Day).Load();
            habit.Days.ForEach(hd => days.Add(hd.Day));
            viewModel.Days = days;
            //habit entries
            viewModel.HabitEntries = habit.HabitEntries;

            return viewModel;
        }

        // GET: Habits
        public async Task<IActionResult> Index()
        {
            return View(await _context.Habit.Where(h => h.User == HttpContext.User.Identity.Name).ToListAsync());
        }

        // GET: Habits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Habit == null)
            {
                return NotFound();
            }

            var habit = GetHabit(id).Result;
            if (habit == null || habit.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }
            
            return View(CreateHabitView(habit));
        }

        // GET: Habits/Create
        public IActionResult Create()
        {
            Habit habit = new Habit();
            habit.User = HttpContext.User.Identity.Name;
            return View(habit);
        }

        // POST: Habits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,User")] Habit habit)
        {
            //if (ModelState.IsValid)
            if (!string.IsNullOrEmpty(habit.User) && !string.IsNullOrEmpty(habit.Name))
            {
                _context.Add(habit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(habit);
        }

        // GET: Habits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Habit == null)
            {
                return NotFound();
            }

            var habit = GetHabit(id).Result;
            if (habit == null || habit.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }
            HabitViewModel viewModel = CreateHabitView(habit);
            PopulateAppliedLabelData(viewModel);
            return View(viewModel);
        }

        private void PopulateAppliedLabelData(HabitViewModel habitViewModel)
        {
            var allLabels = _context.Label.Where(l => l.User == HttpContext.User.Identity.Name);
            var habitLabels = new HashSet<int>(habitViewModel.Labels.Select(l => l.Id));
            var labelViewModel = new List<AppliedLabelData>();
            foreach (var label in allLabels)
            {
                labelViewModel.Add(new AppliedLabelData
                {
                    Id = label.Id,
                    Name = label.Name,
                    IsApplied = habitLabels.Contains(label.Id)
                });
            }
            ViewBag.Labels = labelViewModel;
        }

        private void UpdateHabitLabels(Habit habitToUpdate, string[] selectedLabels)
        {
            if(selectedLabels == null)
            {
                habitToUpdate.Labels = new List<HabitLabel>();
                return;
            }

            var selectedLabelsHS = new HashSet<string>(selectedLabels);
            var habitLabels = new HashSet<int>(habitToUpdate.Labels.Select(l => l.LabelId));
            foreach (var label in _context.Label)
            {
                if (selectedLabelsHS.Contains(label.Id.ToString()))
                {
                    if(!habitLabels.Contains(label.Id))
                    {
                        habitToUpdate.Labels.Add(new HabitLabel
                        {
                            HabitId = habitToUpdate.Id,
                            LabelId = label.Id
                        });
                    }
                } 
                else if(habitLabels.Contains(label.Id))
                {
                    habitToUpdate.Labels.Remove(habitToUpdate.Labels.Find(hl => hl.LabelId == label.Id));
                }
            }
        }

        // POST: Habits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string habitName, string[] selectedLabels)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitToUpdate = GetHabit(id).Result;

            if(habitToUpdate == null || habitToUpdate.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            //if(TryUpdateModelAsync<Habit>(habitToUpdate, "", h => h.Name).Result)
            //{
                try
                {
                    habitToUpdate.Name = habitName;
                    UpdateHabitLabels(habitToUpdate, selectedLabels);
                    ///TODO: also update days and entries
                    _context.SaveChanges();

                    return RedirectToAction("Details", new { id = id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
            //}
            HabitViewModel viewModel = CreateHabitView(habitToUpdate);
            PopulateAppliedLabelData(viewModel);
            return View(viewModel);
        }

        // GET: Habits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Habit == null)
            {
                return NotFound();
            }

            var habit = await _context.Habit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habit == null || habit.User != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            return View(habit);
        }

        // POST: Habits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Habit == null)
            {
                return Problem("Entity set 'HabitTrackerContext.HabitModel'  is null.");
            }
            var habit = await _context.Habit.FindAsync(id);
            if (habit != null && habit.User == HttpContext.User.Identity.Name)
            {
                _context.Habit.Remove(habit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitExists(int id)
        {
          return _context.Habit.Any(e => e.Id == id);
        }
    }
}
