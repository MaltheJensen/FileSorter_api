using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nederman_api.Function;

namespace TestProject
{
    [TestClass]
    public class UnitTest
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
