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
using System.Security.Claims;
using maealim.Extensions.Alerts;
using AutoMapper;
using maealim.ModelViews.GuestReservations;

namespace maealim.Controllers
{
    [Route("GuestReservations")]
    [Authorize(Roles = "Admin,ProgramsSupervisor,RoleGuide")]
    public class GuestReservationsController : Controller
    {
        private readonly IMaealimRepository _repository;
        private readonly IMapper _mapper;

        public GuestReservationsController(IMaealimRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            
            if (User.IsInRole("RoleGuide")) { 
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var guide = await _repository.GetGuideByUserId(userId);
                return View(await _repository.GetGuestReservationsByGuideId(guide.Id)); 
            }
            return View(await _repository.GetGuestReservations());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("RoleGuide"))
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var guide = await _repository.GetGuideByUserId(userId);
                var guids = await _repository.GetGuides();
                ViewData["MGuideId"] = new SelectList(guids.Where(g=>g.Id==guide.Id), "Id", "Name", guide.Id);
            }
            else
            {
                ViewData["MGuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name");

            }

            ViewData["SheikhId"] = new SelectList(await _repository.GetSheikhs(),"Id","Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(GuestReservation guestReservation)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<GuestReservation>(guestReservation);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MGuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name", guestReservation.MGuideId);
            ViewData["SheikhId"] = new SelectList(await _repository.GetSheikhs(), "Id", "Name", guestReservation.SheikhId);
            return View(guestReservation);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var guestReservation = await _repository.GetGuestReservation(id);
            if (guestReservation == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            ViewData["MGuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name", guestReservation.MGuideId);
            ViewData["SheikhId"] = new SelectList(await _repository.GetSheikhs(), "Id", "Name", guestReservation.SheikhId);
            return View(guestReservation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, GuestReservation guestReservation)
        {
            if (id != guestReservation.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<GuestReservation>(guestReservation);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetBank(guestReservation.Id) == null)
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

            ViewData["MGuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name", guestReservation.MGuideId);
            ViewData["SheikhId"] = new SelectList(await _repository.GetSheikhs(), "Id", "Name", guestReservation.SheikhId);
            return View(guestReservation);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var guestReservation = await _repository.GetGuestReservation(id);
            if (guestReservation == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<GuestReservation>(guestReservation);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {guestReservation.SessionNo} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }


        [Route("AddEditShikh/{id:int}")]
        public async Task<IActionResult> AddEditShikh(int id)
        {


            var guestReservation = await _repository.GetGuestReservation(id);
            if (guestReservation == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            ViewData["SheikhId"] = new SelectList(await _repository.GetSheikhs(), "Id", "Name", guestReservation.SheikhId);
            var map = _mapper.Map<AddEditShikh>(guestReservation);
            return View(map);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AddEditShikh/{id:int}")]
        public async Task<IActionResult> AddEditShikh(int id, AddEditShikh guestReservation)
        {
            if (id != guestReservation.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var guest = await _repository.GetGuestReservation(guestReservation.Id);
                    guest.SessionNo = guestReservation.SessionNo;
                    guest.SheikhId = guestReservation.SheikhId;
                    guest.Status =1;
                    _repository.Update<GuestReservation>(guest);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetBank(guestReservation.Id) == null)
                    {
                        ViewBag.ErrorMessage = "لايوجد   بيانات";
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("NotablesByReservation", "Notables",new { guestReservationId=id }).WithSuccess("success","تمت الاضافة بنجاح");
            }

            ViewData["SheikhId"] = new SelectList(await _repository.GetSheikhs(), "Id", "Name", guestReservation.SheikhId);
            return View(guestReservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ConfirmReservation/{id:int}")]
        public async Task<IActionResult> ConfirmReservation(int id,int status)
        {
            var guestReservation = await _repository.GetGuestReservation(id);
            if (guestReservation == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                guestReservation.Status = 2;
                _repository.Update<GuestReservation>(guestReservation);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {guestReservation.SessionNo} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }


        [ValidateAntiForgeryToken]
        [Route("CancelReservation/{id:int}")]
        public async Task<IActionResult> CancelReservation(int id, int status)
        {
            var guestReservation = await _repository.GetGuestReservation(id);
            if (guestReservation == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                guestReservation.Status = 0;
                _repository.Update<GuestReservation>(guestReservation);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {guestReservation.SessionNo} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }



        [ValidateAntiForgeryToken]
        [Route("WasAttendance/{id:int}")]
        public async Task<IActionResult> WasAttendance(int id)
        {
            var guestReservation = await _repository.GetGuestReservation(id);
            if (guestReservation == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                guestReservation.Status = 1;
                _repository.Update<GuestReservation>(guestReservation);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {guestReservation.SessionNo} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction("NotablesByReservation", 
                "Notables",
                new { guestReservationId = id })
                .WithSuccess("success","تم الحفظ بنجاح"); 
        }

    }
}
