using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace SharedDTO
{
    public class ChildDTO
        {
        public int ChildId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Disability { get; set; }



        // Az EmployeeId-k listája, ami összeköti a munkavállalókat a gyermekkel
        //public int EmployeeId { get; set; }
    }
}

