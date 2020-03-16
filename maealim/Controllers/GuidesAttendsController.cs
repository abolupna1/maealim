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
using AutoMapper;
using maealim.Helpers;
using maealim.ModelViews.GuidesAttends;
using System.Security.Claims;
using maealim.Extensions.Alerts;

namespace maealim.Controllers
{
    [Route("GuidesAttends")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class GuidesAttendsController : Controller
    {
        private readonly IMaealimRepository _repository;
        private readonly IMapper _mapper;

        public GuidesAttendsController(IMaealimRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetGuideAttends());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["GuideId"] = new SelectList(await _repository.GetGuideContractActive(), "Id", "Name");
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
        public async Task<IActionResult> Create(GuidesAttendsCMV guidesAttendsCMV)
        {
            if (ModelState.IsValid)
            {
                var contract = await _repository.GetGuideContractByGuideId(guidesAttendsCMV.GuideId);
                var attend = new Attend()
                {
                    AppUserId= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    GuideContractId= contract.Id,
                    TheWork= guidesAttendsCMV.TheWork,
                    WorkingHours= contract.DailyWorkingHours,
                    GuideId=guidesAttendsCMV.GuideId,
                    AttendDate=guidesAttendsCMV.AttendDate
                };
                _repository.Add<Attend>(attend);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index)).WithSuccess("success", "تم الحفظ بنجاح");
            }
            ViewData["TheWork"] = new SelectList(TheWorName(), "ThWork", "ThWork", guidesAttendsCMV.TheWork);
            ViewData["EmployeeId"] = new SelectList(await _repository.GetGuideContractActive(), "Id", "Name", guidesAttendsCMV.GuideId);
            return View();
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var attend = await _repository.GetAttend(id);
            if (attend == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            var guideAttendCMV = _mapper.Map<GuidesAttendsCMV>(attend);
            ViewData["TheWork"] = new SelectList(TheWorName(), "ThWork", "ThWork", attend.TheWork);
            ViewData["GuideId"] = new SelectList(await _repository.GetGuideContractActive(), "Id", "Name", attend.GuideId);
            return View(guideAttendCMV);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, GuidesAttendsCMV guidesAttendsCMV)
        {
            if (id != guidesAttendsCMV.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var attend = await _repository.GetAttend(id);
                    var attendUpdate = _mapper.Map<GuidesAttendsCMV, Attend>(guidesAttendsCMV, attend);
                    _repository.Update<Attend>(attendUpdate);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetAttend(guidesAttendsCMV.Id) == null)
                    {
                        ViewBag.ErrorMessage = "لايوجد   بيانات";
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)).WithSuccess("success", "تم الحفظ بنجاح");

            }

            ViewData["TheWork"] = new SelectList(TheWorName(), "ThWork", "ThWork", guidesAttendsCMV.TheWork);
            ViewData["EmployeeId"] = new SelectList(await _repository.GetGuideContractActive(), "Id", "Name", guidesAttendsCMV.GuideId);
            return View(guidesAttendsCMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var attend = await _repository.GetAttend(id);
            if (attend == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<Attend>(attend);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {attend.Id} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index)).WithSuccess("success", "تم الحذف بنجاح");

        }

    }
}
