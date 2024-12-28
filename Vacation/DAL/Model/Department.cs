using System.ComponentModel.DataAnnotations;

namespace Vacation.DAL.Model
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        public string? DepartmentDescription { get; set; }

        public virtual ICollection<Approver> Approvers { get; set; } = new List<Approver>();

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
