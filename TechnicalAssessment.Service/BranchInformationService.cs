using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Data;
using TechnicalAssessment.Data.Models;

namespace TechnicalAssessment.Service
{
    public class BranchInformationService : IBranchInformation
    {
        private readonly ApplicationDbContext _context;
        public BranchInformationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(BranchInformation branchInformation)
        {
            _context.Add(branchInformation);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int branchInformationId)
        {
            BranchInformation branchInformation = this.GetById(branchInformationId);
            _context.Remove(branchInformation);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<BranchInformation> GetAll()
        {
            return _context.BranchesInformation;
        }

        public BranchInformation GetBranchByPersonalInformation(int branchInformationId)
        {
            var branch = _context.PeoplesInformation.Where(p => p.Id == branchInformationId)
                .FirstOrDefault()
                .Branch;

            return branch;
        }

        public BranchInformation GetById(int branchInformationId)
        {
             var branchInformation = _context.BranchesInformation.Where(b => b.Id == branchInformationId)
            .FirstOrDefault();

            return branchInformation;
        }

        public async Task UpdateBranchInformation(int branchlInformationId, string newBranchName, string newBranchCode, string newCity, string newProvince)
        {
            BranchInformation branchInformation = this.GetById(branchlInformationId);
            branchInformation.BranchName = newBranchName;
            branchInformation.BranchCode = newBranchCode;
            branchInformation.City = newCity;
            branchInformation.Province = newProvince;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBranchInformationBranchName(int branchlInformationId, string newBranchName)
        {
            BranchInformation branchInformation = this.GetById(branchlInformationId);
            branchInformation.BranchName = newBranchName;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBranchInformationBranchCode(int branchInformationId, string newBranchCode)
        {
            BranchInformation branchInformation = this.GetById(branchInformationId);
            branchInformation.BranchCode = newBranchCode;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBranchInformationCity(int branchInformationId, string newCity)
        {
            BranchInformation branchInformation = this.GetById(branchInformationId);
            branchInformation.City = newCity;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBranchInformationProvince(int branchInformationId, string newProvince)
        {
            BranchInformation branchInformation = this.GetById(branchInformationId);
            branchInformation.Province = newProvince;

            await _context.SaveChangesAsync();
        }
    }
}
