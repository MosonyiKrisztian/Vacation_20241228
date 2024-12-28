using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class VacationRequestDTO
    {
        public int RequestId { get; set; }

        public int status { get; set; } = 0;

        public string? Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ToDate { get; set; }

        public string? DateValue { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public int EmployeeId { get; set; }

        //Connection to approver
        public int ApproverID { get; set; } = 1;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<EmployeeDTO> Employee { get; set; } = new List<EmployeeDTO>();

    }
}
