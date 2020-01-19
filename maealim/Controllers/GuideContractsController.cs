using System;
using System.Threading.Tasks;
using maealim.Data.Repositories;
using maealim.Extensions.Alerts;
using maealim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace maealim.Controllers
{
    [Route("GuideContracts")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]

    public class GuideContractsController : Controller
    {
       
            private readonly IMaealimRepository _repository;


            public GuideContractsController(IMaealimRepository repository)
            {
                _repository = repository;
            }



            public async Task<IActionResult> Index()
            {

                return View(await _repository.GetGuideContracts());
            }



            [Route("Create")]
            public async Task<IActionResult> Create()
            {
                ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name");
                ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name");
                ViewData["GuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name");

                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            [Route("Create")]
            public async Task<IActionResult> Create(GuideContract guideContract)
            {
                if (ModelState.IsValid)
                {

                    var status = await _repository.IsGuideHasContractActive(guideContract.GuideId);
                    if (status == true && guideContract.Status == true)
                    {
                        ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name");
                        ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name");
                        ViewData["GuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name");
                        return View(guideContract).WithDanger("danger", "لايمكن اضافة العقد . يوجد عقد سابق  ");
                    }

                    _repository.Add<GuideContract>(guideContract);
                    await _repository.SavaAll();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name");
                ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name");
                ViewData["EmployeeId"] = new SelectList(await _repository.GetEmployees(), "Id", "Name");
                return View(guideContract);
            }

            [Route("Edit/{id:int}")]
            public async Task<IActionResult> Edit(int id)
            {


                var guideContract = await _repository.GetGuideContract(id);
                if (guideContract == null)
                {
                    ViewBag.ErrorMessage = "لايوجد   بيانات";
                    return View("NotFound");
                }

                ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name", guideContract.JopId);
                ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name", guideContract.SeasonId);
                ViewData["GuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name", guideContract.GuideId);
                return View(guideContract);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            [Route("Edit/{id:int}")]
            public async Task<IActionResult> Edit(int id, GuideContract guideContract)
            {
                if (id != guideContract.Id)
                {
                    ViewBag.ErrorMessage = "لايوجد   بيانات";
                    return View("NotFound");
                }

                if (ModelState.IsValid)
                {
                    var guide = await _repository.GetGuideContract(id);
                    if (await _repository.IsGuideHasContractActive(id, guideContract.GuideId) && guideContract.Status == true)
                    {
                        return RedirectToAction(nameof(Index)).WithDanger("danger", "لايمكن تعديل العقد بلحالة نشط  . يوجد عقد سابق نشط    ");
                    }
                    try
                    {
                        guide.GuideId = guideContract.GuideId;
                        guide.FromDate = guideContract.FromDate;
                        guide.HourlyPay = guideContract.HourlyPay;
                        guide.JopId = guideContract.JopId;
                        guide.SeasonId = guideContract.SeasonId;
                        guide.ToDate = guideContract.ToDate;
                        guide.Status = guideContract.Status;
                        _repository.Update<GuideContract>(guide);
                        await _repository.SavaAll();

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (_repository.GetGuideContract(guideContract.Id) == null)
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

                ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name", guideContract.JopId);
                ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name", guideContract.SeasonId);
                ViewData["GudeId"] = new SelectList(await _repository.GetGuides(), "Id", "Name", guideContract.GuideId);
                return View(guideContract);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [Route("Delete/{id:int}")]
            public async Task<IActionResult> Delete(int id)
            {
                var guideContract = await _repository.GetGuideContract(id);
                if (guideContract == null)
                {
                    ViewBag.ErrorMessage = "لايوجد   بيانات";
                    return View("NotFound");
                }

                try
                {
                    _repository.Delete<GuideContract>(guideContract);
                    await _repository.SavaAll();


                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"لايمكن حذف   : ( {guideContract.Guide.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة   ";
                    ViewBag.ex = ex.GetBaseException();
                    return View("NotFound");


                }

                return RedirectToAction(nameof(Index));
            }



            [HttpPost]
            [ValidateAntiForgeryToken]
            [Route("CancelContract/{id:int}")]
            public async Task<IActionResult> CancelContract(int id)
            {
                var guideContract = await _repository.GetGuideContract(id);
                if (guideContract == null)
                {
                    ViewBag.ErrorMessage = "لايوجد   بيانات";
                    return View("NotFound");
                }




                try
                {
                    guideContract.Status = !guideContract.Status;
                    var status = await _repository.IsGuideHasContractActive(guideContract.GuideId);
                    if (status == true && guideContract.Status == true)
                    {

                        return RedirectToAction(nameof(Index)).WithDanger("danger", "لايمكن اعادة تنشيط العقد . يوجد عقد سابق  ");
                    }
                    _repository.Update<GuideContract>(guideContract);
                    await _repository.SavaAll();

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"لم يتم الغاء العقد     : ( {guideContract.Guide.Name} )   ";
                    ViewBag.ex = ex.GetBaseException();
                    return View("NotFound");


                }

                return RedirectToAction(nameof(Index));
            }




        
    }
}