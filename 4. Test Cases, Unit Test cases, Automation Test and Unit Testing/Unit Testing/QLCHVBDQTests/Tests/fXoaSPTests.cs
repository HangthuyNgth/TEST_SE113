using NUnit.Framework; // Hoặc sử dụng MSTest nếu bạn thích
using System;
using QLCHVBDQ.DAO;

namespace QLCHVBDQTests.Tests
{
    [TestFixture]
    public class fXoaSPTests
    {
        // Hàm khởi tạo sẽ chạy trước mỗi test case
        [SetUp]
        public void SetUp()
        {
            // Có thể thêm mã để thiết lập cơ sở dữ liệu cho mỗi lần kiểm thử
            // Ví dụ: Thêm một số sản phẩm mẫu vào cơ sở dữ liệu
        }

        [Test]
        public void DeleteProduct_WhenProductHasOrders_ShouldNotDelete()
        {
            // Arrange
            string productIdWithOrders = "SP001"; // Thay đổi với sản phẩm có đơn hàng liên quan
            // Có thể thêm mã để kiểm tra rằng sản phẩm này tồn tại trong cơ sở dữ liệu

            // Act
            int result = SanPhamDAO.Instance.DeleteSP_MaSanPham(productIdWithOrders);

            // Assert
            Assert.That(result, Is.EqualTo(-1), "Sản phẩm có đơn hàng liên quan nên không thể xóa.");
        }

        [Test]
        public void DeleteProduct_WhenProductHasNoOrders_ShouldDeleteSuccessfully()
        {
            // Arrange
            string productIdWithoutOrders = "SP004"; // Đảm bảo rằng sản phẩm này không có đơn hàng liên quan

            // Kiểm tra trước khi xóa, sản phẩm có tồn tại
            string checkQuery = $"SELECT COUNT(*) FROM SANPHAM WHERE MaSP = '{productIdWithoutOrders}'";
            int beforeCount = (int)DataProvider.Instance.ExecuteScalar(checkQuery);
            Assert.That(beforeCount, Is.GreaterThan(0), "Sản phẩm không tồn tại trong cơ sở dữ liệu trước khi xóa.");

            // Act
            int result = SanPhamDAO.Instance.DeleteSP_MaSanPham(productIdWithoutOrders);

            // Assert
            Assert.That(result, Is.EqualTo(1), "Sản phẩm không được xóa khi không có đơn hàng liên quan.");

            // Kiểm tra sau khi xóa, sản phẩm không còn tồn tại
            int afterCount = (int)DataProvider.Instance.ExecuteScalar(checkQuery);
            Assert.That(afterCount, Is.EqualTo(0), "Sản phẩm chưa được xóa khỏi cơ sở dữ liệu.");
        }


        [Test]
        public void DeleteProduct_ShouldRemoveFromDatabase()
        {
            // Arrange
            string productIdToDelete = "SP003"; // Đảm bảo sản phẩm này có sẵn để kiểm tra
            // Thêm mã để kiểm tra rằng sản phẩm này tồn tại trong cơ sở dữ liệu

            // Act
            SanPhamDAO.Instance.DeleteSP_MaSanPham(productIdToDelete);

            // Kiểm tra lại
            string query = "SELECT COUNT(*) FROM SANPHAM WHERE MaSP = '" + productIdToDelete + "'";
            int count = (int)DataProvider.Instance.ExecuteScalar(query);

            // Assert
            Assert.That(count, Is.EqualTo(0), "Sản phẩm chưa được xóa khỏi cơ sở dữ liệu.");
        }
    }
}
