using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedDTO;
using Vacation.DAL.Data;
using Vacation.DAL.Model;
using static SharedDTO.EmployeeDTO;


namespace DataManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataManagementController : ControllerBase
    {
        private readonly MySQLDBContext _DBContext;

        public DataManagementController(MySQLDBContext dbContext)
        {
            _DBContext = dbContext;
        }


        [HttpPost("AddIrregularDay")]
        public async Task<ActionResult<List<IrregularDaysDTO>>> AddPlayer([FromBody] IrregularDaysDTO irregulardaydto)
        {
            var irregularday = irregulardaydto.Adapt<IrregularDay>();
            _DBContext.irregularDays.Add(irregularday);
            await _DBContext.SaveChangesAsync();

            //dto lista visszaadása
            var irregulardayx = await _DBContext.irregularDays
                    .ToListAsync();

            var irregulardayDtos = irregulardayx.Adapt<List<IrregularDaysDTO>>();
            return Ok(irregulardayDtos);
        }


        [HttpDelete]
        [Route("IrregularDayDelete/{date}")]
        public async Task<ActionResult<List<IrregularDaysDTO>>> DeleteIrregularDay(DateTime date)
        {
            var deleteIrregularDay = await _DBContext.irregularDays.FindAsync(date);

            if (deleteIrregularDay == null)
            {
                return NotFound("Nincs találat!");
            }
            _DBContext.irregularDays.Remove(deleteIrregularDay);
            await _DBContext.SaveChangesAsync();
            return Ok(await _DBContext.irregularDays.ToListAsync());
        }

        [HttpPost("AddVacationRequest")]
        public async Task<ActionResult<List<NewVacationRequestDTO>>> AddVacationRequest([FromBody] NewVacationRequestDTO vacationrequestdto)
        {
            var vacationrequest = vacationrequestdto.Adapt<VacationRequest>();
            _DBContext.vacationrequests.Add(vacationrequest);
            await _DBContext.SaveChangesAsync();

            //dto lista visszaadása
            var vacationrequestx = await _DBContext.vacationrequests
                    .ToListAsync();

            var vacationreuestDtos = vacationrequestx.Adapt<List<NewVacationRequestDTO>>();
            return Ok(vacationreuestDtos);
        }

        [HttpPatch]
        [Route("ApproveRejectRequest/{id}")]
        public async Task<ActionResult<List<ApproveRejectDTO>>> ApproveRejectRequest(int id, [FromBody] ApproveRejectDTO approverejectRequest)
        {
            var selectRequest = await _DBContext.vacationrequests.FindAsync(id);
            if (selectRequest == null)
            {
                return NotFound("Nincs találat!");
            }
            selectRequest.RequestId = approverejectRequest.RequestId;
            selectRequest.EmployeeId = approverejectRequest.employeeId;
            selectRequest.ToDate = approverejectRequest.ToDate;
            selectRequest.RequestDate = approverejectRequest.RequestDate;
            selectRequest.status = approverejectRequest.status;

            await _DBContext.SaveChangesAsync();
            var request = await _DBContext.vacationrequests.Include(e => e.Employee).ToListAsync();
            var requestDTO = request.Adapt<List<ApproveRejectDTO>>();
            return Ok(requestDTO);

        }
    }
}

