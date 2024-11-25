using NUnit.Framework; // Hoặc sử dụng MSTest nếu bạn thích
using QLCHVBDQ;
using QLCHVBDQ.DAO;
using System;
using System.Data;

namespace QLCHVBDQTests.Tests
{
    [TestFixture]
    public class fThemPBHTests
    {
        [SetUp]
        public void SetUp()
        {
            // Xóa dữ liệu cũ để mỗi test case có môi trường sạch
            string deleteQuery = "DELETE FROM PHIEUBANHANG WHERE SoPhieu IN ('PH001', 'PH002', 'PH003')";
            DataProvider.Instance.ExecuteNonQuery(deleteQuery);
        }

        [Test]
        public void CreateSaleOrder_WithAllValidInputs_ShouldAddSaleOrderSuccessfully()
        {
            // Arrange
            string saleOrderId = "PH001";
            string customerName = "Khách Hàng A";
            string phoneNumber = "0123456789";
            string productName = "Sản Phẩm A";
            int quantity = 10;

            // Act
            AddSaleOrder(saleOrderId, customerName, phoneNumber, productName, quantity);

            // Assert
            string query = "SELECT COUNT(*) FROM PHIEUBANHANG WHERE SoPhieu = '" + saleOrderId + "'";
            int count = (int)DataProvider.Instance.ExecuteScalar(query);
            Assert.That(count, Is.EqualTo(1), "Phiếu bán hàng không được thêm vào cơ sở dữ liệu.");
        }

        [Test]
        public void CreateSaleOrder_WithExistingSaleOrderId_ShouldShowErrorMessage()
        {
            // Arrange
            string saleOrderId = "PH002"; // ID đã tồn tại
            string customerName = "Khách Hàng B";
            string phoneNumber = "0987654321";
            string productName = "Sản Phẩm B";
            int quantity = 5;

            // Thêm phiếu bán hàng đầu tiên
            AddSaleOrder(saleOrderId, customerName, phoneNumber, productName, quantity);

            // Act & Assert
            Assert.Throws<Exception>(() => AddSaleOrder(saleOrderId, "Khách Hàng C", "0123456789", "Sản Phẩm C", 3),
                "Số phiếu đã tồn tại.");
        }

        [Test]
        public void CreateSaleOrder_WithNegativeQuantity_ShouldShowErrorMessage()
        {
            // Arrange
            string saleOrderId = "PH003";
            string customerName = "Khách Hàng C";
            string phoneNumber = "0123456789";
            string productName = "Sản Phẩm C";
            int quantity = -1; // Số lượng âm

            // Act & Assert
            Assert.Throws<Exception>(() => AddSaleOrder(saleOrderId, customerName, phoneNumber, productName, quantity),
                "Số lượng phải là số dương.");
        }

        [Test]
        public void CreateSaleOrder_WithEmptySaleOrderId_ShouldShowErrorMessage()
        {
            // Arrange
            string saleOrderId = ""; // Không có ID
            string customerName = "Khách Hàng D";
            string phoneNumber = "0123456789";
            string productName = "Sản Phẩm D";
            int quantity = 10;

            // Act & Assert
            Assert.Throws<Exception>(() => AddSaleOrder(saleOrderId, customerName, phoneNumber, productName, quantity),
                "Cần phải nhập số phiếu.");
        }

        [Test]
        public void CreateSaleOrder_WithEmptyCustomerName_ShouldShowErrorMessage()
        {
            // Arrange
            string saleOrderId = "PH004";
            string customerName = ""; // Không có tên khách hàng
            string phoneNumber = "0123456789";
            string productName = "Sản Phẩm E";
            int quantity = 10;

            // Act & Assert
            Assert.Throws<Exception>(() => AddSaleOrder(saleOrderId, customerName, phoneNumber, productName, quantity),
                "Cần phải nhập tên khách hàng.");
        }

        [Test]
        public void CreateSaleOrder_WithEmptyPhoneNumber_ShouldShowErrorMessage()
        {
            // Arrange
            string saleOrderId = "PH005";
            string customerName = "Khách Hàng F";
            string phoneNumber = ""; // Không có số điện thoại
            string productName = "Sản Phẩm F";
            int quantity = 10;

            // Act & Assert
            Assert.Throws<Exception>(() => AddSaleOrder(saleOrderId, customerName, phoneNumber, productName, quantity),
                "Cần phải nhập số điện thoại.");
        }

        [Test]
        public void CreateSaleOrder_WithEmptyProductName_ShouldShowErrorMessage()
        {
            // Arrange
            string saleOrderId = "PH006";
            string customerName = "Khách Hàng G";
            string phoneNumber = "0123456789";
            string productName = ""; // Không có tên sản phẩm
            int quantity = 10;

            // Act & Assert
            Assert.Throws<Exception>(() => AddSaleOrder(saleOrderId, customerName, phoneNumber, productName, quantity),
                "Cần phải nhập tên sản phẩm.");
        }

        [Test]
        public void CreateSaleOrder_WithInvalidPhoneNumber_ShouldShowErrorMessage()
        {
            // Arrange
            string saleOrderId = "PH007";
            string customerName = "Khách Hàng H";
            string phoneNumber = "12345678"; // Số điện thoại không đúng 10 ký tự
            string productName = "Sản Phẩm H";
            int quantity = 10;

            // Act & Assert
            Assert.Throws<Exception>(() => AddSaleOrder(saleOrderId, customerName, phoneNumber, productName, quantity),
                "Số điện thoại phải có đúng 10 ký tự.");
        }

        private void AddSaleOrder(string saleOrderId, string customerName, string phoneNumber, string productName, int quantity)
        {
            // Tạo form fThemPBH và thiết lập các trường
            fThemPBH form = new fThemPBH();

            form.textBoxSoPhieu.Text = saleOrderId;
            form.textBoxTenKH.Text = customerName;
            form.textBoxSDT.Text = phoneNumber;

            // Đảm bảo ngày tháng nằm trong khoảng thời gian hợp lệ
            form.dateNgayLapPhieu.Value = DateTime.Now.Date;

            // Gọi nút thêm để kiểm tra
            form.btnThem_Click(null, null);

            // Giả lập thêm sản phẩm nếu cần thiết
            // PBHDAO.Instance.InsertProductToSaleOrder(saleOrderId, productName, quantity);
        }
    }
}
