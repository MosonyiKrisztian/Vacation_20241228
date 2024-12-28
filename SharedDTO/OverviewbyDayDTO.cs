using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class OverviewbyDayDTO
    {
        public int EmployeeId { get; set; }
        public List<DateTime> ToDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> status { get; set; }
    }
}
