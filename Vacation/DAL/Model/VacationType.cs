using System.ComponentModel.DataAnnotations;

namespace Vacation.DAL.Model
{
    public class VacationType
    {
        [Key]
        public string VRequestTypeID { get; set; }
        public string? VRequestTypeDEscription { get; set; }
    }
}
