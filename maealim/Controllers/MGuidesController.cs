using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using maealim.Data;
using maealim.Models;
using Microsoft.AspNetCore.Authorization;
using maealim.Data.Repositories;
using maealim.Extensions.Alerts;

namespace maealim.Controllers
{
    [Route("MGuides")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class MGuidesController : Controller
    {
        private readonly IMaealimRepository _repository;

        public MGuidesController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetGuides());
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name");
            ViewData["BankId"] = new SelectList(await _repository.GetBanks(), "Id", "Name");
            ViewData["UniversityId"] = new SelectList(await _repository.GetUniversities(), "Id", "Name");
            ViewData["StageId"] = new SelectList(await _repository.GetStages(), "Id", "Name");
            ViewData["LevelId"] = new SelectList(await _repository.GetLevels(), "Id", "Name");
            ViewData["LanguageId"] = new SelectList(await _repository.GetLanguages(), "Id", "Name");
            ViewData["SpecializationId"] = new SelectList(await _repository.GetSpecializations(), "Id", "Name");
            ViewData["AppUserId"] = new SelectList(await _repository.GetUsers(), "Id", "FullName");
            ViewData["CollegeId"] = new SelectList(await _repository.GetColleges(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(MGuide mGuide)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<MGuide>(mGuide);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index)).WithSuccess("success","تم حفظ البيانات بنجاح");
            }
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name", mGuide.CountryId);
            ViewData["BankId"] = new SelectList(await _repository.GetBanks(), "Id", "Name", mGuide.BankId);
            ViewData["UniversityId"] = new SelectList(await _repository.GetUniversities(), "Id", "Name", mGuide.UniversityId);
            ViewData["StageId"] = new SelectList(await _repository.GetStages(), "Id", "Name", mGuide.StageId);
            ViewData["LevelId"] = new SelectList(await _repository.GetLevels(), "Id", "Name", mGuide.LevelId);
            ViewData["LanguageId"] = new SelectList(await _repository.GetLanguages(), "Id", "Name", mGuide.LanguageId);
            ViewData["SpecializationId"] = new SelectList(await _repository.GetSpecializations(), "Id", "Name", mGuide.SpecializationId);
            ViewData["AppUserId"] = new SelectList(await _repository.GetUsers(), "Id", "FullName", mGuide.AppUserId);
            ViewData["CollegeId"] = new SelectList(await _repository.GetColleges(), "Id", "Name", mGuide.CollegeId);
            return View(mGuide);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var mGuide = await _repository.GetGuide(id);
            if (mGuide == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name", mGuide.CountryId);
            ViewData["BankId"] = new SelectList(await _repository.GetBanks(), "Id", "Name", mGuide.BankId);
            ViewData["UniversityId"] = new SelectList(await _repository.GetUniversities(), "Id", "Name", mGuide.UniversityId);
            ViewData["StageId"] = new SelectList(await _repository.GetStages(), "Id", "Name", mGuide.StageId);
            ViewData["LevelId"] = new SelectList(await _repository.GetLevels(), "Id", "Name", mGuide.LevelId);
            ViewData["LanguageId"] = new SelectList(await _repository.GetLanguages(), "Id", "Name", mGuide.LanguageId);
            ViewData["SpecializationId"] = new SelectList(await _repository.GetSpecializations(), "Id", "Name", mGuide.SpecializationId);
            ViewData["AppUserId"] = new SelectList(await _repository.GetUsers(), "Id", "FullName", mGuide.AppUserId);
            ViewData["CollegeId"] = new SelectList(await _repository.GetColleges(), "Id", "Name", mGuide.CollegeId);
            return View(mGuide);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, MGuide mGuide)
        {
            if (id != mGuide.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<MGuide>(mGuide);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetGuide(mGuide.Id) == null)
                    {
                        ViewBag.ErrorMessage = "لايوجد   بيانات";
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)).WithSuccess("success", "تم تحديث البيانات بنجاح");
            }
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name", mGuide.CountryId);
            ViewData["BankId"] = new SelectList(await _repository.GetBanks(), "Id", "Name", mGuide.BankId);
            ViewData["UniversityId"] = new SelectList(await _repository.GetUniversities(), "Id", "Name", mGuide.UniversityId);
            ViewData["StageId"] = new SelectList(await _repository.GetStages(), "Id", "Name", mGuide.StageId);
            ViewData["LevelId"] = new SelectList(await _repository.GetLevels(), "Id", "Name", mGuide.LevelId);
            ViewData["LanguageId"] = new SelectList(await _repository.GetLanguages(), "Id", "Name", mGuide.LanguageId);
            ViewData["SpecializationId"] = new SelectList(await _repository.GetSpecializations(), "Id", "Name", mGuide.SpecializationId);
            ViewData["AppUserId"] = new SelectList(await _repository.GetUsers(), "Id", "FullName", mGuide.AppUserId);
            ViewData["CollegeId"] = new SelectList(await _repository.GetColleges(), "Id", "Name", mGuide.CollegeId);
            return View(mGuide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mGuide = await _repository.GetGuide(id);
            if (mGuide == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<MGuide>(mGuide);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {mGuide.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطين بهذا الموسم ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index)).WithSuccess("success", "تم الحذف بنجاح"); 
        }


    }
}
