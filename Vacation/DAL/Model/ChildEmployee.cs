using Microsoft.EntityFrameworkCore;

namespace Vacation.DAL.Model
{
    [PrimaryKey(nameof(ChildId), nameof(EmployeeId))]
    public class ChildEmployee
    {
        public int ChildId { get; set; }

        public int EmployeeId { get; set; }

        public Child Children { get; set; } = null;

        public Employee Employee { get; set; } = null;
    }
}
