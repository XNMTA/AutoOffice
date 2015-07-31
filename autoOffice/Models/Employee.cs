using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace autoOffice.Models
{

    [Table("rc_Employees", Schema = "dbo")]
    public class Employee
    {
        [Key]
        public Guid ID { get; set; }
        public String Name { get; set; }
        public String MailAddress { get; set; }
        public String Department { get; set; }
        public Guid ReportTo { get; set; }
    }
}