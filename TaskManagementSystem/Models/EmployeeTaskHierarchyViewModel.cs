using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class EmployeeTaskHierarchyViewModel
    {
        public Employee Employee { get; set; }
        public IEnumerable<EmployeeTaskHierarchyViewModel> Subordinates { get; set; }
        public IEnumerable<Tasks> Tasks { get; set; }
    }
}
