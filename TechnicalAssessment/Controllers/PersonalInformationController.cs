﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalAssessment.Data;
using TechnicalAssessment.Data.Models;
using TechnicalAssessment.Models.BranchInformationViewModels;
using TechnicalAssessment.Models.PersonalInformationViewModels;

namespace TechnicalAssessment.Controllers
{
    public class PersonalInformationController : Controller
    {
        private readonly IPersonalInformation _personalInformationService;
        private readonly IBranchInformation _branchInformationService;

        private static UserManager<ApplicationUser> _userManager;
        public PersonalInformationController(IPersonalInformation personalInformationService, IBranchInformation branchInformationService, UserManager<ApplicationUser> userManager)
        {
            _personalInformationService = personalInformationService;
            _branchInformationService = branchInformationService;
            _userManager = userManager;
        }

        public IActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if(searchString == null)
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //IEnumerable<PersonalInformationViewModel> peoplesInformation = _personalInformationService.GetAll() Further simplified using var
            var peoplesInformation = _personalInformationService.GetAll()
                .Select(personalInformation => new PersonalInformationViewModel {
                    Id = personalInformation.Id,
                    FirstName = personalInformation.FirstName,
                    LastName = personalInformation.LastName,
                    ProfileImageUrl = personalInformation.ProfileImageUrl,
                    JoiningDate = personalInformation.JoiningDate,
                    Branch = personalInformation.Branch,
                    AppUser = personalInformation.AppUser,
                    IsAuthorAdmin = IsAuthorAdmin(personalInformation.AppUser)
                });

            if (!String.IsNullOrEmpty(searchString))
            {
                peoplesInformation = peoplesInformation.Where(s => s.FirstName.Contains(searchString) 
                                                          || s.LastName.Contains(searchString)
                                                          || s.Branch.BranchName.Contains(searchString)
                                                          || s.Branch.Province.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "firstname_desc":
                    peoplesInformation = peoplesInformation.OrderByDescending(s => s.FirstName);
                    break;
                case "lastname_desc":
                    peoplesInformation = peoplesInformation.OrderByDescending(s => s.LastName);
                    break;
                case "date_desc":
                    peoplesInformation = peoplesInformation.OrderByDescending(s => s.JoiningDate);
                    break;
                case "Date":
                    peoplesInformation = peoplesInformation.OrderBy(s => s.JoiningDate);
                    break;
                default: //Name ascending
                    peoplesInformation = peoplesInformation.OrderBy(s => s.LastName);
                    break;
            }

            // PersonalInformationListModel.cs wraps the collection of PersonalInformationViewModel
            // This is a light weight rapper for the PersonalInformationViewModel
            // In the future our data might become complex 

            var model = new PersonalInformationIndexModel
            {
                PersonalInformationList = peoplesInformation  // User name PersonalInformationList in View
            };

            return View(model);

            //The MVC framework is going to be looking for a view called Index in a folder called PersonalInformation
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user)
                .Result.Contains("Admin");
        }


        // GET: PersonalInformation/Create
        public IActionResult Create()
        {
            /**
             The HttpGet Create method calls the PopulateDepartmentsDropDownList method without setting the selected item,
             because for a new personal information record the branch is not established yet:
             **/
            PopulateBranchesDropDownList();
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,EmailAddress,ContactNumber,AlternativeContactNumber,Address,MethodOfContact,ProfileImageUrl,JoiningDate,BranchId")] NewPersonalInformation model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var personalInformation = BuildPersonalInformation(model, user);

            await _personalInformationService.Create(personalInformation);

            return RedirectToAction("Index", "PersonalInformation", new { id = personalInformation.Id });
        }

        private PersonalInformation BuildPersonalInformation(NewPersonalInformation model, ApplicationUser user)
        {

            return new PersonalInformation
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                ContactNumber = model.ContactNumber,
                AlternativeContactNumber = model.AlternativeContactNumber,
                Address = model.Address,
                MethodOfContact = model.MethodOfContact,
                ProfileImageUrl = "/images/personalinformation/" + model.ProfileImageUrl,
                JoiningDate = model.JoiningDate,
                Branch = _branchInformationService.GetById(model.BranchId),
                AppUser = user

            };
        }



        /**
         The PopulateBranchDropDownList method gets a list of all branches sorted by name, creates a SelectList
         collection for a drop-down list, and passes the collection to the view in a ViewBag property. The method accepts
         the optional selectedBranch parameter that allows the calling code to specify the item that will be selected
         when the drop-down list is rendered. The view will pass the name BranchID to the DropDownList helper, and the
         helper then knows to look in the ViewBag object for a SelectList named BranchID. 
         **/

        private void PopulateBranchesDropDownList(object selectedBranch = null)
        {
            var branchesQuery = from d in _branchInformationService.GetAll()
                                   orderby d.BranchName
                                   select d;
            ViewBag.BranchID = new SelectList(branchesQuery, "Id", "BranchName", selectedBranch);
        }


        // GET: PersonalInformation/Edit/5
        public IActionResult Edit(int id)
        {

            var personalInformation = _personalInformationService.GetById(id);
            var newPersonalInformation = new NewPersonalInformation
            {
                Id = personalInformation.Id,
                FirstName = personalInformation.FirstName,
                LastName = personalInformation.LastName,
                EmailAddress = personalInformation.EmailAddress,
                ContactNumber = personalInformation.ContactNumber,
                AlternativeContactNumber = personalInformation.AlternativeContactNumber,
                Address = personalInformation.Address,
                MethodOfContact = personalInformation.MethodOfContact,
                ProfileImageUrl = personalInformation.ProfileImageUrl,
                JoiningDate = personalInformation.JoiningDate,
                BranchId = personalInformation.Branch.Id
            };

            if (newPersonalInformation == null)
            {
                return NotFound();
            }

            PopulateBranchesDropDownList(newPersonalInformation.BranchId);
            return View(newPersonalInformation);
        }

        // POST: BranchInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmailAddress,ContactNumber,AlternativeContactNumber,Address,MethodOfContact,ProfileImageUrl,JoiningDate,BranchId")] NewPersonalInformation newPersonalInformation)
        {
            if (id != newPersonalInformation.Id)
            {
                return NotFound();
            }

            var branchToUpdate = _branchInformationService.GetById(newPersonalInformation.BranchId);

            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;

            if (ModelState.IsValid)
            {
                try
                {
                    await _personalInformationService.UpdatePersonalInformation(newPersonalInformation.Id, newPersonalInformation.FirstName, newPersonalInformation.LastName, newPersonalInformation.EmailAddress,
                        newPersonalInformation.ContactNumber, newPersonalInformation.AlternativeContactNumber, newPersonalInformation.Address, newPersonalInformation.MethodOfContact, newPersonalInformation.ProfileImageUrl,
                        newPersonalInformation.JoiningDate, branchToUpdate, user);
      
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalInformationExists(newPersonalInformation.Id))
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

            PopulateBranchesDropDownList(branchToUpdate.Id);
            return View(newPersonalInformation);
        }


        private bool PersonalInformationExists(int id)
        {
            return _personalInformationService.GetAll().Any(e => e.Id == id);
        }


        // GET: PersonalInformation/Delete/5
        public IActionResult Delete(int id)
        {

            var personalInformation = _personalInformationService.GetById(id);
            var newPersonalInformation = new NewPersonalInformation
            {
                Id = personalInformation.Id,
                FirstName = personalInformation.FirstName,
                LastName = personalInformation.LastName,
                EmailAddress = personalInformation.EmailAddress,
                ContactNumber = personalInformation.ContactNumber,
                AlternativeContactNumber = personalInformation.AlternativeContactNumber,
                Address = personalInformation.Address,
                MethodOfContact = personalInformation.MethodOfContact,
                ProfileImageUrl = personalInformation.ProfileImageUrl,
                JoiningDate = personalInformation.JoiningDate,
                BranchId = personalInformation.Branch.Id
            };

            if (newPersonalInformation == null)
            {
                return NotFound();
            }

            return View(newPersonalInformation);
        }

        // POST: PersonalInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _personalInformationService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        // GET: PersonalInformation/Details/5
        public IActionResult Details(int id)
        {

            var personalInformation = _personalInformationService.GetById(id);
            var newPersonalInformation = new NewPersonalInformation
            {
                Id = personalInformation.Id,
                FirstName = personalInformation.FirstName,
                LastName = personalInformation.LastName,
                EmailAddress = personalInformation.EmailAddress,
                ContactNumber = personalInformation.ContactNumber,
                AlternativeContactNumber = personalInformation.AlternativeContactNumber,
                Address = personalInformation.Address,
                MethodOfContact = personalInformation.MethodOfContact,
                ProfileImageUrl = personalInformation.ProfileImageUrl,
                JoiningDate = personalInformation.JoiningDate,
                BranchId = personalInformation.Branch.Id
            };

            if (newPersonalInformation == null)
            {
                return NotFound();
            }

            return View(newPersonalInformation);
        }


        // GET: /PersonalInformation/Get
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<PersonalInformation> peoplesInformation = _personalInformationService.GetAll();
            return Ok(peoplesInformation);
        }

        // GET: /PersonalInformation/Get/1
        [HttpGet("/PersonalInformation/Get/{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var personalInformation = _personalInformationService.GetById(id);

            if (personalInformation == null)
            {
                return NotFound("The Personal Information record could not be found");
            }

            return Ok(personalInformation);
        }

        public IActionResult Branch(int id)
        {
            var personalInformation = _personalInformationService.GetById(id);
            var branch = _branchInformationService.GetBranchByPersonalInformation(id);

            var model = new BranchInformationViewModel
            {
                Id = branch.Id,
                BranchName = branch.BranchName,
                BranchCode = branch.BranchCode,
                City = branch.City,
                Province = branch.Province
            };

            return View(model);
        }

    }
}