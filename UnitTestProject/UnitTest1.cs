using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nederman_api.Function;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_GetCompanyByName_GetRightCompany()
        {
            GetCompanyByName getCompanyByName = new GetCompanyByName();

            var company = getCompanyByName.GetCompany("Firma1");
            var address = "Gaden 1";

            Assert.AreEqual(address, company.Address);
        }
    }
}
