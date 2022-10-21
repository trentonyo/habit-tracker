using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Habit_Tracker___Doveloop.Data;
using System;
using System.Linq;

namespace Habit_Tracker___Doveloop.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HabitTrackerContext(
                serviceProvider.GetRequiredService<DbContextOptions<HabitTrackerContext>>()))
            {
                //seed if no days
                if (!context.Day.Any())
                {
                    context.Day.AddRange(
                        new Day
                        {
                            Name = "Monday"
                        },
                        new Day
                        {
                            Name = "Tuesday"
                        },
                        new Day
                        {
                            Name = "Wednesday"
                        },
                        new Day
                        {
                            Name = "Thursday"
                        },
                        new Day
                        {
                            Name = "Friday"
                        },
                        new Day
                        {
                            Name = "Saturday"
                        },
                        new Day
                        {
                            Name = "Sunday"
                        }
                        );
                }

                //seed if no habits
                if (!context.Habit.Any())
                {
                    context.Habit.AddRange(
                        new Habit
                        {
                            Name = "Run A Mile"
                        },
                        new Habit 
                        { 
                            Name = "Sweep Floors"
                        }
                        );
                }

                //seed labels if no habits
                if(!context.Label.Any())
                {
                    context.Label.AddRange(
                        new Label
                        {
                            Name = "Self-Improvement"
                        },
                        new Label
                        {
                            Name = "Cleaning"
                        }
                        );
                }
            }
        }
    }
}
