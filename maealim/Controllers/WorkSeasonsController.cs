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
using AutoMapper;
using maealim.ModelViews.WorkSeasons;

namespace maealim.Controllers
{
    [Route("WorkSeasons")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class WorkSeasonsController : Controller
    {
        private readonly IMaealimRepository _repository;
        private readonly IMapper _mapper;
        public WorkSeasonsController(IMaealimRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetWorkSeasons());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
           
            ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(WorkSeason workSeason)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<WorkSeason>(workSeason);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name");
            return View(workSeason);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {

            var workSeason = await _repository.GetWorkSeason(id);
            if (workSeason == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            var workSeasonEMV = _mapper.Map<WorkSeasonEMV>(workSeason);

            ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name",workSeason.SeasonId);
            return View(workSeasonEMV);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, WorkSeason workSeason)
        {
            if (id != workSeason.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<WorkSeason>(workSeason);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetWorkSeason(workSeason.Id) == null)
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

            ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name", workSeason.SeasonId);
            return View(workSeason);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var workSeason = await _repository.GetWorkSeason(id);
            if (workSeason == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<WorkSeason>(workSeason);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   :  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }
    }
}
