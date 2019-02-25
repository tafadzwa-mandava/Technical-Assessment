using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    User = personalInformation.User,
                    IsAuthorAdmin = IsAuthorAdmin(personalInformation.User)
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

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user)
                .Result.Contains("Admin");
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

        [HttpPost]
        public async Task<IActionResult> AddPersonalInformation(NewPesornalInformation model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var personalInformation = BuildPersonalInformation(model, user);

             //_personalInformationService.Create(personalInformation).Wait(); //Block the current threas until the task is complete
            await _personalInformationService.Add(personalInformation);

            return RedirectToAction("Index", "PersonalInformation", new { id = personalInformation.Id });
        }

        private PersonalInformation BuildPersonalInformation(NewPesornalInformation model, ApplicationUser user)
        {
            var personalInformation = _personalInformationService.GetById(model.Id);

            return new PersonalInformation
            {
                FirstName = personalInformation.FirstName,
                LastName = personalInformation.LastName,
                EmailAddress = personalInformation.EmailAddress,
                ContactNumber = personalInformation.ContactNumber,
                AlternativeContactNumber = personalInformation.AlternativeContactNumber,
                Address = personalInformation.Address,
                MethodOfContact = personalInformation.MethodOfContact,
                ProfileImageUrl = personalInformation.ProfileImageUrl,
                JoiningDate = personalInformation.JoiningDate,
                Branch = new BranchInformation
                        {
                            Id = personalInformation.Branch.Id,
                            BranchName = personalInformation.Branch.BranchName,
                            BranchCode = personalInformation.Branch.BranchCode,
                            City = personalInformation.Branch.City,
                            Province = personalInformation.Branch.Province
                        },
                User = user

            };
        }


        private int CalculateYears(DateTime joinDate)
        {
            throw new NotImplementedException();
        }


        //TESTING the APIs below in POSTMAN 

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



        // POST: /PersonalInformation/Post
        //[HttpPost]
        public IActionResult Post([FromBody] PersonalInformation personalInformation)
        {
            _personalInformationService.Add(personalInformation);
            return CreatedAtRoute("PersonalInformation/Get", new { id = personalInformation.Id }, personalInformation);
        }


        // PUT: /ApplicationInformation/Put/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PersonalInformation personalInformation)
        {
            if (personalInformation == null)
            {
                return BadRequest("The Personal Information record couldn't be found.");
            }

            PersonalInformation personalInformationToUpdate = _personalInformationService.GetById(id);
            if(personalInformationToUpdate == null)
            {
                return NotFound("The Personal Information record couldn't be found.");
            }

            _personalInformationService.UpdatePersonalInformation(id,
                personalInformationToUpdate.FirstName,
                personalInformationToUpdate.LastName,
                personalInformationToUpdate.EmailAddress,
                personalInformationToUpdate.ContactNumber,
                personalInformationToUpdate.AlternativeContactNumber,
                personalInformationToUpdate.Address,
                personalInformationToUpdate.MethodOfContact,
                personalInformationToUpdate.ProfileImageUrl,
                personalInformationToUpdate.JoiningDate,
                new BranchInformation {
                    Id = personalInformationToUpdate.Branch.Id,
                    BranchName = personalInformationToUpdate.Branch.BranchName,
                    BranchCode = personalInformationToUpdate.Branch.BranchCode,
                    City = personalInformationToUpdate.Branch.City,
                    Province = personalInformationToUpdate.Branch.Province
                }
             );

            return NoContent();
        }


        // DELETE: /PersonalInformation/Delete/5
        //[HttpDelete("{id}")]
        [HttpGet("/PersonalInformation/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _personalInformationService.Delete(id); 
            return NoContent();
        }

    }
}