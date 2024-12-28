using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.IO;
using SharedDTO;
using System.Numerics;
using Vacation.DAL.Data;
using Vacation.DAL.Model;

namespace Vacation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MySQLDBContext _DBContext;

        public AccountController(MySQLDBContext dbContext)
        {
            _DBContext = dbContext;
        }

        /// GET - adatok lekérése - GET apik

        //Lehívások
        [HttpGet("EmployeeGet")]  //felhasználó lekérdezése
        public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployee()
        {
            var employee = await _DBContext.employees.ToListAsync();

            return Ok(employee);
        }

        [HttpGet("DepartmentGet")]  //részleg lekérdezése
        public async Task<ActionResult<List<Department>>> GetAllDepartment()
        {
            var department = await _DBContext.departments.ToListAsync();

            return Ok(department);
        }

        [HttpGet("ChildGet")]  //gyerek lekérdezése
        public async Task<ActionResult<List<ChildDTO>>> GetAllChildren()
        {
            var children = await _DBContext.children
                .Select(e => new ChildDTO
                {
                    ChildId =e.ChildId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateOfBirth = e.DateOfBirth,
                    Disability = e.Disability,

                }).ToListAsync();

            return Ok(children);
        }

        [HttpGet("ChildEmployeeGet")]
        public async Task<ActionResult<List<ChildEmployeeDTO>>> GetChildEmployee()
        {
            var childemployee = await _DBContext.employees
                .Include(c => c.Children)
                .Select(e => new ChildEmployeeDTO

                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateOfBirth = e.DateOfBirth,
                    ChildrenId = e.Children.Select(c=>c.ChildId).ToList(),
                    ChildrenFirstName = e.Children.Select(c => c.FirstName).ToList(),
                    ChildrenLastName = e.Children.Select(c => c.LastName).ToList(),
                })
                .ToListAsync();

            return Ok(childemployee);
        }



        [HttpGet("SearchE/{EmployeeName}")] //szülő keresése név alapján
        public async Task<ActionResult<List<EmployeeDTO>>> SearchEmployee(string EmployeeName)
        {
            var employees = await _DBContext.employees
                .Include(d => d.Department)
                .Include(c=> c.Children)
                .Where(e =>
                    e.FirstName.Contains(EmployeeName) ||
                    e.LastName.Contains(EmployeeName))

                .Select(e => new EmployeeDTO
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateOfBirth = e.DateOfBirth,

                    DepartmentDescription = e.Department.DepartmentDescription,
                   
                })

                  .ToListAsync();
            if (!employees.Any()) { return NotFound(); }
            return Ok(employees);
        }

        [HttpGet("SearchEmployee/{Id}")]  //szülő keresése id alapján
        public async Task<ActionResult<UpdateEmployeeDTO>> GetEmployeeBYId(int Id)
        {
            var employee = await _DBContext.employees
                .Include(t => t.Department)
                .Where(t => t.EmployeeId == Id)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }
            var employeeDTO = employee.Adapt<UpdateEmployeeDTO>();
            return Ok(employeeDTO);
        }

        [HttpGet("SearchChild/{Id}")]  //gyerek keresése id alapján
        public async Task<ActionResult<UpdateChildDTO>> GetChildBYId(int Id)
        {
            var child = await _DBContext.children
                .Where(t => t.ChildId == Id)
                .FirstOrDefaultAsync();

            if (child == null)
            {
                return NotFound();
            }
            var childDTO = child.Adapt<UpdateChildDTO>();
            return Ok(childDTO);
        }
      

        [HttpGet("SearchDepartment/{Id}")]  //részleg keresése id alapján   
        public async Task<ActionResult<UpdateDepartmentDTO>> GetDepartmentBYId(int Id)
        {
            var department = await _DBContext.departments
                .Where(t => t.DepartmentID == Id)
                .FirstOrDefaultAsync();

            if (department == null)
            {
                return NotFound();
            }
            var departmentDTO = department.Adapt<UpdateDepartmentDTO>();
            return Ok(departmentDTO);
        }

        [HttpGet("SearchChildEmployee/{Id}")]  //gyerek-szülő keresése id alapján
        public async Task<ActionResult<EmployeeDTO>> GetChildEmployeeBYId(int Id)
        {
            var employee = await _DBContext.employees
                .Include(c=>c.Children)
                .Where(e=>e.EmployeeId== Id)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }
            var employeeDTO = employee.Adapt<EmployeeDTO>();
            return Ok(employeeDTO);
        }


        /// POST - adatok hozzáadása  - POST apik
        [HttpPost("AddEmployee")]  //felhasználó felvétele 
        public async Task<ActionResult<List<EmployeeDTO>>> AddEmployee([FromBody] EmployeeDTO employeedto)
        {
            var employee = employeedto.Adapt<Employee>();
            _DBContext.employees.Add(employee);
            await _DBContext.SaveChangesAsync();

            var employees = await _DBContext.employees
                .Include(d => d.Department)
                .ToListAsync();

            var employeeDtos = employees.Adapt<List<EmployeeDTO>>();
            return Ok(employeeDtos);
        }

        [HttpPost("AddDepartment")]  //részleg felvétele
        public async Task<ActionResult<List<DepartmentDTO>>> AddDepartment([FromBody] DepartmentDTO departmentdto)
        {
            var department = departmentdto.Adapt<Department>();
            _DBContext.departments.Add(department);
            await _DBContext.SaveChangesAsync();

            var departments = await _DBContext.departments
                .Include(e => e.Employees)
                .ToListAsync();

            var departmentDtos = department.Adapt<List<DepartmentDTO>>();
            return Ok(departmentDtos);
        }

        [HttpPost("AddChild")]  //gyermek felvétele
        public async Task<ActionResult<List<ChildDTO>>> AddChild([FromBody] ChildDTO childdto)
        {
            var child = childdto.Adapt<Child>();
            _DBContext.children.Add(child);
            await _DBContext.SaveChangesAsync();

            var children = await _DBContext.children
                 .Select(e => new ChildDTO
                 {
                     ChildId = e.ChildId,
                     FirstName = e.FirstName,
                     LastName = e.LastName,
                     DateOfBirth = e.DateOfBirth,
                     Disability = e.Disability,

                 }).ToListAsync();

            var childrenDtos = children.Adapt<List<ChildDTO>>();
            return Ok(childrenDtos);
        }

        //[HttpPost("AddChildEmployee")]  //gyerek-szülő kapcsolat hozzáadása
        //public async Task<ActionResult<List<ChildEmployeeDTO>>> AddChildEmployee([FromBody] ChildEmployeeDTO childemployeedto)
        //{
        //    var child = await _DBContext.children
        //         .Select(e => new ChildEmployeeDTO
        //         {

        //             ChildrenId = e.ChildId,
        //             EmployeeId = e.Employees.First().EmployeeId
        //         })
        //        .ToListAsync();
        //    //var employee = await _DBContext.employees.ToListAsync();

        //    if (child == null)
        //    {
        //        return NotFound();
        //    }
        //    //var childrenemployeeDtos = childemployeedto.Adapt<List<ChildDTO>>();
        //    _DBContext.childemployee.Add(childemployeedto);
        //    await _DBContext.SaveChangesAsync();
        //    return Ok(childemployeedto);

        //    //    /////////////////////////////////////////////////////
        //    //    ///
        //    //    var childemployee = childemployeedto.Adapt<ChildEmployee>();
        //    //    _DBContext.childemployee.Add(childemployee);
        //    //    await _DBContext.SaveChangesAsync();

        //    //    var childempolyees = await _DBContext.childemployee
        //    //           .ToListAsync();

        //    //    var childrenDtos = childemployee.Adapt<List<ChildEmployeeDTO>>();
        //    //    return Ok(childrenDtos);

        //}


            /// PATCH - adatok módosítása  - PATCH apik
            // Gyermek frissítése 
            [HttpPatch("PatchChild/{Id}")]
        public async Task<ActionResult<List<ChildDTO>>> UpdateChild(int Id, [FromBody] ChildDTO updatechilddto)
        {
            var selectChild = await _DBContext.children.FindAsync(Id);
            if (selectChild == null)
            {
                return NotFound("A gyermek nem található");
            }
            selectChild.FirstName = updatechilddto.FirstName;
            selectChild.LastName = updatechilddto.LastName;
            selectChild.DateOfBirth = updatechilddto.DateOfBirth;
            selectChild.Disability = updatechilddto.Disability;
            await _DBContext.SaveChangesAsync();

            var children = await _DBContext.children.Include(e => e.Employees).ToListAsync();
            var childDTO = children.Adapt<List<ChildDTO>>();
            return Ok(childDTO);
        }

        //Szülők frissítése
        [HttpPatch("PatchEmployee/{Id}")] 
        public async Task<ActionResult<List<EmployeeDTO>>> UpdateEmployee(int Id, [FromBody] EmployeeDTO updateemployeedto)
        {
            var selectEmployee = await _DBContext.employees.FindAsync(Id);
            if (selectEmployee == null)
            {
                return NotFound("A munkavállaló nem található")
;
            }

            selectEmployee.FirstName = updateemployeedto.FirstName;
            selectEmployee.LastName = updateemployeedto.LastName;
            selectEmployee.NameOfMother = updateemployeedto.NameOfMother;
            selectEmployee.DateOfBirth = updateemployeedto.DateOfBirth;
            selectEmployee.InsuranceNumber = updateemployeedto.InsuranceNumber;
            selectEmployee.StartOfEmployment = updateemployeedto.StartOfEmployment;
            selectEmployee.Role = updateemployeedto.Role;
            selectEmployee.Disability = updateemployeedto.Disability;
            selectEmployee.DepartmentID = updateemployeedto.DepartmentID;

            await _DBContext.SaveChangesAsync();

            var employees = await _DBContext.employees.Include(t => t.Department).ToListAsync();
            var employeeDTO = employees.Adapt<List<EmployeeDTO>>();

            return Ok(employeeDTO);
        }

        //részleg nevének módosítása
        [HttpPatch("PatchDepartment/{Id}")]
        public async Task<ActionResult<List<UpdateDepartmentDTO>>> UpdateDepartment(int Id, [FromBody] DepartmentDTO updatedepartmentdto)
        {
            var selectDepartment = await _DBContext.departments.FindAsync(Id);
            if (selectDepartment == null)
            {
                return NotFound("A részleg nem található");
            }
            selectDepartment.DepartmentDescription = updatedepartmentdto.DepartmentDescription;
            await _DBContext.SaveChangesAsync();

            var departments = await _DBContext.departments.ToListAsync();
            var DepartmentDTO = departments.Adapt<List<UpdateDepartmentDTO>>();
            return Ok(DepartmentDTO);
        }
    }


    




}

