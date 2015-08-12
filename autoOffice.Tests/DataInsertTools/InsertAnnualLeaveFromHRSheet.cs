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
    public class InsertAnnualLeaveFromHRSheet
    {
        string sqlmetaAddress = @"E:\Code\autoOffice\autoOffice.Tests\DataInsertTools\annual-leave-meta.csv";
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
                String annualLeave = meta[1];

                if (findEmployeeByMail(employeeMail) == null)
                {
                    Console.WriteLine(String.Format("employ : {0} is not exist",employeeMail));
                    continue;
                }
                if (findAnnualLeavePoolByName(employeeMail.Split('@')[0]) != null)
                {
                    continue;
                }
                LeavePool leavePool = helper.LeavePools.Create();
                leavePool.EmployeeName = employeeMail.Split('@')[0];
                leavePool.ID = Guid.NewGuid();
                leavePool.EntryDate = DateTime.Now.Date;
                try{
                    leavePool.Hours = Convert.ToDouble(annualLeave);
                }
                catch (Exception e) {
                    leavePool.Hours = 0;
                }
                helper.LeavePools.Add(leavePool);
                helper.SaveChanges();
            }
        }
        private LeavePool findAnnualLeavePoolByName(String employeeName)
        {
            var query = from e in helper.LeavePools
                        where e.EmployeeName == employeeName
                        select e;
            var reportTo = query.FirstOrDefault();
            return reportTo;
        }
        private Employee findEmployeeByMail(String employeeMail)
        {
            var query = from e in helper.Employees
                        where e.MailAddress == employeeMail
                        select e;
            var emp = query.FirstOrDefault();
            return emp;
        }
    }
}
