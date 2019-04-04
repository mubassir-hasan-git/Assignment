using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Web.Models;
using Assignment.Web.Persistence;
using Assignment.Web.Service.Helper;
using Assignment.Web.Service.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Web.Controllers
{
    public class AgentController : Controller
    {
        private readonly AssignmentDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AgentController(AssignmentDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetAgentsDatatable(AgentDatatableViewModel model)
        {
            var queryAgent = _context.BusinessEntities
                .Include(m=>m.MarkupPlan)
                .AsQueryable();

            //searching
            //search by code
            if (!string.IsNullOrWhiteSpace(model.Code))
                queryAgent = queryAgent.Where(a =>
                    a.Code.StartsWith(model.Code, StringComparison.CurrentCultureIgnoreCase));
            //search by name
            if(!string.IsNullOrWhiteSpace(model.Name))
                queryAgent = queryAgent.Where(a =>
                    a.Code.Contains(model.Name, StringComparison.CurrentCultureIgnoreCase));
            //search by markup plan
            if (model.MarkUpId>0)
                queryAgent = queryAgent.Where(a =>
                    a.MarkupPlanId.Equals(model.MarkUpId));

            //todo ordering table

            //total agents
            var recordsTotal = await queryAgent.CountAsync();

            //format as Viewmodel
            var pagedAgents =await queryAgent
                .OrderByDescending(o => o.CreatedOnUtc)
                .Skip(model.start)
                .Take(model.length)
                .ToListAsync();

            //number of agents now showing
            var recordsFiltered = await queryAgent.CountAsync();

            var data=pagedAgents.Select(agent => new AgentListViewModel
            {
                Name = agent.Name,
                Code = agent.Code,
                MarkupPlanId = agent.MarkupPlanId,
                Balance = agent.Balance,
                BusinessEntityId = agent.BusinessId,
                Email = agent.Email,
                JoinDate = agent.CreatedOnUtc.ToShortDateString(),
                MarkupPlanName = agent.MarkupPlan.Name,
                Mobile = agent.Mobile
            }).ToList();

            return Json(new
            {
                draw = model.draw,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal,
                data = data
            });
        }

        public IActionResult Add()
        {

            ViewBag.StatusList = Enum.GetNames(typeof(BusinessStatus)).Select(name => new CheckBoxModel
            {
                IsChecked = false,
                Text = name,
                Value = name
            });

            var markupPlans = _context.MarkupPlan.ToList();

            ViewBag.MarkupPlans = markupPlans.Select(m => new SelectListItem(m.Name, m.Id.ToString()));
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Add(BusinessEntityAddViewModel model,IFormFile logoFile)
        {
            if (ModelState.IsValid)
            {
                /*
                 *Flight APIs, Agent Type, Markup Plan is skipped
                 *Because of not matching with Business Entity Data
                 */

                var businessEntity =new BusinessEntities
                {
                    State = model.State,
                    Balance = model.Balance,
                    City = model.City,
                    Code = model.Code,
                    ContactPerson = model.ContactPerson,
                    Country = model.Country,
                    CreatedOnUtc = DateTime.UtcNow,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    Name = model.Name,
                    Phone = model.Phone,
                    ReferredBy = model.ReferredBy,
                    SMTPPassword = model.SMTPPassword,
                    SMTPPort = model.SMTPPort,
                    SMTPServer = model.SMTPServer,
                    SMTPUsername = model.SMTPUsername,
                    Status = model.Status,
                    Street = model.Street,
                    UpdatedOnUtc = DateTime.UtcNow,
                    SecurityCode = model.SecurityCode,
                    Zip = model.Zip,
                    MarkupPlanId = model.MarkupPlanId,
                    Logo =await UploadFile(logoFile,model.Name),
                };
                await _context.BusinessEntities.AddAsync(businessEntity);
                await _context.SaveChangesAsync();
                return Json(new {success = true,message="Successfully Added to Agent"});
            }

            
            
            return Json(new { success = false, message = "Something Wrong please check all Inputs" });
        }

       

        private async Task<string> UploadFile(IFormFile image, string agentName)
        {
            if (image == null || image.Length <= 0) return "defaultAgent.png";


            var file = image;
            //There is an error here
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads\\agents");

            bool exists = Directory.Exists(uploads);

            if (!exists)
                Directory.CreateDirectory(uploads);
                
                
            var fileName = agentName+"_"+Guid.NewGuid();
            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}