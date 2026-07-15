using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveManagementSystem.Models
{
    public class LeaveTypes
    {
        public int Id { get; set; }

        [Required]
        public string LeaveTypeName { get; set; }

        [Range(1, 365)]
        public int MaximumDaysAllowed { get; set; }

        public ICollection<LeaveRequest>? LeaveRequests { get; set; }


    }

}
