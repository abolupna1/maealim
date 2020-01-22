using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maealim.Data.Repositories;
using maealim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maealim.Controllers
{
    [Route("TypeOfProducts")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class TypeOfProductsController : Controller
    {
        private readonly IMaealimRepository _repository;

        public TypeOfProductsController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetTypeOfProducts());
        }


        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(TypeOfProduct typeOfProduct)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<TypeOfProduct>(typeOfProduct);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfProduct);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var typeOfProduct = await _repository.GetTypeOfProduct(id);
            if (typeOfProduct == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            return View(typeOfProduct);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, TypeOfProduct typeOfProduct)
        {
            if (id != typeOfProduct.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<TypeOfProduct>(typeOfProduct);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetLevel(typeOfProduct.Id) == null)
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
            return View(typeOfProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var typeOfProduct = await _repository.GetTypeOfProduct(id);
            if (typeOfProduct == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<TypeOfProduct>(typeOfProduct);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {typeOfProduct.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطين بهذا الموسم ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }

    }
}