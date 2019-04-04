using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Web.Models
{
    public class MarkupPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BusinessEntities> BusinessEntities { get; set; }
    }
}
