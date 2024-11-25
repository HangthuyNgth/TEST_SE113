using NUnit.Framework;
using QLCHVBDQ;

namespace QLCHVBDQTests.Tests
{
    [TestFixture]
    public class fLoginTests
    {
        private fLogin loginForm;
        private string validEmail;
        private string validPassword;

        [SetUp]
        public void SetUp()
        {
            loginForm = new fLogin();
            validEmail = "0";          // Email hợp lệ
            validPassword = "1111";    // Mật khẩu hợp lệ
        }

        [Test]
        public void Login_WithValidEmailAndPassword_ShouldReturnTrue()
        {
            // Act
            bool result = loginForm.Login(validEmail, validPassword);

            // Assert
            Assert.That(result, Is.True, "Login should succeed with valid email and password.");
        }

        [Test]
        public void Login_WithInvalidEmailOrPassword_ShouldReturnFalse()
        {
            // Arrange
            string invalidEmail = "0";   // Email không tồn tại trong cơ sở dữ liệu
            string invalidPassword = "1111";          // Mật khẩu sai

            // Act
            bool resultWithInvalidEmail = loginForm.Login(invalidEmail, validPassword);  // Email sai, mật khẩu đúng
            bool resultWithInvalidPassword = loginForm.Login(validEmail, invalidPassword); // Email đúng, mật khẩu sai
            bool resultWithBothInvalid = loginForm.Login(invalidEmail, invalidPassword);   // Cả email và mật khẩu đều sai

            // Assert
            Assert.That(resultWithInvalidEmail, Is.False, "Login should fail with invalid email.");
            Assert.That(resultWithInvalidPassword, Is.False, "Login should fail with invalid password.");
            Assert.That(resultWithBothInvalid, Is.False, "Login should fail with both email and password invalid.");
        }
    }
}
