using System.ComponentModel.DataAnnotations;

namespace Vacation.DAL.Model
{
    public class IrregularDay
    {
        [Key]
        public DateTime IrrDate { get; set; } = DateTime.Now;

        public bool Workday { get; set; } = false;
    }
}
