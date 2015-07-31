using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using autoOffice.Models;

namespace autoOffice.Tests.ConnectionTests
{
    [TestClass]
    public class MsSqlConnectionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            DbHelper helper = new DbHelper();
            Leave l = new Leave();
            l.ID = Guid.NewGuid();
            l.EmployId = 123;
            l.status = LeaveApproveStatus.Pending;
            l.Approver = "Some";
            helper.Leaves.Add(l);
            var a=helper.Leaves;
        }
    }
}
