using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementSystem.Data
{
    public class ApplicationDbContext:DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
       {
       }
        public DbSet<EmployeeLeaveManagementSystem.Models.Employee> Employees { get; set; }
        public DbSet<EmployeeLeaveManagementSystem.Models.LeaveTypes> LeaveTypes { get; set; }
        public DbSet<EmployeeLeaveManagementSystem.Models.LeaveRequest> LeaveRequests { get; set; }
    }
}
