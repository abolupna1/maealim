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

namespace maealim.Controllers
{
    [Route("Employees")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class EmployeesController : Controller
    {
        private readonly IMaealimRepository _repository;

        public EmployeesController(IMaealimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetEmployees());
        }


        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name");
            ViewData["UserId"] = new SelectList(await _repository.GetUsers(), "Id", "FullName");
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repository.Add<Employee>(employee);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name");
            ViewData["UserId"] = new SelectList(await _repository.GetUsers(), "Id", "FullName");
           
            return View(employee);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var employee = await _repository.GetEmployee(id);
            if (employee == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name",employee.JopId);
            ViewData["UserId"] = new SelectList(await _repository.GetUsers(), "Id", "FullName",employee.UserId);

            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update<Employee>(employee);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetSeason(employee.Id) == null)
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

            ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name", employee.JopId);
            ViewData["UserId"] = new SelectList(await _repository.GetUsers(), "Id", "FullName", employee.UserId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _repository.GetEmployee(id);
            if (employee == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<Employee>(employee);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {employee.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }

    }
}
