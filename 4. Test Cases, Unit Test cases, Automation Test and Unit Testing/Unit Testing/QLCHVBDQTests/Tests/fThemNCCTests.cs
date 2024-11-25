using NUnit.Framework; // Hoặc sử dụng MSTest nếu bạn thích
using QLCHVBDQ;
using QLCHVBDQ.DAO;
using System;
using System.Data;

namespace QLCHVBDQTests.Tests
{
    [TestFixture]
    public class fThemNCCTests
    {
        // Hàm khởi tạo sẽ chạy trước mỗi test case
        [SetUp]
        public void SetUp()
        {
            // Có thể thêm mã để thiết lập cơ sở dữ liệu cho mỗi lần kiểm thử
            // Ví dụ: Xóa nhà cung cấp cũ nếu tồn tại
            string query = "DELETE FROM NHACUNGCAP WHERE MaNCC IN ('NCC001', 'NCC002', 'NCC003')";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        [Test]
        public void AddProvider_WithAllValidInputs_ShouldAddProviderSuccessfully()
        {
            // Arrange
            string providerId = "NCC001";
            string providerName = "Nhà Cung Cấp A";
            string phoneNumber = "0123456789";
            string address = "Địa chỉ A";

            // Act
            AddProvider(providerId, providerName, phoneNumber, address);

            // Assert
            string query = "SELECT COUNT(*) FROM NHACUNGCAP WHERE MaNCC = '" + providerId + "'";
            int count = (int)DataProvider.Instance.ExecuteScalar(query);
            Assert.That(count, Is.EqualTo(1), "Nhà cung cấp không được thêm vào cơ sở dữ liệu.");
        }

        [Test]
        public void AddProvider_WithEmptyProviderId_ShouldShowErrorMessage()
        {
            // Arrange
            string providerId = ""; // Không có ID
            string providerName = "Nhà Cung Cấp B";
            string phoneNumber = "0123456789";
            string address = "Địa chỉ B";

            // Act & Assert
            Assert.Throws<Exception>(() => AddProvider(providerId, providerName, phoneNumber, address),
                "Cần phải nhập mã nhà cung cấp.");
        }

        [Test]
        public void AddProvider_WithEmptyProviderName_ShouldShowErrorMessage()
        {
            // Arrange
            string providerId = "NCC002";
            string providerName = ""; // Không có tên
            string phoneNumber = "0123456789";
            string address = "Địa chỉ C";

            // Act & Assert
            Assert.Throws<Exception>(() => AddProvider(providerId, providerName, phoneNumber, address),
                "Cần phải nhập tên nhà cung cấp.");
        }

        [Test]
        public void AddProvider_WithEmptyPhoneNumber_ShouldShowErrorMessage()
        {
            // Arrange
            string providerId = "NCC003";
            string providerName = "Nhà Cung Cấp C";
            string phoneNumber = ""; // Không có số điện thoại
            string address = "Địa chỉ D";

            // Act & Assert
            Assert.Throws<Exception>(() => AddProvider(providerId, providerName, phoneNumber, address),
                "Cần phải nhập số điện thoại nhà cung cấp.");
        }

        [Test]
        public void AddProvider_WithInvalidPhoneNumber_ShouldShowErrorMessage()
        {
            // Arrange
            string providerId = "NCC004";
            string providerName = "Nhà Cung Cấp D";
            string phoneNumber = "12345"; // Số điện thoại không đúng định dạng
            string address = "Địa chỉ E";

            // Act & Assert
            Assert.Throws<Exception>(() => AddProvider(providerId, providerName, phoneNumber, address),
                "Số điện thoại phải có 10 ký tự.");
        }

        [Test]
        public void AddProvider_WithEmptyAddress_ShouldShowErrorMessage()
        {
            // Arrange
            string providerId = "NCC005";
            string providerName = "Nhà Cung Cấp E";
            string phoneNumber = "0123456789";
            string address = ""; // Không có địa chỉ

            // Act & Assert
            Assert.Throws<Exception>(() => AddProvider(providerId, providerName, phoneNumber, address),
                "Cần phải nhập địa chỉ nhà cung cấp.");
        }

        [Test]
        public void AddProvider_WithDuplicateProviderId_ShouldShowErrorMessage()
        {
            // Arrange
            string providerId = "NCC001"; // ID đã tồn tại
            string providerName1 = "Nhà Cung Cấp A";
            string phoneNumber1 = "0123456789";
            string address1 = "Địa chỉ A";

            // Thêm nhà cung cấp đầu tiên
            AddProvider(providerId, providerName1, phoneNumber1, address1);

            // Act & Assert
            Assert.Throws<Exception>(() => AddProvider(providerId, "Nhà Cung Cấp B", "0123456789", "Địa chỉ B"),
                "Mã nhà cung cấp đã tồn tại.");
        }

        private void AddProvider(string providerId, string providerName, string phoneNumber, string address)
        {
            // Gọi đến hàm thêm nhà cung cấp trong fThemNCC
            fThemNCC form = new fThemNCC();
            form.textBoxMaNCC.Text = providerId;
            form.textBoxTenNCC.Text = providerName;
            form.textBoxSDT.Text = phoneNumber;
            form.textBoxDiaChi.Text = address;

            // Giả lập nhấn nút thêm
            form.btnThem_Click(null, null);
        }
    }
}
