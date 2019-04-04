using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Web.Service.Helper;

namespace Assignment.Web.Service.ViewModels
{
    public class AgentDatatableViewModel:DatatableHelper
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int MarkUpId { get; set; }
    }
}
