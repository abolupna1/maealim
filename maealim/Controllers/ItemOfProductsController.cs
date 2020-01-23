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
    [Route("ItemOfProducts")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class ItemOfProductsController : Controller
    {
        private readonly IMaealimRepository _repository;

        public ItemOfProductsController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetItemOfProducts());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["TypeOfProductId"] = new SelectList(await _repository.GetTypeOfProducts(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(ItemOfProduct itemOfProduct)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<ItemOfProduct>(itemOfProduct);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeOfProductId"] = new SelectList(await _repository.GetTypeOfProducts(), "Id", "Name", itemOfProduct.Id);

            return View(itemOfProduct);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var itemOfProduct = await _repository.GetItemOfProduct(id);
            if (itemOfProduct == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            ViewData["TypeOfProductId"] = new SelectList(await _repository.GetTypeOfProducts(), "Id", "Name", itemOfProduct.Id);

            return View(itemOfProduct);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, ItemOfProduct itemOfProduct)
        {
            if (id != itemOfProduct.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<ItemOfProduct>(itemOfProduct);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetItemOfProduct(itemOfProduct.Id) == null)
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

            ViewData["TypeOfProductId"] = new SelectList(await _repository.GetTypeOfProducts(), "Id", "Name", itemOfProduct.Id);

            return View(itemOfProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var itemOfProduct = await _repository.GetItemOfProduct(id);
            if (itemOfProduct == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<ItemOfProduct>(itemOfProduct);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {itemOfProduct.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }
    }
}
