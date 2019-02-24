using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAssessment.Models.BranchInformationViewModels
{
    public class BranchInformationViewModel
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}
