using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class IrregularDaysDTO
    {
        public DateTime IrrDate { get; set; } = DateTime.Now;
        public bool Workday { get; set; }
    }
}
