using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class UpdateEmployeeDTO
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        public string NameOfMother { get; set; }

        public DateTime StartOfEmployment { get; set; } = DateTime.Now;

        [RegularExpression(@"^\d{3} \d{3} \d{3}$", ErrorMessage = "A helyes formátum 123 456 789")]
        public string? InsuranceNumber { get; set; }

        public string Role { get; set; }
        [Range(0, 1)]
        public int Disability { get; set; } = 0;

        public int DepartmentID { get; set; }

        public int ChildId { get; set; }
    }
}
