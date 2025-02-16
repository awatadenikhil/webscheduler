using Microsoft.AspNetCore.Mvc;
using SchedulerEngine;
using WebAppSch.Models;

namespace WebAppSch.Controllers
{
    public class JobController : Controller
    {
        private readonly SchedulerHandler _schedulerHandler;

        public JobController(SchedulerHandler schedulerHandler)
        {
            _schedulerHandler = schedulerHandler;
        }

        // GET: Job/Dashboard
        public IActionResult Dashboard()
        {
            var jobs = _schedulerHandler.GetAll();
            return View(jobs);
        }

        // GET: Job/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Job/Create
        [HttpPost]
        public IActionResult Create(SchedulerModel model)
        {
            if (ModelState.IsValid)
            {
                _schedulerHandler.Add(model);
                return RedirectToAction("Dashboard");
            }
            return View(model);
        }

        // GET: Job/Edit/{id}
        public IActionResult Edit(int id)
        {
            var job = _schedulerHandler.Get(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Job/Edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, SchedulerModel model)
        {
            if (ModelState.IsValid)
            {
                _schedulerHandler.Edit(model);
                return RedirectToAction("Dashboard");
            }
            return View(model);
        }

        // GET: Job/Delete/{id}
        public IActionResult Delete(int id)
        {
            _schedulerHandler.Remove(id);
            return RedirectToAction("Dashboard");
        }
    }
}
