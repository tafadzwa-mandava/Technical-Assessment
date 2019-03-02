using System;
using TechnicalAssessment.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TechnicalAssessment.Data
{
    public interface IPersonalInformation
    {
        PersonalInformation GetById(int personalInformationId);
        IEnumerable<PersonalInformation> GetAll();
        IEnumerable<PersonalInformation> GetFilteredPersonalInformation(string searchQuery);
        IEnumerable<PersonalInformation> GetLatestPersonalInformation(int count);
        IEnumerable<ApplicationUser> GetAllAdminUsers();

        Task Create(PersonalInformation personalInformation);
        Task Delete(int personalInformationId);

        Task UpdatePersonalInformation(int personalInformationId, string FirstName, string newLastName, string newEmailAddress,
                       string newContactNumber, string AlternativeContactNumber, string Address,
                       string newMethodOfContact, string newProfileImageUrl, DateTime newJoiningDate,
                       BranchInformation newBranchInformation, ApplicationUser newAppUser);
        Task UpdatePersonalInformationFirstName (int personalInformationId, string newFirstName);
        Task UpdatePersonalInformationLastName(int personalInformationId, string newLastName);
        Task UpdatePersonalInformationEmailAddress(int personalInformationId, string newEmailAddress);
        Task UpdatePersonalInformationContactNumber(int personalInformationId, string newContactNumber);
        Task UpdatePersonalInformationAlternativeContactNumber(int personalInformationId, string newAlternativeContactNumber);
        Task UpdatePersonalInformationAddress(int personalInformationId, string newAddress);
        Task UpdatePersonalInformationProfileImageUrl(int personalInformationId, string newProfileImageUrl);
        Task UpdatePersonalInformationJoiningDate(int personalInformationId, DateTime newJoiningDate);
        Task UpdatePersonalInformationBranchInformation(int personalInformationId, BranchInformation newBranchInformation);
        Task UpdatePersonalInformationUser(int personalInformationId, ApplicationUser newAppUser);

    }
}
