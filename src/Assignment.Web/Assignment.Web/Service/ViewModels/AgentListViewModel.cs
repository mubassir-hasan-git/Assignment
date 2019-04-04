using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Web.Service.ViewModels
{
    public class AgentListViewModel
    {
        public string JoinDate { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MarkupPlanName { get; set; }
        public int MarkupPlanId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public float Balance { get; set; }
        public int BusinessEntityId { get; set; }
    
    }
}
