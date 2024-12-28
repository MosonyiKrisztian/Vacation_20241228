using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SharedDTO;


namespace VacationFrontend.Calculation
{
    public class VacationCalculation
    {
        Dictionary<string, double> typeNrDays = new Dictionary<string, double>();
        public double baseVacationDaysNr;
        public double baseVacationDaysAgeNr;
        public double youngEmployeeDaysNr;
        public double childVacationDaysNr;
        public double disabiltyVacationDaysNr;
        public double totalVacationDaysNr;
        public int age = 0;
        public int currentYear = DateTime.Now.Year;
        public bool timeProportional = false;
        public TimeSpan diffDates;
        public double multiplicator;

        public Dictionary<string, double> GetVacationNr(VacationCountDTO employee)
        {
            typeNrDays.Clear();
            DateTime endOfYear = new DateTime(currentYear, 12, 31);


            if (currentYear == employee.StartOfEmployment.Year)
            {
                diffDates = endOfYear.Subtract(employee.StartOfEmployment);
                timeProportional = true;
                multiplicator = diffDates.Days;
            }

            age = 0;
            baseVacationDaysNr = 20;
            typeNrDays.Add("alapszabadasag", Math.Round(timeProportional ? baseVacationDaysNr / 365 * multiplicator : baseVacationDaysNr));


            youngEmployeeDaysNr = 0;
            age = currentYear - employee.DateOfBirth.Year;
            if (age <= 18) youngEmployeeDaysNr += 5;
            typeNrDays.Add("fiatalmvallalosz", Math.Round(timeProportional ? youngEmployeeDaysNr / 365 * multiplicator : youngEmployeeDaysNr));


            //baseVacationDaysAgeNr = 0;
            //if (25 <= age && age < 28) baseVacationDaysAgeNr += 1;
            //if (28 <= age && age < 31) baseVacationDaysAgeNr += 2;
            //if (31 <= age && age < 33) baseVacationDaysAgeNr += 3;
            //if (33 <= age && age < 35) baseVacationDaysAgeNr += 4;
            //if (35 <= age && age < 37) baseVacationDaysAgeNr += 5;
            //if (37 <= age && age < 39) baseVacationDaysAgeNr += 6;
            //if (39 <= age && age < 41) baseVacationDaysAgeNr += 7;
            //if (41 <= age && age < 43) baseVacationDaysAgeNr += 8;
            //if (43 <= age && age < 45) baseVacationDaysAgeNr += 9;
            //if (45 <= age) baseVacationDaysAgeNr += 10;

            //int age = DateTime.Now.Year - emp.DateOfBirth.Year;
            //if (DateTime.Now < emp.DateOfBirth.AddYears(age)) age--;

            int baseVacationDaysAgeNr = age switch
            {
                >= 45 => 10,
                >= 43 => 9,
                >= 41 => 8,
                >= 39 => 7,
                >= 37 => 6,
                >= 35 => 5,
                >= 33 => 4,
                >= 31 => 3,
                >= 28 => 2,
                >= 25 => 1,
                _ => 0
            };

            typeNrDays.Add("potkorszabadasag", Math.Round(timeProportional ? baseVacationDaysAgeNr / 365 * multiplicator : baseVacationDaysAgeNr));


            disabiltyVacationDaysNr = 0;
            if (employee.Disability == 1)
            {
                disabiltyVacationDaysNr += 5;
            }
            typeNrDays.Add("fogyatekszabadasag", Math.Round(timeProportional ? disabiltyVacationDaysNr / 365 * multiplicator : disabiltyVacationDaysNr));


            childVacationDaysNr = 0;
            int counter = 0;
            foreach (var Child in employee.ChildrenId)
            {
                if (counter == 0)
                {
                    if (3 <= employee.ChildrenId.Count)
                    {
                        childVacationDaysNr += 7;
                    }
                    if (3 > employee.ChildrenId.Count && employee.ChildrenId.Count >= 2)
                    {
                        childVacationDaysNr += 4;
                    }

                    if (employee.ChildrenId.Count == 1)
                    {
                        childVacationDaysNr += 2;
                    }
                    counter = 1;
                }

                if (Child == 1)
                {
                    childVacationDaysNr += 2;
                }
            }
            typeNrDays.Add("gyerekszabadasag", Math.Round(timeProportional ? childVacationDaysNr / 365 * multiplicator : childVacationDaysNr));


            totalVacationDaysNr = 0;
            totalVacationDaysNr = baseVacationDaysNr +
                                  baseVacationDaysAgeNr +
                                  youngEmployeeDaysNr +
                                  disabiltyVacationDaysNr +
                                  childVacationDaysNr;
            typeNrDays.Add("totalszabadasag", Math.Round(timeProportional ? totalVacationDaysNr / 365 * multiplicator : totalVacationDaysNr));

            timeProportional = false;

            //return Math.Round(vacationdaysNr,2);

            return typeNrDays;
        }

        public int GetUsedVacationNr(VacationCountDTO employee, List<ApproveRejectDTO> request)
        {
            int counter = 0;
            foreach (var employeerequest in request)
            {
                if (employee.EmployeeId == employeerequest.employeeId && employeerequest.status != 4)
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
