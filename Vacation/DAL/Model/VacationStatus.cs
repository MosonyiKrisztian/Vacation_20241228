using System.ComponentModel.DataAnnotations;

namespace Vacation.DAL.Model
{
    public class VacationStatus
    {
        [Key]
        public int VRequestStatusID { get; set; }
        public string? VRequestStatusDescription { get; set; }
    }
}
