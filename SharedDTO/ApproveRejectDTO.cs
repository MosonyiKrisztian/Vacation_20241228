using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class ApproveRejectDTO
    {
        public int RequestId { get; set; }
        [Range(1, 4)]
        public int status { get; set; }
        public int employeeId { get; set; }
        public int approverId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
    }
}
