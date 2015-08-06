using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace autoOffice.Models
{
    public enum AccessType
    {
        Common = 1,
        UserAdmin = 2
    }

    [Table("rc_Accesses", Schema = "dbo")]
    public class Access
    {
        [Key]
        public Guid ID { get; set; }
        public String UserName { get; set; }
        public AccessType AccessType { get; set; }
    }
}