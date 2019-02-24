using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                    User = personalInformation.User
                });

            // PersonalInformationListModel.cs wraps the collection of PersonalInformationViewModel
            // This is a light weight rapper for the PersonalInformationViewModel
            // In the future our data might become complex 

            var model = new PersonalInformationIndexModel
            {
                PersonalInformationList = peoplesInformation
            };

            return View(model);

            //The MVC framework is going to be looking for a view called Index in a folder called PersonalInformation
        }

        private int CalculateYears(DateTime joinDate)
        {
            throw new NotImplementedException();
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