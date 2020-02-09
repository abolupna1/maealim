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
using maealim.Extensions.Alerts;

namespace maealim.Controllers
{
    [Route("Notables")]
    [Authorize(Roles = "Admin,ProgramsSupervisor,RoleGuide")]
    public class NotablesController : Controller
    {
        private readonly IMaealimRepository _repository;

        public NotablesController(IMaealimRepository repository)
        {
            _repository = repository;
        }
        // GET: Notables
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetNotables());
        }
        [HttpGet]
        [Route("NotablesByReservation/{guestReservationId:int}")]
        public async Task<IActionResult> NotablesByReservation(int guestReservationId)
        {
            var guestReservation= await _repository.GetGuestReservation(guestReservationId);
            if (guestReservation==null)
            {
                string message = $" لايوجد حجز بهذا الرقم : {guestReservationId}";
               return RedirectToAction("Index", "GuestReservations").WithDanger("danger", message);
            }
            ViewBag.guestReservationId = guestReservationId;
            return View(guestReservation);
        }



        [Route("Create/{guestReservationId:int}")]
        public async Task<IActionResult> Create(int guestReservationId)
        {

            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name");
            ViewData["JobNotableId"] = new SelectList(await _repository.GetJobNotables(), "Id", "Name");
            ViewBag.guestReservationId = guestReservationId;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create/{guestReservationId:int}")]
        public async Task<IActionResult> Create(int guestReservationId,Notable notable)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<Notable>(notable);
                await _repository.SavaAll();
                return RedirectToAction(nameof(NotablesByReservation),
                    new { guestReservationId = guestReservationId });
            }
            ViewBag.guestReservationId = guestReservationId;
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name", notable.CountryId);
            ViewData["JobNotableId"] = new SelectList(await _repository.GetJobNotables(), "Id", "Name", notable.JobNotableId);
            return View(notable);
        }



        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var notable = await _repository.GetNotable(id);
            if (notable == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name", notable.CountryId);
            ViewData["JobNotableId"] = new SelectList(await _repository.GetJobNotables(), "Id", "Name", notable.JobNotableId);
            ViewBag.guestReservationId = notable.GuestReservationId;
            return View(notable);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, Notable notable)
        {
            if (id != notable.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<Notable>(notable);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetNotable(notable.Id) == null)
                    {
                        ViewBag.ErrorMessage = "لايوجد   بيانات";
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(NotablesByReservation),
              new { guestReservationId = notable.GuestReservationId });
            }
            ViewBag.guestReservationId = notable.GuestReservationId;
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name", notable.CountryId);
            ViewData["JobNotableId"] = new SelectList(await _repository.GetJobNotables(), "Id", "Name", notable.JobNotableId);
            return View(notable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notable = await _repository.GetNotable(id);
            if (notable == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<Notable>(notable);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {notable.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(NotablesByReservation),
              new { guestReservationId = notable.GuestReservationId });
        }


    }
}
