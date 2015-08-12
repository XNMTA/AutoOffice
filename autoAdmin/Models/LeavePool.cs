using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace autoOffice.Models
{
    [Table("rc_LeavePools", Schema = "dbo")]
    public class LeavePool
    {
        [Key]
        public Guid ID { get; set; }
        public double Hours { get; set; }
        [MaxLength(50)]
        public String EmployName { get; set; }
        public DateTime EntryDate { get; set; }
    }
}