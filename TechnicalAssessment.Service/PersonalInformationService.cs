using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAssessment.Data;
using TechnicalAssessment.Data.Models;

namespace TechnicalAssessment.Service
{
    public class PersonalInformationService : IPersonalInformation
    {
        private readonly ApplicationDbContext _context;
        public PersonalInformationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(PersonalInformation personalInformation)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int personalInformationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonalInformation> GetAll()
        {
            return _context.PeoplesInformation
                .Include(personalInformation => personalInformation.Branch);
        }

        public IEnumerable<ApplicationUser> GetAllAdminUsers()
        {
            throw new NotImplementedException();
        }

        public PersonalInformation GetById(int id)
        {
            var personalInformation = _context.PeoplesInformation.Where(p => p.Id == id)
                .Include(p => p.Branch)
                .Include(p => p.User)
                .FirstOrDefault();

            return personalInformation;
        }

        public IEnumerable<PersonalInformation> GetFilteredPersonalInformation(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformation(int personalInformationId, string newLastName, string newEmailAddress, string newContactNumber, string AlternativeContactNumber, string Address, string newMethodOfContact, string newProfileImageUrl, DateTime newJoiningDate, BranchInformation newBranchInformation)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformationAddress(int personalInformationId, string newAddress)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformationAlternativeContactNumber(int personalInformationId, string newAlternativeContactNumber)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformationBranchInformation(int personalInformationId, BranchInformation newBranchInformation)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformationContactNumber(int personalInformationId, string newContactNumber)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformationEmailAddress(int personalInformationId, string newEmailAddress)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformationFirstName(int personalInformationId, string newFirstName)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformationJoiningDate(int personalInformationId, DateTime newJoiningDate)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformationLastName(int personalInformationId, string newLastName)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonalInformationProfileImageUrl(int personalInformationId, string newProfileImageUrl)
        {
            throw new NotImplementedException();
        }
    }
}
