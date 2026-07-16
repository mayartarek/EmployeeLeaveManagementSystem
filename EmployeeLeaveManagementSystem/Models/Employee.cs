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
        [Display(Name = "Employee Name")]

        public string EmployeeName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Employee Email")]
        public string EmployeeEmail { get; set; } 
        [Required]
        [Display(Name = "Employee Phone")]
        [Phone]
        public string EmployeePhone { get; set; }
        [Required]
        [Display(Name = "Employee Code")]

        public string EmployeeCode { get; set; }
        [Required]
        [Display(Name = "Employee Department")]
        public string EmployeeDepartment  { get; set; }




    }
}
