using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace autoOffice.Models
{
    public enum LeaveApproveStatus{
        Approved=1,
        Reject=2,
        Pending=3
    }
    [Table("rc_Leaves", Schema = "dbo")]
    public class Leave
    {
        [Key]
        public Guid ID { get; set; }
        public float Hours { get; set; }
        public DateTime FromDate { get; set; }
        public int EmployId { get; set; }
        [MaxLength(250)]
        public String Comments { get; set; }
        [Column(TypeName="int")]
        public LeaveApproveStatus status { get; set; }
        [MaxLength(50)]
        public string Approver { get; set; }
    }
}