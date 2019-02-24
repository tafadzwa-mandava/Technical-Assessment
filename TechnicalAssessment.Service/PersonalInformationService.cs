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

        public async Task Create(PersonalInformation personalInformation)
        {
            _context.Add(personalInformation);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int personalInformationId)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            _context.Remove(personalInformation);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PersonalInformation> GetAll()
        {
            return _context.PeoplesInformation
                .Include(personalInformation => personalInformation.Branch)
                .Include(personalInformation => personalInformation.User);
        }

        public IEnumerable<ApplicationUser> GetAllAdminUsers()
        {
            throw new NotImplementedException();
        }

        public PersonalInformation GetById(int personalInformationId)
        {
            var personalInformation = _context.PeoplesInformation.Where(p => p.Id == personalInformationId)
                .Include(p => p.Branch)
                .Include(p => p.User)
                .FirstOrDefault();

            return personalInformation;
        }

        public IEnumerable<PersonalInformation> GetFilteredPersonalInformation(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonalInformation> GetLatestPersonalInformation(int count)
        {
            var personalInformation = this.GetAll();
            var latestPersonalInformation = personalInformation.OrderByDescending(p => p.JoiningDate).Take(count);
            return latestPersonalInformation;
        }

        public async Task UpdatePersonalInformation(int personalInformationId, string newLastName, string newEmailAddress, string newContactNumber, string newAlternativeContactNumber, string newAddress, string newMethodOfContact, string newProfileImageUrl, DateTime newJoiningDate, BranchInformation newBranchInformation, ApplicationUser newUser)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.LastName = newLastName;
            personalInformation.EmailAddress = newEmailAddress;
            personalInformation.ContactNumber = newContactNumber;
            personalInformation.AlternativeContactNumber = newAlternativeContactNumber;
            personalInformation.Address = newAddress;
            personalInformation.MethodOfContact = newMethodOfContact;
            personalInformation.ProfileImageUrl = newProfileImageUrl;
            personalInformation.JoiningDate = newJoiningDate;
            personalInformation.Branch = newBranchInformation;
            personalInformation.User = newUser;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationAddress(int personalInformationId, string newAddress)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.Address = newAddress;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationAlternativeContactNumber(int personalInformationId, string newAlternativeContactNumber)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.AlternativeContactNumber = newAlternativeContactNumber;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationBranchInformation(int personalInformationId, BranchInformation newBranchInformation)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.Branch = newBranchInformation;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationContactNumber(int personalInformationId, string newContactNumber)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.ContactNumber = newContactNumber;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationEmailAddress(int personalInformationId, string newEmailAddress)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.EmailAddress = newEmailAddress;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationFirstName(int personalInformationId, string newFirstName)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.FirstName = newFirstName;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationJoiningDate(int personalInformationId, DateTime newJoiningDate)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.JoiningDate = newJoiningDate;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationLastName(int personalInformationId, string newLastName)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.LastName = newLastName;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationProfileImageUrl(int personalInformationId, string newProfileImageUrl)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.ProfileImageUrl = newProfileImageUrl;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalInformationUser(int personalInformationId, ApplicationUser newUser)
        {
            PersonalInformation personalInformation = this.GetById(personalInformationId);
            personalInformation.User = newUser;

            await _context.SaveChangesAsync();
        }

    }
}
