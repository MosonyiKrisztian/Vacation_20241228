namespace VacationFrontend.Calculation
{
    public class CurrentYear
    {

        public int currentYear;
        public int GetCurrentYear()
        {
            currentYear = DateTime.Now.Year;
            return currentYear;
        }
    }
}