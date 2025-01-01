using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SharedDTO;
using ZstdSharp.Unsafe;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace VacationFrontend.Calculation
{
    public class VacationCalculation
    {
        Dictionary<string, double> typeNrDays = new Dictionary<string, double>();
        List<VacationCountDTO> employeeList = new();
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
        public double multiplicator = 365;

        public Dictionary<string, double> GetVacationNr(VacationCountDTO employee, int Year)
        {
            currentYear = (currentYear == Year ? currentYear : Year);
            typeNrDays.Clear();
            DateTime endOfYear = new DateTime(currentYear, 12, 31);
            age = currentYear - employee.DateOfBirth.Year;

                if (currentYear == employee.StartOfEmployment.Year)
                {
                    diffDates = endOfYear.Subtract(employee.StartOfEmployment);
                    timeProportional = true;
                    multiplicator = diffDates.Days;
                }

                //age = 0;
                baseVacationDaysNr = 20;
                typeNrDays.Add("alapszabadasag", Math.Round(timeProportional ? baseVacationDaysNr / 365 * multiplicator : baseVacationDaysNr));


                youngEmployeeDaysNr = 0;

                if (age >= 16 && age <= 18) youngEmployeeDaysNr += 5;   //2024.12.31, KM (added: age >= 16 &&)
                typeNrDays.Add("fiatalmvallalosz", Math.Round(timeProportional ? youngEmployeeDaysNr / 365 * multiplicator : youngEmployeeDaysNr));


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
                if (employee.ChildrenId != null)
                {
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
                }

                totalVacationDaysNr = 0;
                totalVacationDaysNr = baseVacationDaysNr +
                                      baseVacationDaysAgeNr +
                                      youngEmployeeDaysNr +
                                      disabiltyVacationDaysNr +
                                      childVacationDaysNr;
                typeNrDays.Add("totalszabadasag", Math.Round(timeProportional ? totalVacationDaysNr / 365 * multiplicator : totalVacationDaysNr));

                timeProportional = false;

            //return Math.Round(vacationdaysNr,2);

            if (age < 16)  //no calculation if age uder 16 years
            {
                foreach(var dictItem in typeNrDays.ToList())
                {
                  typeNrDays[dictItem.Key] = 0;      
                }   
            }
            return typeNrDays;
        }   

        public int GetUsedVacationNr(VacationCountDTO employee, List<ApproveRejectDTO> request, int Year)
        {
            int counter = 0;
            foreach (var employeerequest in request)
            {
                if (employee.EmployeeId == employeerequest.employeeId && employeerequest.status != 4 && employeerequest.ToDate.Year == Year)
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
