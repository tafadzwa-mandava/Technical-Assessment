using System;
using System.ComponentModel.DataAnnotations;

namespace TechnicalAssessment.Data.Models
{
    public class PersonalInformation
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
    
        public virtual BranchInformation Branch { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
    }
}
