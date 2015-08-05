using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using autoOffice.Common;

namespace autoOffice.Tests.CommonTests
{
    [TestClass]
    public class CommonTests
    {
        [TestMethod]
        public void UserCommonTest()
        {
            String pureName=CommonUser.pureName(@"rcoffice\bob.zhu");
            Assert.AreEqual(pureName,"bob.zhu");
        }
    }
}
