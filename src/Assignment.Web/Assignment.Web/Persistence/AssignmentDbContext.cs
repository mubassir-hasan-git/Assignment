using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Web.Persistence
{
    public class AssignmentDbContext:DbContext
    {
        public AssignmentDbContext(DbContextOptions<AssignmentDbContext> options) : base(options)
        {
        }
        public DbSet<BusinessEntities> BusinessEntities { get; set; }
        public DbSet<MarkupPlan> MarkupPlan { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarkupPlan>().HasData(
                new MarkupPlan
                {
                    Id = 1,
                    Name = "HT Agent and Transaction Above 15 Lac"
                },
                new MarkupPlan
                {
                    Id = 2,
                    Name = "HT Agent and Transaction Above 35 Lac"
                },
                new MarkupPlan
                {
                    Id = 3,
                    Name = "HT Agent and Transaction Above 45 Lac"
                }
            );

            modelBuilder.Entity<BusinessEntities>().HasData(
                new BusinessEntities
                {
                    BusinessId = 1,
                    Name = "Eco Travels",
                    Code = "H1213",
                    Email = "test@gmail.com",
                    Balance = 0,
                    City = "Dhaka",
                    Country = "Bangladesh",
                    CreatedOnUtc = DateTime.UtcNow,
                    CurrentBalance = 0,
                    Logo = "default.png",
                    MarkupPlanId = 1,
                    Mobile = "01455654255",
                    State = "Dhaka",
                    Status = BusinessStatus.Active,
                    UpdatedOnUtc = DateTime.UtcNow,
                    Zip = "1522"
                },
                new BusinessEntities
                {
                    BusinessId = 2,
                    Name = "Nazma Travels",
                    Code = "1213",
                    Email = "nz@gmail.com",
                    Balance = 0,
                    City = "Dhaka",
                    Country = "Bangladesh",
                    CreatedOnUtc = DateTime.UtcNow,
                    CurrentBalance = 0,
                    Logo = "default.png",
                    MarkupPlanId = 1,
                    Mobile = "01455654255",
                    State = "Dhaka",
                    Status = BusinessStatus.Active,
                    UpdatedOnUtc = DateTime.UtcNow,
                    Zip = "1522"
                },
                new BusinessEntities
                {
                    BusinessId = 3,
                    Name = "Desh Travels",
                    Code = "F4555",
                    Email = "desh@gmail.com",
                    Balance = 0,
                    City = "Dhaka",
                    Country = "Bangladesh",
                    CreatedOnUtc = DateTime.UtcNow,
                    CurrentBalance = 0,
                    Logo = "default.png",
                    MarkupPlanId = 1,
                    Mobile = "01455654255",
                    State = "Dhaka",
                    Status = BusinessStatus.Active,
                    UpdatedOnUtc = DateTime.UtcNow,
                    Zip = "1522"
                },
                new BusinessEntities
                {
                    BusinessId = 4,
                    Name = "Alone Travels",
                    Code = "H1213",
                    Email = "test@gmail.com",
                    Balance = 0,
                    City = "Dhaka",
                    Country = "Bangladesh",
                    CreatedOnUtc = DateTime.UtcNow,
                    CurrentBalance = 0,
                    Logo = "default.png",
                    MarkupPlanId = 2,
                    Mobile = "01455654255",
                    State = "Dhaka",
                    Status = BusinessStatus.Active,
                    UpdatedOnUtc = DateTime.UtcNow,
                    Zip = "1522"
                },
                new BusinessEntities
                {
                    BusinessId = 5,
                    Name = "Undefined Travels",
                    Code = "G343422",
                    Email = "test@gmail.com",
                    Balance = 0,
                    City = "Dhaka",
                    Country = "Bangladesh",
                    CreatedOnUtc = DateTime.UtcNow,
                    CurrentBalance = 0,
                    Logo = "default.png",
                    MarkupPlanId = 1,
                    Mobile = "01455654255",
                    State = "Dhaka",
                    Status = BusinessStatus.Active,
                    UpdatedOnUtc = DateTime.UtcNow,
                    Zip = "1522"
                },
                new BusinessEntities
                {
                    BusinessId = 6,
                    Name = "No Name Travels",
                    Code = "23552",
                    Email = "test@gmail.com",
                    Balance = 0,
                    City = "Dhaka",
                    Country = "Bangladesh",
                    CreatedOnUtc = DateTime.UtcNow,
                    CurrentBalance = 0,
                    Logo = "default.png",
                    MarkupPlanId = 3,
                    Mobile = "01455654255",
                    State = "Dhaka",
                    Status = BusinessStatus.Active,
                    UpdatedOnUtc = DateTime.UtcNow,
                    Zip = "1522"
                }
            );
        }
    }
}
