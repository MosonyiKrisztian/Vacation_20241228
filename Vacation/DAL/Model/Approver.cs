using System.ComponentModel.DataAnnotations;

namespace Vacation.DAL.Model
{
    public class Approver
    {
        [Key]
        public int ApproverID { get; set; }

        public int EmployeeId { get; set; }

        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

        public virtual ICollection<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>();
    }
}
