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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using maealim.ModelViews.EmployeeAttends;
using maealim.Helpers;
using maealim.Extensions.Alerts;
using System.Security.Claims;

namespace maealim.Controllers
{
    [Route("EmployeeAttends")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class EmployeeAttendsController : Controller
    {
        private readonly IMaealimRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeAttendsController(IMaealimRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetEmployeeAttends());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["EmployeeId"] = new SelectList(await _repository.GetEmployeeContractActive(),"Id", "Name");
            ViewData["TheWork"] = new SelectList(TheWorName(), "ThWork", "ThWork");
            //انشاء موديل فيو 
            return View();
        }

        private List<TheWorkList> TheWorName()
        {
            var model = new List<TheWorkList>();
            model.Add(new TheWorkList { ThWork = "تنسيق" });
            model.Add(new TheWorkList { ThWork = "وجهاء" });
            model.Add(new TheWorkList { ThWork = "معالم" });
            model.Add(new TheWorkList { ThWork = "اشراف" });
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(EmployeeAttendCMV employeeAttendCMV)
        {
            if (ModelState.IsValid)
            {
                var attend = _mapper.Map<Attend>(employeeAttendCMV);
              var contract = await _repository.GetEmployeeContractByEmpId(employeeAttendCMV.EmployeeId);
                attend.EmployeeContractId = contract.Id;
                attend.WorkingHours = contract.DailyWorkingHours;
                attend.AppUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                _repository.Add<Attend>(attend);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index)).WithSuccess("success","تم الحفظ بنجاح");
            }
            ViewData["TheWork"] = new SelectList(TheWorName(), "ThWork", "ThWork", employeeAttendCMV.TheWork);
            ViewData["EmployeeId"] = new SelectList(await _repository.GetEmployeeContractActive(), "Id", "Name",employeeAttendCMV.EmployeeId);
            return View();
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

    }
}
