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
    [Route("Countries")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class CountriesController : Controller
    {
        private readonly IMaealimRepository _repository;

        public CountriesController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetCountries());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["ContinentId"] = new SelectList(await _repository.GetContinents(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<Country>(country);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContinentId"] = new SelectList(await _repository.GetContinents(), "Id", "Name",country.ContinentId);
            return View(country);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var country = await _repository.GetCountry(id);
            if (country == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            ViewData["ContinentId"] = new SelectList(await _repository.GetContinents(), "Id", "Name",country.ContinentId);

            return View(country);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<Country>(country);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetCountry (country.Id) == null)
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

            ViewData["ContinentId"] = new SelectList(await _repository.GetContinents(), "Id", "Name", country.ContinentId);

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var country = await _repository.GetCountry(id);
            if (country == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<Country>(country);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {country.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }
    }
}
