using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using maealim.Data;
using maealim.Models;

namespace maealim.Controllers
{
    public class AttendsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attends
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attends.Include(a => a.AppUser).Include(a => a.Employee).Include(a => a.EmployeeContract).Include(a => a.Guide).Include(a => a.GuideContract);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attend = await _context.Attends
                .Include(a => a.AppUser)
                .Include(a => a.Employee)
                .Include(a => a.EmployeeContract)
                .Include(a => a.Guide)
                .Include(a => a.GuideContract)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attend == null)
            {
                return NotFound();
            }

            return View(attend);
        }

        // GET: Attends/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email");
            ViewData["EmployeeContractId"] = new SelectList(_context.EmployeeContracts, "Id", "Id");
            ViewData["GuideId"] = new SelectList(_context.MGuides, "Id", "Name");
            ViewData["GuideContractId"] = new SelectList(_context.GuideContracts, "Id", "Id");
            return View();
        }

        // POST: Attends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,EmployeeContractId,GuideId,GuideContractId,AttendDate,TheWork,AppUserId,WorkingHours")] Attend attend)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", attend.AppUserId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", attend.EmployeeId);
            ViewData["EmployeeContractId"] = new SelectList(_context.EmployeeContracts, "Id", "Id", attend.EmployeeContractId);
            ViewData["GuideId"] = new SelectList(_context.MGuides, "Id", "Name", attend.GuideId);
            ViewData["GuideContractId"] = new SelectList(_context.GuideContracts, "Id", "Id", attend.GuideContractId);
            return View(attend);
        }

        // GET: Attends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attend = await _context.Attends.FindAsync(id);
            if (attend == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", attend.AppUserId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", attend.EmployeeId);
            ViewData["EmployeeContractId"] = new SelectList(_context.EmployeeContracts, "Id", "Id", attend.EmployeeContractId);
            ViewData["GuideId"] = new SelectList(_context.MGuides, "Id", "Name", attend.GuideId);
            ViewData["GuideContractId"] = new SelectList(_context.GuideContracts, "Id", "Id", attend.GuideContractId);
            return View(attend);
        }

        // POST: Attends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,EmployeeContractId,GuideId,GuideContractId,AttendDate,TheWork,AppUserId,WorkingHours")] Attend attend)
        {
            if (id != attend.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendExists(attend.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", attend.AppUserId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", attend.EmployeeId);
            ViewData["EmployeeContractId"] = new SelectList(_context.EmployeeContracts, "Id", "Id", attend.EmployeeContractId);
            ViewData["GuideId"] = new SelectList(_context.MGuides, "Id", "Name", attend.GuideId);
            ViewData["GuideContractId"] = new SelectList(_context.GuideContracts, "Id", "Id", attend.GuideContractId);
            return View(attend);
        }

        // GET: Attends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attend = await _context.Attends
                .Include(a => a.AppUser)
                .Include(a => a.Employee)
                .Include(a => a.EmployeeContract)
                .Include(a => a.Guide)
                .Include(a => a.GuideContract)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attend == null)
            {
                return NotFound();
            }

            return View(attend);
        }

        // POST: Attends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attend = await _context.Attends.FindAsync(id);
            _context.Attends.Remove(attend);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendExists(int id)
        {
            return _context.Attends.Any(e => e.Id == id);
        }
    }
}
