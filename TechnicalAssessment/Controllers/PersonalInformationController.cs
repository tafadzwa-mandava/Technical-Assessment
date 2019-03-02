using System;
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

        public IActionResult Index()
        {
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
                ProfileImageUrl = model.ProfileImageUrl,
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
            if (personalInformation == null)
            {
                return NotFound();
            }

            PopulateBranchesDropDownList(personalInformation.Branch.Id);
            return View(personalInformation);
        }

        // POST: BranchInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmailAddress,ContactNumber,AlternativeContactNumber,Address,MethodOfContact,ProfileImageUrl,JoiningDate,Branch.Id")] PersonalInformation personalInformation)
        {
            if (id != personalInformation.Id)
            {
                return NotFound();
            }

            var branchToUpdate = _branchInformationService.GetBranchByPersonalInformation(personalInformation.Branch.Id);

            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;

            if (ModelState.IsValid)
            {
                try
                {
                    await _personalInformationService.UpdatePersonalInformation(personalInformation.Id, personalInformation.FirstName, personalInformation.LastName, personalInformation.EmailAddress,
                        personalInformation.ContactNumber, personalInformation.AlternativeContactNumber, personalInformation.Address, personalInformation.MethodOfContact, personalInformation.ProfileImageUrl,
                        personalInformation.JoiningDate, branchToUpdate, user);
      
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

            PopulateBranchesDropDownList(branchToUpdate.Id);
            return View(personalInformation);
        }


        private bool PersonalInformationExists(int id)
        {
            return _personalInformationService.GetAll().Any(e => e.Id == id);
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