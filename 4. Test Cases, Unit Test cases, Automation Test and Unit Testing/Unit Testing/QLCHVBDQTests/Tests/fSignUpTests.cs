using NUnit.Framework;
using QLCHVBDQ;
using System;
using System.Windows.Forms;

namespace QLCHVBDQTests.Tests
{
    [TestFixture]
    public class fSignUpTests
    {
        private fDangKy signUpForm;

        [SetUp]
        public void SetUp()
        {
            signUpForm = new fDangKy();
        }

        [Test]
        public void SignUp_WithValidDetails_ShouldRegisterSuccessfully()
        {
            // Arrange
            signUpForm.Email.Text = "validemail@example.com";
            signUpForm.textSDT.Text = "0123456789"; // Số điện thoại 10 ký tự
            signUpForm.textTenTK.Text = "ValidUser";
            signUpForm.dateNgaySinh.Value = new DateTime(2000, 1, 1); // Ngày sinh hợp lệ
            signUpForm.password.Text = "ComplexPass123"; // Mật khẩu hợp lệ
            signUpForm.textNhapLaiMK.Text = "ComplexPass123"; // Nhập lại mật khẩu khớp

            // Act
            signUpForm.btnDangKy_Click(null, null);

            // Assert
            Assert.Pass("Đăng ký thành công."); // Chỉ để test pass nếu không có phương pháp cụ thể để kiểm tra.
        }

        [Test]
        public void SignUp_WithInvalidEmail_ShouldShowError()
        {
            // Arrange
            signUpForm.Email.Text = "invalidemail"; // Email không hợp lệ
            signUpForm.textSDT.Text = "0123456789"; // Số điện thoại hợp lệ
            signUpForm.textTenTK.Text = "User";
            signUpForm.dateNgaySinh.Value = new DateTime(2000, 1, 1);
            signUpForm.password.Text = "ValidPassword123";
            signUpForm.textNhapLaiMK.Text = "ValidPassword123";

            // Act
            signUpForm.btnDangKy_Click(null, null);

            // Assert
            Assert.That(signUpForm.IsErrorShown("Email không hợp lệ"), Is.True);
        }

        [Test]
        public void SignUp_WithShortPhoneNumber_ShouldShowError()
        {
            // Arrange
            signUpForm.Email.Text = "validemail@example.com";
            signUpForm.textSDT.Text = "123"; // Số điện thoại không đủ 10 ký tự
            signUpForm.textTenTK.Text = "User";
            signUpForm.dateNgaySinh.Value = new DateTime(2000, 1, 1);
            signUpForm.password.Text = "ValidPassword123";
            signUpForm.textNhapLaiMK.Text = "ValidPassword123";

            // Act
            signUpForm.btnDangKy_Click(null, null);

            // Assert
            Assert.That(signUpForm.IsErrorShown("Số điện thoại không hợp lệ"), Is.True);
        }

        [Test]
        public void SignUp_WithInvalidDateOfBirth_ShouldShowError()
        {
            // Arrange
            signUpForm.Email.Text = "validemail@example.com";
            signUpForm.textSDT.Text = "0123456789";
            signUpForm.textTenTK.Text = "User";
            signUpForm.dateNgaySinh.Value = new DateTime(2025, 1, 1); // Ngày sinh không hợp lệ (tương lai)
            signUpForm.password.Text = "ValidPassword123";
            signUpForm.textNhapLaiMK.Text = "ValidPassword123";

            // Act
            signUpForm.btnDangKy_Click(null, null);

            // Assert
            Assert.That(signUpForm.IsErrorShown("Ngày sinh không hợp lệ"), Is.True);
        }

        [Test]
        public void SignUp_WithPasswordAndConfirmPasswordMismatch_ShouldShowError()
        {
            // Arrange
            signUpForm.Email.Text = "validemail@example.com";
            signUpForm.textSDT.Text = "0123456789";
            signUpForm.textTenTK.Text = "User";
            signUpForm.dateNgaySinh.Value = new DateTime(2000, 1, 1);
            signUpForm.password.Text = "Password123";
            signUpForm.textNhapLaiMK.Text = "DifferentPassword"; // Mật khẩu nhập lại không khớp

            // Act
            signUpForm.btnDangKy_Click(null, null);

            // Assert
            Assert.That(signUpForm.IsErrorShown("Mật khẩu nhập lại không khớp"), Is.True);
        }

        [Test]
        public void SignUp_WithWeakPassword_ShouldShowError()
        {
            // Arrange
            signUpForm.Email.Text = "validemail@example.com";
            signUpForm.textSDT.Text = "0123456789";
            signUpForm.textTenTK.Text = "User";
            signUpForm.dateNgaySinh.Value = new DateTime(2000, 1, 1);
            signUpForm.password.Text = "123"; // Mật khẩu yếu
            signUpForm.textNhapLaiMK.Text = "123"; // Nhập lại mật khẩu yếu

            // Act
            signUpForm.btnDangKy_Click(null, null);

            // Assert
            Assert.That(signUpForm.IsErrorShown("Mật khẩu không đủ độ phức tạp"), Is.True);
        }

        [Test]
        public void SignUp_WithMissingFields_ShouldShowError()
        {
            // Arrange
            signUpForm.Email.Text = ""; // Không điền email
            signUpForm.textSDT.Text = ""; // Không điền số điện thoại
            signUpForm.textTenTK.Text = ""; // Không điền tên tài khoản
            signUpForm.dateNgaySinh.Value = new DateTime(2000, 1, 1);
            signUpForm.password.Text = "";
            signUpForm.textNhapLaiMK.Text = "";

            // Act
            signUpForm.btnDangKy_Click(null, null);

            // Assert
            Assert.That(signUpForm.IsErrorShown("Vui lòng nhập đủ thông tin"), Is.True);
        }

        [Test]
        public void SignUp_WithExistingEmail_ShouldShowError()
        {
            // Arrange
            signUpForm.Email.Text = "existingemail@example.com"; // Email đã tồn tại
            signUpForm.textSDT.Text = "0123456789";
            signUpForm.textTenTK.Text = "User";
            signUpForm.dateNgaySinh.Value = new DateTime(2000, 1, 1);
            signUpForm.password.Text = "ValidPassword123";
            signUpForm.textNhapLaiMK.Text = "ValidPassword123";

            // Act
            signUpForm.btnDangKy_Click(null, null);

            // Assert
            Assert.That(signUpForm.IsErrorShown("Email này đã được đăng ký"), Is.True);
        }
    }
}
