using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TaskManagementSystem.Models
{
    public partial class Document
    {
        public int NId { get; set; }
        public string SFilePath { get; set; }
        public int NTaskId { get; set; }
        public DateTime DtCreatedAt { get; set; }
        public DateTime? DtUpdatedAt { get; set; }

        public virtual Tasks NTask { get; set; }
    }
}
