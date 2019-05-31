using DIContainers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DI_Tests
{
    [TestFixture]
    class MockLoginValidatorTest
    {
        [Test]
        public void test_if_login_is_valid()
        {
            var mock= new Mock<ILoginValidator>();

            mock.Setup(x => x.LoginValidate("assd12")).Returns(() => true);

            bool ifloginvalid = mock.Object.LoginValidate("assd12");

            Assert.IsTrue(ifloginvalid);
        }

        [Test]
        public void test_if_login_is_not_valid()
        {
            var mock = new Mock<ILoginValidator>();

            mock.Setup(x => x.LoginValidate("adasd123@")).Returns(() => false);

            bool ifloginnotvalid = mock.Object.LoginValidate("asdasd123@");

            Assert.IsFalse(ifloginnotvalid);
        }
    }
}
