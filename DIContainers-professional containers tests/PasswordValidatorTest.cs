using DIContainers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DI_Tests
{
    [TestFixture]
    public class PasswordValidatorTest
    {
        IPasswordValidator passwordvalidator;

        [SetUp]
        public void TestSetup()
        {
            passwordvalidator = new PasswordValidator();
        }

        [Test]
        public void test_if_password_is_valid()
        {
            bool ifpasswordvalid = passwordvalidator.PasswordValidate("asdasd123@");

            Assert.IsTrue(ifpasswordvalid);
        }

        [Test]
        public void test_if_password_is_not_valid()
        {
            bool ifpasswordnotvalid = passwordvalidator.PasswordValidate("asdasd1");

            Assert.IsFalse(ifpasswordnotvalid);
        }
    }
}
