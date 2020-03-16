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
using System.Security.Claims;

namespace maealim.Controllers
{
    [Route("Gifts")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class GiftsController : Controller
    {
        private readonly IMaealimRepository _repository;

        public GiftsController(IMaealimRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetGifts());
        }


        [Route("Create/{guestReservationId:int}")]
        public async Task<IActionResult> Create(int guestReservationId)
        {
            ViewData["ItemOfProducts"] = await _repository.GetItemOfProducts();
            ViewBag.guestReservationId = guestReservationId;
            var JobNotablesNormal = await _repository.GetJobNotablesNormal(guestReservationId);
            var JobNotablesNotNormal = await _repository.GetJobNotablesNotNormal(guestReservationId);
            ViewBag.notableNormal = JobNotablesNormal.Count();
            ViewBag.notableNotNormal = JobNotablesNotNormal.Count();
           var giftsall = await _repository.GetGiftsByGuestReservationId(guestReservationId);
            if (giftsall.Count()>0)
            {
                return View(giftsall.OrderBy(f=>f.ItemOfProductId).ToList());

            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create/{guestReservationId:int}")]
        public async Task<IActionResult> Create(int guestReservationId,List<Gift> gifts)
        {
            if (ModelState.IsValid)
            {
                var allGifts = await _repository.GetGiftsByGuestReservationId(guestReservationId);
                foreach (var gift in allGifts)
                {
                    _repository.Delete<Gift>(gift);
                }
                await _repository.SavaAll();
                foreach (var gift in gifts)
                {
                    _repository.Add<Gift>(gift);
                }
                await _repository.SavaAll();
                var guest = await _repository.GetGuestReservation(guestReservationId) ;
                if(!await _repository.GetCheckGuidHaveWorkInTheDay(guest.MGuideId.Value, guest.ReservationDate))
                {
                    var contract = await _repository.GetGuideContractByGuideId(guest.MGuideId.Value);
                    var attend = new Attend()
                    {
                       
                        GuideContractId = contract.Id,
                        TheWork = "وجهاء",
                        WorkingHours = contract.DailyWorkingHours,
                        GuideId = guest.MGuideId.Value,
                        AttendDate = guest.ReservationDate
                    };
                    _repository.Add<Attend>(attend);
                    await _repository.SavaAll();
                }
                return RedirectToAction("NotablesByReservation", "Notables",
                new { guestReservationId = guestReservationId }).WithSuccess("success","تم الحفظ بنجاخ");
            }
            ViewData["ItemOfProducts"] = await _repository.GetItemOfProducts();
            ViewBag.guestReservationId = guestReservationId;
            var JobNotablesNormal = await _repository.GetJobNotablesNormal(guestReservationId);
            var JobNotablesNotNormal = await _repository.GetJobNotablesNotNormal(guestReservationId); await _repository.GetJobNotablesNormal(guestReservationId);
            ViewBag.notableNormal = JobNotablesNormal.Count();
            ViewBag.notableNotNormal = JobNotablesNotNormal.Count();
            return View(gifts);
        }


     
    }
}
