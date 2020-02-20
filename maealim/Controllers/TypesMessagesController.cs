using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using maealim.Data;
using maealim.Models;
using Microsoft.AspNetCore.Authorization;
using maealim.Data.Repositories;
using System;

namespace maealim.Controllers
{
    [Route("TypesMessages")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class TypesMessagesController : Controller
    {
        private readonly IMaealimRepository _repository;

        public TypesMessagesController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetTypesMessages());
        }


        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(TypesMessage typesMessage)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<TypesMessage>(typesMessage);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            return View(typesMessage);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var typesMessage = await _repository.GetTypesMessage(id);
            if (typesMessage == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            return View(typesMessage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, TypesMessage typesMessage)
        {
            if (id != typesMessage.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<TypesMessage>(typesMessage);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetTypesMessage(typesMessage.Id) == null)
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
            return View(typesMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var typesMessage = await _repository.GetTypesMessage(id);
            if (typesMessage == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<TypesMessage>(typesMessage);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {typesMessage.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطين   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }

    }
}
