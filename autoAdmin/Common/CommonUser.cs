using autoOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace autoOffice.Common
{
    public static class CommonUser
    {
        public static string pureName(string longName)
        {
            int index = longName.IndexOf("\\");
            return longName.Substring(index+1);
        }
        public static string pureMailName(string mailName) {
            return mailName.Split('@')[0];
        }
        public static Employee findEmployeeByMail(String mail, DbHelper helper)
        {
            var query = from e in helper.Employees
                        where e.MailAddress == mail
                        select e;
            var em = query.FirstOrDefault();
            return em;
        }
        public static Employee findEmployeeByName(String name, DbHelper helper)
        {
            var query = from e in helper.Employees
                        where e.Name == name
                        select e;
            var em = query.FirstOrDefault();
            return em;
        }
        public static Employee findEmployeeById(Guid id, DbHelper helper)
        {
            var query = from e in helper.Employees
                        where e.ID == id
                        select e;
            var em = query.FirstOrDefault();
            return em;
        }
    }
}