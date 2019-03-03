using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAssessment.Data.Models;

namespace TechnicalAssessment.Models.PersonalInformationViewModels
{
    public class NewPersonalInformation
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Alternative Contact Number")]
        public string AlternativeContactNumber { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Method of Contact")]
        public string MethodOfContact { get; set; }

        [Display(Name = "Profile Image")]
        public string ProfileImageUrl { get; set; }

        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }

        [Display(Name = "Branch ID")]
        public int BranchId { get; set; }

    } 
}
