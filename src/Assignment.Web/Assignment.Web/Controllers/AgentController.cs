using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Web.Models;
using Assignment.Web.Persistence;
using Assignment.Web.Service.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Add()
        {

            ViewBag.StatusList = Enum.GetNames(typeof(BusinessStatus)).Select(name => new CheckBoxModel
            {
                IsChecked = false,
                Text = name,
                Value = name
            });

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
                    Logo =await UploadFile(model.LogoFile,model.Name),
                };
                await _context.BusinessEntities.AddAsync(businessEntity);
                await _context.SaveChangesAsync();
                return Json(new {success = true,message="Successfully Added to Agent"});
            }

            var errorMessage = string.Empty;
            foreach (var modelStateVal in ViewData.ModelState.Values)
            {
                foreach (var error in modelStateVal.Errors)
                {
                    errorMessage=errorMessage+ error.ErrorMessage+ " </br>";
                    // You may log the errors if you want
                }
            }
            return Json(new { success = false, message = errorMessage });
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