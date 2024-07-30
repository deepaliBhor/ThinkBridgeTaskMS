using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThinkBridgeTask.Models;

namespace ThinkBridgeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskManagementContext _context;

        public TasksController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);

            if (tasks == null)
            {
                return NotFound();
            }

            return tasks;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks(int id, Tasks tasks)
        {
            if (id != tasks.NId)
            {
                return BadRequest();
            }

            _context.Entry(tasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks(Tasks tasks)
        {
            _context.Tasks.Add(tasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasks", new { id = tasks.NId }, tasks);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tasks>> DeleteTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();

            return tasks;
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.NId == id);
        }

        [HttpGet("dueinweek/{id}")]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasksDueInWeek(int id)
        {
            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(7);

            var tasksListDueInWeek = await _context.Tasks
                .Where(t => t.NEmployeeId == id && t.DtDueDate >= startDate && t.DtDueDate <= endDate)
                .ToListAsync();

            if (tasksListDueInWeek == null || !tasksListDueInWeek.Any())
            {
                return NotFound();
            }

            return Ok(tasksListDueInWeek);
        }

        [HttpGet("dueinmonth/{id}")]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasksDueInMonth(int id)
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var tasksListDueInWeek = await _context.Tasks
                .Where(t => t.NEmployeeId == id && t.DtDueDate >= startDate && t.DtDueDate <= endDate)
                .ToListAsync();

            if (tasksListDueInWeek == null || !tasksListDueInWeek.Any())
            {
                return NotFound();
            }

            return Ok(tasksListDueInWeek);
        }


        // GET: api/tasks/{employeeId}
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeTaskHierarchyViewModel>> GetTaskHierarchy(int employeeId)
        {
            var employee = await _context.Employee
                .Include(e => e.Tasks)
                .Include(e => e.InverseNManager)
                .ThenInclude(m => m.Tasks)
                .FirstOrDefaultAsync(m => m.NId == employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            var hierarchy = BuildHierarchy(employee);
            return Ok(hierarchy);
        }

        private EmployeeTaskHierarchyViewModel BuildHierarchy(EmployeeDTO employee)
        {
            //return new EmployeeTaskHierarchyViewModel
            //{
            //    Employee = new EmployeeDTO
            //    {
            //        NId = employee.NId,
            //        SName = employee.SName,
            //        SEmail = employee.SEmail,
            //        NManagerId = employee.NManagerId
            //    },
            //    Tasks = employee.Tasks?.ToList() ?? new List<Tasks>(),
            //    Subordinates = employee.InverseNManager?.Select(e => BuildHierarchy(e)).ToList() ?? new List<EmployeeTaskHierarchyViewModel>()
            //};
            return new EmployeeTaskHierarchyViewModel
            {
                Employee = employee,
                Tasks = employee.Tasks,
                Subordinates = employee.InverseNManager.Select(e => BuildHierarchy(e)).ToList()
            };
        }
    }
}

