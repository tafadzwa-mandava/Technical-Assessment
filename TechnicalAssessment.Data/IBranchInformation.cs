using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Data.Models;

namespace TechnicalAssessment.Data
{
    public interface IBranchInformation
    {
        BranchInformation GetById(int branchInformationId);
        IEnumerable<BranchInformation> GetAll();

        Task Create(BranchInformation branchInformation);
        Task Delete(int branchInformationId);

        Task UpdateBranchInformation(int branchlInformationId, string newBranchName, string newBranchCode,
                                     string newCity, string newProvince);
        Task UpdateBranchInformationBranchName(int branchlInformationId, string newBranchName);
        Task UpdateBranchInformationBranchCode(int branchInformationId, string newBranchCode);
        Task UpdateBranchInformationCity(int branchInformationId, string newCity);
        Task UpdateBranchInformationProvince(int branchInformationId, string newProvince);
    }
}
