using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class ChildEmployeeDTO
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        //Kapcsolat gyermekekkel
        public ICollection<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();

        public virtual List<int>? ChildrenId { get; set; }
        public virtual List<string>? ChildrenFirstName { get; set; }
        public virtual List<string>? ChildrenLastName { get; set; }



    }
}

