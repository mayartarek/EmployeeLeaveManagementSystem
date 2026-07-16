
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeLeaveManagementSystem.Models;
using EmployeeLeaveManagementSystem.Data;

public class LeaveTypesController : Controller
{
    private readonly ApplicationDbContext _context;

    public LeaveTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: LEAVETYPESS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.LeaveTypes.ToListAsync());
    }

    // GET: LEAVETYPESS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetypes = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leavetypes == null)
        {
            return NotFound();
        }

        return View(leavetypes);
    }

    // GET: LEAVETYPESS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LEAVETYPESS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LeaveTypeName,MaximumDaysAllowed,LeaveRequests")] LeaveTypes leavetypes)
    {
        if (ModelState.IsValid)
        {
            _context.Add(leavetypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(leavetypes);
    }

    // GET: LEAVETYPESS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetypes = await _context.LeaveTypes.FindAsync(id);
        if (leavetypes == null)
        {
            return NotFound();
        }
        return View(leavetypes);
    }

    // POST: LEAVETYPESS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,LeaveTypeName,MaximumDaysAllowed,LeaveRequests")] LeaveTypes leavetypes)
    {
        if (id != leavetypes.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(leavetypes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypesExists(leavetypes.Id))
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
        return View(leavetypes);
    }

    // GET: LEAVETYPESS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetypes = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leavetypes == null)
        {
            return NotFound();
        }

        return View(leavetypes);
    }

    // POST: LEAVETYPESS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var leavetypes = await _context.LeaveTypes.FindAsync(id);
        if (leavetypes != null)
        {
            _context.LeaveTypes.Remove(leavetypes);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LeaveTypesExists(int? id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }
}
