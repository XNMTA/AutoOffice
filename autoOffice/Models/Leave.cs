using System;
using System.Data.Entity;

namespace autoOffice.Models
{
    public enum LeaveApproveStatus{
        Approved,
        Reject,
        Pending
    }
    public class Leave
    {
        public Guid ID { get; set; }
        public float Hours { get; set; }
        public DateTime FromDate { get; set; }
        public Guid EmployId { get; set; }
        public String Comments { get; set; }
        public LeaveApproveStatus status { get; set; }
        public string Approver { get; set; }
    }

    public class LeaveDBContext : DbContext
    {
        public DbSet<Leave> Leaves { get; set; }

    }
}