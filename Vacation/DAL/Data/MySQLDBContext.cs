using Microsoft.EntityFrameworkCore;
using SharedDTO;
using Vacation.DAL.Model;

namespace Vacation.DAL.Data
{
    public class MySQLDBContext : DbContext
    {
        public MySQLDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Approver> approvers { get; set; }
        public DbSet<Child> children { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<VacationRequest> vacationrequests { get; set; }
        public DbSet<VacationStatus> vacationstatuses { get; set; }
        public DbSet<VacationType> vacationtypes { get; set; }
        public DbSet<IrregularDay> irregularDays { get; set; }

       // public DbSet<ChildEmployee> childemployee { get; set; }   
    }
}

