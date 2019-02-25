using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAssessment.Data.Models;

namespace TechnicalAssessment.Models.PersonalInformationViewModels
{
    public class PersonalInformationViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime JoiningDate { get; set; }

        public virtual BranchInformation Branch { get; set; }
        public virtual ApplicationUser User { get; set; }

        public bool IsAuthorAdmin { get; set; }
    }
}
