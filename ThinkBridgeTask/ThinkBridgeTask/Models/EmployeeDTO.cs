using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ThinkBridgeTask.Models
{
    public partial class EmployeeDTO
    {
        public EmployeeDTO()
        {
            InverseNManager = new HashSet<EmployeeDTO>();
            Tasks = new HashSet<Tasks>();
        }

        public int NId { get; set; }
        public string SName { get; set; }
        public string SEmail { get; set; }
        public int? NManagerId { get; set; }
        //public string? SManagerName { get; set; }
        public virtual EmployeeDTO NManager { get; set; }
        public virtual ICollection<EmployeeDTO> InverseNManager { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
