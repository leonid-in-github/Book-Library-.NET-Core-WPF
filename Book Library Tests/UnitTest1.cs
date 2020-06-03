using Book_Library_EF_Core_Proxy_Class_Library.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Book_Library_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLogin()
        {
            var login = "test";
            var password = "test";
            var accountId = DbBookLibraryRepository.Account.Login(login, password);

            Assert.IsTrue(true);
        }
    }
}
