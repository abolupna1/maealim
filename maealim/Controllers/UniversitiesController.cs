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
    [Route("Universities")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class UniversitiesController : Controller
    {
        private readonly IMaealimRepository _repository;

        public UniversitiesController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetUniversities());
        }


        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(University university)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<University>(university);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            return View(university);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var university = await _repository.GetUniversity(id);
            if (university == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            return View(university);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, University university)
        {
            if (id != university.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<University>(university);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetUniversity(university.Id) == null)
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
            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var university = await _repository.GetUniversity(id);
            if (university == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<University>(university);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {university.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطين بهذا الموسم ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }

    }
}
