using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAssessment.Data.Models;
using TechnicalAssessment.Models.PersonalInformationViewModels;

namespace TechnicalAssessment.Models.HomeViewModels
{
    public class HomeIndexModel
    {
        public IEnumerable<PersonalInformationViewModel> LatestPeoplesInformation { get; set; }
        public string SearchQuery { get; set; }
    }
}
