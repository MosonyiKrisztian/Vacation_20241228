using System.ComponentModel.DataAnnotations;

namespace Vacation.DAL.Model
{
    public class VacationRequest
    {
        [Key]
        public int RequestId { get; set; }

        public int status { get; set; } = 0;

        public string? Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ToDate { get; set; }

        public string? DateValue { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        //Connection to employee
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        //Connection to approver
        public int ApproverID { get; set; }

        public virtual Approver? Approver { get; set; }

    }
}
