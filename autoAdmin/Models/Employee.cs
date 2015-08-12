using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace autoOffice.Models
{

    [Table("rc_Employees", Schema = "dbo")]
    [Bind(Exclude = "LeaveReportName")]
    public class Employee
    {
        [Key]
        public Guid ID { get; set; }
        public String Name { get; set; }
        [DisplayName("Mail")]
        public String MailAddress { get; set; }
        [DisplayName("Dep")]
        public String Department { get; set; }
        [DisplayName("Report-to")]
        public String LeaveReportTo { get; set; }
        public DateTime StartDate { get; set; }
    }
}