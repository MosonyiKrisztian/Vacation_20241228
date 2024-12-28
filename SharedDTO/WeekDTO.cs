using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class WeekDTO
    {
        public int Id { get; set; }

        public List<NewVacationRequestDTO> Dates { get; set; } = new List<NewVacationRequestDTO>();
    }
}
