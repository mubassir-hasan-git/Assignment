using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Web.Models;
using Assignment.Web.Persistence;
using Assignment.Web.Service.Helper;
using Assignment.Web.Service.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Web.Controllers
{
    /// <summary>
    /// GetAgentsDatatable used a custom class for passing jQuery Datatable parameters
    ///
    /// Edit and Add operation handled by Add Method
    /// 
    /// </summary>
    public class AgentController : Controller
    {
        private readonly AssignmentDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public AgentController(AssignmentDbContext context, IHostingEnvironment hostingEnvironment, IMapper mapper)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            var markupPlans = _context.MarkupPlan.ToList();

            ViewBag.MarkupPlans = markupPlans.Select(m => new SelectListItem(m.Name, m.Id.ToString()));
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
            var recordsTotal =  queryAgent.Count();

            //format as Viewmodel
            var pagedAgents =await queryAgent
                .OrderByDescending(o => o.CreatedOnUtc)
                .Skip(model.start)
                .Take(model.length)
                .ToListAsync();

            //number of agents now showing
            var recordsFiltered =  queryAgent.Count();

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


        /// <summary>
        /// Adding or Editing Agent
        /// </summary>
        /// <param name="agentId">For editing agent must be provide an agent Id</param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int? agentId)
        {
            //model dropdowns
            PopulateDropdown();

            var model=new BusinessEntityAddViewModel();
            if (agentId == null) return View(model);

            var agent = await _context.BusinessEntities.FirstOrDefaultAsync(f => f.BusinessId == agentId);
            if (agent == null)
                return NotFound();
            PopulateDropdown(agent.Status);
            model = _mapper.Map<BusinessEntityAddViewModel>(agent);
            ViewBag.AgentId = agent.BusinessId;
            
            return View(model);

        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agentId">Agent id set in hidden field </param>
        /// <returns></returns>

        [HttpPost]
        public async Task<JsonResult> Add(BusinessEntityAddViewModel model,int? agentId)
        {
            if (ModelState.IsValid)
            {
                /*
                 *Flight APIs, Agent Type, Markup Plan is skipped
                 *Because of not matching with Business Entity Data
                 */
                //Edit
                if (agentId!=null)
                {
                    var dbAgent = await _context.BusinessEntities.FirstOrDefaultAsync(f => f.BusinessId == agentId);
                    if (dbAgent == null)
                        return Json(new { success = false, message = "Can't find this agent" });
                    dbAgent = _mapper.Map(model,dbAgent);
                    dbAgent.UpdatedOnUtc = DateTime.UtcNow;

                    try
                    {
                        _context.Update(dbAgent);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        //log
                        Console.WriteLine(e.Message);
                        return Json(new { success = false, message = "Can't update the agent" });
                    }
                     
                    return Json(new { success = true, message = "Successfully Updated Agent" });
                }


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
                    Logo =await UploadFile(model.LogoFile,model.Name),
                };
                await _context.BusinessEntities.AddAsync(businessEntity);
                await _context.SaveChangesAsync();
                return Json(new {success = true,message="Successfully Added Agent"});
            }

            
            
            return Json(new { success = false, message = "Something Wrong please check all Inputs" });
        }

        /// <summary>
        /// Populate dropdown for add and edit model
        /// </summary>
        /// <param name="status"></param>
        private void PopulateDropdown(BusinessStatus status=0)
        {
            ViewBag.StatusList = Enum.GetNames(typeof(BusinessStatus)).Select(name => new CheckBoxModel
            {
                //get enum from string using
                IsChecked = status.Equals(EnumHelper.ParseEnum<BusinessStatus>(name)),
                Text = name,
                Value = name
            });

            var markupPlans = _context.MarkupPlan.ToList();

            ViewBag.MarkupPlans = markupPlans.Select(m => new SelectListItem(m.Name, m.Id.ToString()));
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

        


        #region Delete

        public async Task<IActionResult> Delete(int agentId)
        {
            if (agentId<=0)
            {
                return BadRequest();
            }

            var agent = await _context.BusinessEntities.FirstOrDefaultAsync(f => f.BusinessId.Equals(agentId));
            if (agent == null) return NotFound();

            ViewBag.AgentId = agent.BusinessId;
            ViewBag.AgentName = agent.Name;
            return PartialView("_Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int agentId,IFormCollection form)
        {
            if (agentId <= 0)
            {
                return BadRequest();
            }
            var agent = await _context.BusinessEntities.FirstOrDefaultAsync(f => f.BusinessId.Equals(agentId));
            if (agent == null) return NotFound();

            _context.BusinessEntities.Remove(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Agent");

        }

        #endregion
    }
}