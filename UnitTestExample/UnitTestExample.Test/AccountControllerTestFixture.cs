using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [Test,
    TestCase("abcd1234", false),
    TestCase("irf@uni-corvinus", false),
    TestCase("irf.uni-corvinus.hu", false),
    TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // Arrange

            var accountController = new AccountController();

            //Act

            var result = accountController.ValidateEmail(email);

            //Assert

            Assert.AreEqual(expectedResult, result);
        }

        [Test,
            TestCase("abcd1234", false),
            TestCase("ABCD1234", false),
            TestCase("abcdABCD", false),
            TestCase("abCD12", false),
            TestCase("abCD1234", true),
            TestCase("ABcde1234", true)

            ]

        public void TestValidatePassword(string password, bool expectedResult)
        {
            // Arrange

            var accountController = new AccountController();

            //Act

            var result = accountController.ValidatePassword(password);

            //Assert

            Assert.AreEqual(expectedResult, result);
        }

        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234567"),
        ]

        public void TestRegisterHappyPath(string email, string password)
        {
            // Arrange

            var accountController = new AccountController();

            //Act

            var result = accountController.Register(email, password);

            //Assert
            Assert.AreEqual(email, result.Email);
            Assert.AreEqual(password, result.Password);
            Assert.AreNotEqual(Guid.Empty, result.ID);
        }

        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234"),
            TestCase("irf.uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "abcd1234"),
            TestCase("irf@uni-corvinus.hu", "ABCD1234"),
            TestCase("irf@uni-corvinus.hu", "abcdABCD"),
            TestCase("irf@uni-corvinus.hu", "Ab1234"),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            // Arrange

            var accountController = new AccountController();

            //Act
            //Assert

            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }

            

        }
    }
}
