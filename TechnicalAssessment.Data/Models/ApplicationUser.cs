using Microsoft.AspNetCore.Identity;
using System;

namespace TechnicalAssessment.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string ProfileImageUrl { get; set; }
        public DateTime AdministratorSince { get; set; }
        public bool IsActive { get; set; }
    }
}
