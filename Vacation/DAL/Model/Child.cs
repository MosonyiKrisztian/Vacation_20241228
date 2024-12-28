using System.ComponentModel.DataAnnotations;

namespace Vacation.DAL.Model
{
    public class Child
    {
        [Key]
        public int ChildId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Disability { get; set; } = 0;

        //public int EmployeeId { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        
    }
}
