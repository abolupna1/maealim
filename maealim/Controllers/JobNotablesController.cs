using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using maealim.Data;
using maealim.Models;
using maealim.Data.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace maealim.Controllers
{
    [Route("JobNotables")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class JobNotablesController : Controller
    {
        private readonly IMaealimRepository _repository;

        public JobNotablesController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetJobNotables());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["TypeNotableId"] = new SelectList(await _repository.GetTypeNotables(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(JobNotable jobNotable)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<JobNotable>(jobNotable);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeNotableId"] = new SelectList(await _repository.GetTypeNotables(), "Id", "Name", jobNotable.TypeNotableId);
            return View(jobNotable);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var jobNotable = await _repository.GetJobNotable(id);
            if (jobNotable == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            ViewData["TypeNotableId"] = new SelectList(await _repository.GetTypeNotables(), "Id", "Name", jobNotable.TypeNotableId);


            return View(jobNotable);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, JobNotable jobNotable)
        {
            if (id != jobNotable.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<JobNotable>(jobNotable);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetJobNotable(jobNotable.Id) == null)
                    {
                        ViewBag.ErrorMessage = "لايوجد   بيانات";
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["TypeNotableId"] = new SelectList(await _repository.GetTypeNotables(), "Id", "Name", jobNotable.TypeNotableId);

            return View(jobNotable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobNotable = await _repository.GetJobNotable(id);
            if (jobNotable == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<JobNotable>(jobNotable);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {jobNotable.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }
    }
}
