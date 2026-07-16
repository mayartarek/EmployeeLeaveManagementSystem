
using EmployeeLeaveManagementSystem.Data;
using EmployeeLeaveManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class LeaveRequestsController : Controller
{
    private readonly ApplicationDbContext _context;

    public LeaveRequestsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: LEAVEREQUESTS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.LeaveRequests.Include(a=>a.LeaveTypes).Include(a=>a.Employee).ToListAsync());
    }

    // GET: LEAVEREQUESTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaverequest = await _context.LeaveRequests
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leaverequest == null)
        {
            return NotFound();
        }

        return View(leaverequest);
    }

    // GET: LEAVEREQUESTS/Create
    public IActionResult Create()
    {
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "EmployeeName");
        ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "LeaveTypeName");

        return View();
    }
    // POST: LEAVEREQUESTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create([Bind("Id,EmployeeId,LeaveTypeId,StartDate,EndDate,Reason,Status")] LeaveRequest leaverequest)
    {
        leaverequest.Status = LeaveStatus.Pending;
        if (leaverequest.StartDate.Date < DateTime.Today)
        {
            ModelState.AddModelError("StartDate", "Start Date cannot be earlier than today.");
        }

        if (leaverequest.EndDate.Date < leaverequest.StartDate.Date)
        {
            ModelState.AddModelError("EndDate", "End Date must be after or equal to Start Date.");
        }

        int leaveDays = (leaverequest.EndDate - leaverequest.StartDate).Days + 1;

        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(x => x.Id == leaverequest.LeaveTypeId);

        if (leaveType != null && leaveDays > leaveType.MaximumDaysAllowed)
        {
            ModelState.AddModelError("", $"Maximum allowed days is {leaveType.MaximumDaysAllowed}");
        }

        bool overlap = await _context.LeaveRequests.AnyAsync(x =>
            x.EmployeeId == leaverequest.EmployeeId &&
            x.Status != LeaveStatus.Rejected &&
            leaverequest.StartDate <= x.EndDate &&
            leaverequest.EndDate >= x.StartDate);
        leaverequest.LeaveTypes = leaveType;
        if (overlap)
        {
            ModelState.AddModelError("", "This employee already has a leave request during this period.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(leaverequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "EmployeeName", leaverequest.EmployeeId);
        ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "LeaveTypeName", leaverequest.LeaveTypeId);

        return View(leaverequest);
    }

    // GET: LEAVEREQUESTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaverequest = await _context.LeaveRequests.FindAsync(id);
        if (leaverequest == null)
        {
            return NotFound();
        }
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "EmployeeName");
        ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "LeaveTypeName");

        return View(leaverequest);
    }

    // POST: LEAVEREQUESTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,EmployeeId,Employee,LeaveTypeId,LeaveTypes,StartDate,EndDate,Reason,Status")] LeaveRequest leaverequest)
    {
        if (id != leaverequest.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(leaverequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRequestExists(leaverequest.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(leaverequest);
    }

    // GET: LEAVEREQUESTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaverequest = await _context.LeaveRequests
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leaverequest == null)
        {
            return NotFound();
        }

        return View(leaverequest);
    }
    /// <summary>
    /// Approves a leave request with the specified ID by updating its status to "Approved" in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Approve(int id)
    {
        var leaveRequest = await _context.LeaveRequests.FindAsync(id);
        if (leaveRequest == null)
        {
            return NotFound();
        }
        leaveRequest.Status = LeaveStatus.Approved;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    /// <summary>
    /// Rejects a leave request with the specified ID by updating its status to "Rejected" in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Reject(int id)
    {
        var leaveRequest = await _context.LeaveRequests.FindAsync(id);
        if (leaveRequest == null)
        {
            return NotFound();
        }
        leaveRequest.Status = LeaveStatus.Rejected;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    // POST: LEAVEREQUESTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var leaverequest = await _context.LeaveRequests.FindAsync(id);
        if (leaverequest != null)
        {
            _context.LeaveRequests.Remove(leaverequest);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LeaveRequestExists(int? id)
    {
        return _context.LeaveRequests.Any(e => e.Id == id);
    }
}
