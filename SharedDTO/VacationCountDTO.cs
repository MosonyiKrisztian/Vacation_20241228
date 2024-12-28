using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class VacationCountDTO
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string NameOfMother { get; set; }

        public DateTime StartOfEmployment { get; set; }

        public string InsuranceNumber { get; set; }

        public string Role { get; set; }

        public int Disability { get; set; }
        [JsonIgnore]
        public int DepartmentID { get; set; }
        //public virtual DepartmentDTO? Department { get; set; }

        //public int ChildId { get; set; }

        //[JsonIgnore]
        public virtual List<int>? ChildrenId { get; set; }
        [JsonIgnore]
        public virtual List<int>? ChildrenDisab { get; set; }
        [JsonIgnore]
        public virtual List<DateTime>? ChildrenDOB { get; set; }
    }
}
