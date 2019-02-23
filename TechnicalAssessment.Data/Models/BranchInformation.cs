using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalAssessment.Data.Models
{
    public class BranchInformation
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}
