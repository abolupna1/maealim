using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using maealim.Models;
using Microsoft.AspNetCore.Authorization;
using maealim.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace maealim.Controllers
{
    [Route("WjhaaMessages")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class WjhaaMessagesController : Controller
    {
        private readonly IMaealimRepository _repository;

        public WjhaaMessagesController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetWjhaaMessages());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name");
            ViewData["MGuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name");
            ViewData["TypesMessageId"] = new SelectList(await _repository.GetTypesMessages(), "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(WjhaaMessage wjhaaMessage)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<WjhaaMessage>(wjhaaMessage);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name", wjhaaMessage.CountryId);
            ViewData["MGuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name", wjhaaMessage.MGuideId);
            ViewData["TypesMessageId"] = new SelectList(await _repository.GetTypesMessages(), "Id", "Name", wjhaaMessage.TypesMessageId);

            return View(wjhaaMessage);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var wjhaaMessage = await _repository.GetWjhaaMessage(id);
            if (wjhaaMessage == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name", wjhaaMessage.CountryId);
            ViewData["MGuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name", wjhaaMessage.MGuideId);
            ViewData["TypesMessageId"] = new SelectList(await _repository.GetTypesMessages(), "Id", "Name", wjhaaMessage.TypesMessageId);

            return View(wjhaaMessage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, WjhaaMessage wjhaaMessage)
        {
            if (id != wjhaaMessage.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<WjhaaMessage>(wjhaaMessage);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetCollege(wjhaaMessage.Id) == null)
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

            ViewData["CountryId"] = new SelectList(await _repository.GetCountries(), "Id", "Name", wjhaaMessage.CountryId);
            ViewData["MGuideId"] = new SelectList(await _repository.GetGuides(), "Id", "Name", wjhaaMessage.MGuideId);
            ViewData["TypesMessageId"] = new SelectList(await _repository.GetTypesMessages(), "Id", "Name", wjhaaMessage.TypesMessageId);

            return View(wjhaaMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var wjhaaMessage = await _repository.GetWjhaaMessage(id);
            if (wjhaaMessage == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<WjhaaMessage>(wjhaaMessage);
                await _repository.SavaAll();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {wjhaaMessage.Message} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطين   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }

    }
}
