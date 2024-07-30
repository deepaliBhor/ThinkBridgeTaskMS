using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskManagementContext _context;

        public TasksController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            string apiUrl = "http://localhost:5000/api/tasks";
            List<Tasks> tasksList = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    tasksList = JsonConvert.DeserializeObject<List<Tasks>>(data);
                }


            }
            return View(tasksList);

        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.NEmployee)
                .FirstOrDefaultAsync(m => m.NId == id);
            if (tasks == null)
            {
                return NotFound();
            }
            return View(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["NEmployeeId"] = new SelectList(_context.Employee, "NId", "SEmail");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NId,STitle,SDescription,DtDueDate,SStatus,NEmployeeId,DtCreatedAt,DtUpdatedAt")] Tasks tasks)
        {
            string apiUrl = "http://localhost:5000/api/tasks";
            Tasks createdTask = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(tasks);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    createdTask = JsonConvert.DeserializeObject<Tasks>(responseData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["NEmployeeId"] = new SelectList(_context.Employee, "NId", "SEmail", tasks.NEmployeeId);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NId,STitle,SDescription,DtDueDate,SStatus,NEmployeeId,DtCreatedAt,DtUpdatedAt")] Tasks tasks)
        {
            string apiUrl = $"http://localhost:5000/api/tasks/{id}";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(tasks);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var task = JsonConvert.DeserializeObject<Tasks>(responseData);
                    return RedirectToAction("Index"); 
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.NEmployee)
                .FirstOrDefaultAsync(m => m.NId == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string apiUrl = $"http://localhost:5000/api/tasks/{id}";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // or any other action you want to redirect to
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.NId == id);
        }

        public async Task<IActionResult> dueinweek(int id)
        {
            string apiUrl = $"http://localhost:5000/api/tasks/dueinweek/{id}";
            List<Tasks> tasksList = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    tasksList = JsonConvert.DeserializeObject<List<Tasks>>(data);
                }
            }
            return View(tasksList);

        }

        public async Task<IActionResult> dueinmonth(int id)
        {
            string apiUrl = $"http://localhost:5000/api/tasks/dueinmonth/{id}";
            List<Tasks> tasksList = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    tasksList = JsonConvert.DeserializeObject<List<Tasks>>(data);
                }
            }
            return View("dueinweek",tasksList);

        }
        public async Task<IActionResult> ViewTaskHierarchy(int employeeId)
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
            return View(hierarchy);
        }

        private EmployeeTaskHierarchyViewModel BuildHierarchy(Employee employee)
        {
            return new EmployeeTaskHierarchyViewModel
            {
                Employee = employee,
                Tasks = employee.Tasks,
                Subordinates = employee.InverseNManager.Select(e => BuildHierarchy(e)).ToList()
            };
        }
    }
}
