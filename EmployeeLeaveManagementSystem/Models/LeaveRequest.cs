using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveManagementSystem.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; } 
        public Employee? Employee { get; set; } 
        [Required]
        public int LeaveTypeId { get; set; }
         public LeaveTypes? LeaveTypes { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Reason { get; set; }

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    }
}
