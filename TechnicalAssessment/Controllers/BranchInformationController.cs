using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalAssessment.Data;
using TechnicalAssessment.Data.Models;
using TechnicalAssessment.Models.BranchInformationViewModels;

namespace TechnicalAssessment.Controllers
{
    public class BranchInformationController : Controller
    {
        private readonly IBranchInformation _branchInformationService;
        private static UserManager<ApplicationUser> _userManager;
        public BranchInformationController(IBranchInformation branchInformationService, UserManager<ApplicationUser> userManager)
        {
            _branchInformationService = branchInformationService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var branchesInformation = _branchInformationService.GetAll()
                .Select(branchInformation => new BranchInformationViewModel
                {
                    Id = branchInformation.Id,
                    BranchName = branchInformation.BranchName,
                    BranchCode = branchInformation.BranchCode,
                    Province = branchInformation.Province,
                    City = branchInformation.City
                });

            var model = new BranchInformationIndexModel
            {
                BranchInformationList = branchesInformation
            };

            return View(model);
        }


        //TESTING the APIs below in POSTMAN 

        // GET: /BranchInformation/Get
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<BranchInformation> branchesInformation = _branchInformationService.GetAll();
            return Ok(branchesInformation);
        }

        // GET: /BranchInformation/Get/1
        [HttpGet("/BranchInformation/Get/{id}", Name = "Tora")]
        public IActionResult Get(int id)
        {
            var branchInformation = _branchInformationService.GetById(id);

            if (branchInformation == null)
            {
                return NotFound("The Personal Information record could not be found");
            }

            return Ok(branchInformation);
        }

        // GET: ScaffoldedInformation/Details/5
        public IActionResult Details(int id)
        {

            var branchInformation = _branchInformationService.GetById(id);
            if (branchInformation == null)
            {
                return NotFound();
            }

            return View(branchInformation);
        }

        // GET: PersonalInformation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        public IActionResult Create([Bind("Id,BranchName,BranchCode,City,Province")] BranchInformation branchInformation)
        {
            if (branchInformation == null)
            {
                return BadRequest("Branch Information is null");
            }

            _branchInformationService.Create(branchInformation);
            return RedirectToAction(nameof(Index));
        }


        // GET: BranchInformation/Delete/5
        public IActionResult Delete(int id)
        {

            var branchInformation = _branchInformationService.GetById(id);
            if (branchInformation == null)
            {
                return NotFound();
            }

            return View(branchInformation);
        }

        // POST: BranchInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _branchInformationService.Delete(id);

            return RedirectToAction(nameof(Index));
        }


        // GET: BranchInformation/Edit/5
        public IActionResult Edit(int id)
        {

            var branchInformation = _branchInformationService.GetById(id);
            if (branchInformation == null)
            {
                return NotFound();
            }
            return View(branchInformation);
        }

        // POST: BranchInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchName,BranchCode,City,Province")] BranchInformation branchInformation)
        {
            if (id != branchInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _branchInformationService.UpdateBranchInformation(branchInformation.Id, branchInformation.BranchName, branchInformation.BranchCode, branchInformation.City, branchInformation.Province);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchInformationExists(branchInformation.Id))
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
            return View(branchInformation);
        }


        private bool BranchInformationExists(int id)
        {
            return _branchInformationService.GetAll().Any(e => e.Id == id);
        }


    }
}