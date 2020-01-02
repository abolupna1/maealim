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
using Microsoft.AspNetCore.Http;
using maealim.Extensions.Alerts;

namespace maealim.Controllers
{
    [Route("EmployeeContracts")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class EmployeeContractsController : Controller
    {
        private readonly IMaealimRepository _repository;


        public EmployeeContractsController(IMaealimRepository repository )
        {
            _repository = repository;
        }

     

        public async Task<IActionResult> Index()
        {
           
            return View(await _repository.GetEmployeeContracts());
        }

      

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name");
            ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(await _repository.GetEmployees(), "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(EmployeeContract employeeContract)
        {
            if (ModelState.IsValid)
            {
               
                    var status = await _repository.IsEemployeeHasContractActive(employeeContract.EmployeeId);
                    if (status == true && employeeContract.Status == true)
                    {
                    ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name");
                    ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name");
                    ViewData["EmployeeId"] = new SelectList(await _repository.GetEmployees(), "Id", "Name");
                    return View(employeeContract).WithDanger("danger", "لايمكن اضافة العقد . يوجد عقد سابق  ");
                    }
                
                _repository.Add<EmployeeContract>(employeeContract);
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name");
            ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(await _repository.GetEmployees(), "Id", "Name");
            return View(employeeContract);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {


            var employeeContract = await _repository.GetEmployeeContract(id);
            if (employeeContract == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name", employeeContract.JopId);
            ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name", employeeContract.SeasonId);
            ViewData["EmployeeId"] = new SelectList(await _repository.GetEmployees(), "Id", "Name", employeeContract.EmployeeId);
            return View(employeeContract);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, EmployeeContract employeeContract)
        {
            if (id != employeeContract.Id)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
         
            if (ModelState.IsValid)
            {
                var employee = await _repository.GetEmployeeContract(id);
                if (await _repository.IsEemployeeHasContractActive(id, employeeContract.EmployeeId) && employeeContract.Status == true)
                {
                    return RedirectToAction(nameof(Index)).WithDanger("danger", "لايمكن تعديل العقد بلحالة نشط  . يوجد عقد سابق نشط    ");
                }
                try
                {
                    employee.EmployeeId = employeeContract.EmployeeId;
                    employee.FromDate = employeeContract.FromDate;
                    employee.HourlyPay = employeeContract.HourlyPay;
                    employee.JopId = employeeContract.JopId;
                    employee.SeasonId = employeeContract.SeasonId;
                    employee.ToDate = employeeContract.ToDate;
                    employee.Status = employeeContract.Status;
                    _repository.Update<EmployeeContract>(employee);
                    await _repository.SavaAll();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.GetEmployeeContract(employeeContract.Id) == null)
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

            ViewData["JopId"] = new SelectList(await _repository.GetJops(), "Id", "Name", employeeContract.JopId);
            ViewData["SeasonId"] = new SelectList(await _repository.GetSeasons(), "Id", "Name", employeeContract.SeasonId);
            ViewData["EmployeeId"] = new SelectList(await _repository.GetEmployees(), "Id", "Name", employeeContract.EmployeeId);
            return View(employeeContract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeContract = await _repository.GetEmployeeContract(id);
            if (employeeContract == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }

            try
            {
                _repository.Delete<EmployeeContract>(employeeContract);
                await _repository.SavaAll();


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لايمكن حذف   : ( {employeeContract.Employee.Name} )  اذا اردت الحذف الرجاء حذف جميع  الحقول المرتبطة   ";
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
            var employeeContract = await _repository.GetEmployeeContract(id);
            if (employeeContract == null)
            {
                ViewBag.ErrorMessage = "لايوجد   بيانات";
                return View("NotFound");
            }
         



            try
            {
                employeeContract.Status = !employeeContract.Status;
                var status = await _repository.IsEemployeeHasContractActive(employeeContract.EmployeeId);
                if (status == true && employeeContract.Status ==true)
                {

                    return RedirectToAction(nameof(Index)).WithDanger("danger", "لايمكن اعادة تنشيط العقد . يوجد عقد سابق  ");
                }
                _repository.Update<EmployeeContract>(employeeContract);
                await _repository.SavaAll();

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"لم الغاء العقد     : ( {employeeContract.Employee.Name} )   ";
                ViewBag.ex = ex.GetBaseException();
                return View("NotFound");


            }

            return RedirectToAction(nameof(Index));
        }


        

    }
}
