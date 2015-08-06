using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using autoOffice.Models;
using System.Linq;
using System.IO;
using System.Text;

namespace autoOffice.Tests.DataInsertTools
{
    [TestClass]
    public class InsertDataFromTxt
    {
        String sqlmetaAddress = @"E:\Code\autoOffice\autoOffice\bin\SqlMeta\employee-meta.csv";
        DbHelper helper = new DbHelper();
        [TestMethod]
        public void InserDataFromTxtTest()
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
                if (findEmployeeByMail(employeeMail) != null) {
                    continue;
                }
                Employee emp = helper.Employees.Create();
                emp.Name = employeeMail.Split('@')[0];
                emp.ID = Guid.NewGuid();
                emp.Department = department;
                emp.MailAddress = employeeMail;
                var reportTo = findEmployeeByMail(leaderMail);
                emp.LeaveReportTo = reportTo.ID;
                helper.Employees.Add(emp);
                helper.SaveChanges();
                
            }
            //Employee emp = new Employee();
            //emp.Name = "lee.may";
            //emp.ID = Guid.NewGuid();
            //emp.Department = "TA";
            //var query = from e in helper.Employees
            //    where e.Name == "bob.zhu"
            //    select e;
            //var reportTo = query.FirstOrDefault();
            //emp.LeaveReportTo = reportTo.ID;
            //emp.MailAddress = emp.Name + "@ringcentral.com";
            //helper.Employees.Add(emp);
            //helper.SaveChanges();
            
        }

        private Employee findEmployeeByMail(String leaderMail)
        {
            var query = from e in helper.Employees
                        where e.MailAddress == leaderMail
                        select e;
            var reportTo = query.FirstOrDefault();
            return reportTo;
        }
    }
}
