using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyManagerToUniversity.service;

namespace ManageMoneyToUniversityUnitTests
{
    [TestClass]
    public class AccountNumberValidatorUnitTest
    {
        private AccountNumberValidator validator;

        [TestInitialize]
        public void SetUp()
        {
            validator = new AccountNumberValidator();
        }

        [TestMethod]
        [DataRow("1111401560108111018148824")]
        [DataRow("")]
        [DataRow("1")]
        public void ShortNumber(string number)
        {
            Assert.IsFalse(this.validator.Validate(number));
        }

        [TestMethod]
        [DataRow("111140156010811101814882412")]
        [DataRow("111140156010811101814882412111140156010811101814882412")]
        public void ToLongNumber(string number)
        {
            Assert.IsFalse(this.validator.Validate(number));
        }

        [TestMethod]
        [DataRow("FooBar fasnfjas fas f")]
        [DataRow("1111401560108111018148821M")]
        [DataRow("11A1140 1560 1081 1101 8148 8211")]
        public void BadNumber(string number)
        {
            Assert.IsFalse(this.validator.Validate(number));
        }

        [TestMethod]
        [DataRow("1111401560 1081110181488211")]
        [DataRow("11 1140 1560 1081 1101 8148 8211")]
        [DataRow("11 1140 1560  1081 1101 8148  8211")]
        public void WithSpacesNumber(string number)
        {
            Assert.IsTrue(this.validator.Validate(number));
        }

        [TestMethod]
        [DataRow("11114015601081110181488211")]
        public void WithoutSpacesNumber(string number)
        {
            Assert.IsTrue(this.validator.Validate(number));
        }
    }
}
