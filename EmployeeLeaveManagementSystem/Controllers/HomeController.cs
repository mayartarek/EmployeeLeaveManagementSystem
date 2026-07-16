using EmployeeLeaveManagementSystem.Data;
using EmployeeLeaveManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeLeaveManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context_)
        {
            _logger = logger;
            _context = context_;
        }

        public IActionResult Index()
        {
            var dashboardModel = new DashboardViewModel
            {
                TotalNoOfEmployees = _context.Employees.Count(),
                TotalNoOfLeaveRequest = _context.LeaveRequests.Count(),
                TotalPendingLeaveRequest = _context.LeaveRequests.Count(x => x.Status == LeaveStatus.Pending),
                TotalApprovedLeaveRequest = _context.LeaveRequests.Count(x => x.Status == LeaveStatus.Approved),
                TotalRejectedLeaveRequest =     _context.LeaveRequests.Count(x => x.Status == LeaveStatus.Rejected)
            };

            return View(dashboardModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
