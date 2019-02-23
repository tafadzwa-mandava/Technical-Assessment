﻿using System;
using System.Collections.Generic;
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

        public Task Create(BranchInformation branchInformation)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int branchInformationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BranchInformation> GetAll()
        {
            return _context.BranchesInformation;
        }

        public BranchInformation GetById()
        {
            throw new NotImplementedException();
        }

        public Task UpdateBranchInformation(int branchlInformationId, string newBranchName, string newBranchCode, string newCity, string newProvince)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBranchInformationBranchCode(int branchInformationId, string newBranchCode)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBranchInformationBranchName(int branchlInformationId, string newBranchName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBranchInformationCity(int branchInformationId, string newCity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBranchInformationProvince(int branchInformationId, string newProvince)
        {
            throw new NotImplementedException();
        }
    }
}
