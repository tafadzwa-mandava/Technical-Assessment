using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAssessment.Data.Models;

namespace TechnicalAssessment.Models.PersonalInformationViewModels
{
    public class NewPersonalInformation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public string AlternativeContactNumber { get; set; }
        public string Address { get; set; }
        public string MethodOfContact { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime JoiningDate { get; set; }

        public int BranchId { get; set; }

    } 
}
