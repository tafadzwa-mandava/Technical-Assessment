using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalAssessment.Data;
using TechnicalAssessment.Models;
using TechnicalAssessment.Models.HomeViewModels;
using TechnicalAssessment.Models.PersonalInformationViewModels;

namespace TechnicalAssessment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonalInformation _personalInformationService;
        public HomeController(IPersonalInformation personalInformationService)
        {
            _personalInformationService = personalInformationService;
        }

        public IActionResult Index()
        {
            var model = BuildHomeIndexModel();
            return View(model);
        }

        
        private HomeIndexModel BuildHomeIndexModel()
        {
            var latestPeoplesInformation = _personalInformationService.GetLatestPersonalInformation(5);
            var peoplesInformation = latestPeoplesInformation.Select(personalInformation => new PersonalInformationViewModel
                {
                    Id = personalInformation.Id,
                    FirstName = personalInformation.FirstName,
                    LastName = personalInformation.LastName,
                    ProfileImageUrl = personalInformation.ProfileImageUrl,
                    JoiningDate = personalInformation.JoiningDate,
                    Branch = personalInformation.Branch,
                    AppUser = personalInformation.AppUser
                });

            return new HomeIndexModel
            {
                //LatestPeoplesInformation = peoplesInformation;
                LatestPeoplesInformation = peoplesInformation,
                SearchQuery = ""
            };
        }
        

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
