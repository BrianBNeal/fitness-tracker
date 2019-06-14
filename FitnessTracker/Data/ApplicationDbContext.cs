using System;
using System.Collections.Generic;
using System.Text;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Motivation> Motivations { get; set; }
        public DbSet<ExertionLevel> ExertionLevels { get; set; }
        public DbSet<EnjoymentLevel> EnjoymentLevels { get; set; }


    }
}
