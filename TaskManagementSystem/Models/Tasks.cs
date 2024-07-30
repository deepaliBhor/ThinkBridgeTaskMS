using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TaskManagementSystem.Models
{
    public partial class Tasks
    {
        public Tasks()
        {
            Document = new HashSet<Document>();
            Note = new HashSet<Note>();
        }

        public int NId { get; set; }
        public string STitle { get; set; }
        public string SDescription { get; set; }
        public DateTime DtDueDate { get; set; }
        public string SStatus { get; set; }
        public int NEmployeeId { get; set; }
        public DateTime DtCreatedAt { get; set; }
        public DateTime? DtUpdatedAt { get; set; }

        public virtual Employee NEmployee { get; set; }
        public virtual ICollection<Document> Document { get; set; }
        public virtual ICollection<Note> Note { get; set; }
    }
}
