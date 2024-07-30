using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TaskManagementSystem.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseNManager = new HashSet<Employee>();
            Tasks = new HashSet<Tasks>();
        }

        public int NId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string SName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string SEmail { get; set; }
        public int? NManagerId { get; set; }

        public virtual Employee NManager { get; set; }
        public virtual ICollection<Employee> InverseNManager { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
