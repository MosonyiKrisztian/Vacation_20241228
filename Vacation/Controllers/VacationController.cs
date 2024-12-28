using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedDTO;
using Vacation.DAL.Data;

namespace Vacation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly MySQLDBContext _DBContext;

        public VacationController(MySQLDBContext dbContext)
        {
            _DBContext = dbContext;
        }

        [HttpGet("IrregularDay")]
        public async Task<ActionResult<List<IrregularDaysDTO>>> GetAllIrregularDays()
        {
            var irregularday = await _DBContext.irregularDays
                .Select(x => new IrregularDaysDTO
                {
                    IrrDate = x.IrrDate,
                    Workday = x.Workday
                })
                .OrderBy(z => z.IrrDate)
                .ToListAsync();

            //var irregulardayDTO = irregularday.Adapt<List<IrregularDaysDTO>>();
            return Ok(irregularday);

        }
        [HttpGet("EmployeeVacationRequest/{EmployeeId}")]
        public async Task<ActionResult<List<VacationRequestDTO>>> GetAllVacationRequestById(int Employeeid)
        {
            var vacationrequest = await _DBContext.vacationrequests
                .Where(u=>u.EmployeeId == Employeeid)
                .Include(u => u.Employee)
                .Include(a => a.Approver)
                .Select(x => new VacationRequestDTO
                {
                    RequestId = x.RequestId,
                    status = x.status,
                    Type = x.Type,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ToDate = x.ToDate,
                    RequestDate = x.RequestDate,
                    EmployeeId = x.EmployeeId,
                    FirstName = x.Employee.FirstName,
                    LastName = x.Employee.LastName

                })
                //.OrderBy(z => z.status)
                //.OrderBy(z => z.LastName)
                //.ThenBy(z => z.FirstName)
                //.ThenBy(z => z.ToDate)
                .ToListAsync();

            //var irregulardayDTO = irregularday.Adapt<List<IrregularDaysDTO>>();
            return Ok(vacationrequest);

        }

        [HttpGet("VacationRequest")]
        public async Task<ActionResult<List<VacationRequestDTO>>> GetAllVacationRequest()
        {
            var vacationrequest = await _DBContext.vacationrequests
                .Include(u => u.Employee)
                .Include(a => a.Approver)
                .Select(x => new VacationRequestDTO
                {
                    RequestId = x.RequestId,
                    status = x.status,
                    Type = x.Type,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ToDate = x.ToDate,
                    RequestDate = x.RequestDate,
                    EmployeeId =x.EmployeeId,
                    FirstName = x.Employee.FirstName,
                    LastName =x.Employee.LastName

                })
                //.OrderBy(z => z.status)
                //.OrderBy(z => z.LastName)
                //.ThenBy(z => z.FirstName)
                //.ThenBy(z => z.ToDate)
                .ToListAsync();

            //var irregulardayDTO = irregularday.Adapt<List<IrregularDaysDTO>>();
            return Ok(vacationrequest);

        }

        [HttpGet("VacationRequestOverview")]
        public async Task<ActionResult<List<OverviewbyDayDTO>>> GetAllVacationRequestOverview()
        {
            var vacationrequest = await _DBContext.employees
                .Include(u => u.VacationRequests)
                .Select(x => new OverviewbyDayDTO
                {             
                    EmployeeId = x.EmployeeId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    status = x.VacationRequests.Select(x=>x.status).ToList(),
                    ToDate = x.VacationRequests.Select(r=>r.ToDate).ToList(),

                })
                .ToListAsync();

            //var irregulardayDTO = irregularday.Adapt<List<IrregularDaysDTO>>();
            return Ok(vacationrequest);

        }

        [HttpGet("VacationCount")]
        public async Task<ActionResult<List<VacationCountDTO>>> GetAllEmployee()
        {
            var employeedata = await _DBContext.employees
                .Include(c => c.Children)
                .Include(d => d.Department)
                .Select(x => new VacationCountDTO
                {
                    EmployeeId = x.EmployeeId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBirth = x.DateOfBirth,
                    NameOfMother = x.NameOfMother,
                    StartOfEmployment = x.StartOfEmployment,
                    InsuranceNumber = x.InsuranceNumber,
                    Role = x.Role,
                    Disability = x.Disability,
                    ChildrenId = x.Children.Where(d => DateTime.Now.Year - d.DateOfBirth.Year <= 16).Select(x => x.Disability).ToList(),
                    ChildrenDisab = x.Children.Select(d => d.Disability).ToList()
                })
                .OrderBy(z => z.EmployeeId)
                .ToListAsync();

            //var irregulardayDTO = irregularday.Adapt<List<IrregularDaysDTO>>();
            return Ok(employeedata);
        }
    }
}
