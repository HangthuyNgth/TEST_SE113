using NUnit.Framework;
using QLCHVBDQ;
using QLCHVBDQ.DAO;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCHVBDQTests.Tests
{
    [TestFixture]
    public class fCTPMHTests
    {
        [SetUp]
        public void SetUp()
        {
            // Thiết lập môi trường sạch cho các test case, xóa các dữ liệu giả lập trước
            string deleteQuery = "DELETE FROM PHIEUMUAHANG WHERE SoPhieu IN ('PMH001', 'PMH002', 'PMH003')";
            DataProvider.Instance.ExecuteNonQuery(deleteQuery);
        }

        [Test]
        public void LoadCTPMH_ValidPurchaseOrderId_ShouldLoadOrderDetails()
        {
            // Arrange
            fMuaHang.SoPhieu_selected = "PMH001"; // Giả định PMH001 là SoPhieu hợp lệ có dữ liệu
            fCTPMH form = new fCTPMH();

            // Act
            form.LoadCTPMH();

            // Assert
            Assert.That(form.x.Rows.Count, Is.GreaterThan(0), "Phiếu mua hàng không được tải vào.");
            Assert.That(form.textBoxNewSoPhieu.Text, Is.EqualTo("PMH001"));
        }

        [Test]
        public void LoadCTPMH_InvalidPurchaseOrderId_ShouldNotLoadOrderDetails()
        {
            // Arrange
            fMuaHang.SoPhieu_selected = "InvalidID";
            fCTPMH form = new fCTPMH();

            // Act
            form.LoadCTPMH();

            // Assert
            Assert.That(form.x.Rows.Count, Is.EqualTo(0), "Dữ liệu không hợp lệ nhưng vẫn tải vào form.");
            Assert.That(form.textBoxNewSoPhieu.Text, Is.Empty, "Số phiếu vẫn hiển thị khi không tìm thấy phiếu mua hàng.");
        }

        [Test]
        public void AddNewProductToOrder_ShouldRefreshOrderDetails()
        {
            // Arrange
            fMuaHang.SoPhieu_selected = "PMH001"; // Giả định có phiếu mua hàng PMH001
            fCTPMH form = new fCTPMH();

            // Act
            form.btnThem_Click(null, null);

            // Assert
            // Sau khi thêm, kiểm tra bảng chi tiết đơn hàng đã cập nhật lại dữ liệu
            Assert.That(form.dtgvChiTietPMH.Rows.Count, Is.GreaterThan(0), "Chi tiết phiếu mua hàng không được làm mới sau khi thêm.");
        }

        [Test]
        public void DeleteProductFromOrder_ShouldRemoveProductAndRefreshOrderDetails()
        {
            // Arrange
            fMuaHang.SoPhieu_selected = "PMH001"; // Giả định có phiếu mua hàng PMH001 và có sản phẩm trong chi tiết
            fCTPMH form = new fCTPMH();
            form.LoadCTPMH();
            int initialRowCount = form.dtgvChiTietPMH.Rows.Count;

            // Act
            form.dtgvChiTietPMH_CellClick(null, new DataGridViewCellEventArgs(0, 0));

            // Assert
            form.LoadCTPMH(); // Tải lại để kiểm tra cập nhật
            Assert.That(form.dtgvChiTietPMH.Rows.Count, Is.LessThan(initialRowCount), "Sản phẩm không bị xóa khỏi phiếu mua hàng.");
        }

        [Test]
        public void LoadCTPMH_WithEmptyProductName_ShouldShowErrorMessage()
        {
            // Arrange
            fMuaHang.SoPhieu_selected = "PMH003"; // Giả định có SoPhieu nhưng sản phẩm thiếu tên
            fCTPMH form = new fCTPMH();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => form.LoadCTPMH());
            Assert.That(ex.Message, Is.EqualTo("Cần phải nhập tên sản phẩm."));
        }

        [Test]
        public void LoadCTPMH_WithNegativeQuantity_ShouldShowErrorMessage()
        {
            // Arrange
            fMuaHang.SoPhieu_selected = "PMH004"; // Giả định có SoPhieu nhưng số lượng sản phẩm không hợp lệ (âm)
            fCTPMH form = new fCTPMH();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => form.LoadCTPMH());
            Assert.That(ex.Message, Is.EqualTo("Số lượng phải là số dương."));
        }
    }
}
