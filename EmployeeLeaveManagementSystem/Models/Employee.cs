using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveManagementSystem.Models
{
    /// <summary>
    /// Represents an employee in the Employee Leave Management System.
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        [Required]

        public string EmployeeName { get; set; }
        [Required]

        public string EmployeeEmail { get; set; } 
        [Required]
        public string EmployeePhone { get; set; }
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public string EmployeeDepartment  { get; set; }




    }
}
