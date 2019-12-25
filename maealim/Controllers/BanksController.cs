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
    [Route("Banks")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class BanksController : Controller
    {
        private readonly IMaealimRepository _repository;

        public BanksController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetBanks());
        }


        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Bank bank)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<Bank>(bank);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var bank = await _repository.GetBank(id);
            if (bank == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            return View(bank);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, Bank bank)
        {
            if (id != bank.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<Bank>(bank);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetBank(bank.Id) == null)
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
            return View(bank);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bank = await _repository.GetBank(id);
            if (bank == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<Bank>(bank);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {bank.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطين بهذا الموسم ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }

    }
}
