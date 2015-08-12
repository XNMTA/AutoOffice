using autoOffice.Common;
using autoOffice.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoOffice.Tests.DataInsertTools
{
    [TestClass]
    public class InsertEmployeeFromHRSheet
    {
        string sqlmetaAddress = @"E:\Code\autoOffice\autoOffice.Tests\DataInsertTools\employee-meta.csv";
        DbHelper helper = new DbHelper();
        [TestMethod]
        public void InserDataFromHRSheetTest()
        { 
            FileInfo fi = new FileInfo(sqlmetaAddress);
            FileStream fs = new FileStream(sqlmetaAddress, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            String strLine;
            while ((strLine = sr.ReadLine()) != null) {
                var meta=strLine.Split(',');
                String employeeMail = meta[0];
                String leaderMail = meta[2];
                String department = meta[1];
                String startDate = meta[3];

                if (findEmployeeByMail(employeeMail) != null)
                {
                    continue;
                }
                Employee emp = helper.Employees.Create();
                emp.Name = employeeMail.Split('@')[0];
                emp.ID = Guid.NewGuid();
                emp.Department = department;
                emp.MailAddress = employeeMail;
                emp.StartDate = DateTime.Parse(startDate);

                emp.LeaveReportTo = leaderMail.Split('@')[0];
                helper.Employees.Add(emp);
                helper.SaveChanges();

            }
        }
        private Employee findEmployeeByMail(String employeeMail)
        {
            var query = from e in helper.Employees
                        where e.MailAddress == employeeMail
                        select e;
            var reportTo = query.FirstOrDefault();
            return reportTo;
        }
    }
}
