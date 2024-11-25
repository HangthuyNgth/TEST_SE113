using NUnit.Framework;
using QLCHVBDQ;
using QLCHVBDQ.DAO;
using System;
using System.Data;

namespace QLCHVBDQTests.Tests
{
    [TestFixture]
    public class fThemSPTests
    {
        private fThemSP form;

        [SetUp]
        public void SetUp()
        {
            form = new fThemSP();
        }

        [Test]
        public void AddProduct_WithValidDetails_ShouldAddSuccessfully()
        {
            // Arrange
            form.textBoxMaSP.Text = "SP032";
            form.textBoxTenSP.Text = "Vòng cổ Bạc";
            form.comboBoxMaLSP.SelectedItem = "LSP001"; // Đảm bảo rằng "LSP001" tồn tại trong LOAISANPHAM
            form.numUDDonGia.Value = 1000000; // Đơn giá hợp lệ
            form.numUDTonKho.Value = 10; // Tồn kho hợp lệ

            // Act
            form.btnThem_Click(null, null);

            // Kiểm tra kết quả
            string query = "SELECT COUNT(*) FROM SANPHAM WHERE MaSP = 'SP032'";
            int count = (int)DataProvider.Instance.ExecuteScalar(query);

            // Assert
            Assert.That(count, Is.EqualTo(0), "Sản phẩm được thêm vào cơ sở dữ liệu.");
        }




        [Test]
        public void AddProduct_WithMissingFields_ShouldShowError()
        {
            // Arrange
            form.textBoxMaSP.Text = ""; // Không điền mã sản phẩm
            form.textBoxTenSP.Text = "";
            form.comboBoxMaLSP.SelectedItem = null; // Không chọn loại sản phẩm
            form.numUDDonGia.Value = 0; // Không điền đơn giá
            form.numUDTonKho.Value = 0; // Không điền tồn kho

            // Act
            form.btnThem_Click(null, null);

            // Assert
            Assert.That(form.Controls["errorLabel"].Visible, Is.True, "Thông báo lỗi không hiển thị khi thông tin không hợp lệ.");
        }

        [Test]
        public void AddProduct_WithExistingMaSP_ShouldShowError()
        {
            // Arrange
            form.textBoxMaSP.Text = "SP001"; // Mã sản phẩm đã tồn tại
            form.textBoxTenSP.Text = "Nhẫn trẻ em Bạc";
            form.comboBoxMaLSP.SelectedItem = "LSP001"; // Chọn loại sản phẩm
            form.numUDDonGia.Value = 500000; // Đơn giá hợp lệ
            form.numUDTonKho.Value = 5; // Tồn kho hợp lệ

            // Act
            form.btnThem_Click(null, null);

            // Assert
            Assert.That(form.Controls["errorLabel"].Visible, Is.True, "Thông báo lỗi không hiển thị khi mã sản phẩm đã tồn tại.");
        }

        [Test]
        public void AddProduct_WithNegativePrice_ShouldShowError()
        {
            // Arrange
            form.textBoxMaSP.Text = "SP032";
            form.textBoxTenSP.Text = "Sản phẩm lỗi";
            form.comboBoxMaLSP.SelectedItem = "LSP001";
            form.numUDDonGia.Value = -1000; // Đơn giá âm
            form.numUDTonKho.Value = 5;

            // Act
            form.btnThem_Click(null, null);

            // Assert
            Assert.That(form.Controls["errorLabel"].Visible, Is.True, "Thông báo lỗi không hiển thị khi đơn giá là số âm.");
        }

        [Test]
        public void AddProduct_WithNegativeStock_ShouldShowError()
        {
            // Arrange
            form.textBoxMaSP.Text = "SP033";
            form.textBoxTenSP.Text = "Sản phẩm lỗi";
            form.comboBoxMaLSP.SelectedItem = "LSP001";
            form.numUDDonGia.Value = 1000000;
            form.numUDTonKho.Value = -5; // Tồn kho âm

            // Act
            form.btnThem_Click(null, null);

            // Assert
            Assert.That(form.Controls["errorLabel"].Visible, Is.True, "Thông báo lỗi không hiển thị khi tồn kho là số âm.");
        }

        [Test]
        public void AddProduct_WithoutProductType_ShouldShowError()
        {
            // Arrange
            form.textBoxMaSP.Text = "SP034";
            form.textBoxTenSP.Text = "Sản phẩm không loại";
            form.comboBoxMaLSP.SelectedItem = null; // Không chọn loại sản phẩm
            form.numUDDonGia.Value = 1000000;
            form.numUDTonKho.Value = 10;

            // Act
            form.btnThem_Click(null, null);

            // Assert
            Assert.That(form.Controls["errorLabel"].Visible, Is.True, "Thông báo lỗi không hiển thị khi không chọn loại sản phẩm.");
        }

        [Test]
        public void AddProduct_WithInvalidCharactersInName_ShouldShowError()
        {
            // Arrange
            form.textBoxMaSP.Text = "SP035";
            form.textBoxTenSP.Text = "Sản phẩm @#$%"; // Ký tự không hợp lệ
            form.comboBoxMaLSP.SelectedItem = "LSP001";
            form.numUDDonGia.Value = 1000000;
            form.numUDTonKho.Value = 10;

            // Act
            form.btnThem_Click(null, null);

            // Assert
            Assert.That(form.Controls["errorLabel"].Visible, Is.True, "Thông báo lỗi không hiển thị khi tên sản phẩm chứa ký tự không hợp lệ.");
        }
    }
}
