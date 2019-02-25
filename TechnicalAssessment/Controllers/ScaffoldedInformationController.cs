using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalAssessment.Data;
using TechnicalAssessment.Data.Models;

namespace TechnicalAssessment.Controllers
{
    public class ScaffoldedInformationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScaffoldedInformationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ScaffoldedInformation
        public async Task<IActionResult> Index()
        {
            return View(await _context.PeoplesInformation.ToListAsync());
        }

        // GET: ScaffoldedInformation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInformation = await _context.PeoplesInformation
                .SingleOrDefaultAsync(m => m.Id == id);
            if (personalInformation == null)
            {
                return NotFound();
            }

            return View(personalInformation);
        }

        // GET: ScaffoldedInformation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScaffoldedInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,EmailAddress,ContactNumber,AlternativeContactNumber,Address,MethodOfContact,ProfileImageUrl,JoiningDate")] PersonalInformation personalInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personalInformation);
        }

        // GET: ScaffoldedInformation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInformation = await _context.PeoplesInformation.SingleOrDefaultAsync(m => m.Id == id);
            if (personalInformation == null)
            {
                return NotFound();
            }
            return View(personalInformation);
        }

        // POST: ScaffoldedInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmailAddress,ContactNumber,AlternativeContactNumber,Address,MethodOfContact,ProfileImageUrl,JoiningDate")] PersonalInformation personalInformation)
        {
            if (id != personalInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalInformationExists(personalInformation.Id))
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
            return View(personalInformation);
        }

        // GET: ScaffoldedInformation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInformation = await _context.PeoplesInformation
                .SingleOrDefaultAsync(m => m.Id == id);
            if (personalInformation == null)
            {
                return NotFound();
            }

            return View(personalInformation);
        }

        // POST: ScaffoldedInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalInformation = await _context.PeoplesInformation.SingleOrDefaultAsync(m => m.Id == id);
            _context.PeoplesInformation.Remove(personalInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalInformationExists(int id)
        {
            return _context.PeoplesInformation.Any(e => e.Id == id);
        }
    }
}
