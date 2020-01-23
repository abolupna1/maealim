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
    [Route("ItemImports")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class ItemImportsController : Controller
    {
        private readonly IMaealimRepository _repository;

        public ItemImportsController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetItemImports());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["ItemOfProductId"] = new SelectList(await _repository.GetItemOfProducts(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(ItemImport itemImport)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<ItemImport>(itemImport);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemOfProductId"] = new SelectList(await _repository.GetItemOfProducts(), "Id", "Name", itemImport.Id);

            return View(itemImport);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var itemImport = await _repository.GetItemImport(id);
            if (itemImport == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            ViewData["ItemOfProductId"] = new SelectList(await _repository.GetItemOfProducts(), "Id", "Name", itemImport.Id);

            return View(itemImport);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, ItemImport itemImport)
        {
            if (id != itemImport.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<ItemImport>(itemImport);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetItemImport(itemImport.Id) == null)
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

            ViewData["ItemOfProductId"] = new SelectList(await _repository.GetItemOfProducts(), "Id", "Name", itemImport.Id);

            return View(itemImport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var itemImport = await _repository.GetItemImport(id);
            if (itemImport == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<ItemImport>(itemImport);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {itemImport.ItemOfProduct.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }
    }


}
