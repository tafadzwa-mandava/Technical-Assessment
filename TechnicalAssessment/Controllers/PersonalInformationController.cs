using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalAssessment.Data;
using TechnicalAssessment.Data.Models;
using TechnicalAssessment.Models.PersonalInformationViewModels;

namespace TechnicalAssessment.Controllers
{
    public class PersonalInformationController : Controller
    {
        private readonly IPersonalInformation _personalInformationService;
        public PersonalInformationController(IPersonalInformation personalInformationService)
        {
            _personalInformationService = personalInformationService;
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
                    Branch = personalInformation.Branch
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


        public IActionResult Branch(int id)
        {
            var personalInformation = _personalInformationService.GetById(id);

            return View();
        }
    }
}