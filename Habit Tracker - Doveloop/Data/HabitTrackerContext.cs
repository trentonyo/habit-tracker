using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Habit_Tracker___Doveloop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Habit_Tracker___Doveloop.Data
{
    public class HabitTrackerContext : DbContext
    {
        public HabitTrackerContext (DbContextOptions<HabitTrackerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HabitLabel>()
                .HasOne(h => h.Habit)
                .WithMany(l => l.Labels)
                .HasForeignKey(hi => hi.HabitId);

            modelBuilder.Entity<HabitLabel>()
                .HasOne(l => l.Label)
                .WithMany(h => h.Habits)
                .HasForeignKey(li => li.LabelId);

            modelBuilder.Entity<HabitLabel>()
                .HasKey(hl => new { hl.HabitId, hl.LabelId });

            modelBuilder.Entity<HabitDay>()
                .HasOne(h => h.Habit)
                .WithMany(d => d.Days)
                .HasForeignKey(hi => hi.HabitId);

            modelBuilder.Entity<HabitDay>()
                .HasOne(d => d.Day)
                .WithMany(h => h.Habits)
                .HasForeignKey(di => di.DayId);

            modelBuilder.Entity<HabitDay>()
                .HasKey(hd => new {hd.HabitId, hd.DayId});
        }

        public DbSet<Habit> Habit { get; set; } = default!;

        public DbSet<Label> Label { get; set; }

        public DbSet<Day> Day { get; set; }

        public DbSet<HabitEntry> HabitEntry { get; set; }

        public DbSet<HabitLabel> Habits_Labels { get; set; }

        public DbSet<HabitDay> Habits_Days { get; set; }
    }
}
