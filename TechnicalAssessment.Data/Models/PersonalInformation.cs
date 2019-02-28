using System;

namespace TechnicalAssessment.Data.Models
{
    public class PersonalInformation
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
    
        public virtual BranchInformation Branch { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
    }
}
