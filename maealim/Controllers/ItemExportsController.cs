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
    [Route("ItemExports")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class ItemExportsController : Controller
    {
        private readonly IMaealimRepository _repository;

        public ItemExportsController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetItemExports());
        }

        [Route("Items")]
        public async Task<IActionResult> Items()
        {
            return View(await _repository.GetItemOfProducts());
        }



        [Route("Create/{itemOfProductId:int}")]
        public async Task<IActionResult> Create(int itemOfProductId)
        {
            var itemOfProduct = await _repository.GetItemOfProduct(itemOfProductId);
            if (itemOfProduct == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            ViewBag.ItemOfProductId = itemOfProduct.Id;
            ViewBag.ItemOfProductName = itemOfProduct.Name;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create/{itemOfProductId:int}")]
        public async Task<IActionResult> Create(int itemOfProductId, ItemExport itemExport)
        {
            var itemOfProduct = await _repository.GetItemOfProduct(itemOfProductId);
            if (itemOfProduct == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
            if (await _repository.CheckIfImportsEqualExport(itemOfProductId, itemExport.Qty))
            {
                ViewBag.ItemOfProductId = itemOfProduct.Id;
                ViewBag.ItemOfProductName = itemOfProduct.Name;
                return View(itemExport).WithDanger("danger","الكمية غير كافية");
            }

            if (ModelState.IsValid)
            {
                  
                _repository.Add<ItemExport>(itemExport);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ItemOfProductId = itemOfProduct.Id;
            ViewBag.ItemOfProductName = itemOfProduct.Name;
            return View(itemExport);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var itemExport = await _repository.GetItemExport(id);
            if (itemExport == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            ViewData["ItemOfProductId"] = new SelectList(await _repository.GetItemOfProducts(), "Id", "Name", itemExport.Id);

            return View(itemExport);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, ItemExport itemExport)
        {
            if (id != itemExport.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<ItemExport>(itemExport);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetItemExport(itemExport.Id) == null)
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

            ViewData["ItemOfProductId"] = new SelectList(await _repository.GetItemOfProducts(), "Id", "Name", itemExport.Id);

            return View(itemExport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var itemExport = await _repository.GetItemExport(id);
            if (itemExport == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<ItemExport>(itemExport);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {itemExport.ItemOfProduct.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }
    }
}
