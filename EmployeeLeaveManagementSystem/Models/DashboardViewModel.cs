namespace EmployeeLeaveManagementSystem.Models
{
    public class DashboardViewModel
    {
        public int TotalNoOfEmployees { get; set; }  
        public int TotalNoOfLeaveRequest{ get; set; }
        public int TotalPendingLeaveRequest { get; set; }   
        public int TotalApprovedLeaveRequest { get; set; }
        public int TotalRejectedLeaveRequest { get; set; }

    }
}
