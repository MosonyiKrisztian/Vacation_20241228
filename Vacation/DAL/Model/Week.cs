namespace Vacation.DAL.Model
{
    public class Week
    {
        public int Id { get; set; }
        public List<VacationRequest> Dates { get; set; } = new List<VacationRequest>();
    }
}
