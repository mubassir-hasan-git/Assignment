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
    }
}
