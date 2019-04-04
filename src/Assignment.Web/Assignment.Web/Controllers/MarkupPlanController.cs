using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Web.Models;
using Assignment.Web.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.Controllers
{
    public class MarkupPlanController : Controller
    {

        private readonly AssignmentDbContext _context;

        public MarkupPlanController(AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: MarkupPlan
        public ActionResult Index()
        {
            return View(_context.MarkupPlan.ToList());
        }

        // GET: MarkupPlan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MarkupPlan/Create
        public ActionResult Create()
        {

            return View(new MarkupPlan());
        }

        // POST: MarkupPlan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MarkupPlan  model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("","Name Required");
                return View(model);
            }
            try
            {
                _context.MarkupPlan.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        // GET: MarkupPlan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MarkupPlan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarkupPlan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MarkupPlan/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}