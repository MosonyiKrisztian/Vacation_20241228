using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class ApproverDTO
    {
        [Key]
        public int ApproverID { get; set; }

        public int EmployeeId { get; set; }
    }
}
