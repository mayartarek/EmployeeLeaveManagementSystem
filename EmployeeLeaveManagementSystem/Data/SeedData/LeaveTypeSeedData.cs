using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementSystem.Data.SeedData
{
    public static class LeaveTypeSeedData
    {
        public static void SeedLeaveTypes(ApplicationDbContext context)
        {
            if (context.LeaveTypes.Any())
                return;
            context.LeaveTypes.AddRange(

          
                new EmployeeLeaveManagementSystem.Models.LeaveTypes {  LeaveTypeName = "Sick Leave", MaximumDaysAllowed = 10 },
                new EmployeeLeaveManagementSystem.Models.LeaveTypes {  LeaveTypeName = "Casual Leave", MaximumDaysAllowed = 15 },
                new EmployeeLeaveManagementSystem.Models.LeaveTypes { LeaveTypeName = "Maternity Leave", MaximumDaysAllowed = 90 },
                new EmployeeLeaveManagementSystem.Models.LeaveTypes {  LeaveTypeName = "Paternity Leave", MaximumDaysAllowed = 15 }
          );
            context.SaveChanges();
        }
    }
}
