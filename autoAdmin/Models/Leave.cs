using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace autoOffice.Models
{
    public enum LeaveApproveStatus{
        Approved=1,
        Reject=2,
        Pending=0
    }
    [Table("rc_Leaves", Schema = "dbo")]
    public class Leave
    {
        [Key]
        public Guid ID { get; set; }
        public double Hours { get; set; }
        public DateTime FromDate { get; set; }
        public Guid EmployId { get; set; }
        [MaxLength(50)]
        public String EmployName { get; set; }
        [MaxLength(500)]
        public String Comments { get; set; }
        [Column(TypeName="int")]
        public LeaveApproveStatus status { get; set; }
        [MaxLength(50)]
        public string Approver { get; set; }
        [MaxLength(500)]
        public string ApproveComment { get; set; }
    }
}