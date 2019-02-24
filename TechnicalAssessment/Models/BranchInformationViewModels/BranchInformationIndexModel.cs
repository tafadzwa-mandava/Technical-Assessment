using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAssessment.Models.BranchInformationViewModels
{
    public class BranchInformationIndexModel
    {
        public IEnumerable<BranchInformationViewModel> BranchInformationList { get; set; }
    }
}
