using System.ComponentModel.DataAnnotations;

namespace Vacation.DAL.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? NameOfMother { get; set; }

        public DateTime StartOfEmployment { get; set; }

        public string InsuranceNumber { get; set; }

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required, MinLength(8)]
        public string? Password { get; set; }

        public string? Role { get; set; }

        public int Disability { get; set; }

        public int DepartmentID { get; set; }
        public Department? Department { get; set; }

        public virtual ICollection<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>();

        public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    }
}
