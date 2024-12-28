using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedDTO;
using Vacation.DAL.Data;


namespace Vacation.Controllers
{
    [ApiController]
    [Route("Login")]
    public class LoginController : ControllerBase
    {
        private readonly MySQLDBContext _dbContext;

        public LoginController(MySQLDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO loginRequest)
        //public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginDTO loginRequest)
        {
            var employee = _dbContext.employees
                .FirstOrDefault(e => e.Email == loginRequest.Email);

            if (employee == null || employee.Password != loginRequest.Password)
            {
                return Unauthorized("Helytelen email vagy jelszó.");
            }

            return Ok(new
            {
                EmployeeId = employee.EmployeeId,
                Email = employee.Email,
                Password = employee.Password,
            });
        }




        [HttpGet("Employee/{id}")] //konkrét felhasználó adatainak lekérése
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await _dbContext.employees
                .Where(t => t.EmployeeId == id)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound(new { Message = "Nincs ilyen felhasználó" });
            }
            var employeeDto = new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                NameOfMother = employee.NameOfMother,
                Disability = employee.Disability,
                InsuranceNumber = employee.InsuranceNumber,
                StartOfEmployment = employee.StartOfEmployment,
            };

            return Ok(employeeDto);
        }

    }
}