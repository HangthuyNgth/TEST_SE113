using ClosedXML.Excel;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using QLCHVBDQ.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHVBDQ
{
    public partial class fBanHang : Form
    {
        public fBanHang()
        {
            InitializeComponent();
            LoadPhieuBH();
        }
        public static string SoPhieu_selected = "Phieu1";
        void LoadPhieuBH()
        {
            textUserName.Text = fLogin.userName;
            dtgvPBH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvPBH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
            dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            dtgvPBH.Columns.Add(IconCol);
            IconCol.Image = Properties.Resources.More_Vertical;
            foreach (DataGridViewColumn item in dtgvPBH.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtgvPBH.Columns[4].Width = 40;
            dtgvPBH.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void textTimKiem_Enter(object sender, EventArgs e)
        {
            if (textTimKiem.Text == "Tìm kiếm")
            {
                textTimKiem.Text = "";
            }
        }

        private void textTimKiem_Leave(object sender, EventArgs e)
        {
            if (textTimKiem.Text == "")
            {
                textTimKiem.Text = "Tìm kiếm";
            }
        }


        private void dtgvPBH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string r = e.RowIndex.ToString();
            string c = e.ColumnIndex.ToString();
            //MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            if (e.ColumnIndex == 0)
            {
                //dtgvPBH.CurrentRow.Selected = true;
                panelMoreOption.Location = dtgvPBH.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                panelMoreOption.Left = panelMoreOption.Left - 50;
                panelMoreOption.Top = Math.Min(panelMoreOption.Top + panelMoreOption.Height + 100, 720);
                panelMoreOption.Visible = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
            }
        }

        private void dtgvPBH_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1)
            {
                if (e.ColumnIndex != 0 || e.RowIndex != dtgvPBH.CurrentCell.RowIndex)
                {
                    panelMoreOption.Visible = false;
                }
            }

        }
        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvPBH.SelectedRows.Count;
            //MessageBox.Show(String.Format("Bạn đang chọn {0} hàng ", count));            //MessageBox.Show("Bạn đang chọn " + total);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvPBH.SelectedRows[i].Index;
                        int colIndex = 1;
                        string SoPhieu = dtgvPBH.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = PBHDAO.Instance.DeletePBH(SoPhieu);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
                    dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} phiếu đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int rowIndex = dtgvPBH.SelectedRows[0].Index;
            string SoPhieu = dtgvPBH.Rows[rowIndex].Cells[1].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                panelMoreOption.Visible = false;
                int data = PBHDAO.Instance.DeletePBH(SoPhieu);
                if (data == 1)
                {
                    string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
                    dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
                    dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show("Đã xóa phiếu thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Xóa phiếu không thành công", "Thông báo", MessageBoxButtons.OK);
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPBH.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPBH.Rows[rowIndex].Cells[1].Value.ToString();
            fThemCTPBH f = new fThemCTPBH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
            dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnThemPBH_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            fThemPBH f = new fThemPBH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
            dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPBH.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPBH.Rows[rowIndex].Cells[1].Value.ToString();
            fCTPBH f = new fCTPBH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
            dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }




        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            fTrangChu f = new fTrangChu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            fSanPham f = new fSanPham();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }
        private void btnLoaiSanPham_Click(object sender, EventArgs e)
        {
            fLoaiSanPham f = new fLoaiSanPham();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }
        private void btnDichVu_Click(object sender, EventArgs e)
        {
            fDichVu f = new fDichVu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }



        private void btnLoaiDichVu_Click(object sender, EventArgs e)
        {
            fLoaiDichVu f = new fLoaiDichVu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnMuaHang_Click(object sender, EventArgs e)
        {
            fMuaHang f = new fMuaHang();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            fNhaCungCap f = new fNhaCungCap();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            fBaoCao f = new fBaoCao();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnThongTinTaiKhoan_Click(object sender, EventArgs e)
        {
            panelTTTK.Visible = false;
            fThongTinTaiKhoan f = new fThongTinTaiKhoan();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void buttonDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (panelTTTK.Visible == true) panelTTTK.Visible = false;
            else panelTTTK.Visible = true;
        }

        private void buttonXuatPDF_Click(object sender, EventArgs e)
        {
            if (dtgvPBH.SelectedRows.Count == 0)
            {
                MessageBox.Show("Không có phiếu bán hàng được chọn");
            }
            else if (dtgvPBH.SelectedRows.Count == 1)
            {

                MessageBox.Show("Có 1 phiếu bán hàng được chọn");
                DataGridViewRow hangSelected = dtgvPBH.SelectedRows[0];
                string soPhieuSelected = hangSelected.Cells[1].Value.ToString().Trim();

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save an PDF File";
                saveFileDialog.FileName = "PBH_" + soPhieuSelected + ".pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    CreatePDF(path, soPhieuSelected);
                    MessageBox.Show("Ghi file thành công: " + path);
                    OpenPDF(path);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 phiếu bán hàng để xuất PDF.");
            }
        }
        private void CreatePDF(string path, string soPhieuSelected)
        {
            DataTable x;
            string query = String.Format("select SoPhieu, NgayLap, KHACHHANG.TenKH, KHACHHANG.SDT, TongTien  from PHIEUBANHANG, KHACHHANG where SoPhieu = '{0}' and KHACHHANG.MaKH = PHIEUBANHANG.MaKH", soPhieuSelected);
            x = DataProvider.Instance.ExecuteQuery(query);

            string soPhieuPDF = x.Rows[0][0].ToString();
            string ngayLapPhieuPDF = Convert.ToDateTime(x.Rows[0][1]).ToString("dd/MM/yyyy");
            string tenKHPDF = x.Rows[0][2].ToString();
            string sdtPDF = x.Rows[0][3].ToString();
            string tongTien = x.Rows[0][4].ToString();

            // Đường dẫn đến file font .ttf của bạn
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

            FileStream stream = new FileStream(path, FileMode.Create);
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Đặt tiêu đề
            Paragraph title = new Paragraph("THÔNG TIN PHIẾU BÁN HÀNG")
               .SetFont(font)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);
            document.Add(title);

            // Thêm khoảng trống
            document.Add(new Paragraph("\n"));

            // Thông tin chung
            Paragraph info = new Paragraph()
               .Add(new Text("Số phiếu: " + soPhieuPDF).SetFont(font))
               .Add(new Text("\nNgày lập: " + ngayLapPhieuPDF).SetFont(font))
               .Add(new Text("\nTên khách hàng: " + tenKHPDF).SetFont(font))
               .Add(new Text("\nSố điện thoại: " + sdtPDF).SetFont(font));
            document.Add(info);


            // Thêm khoảng trống
            document.Add(new Paragraph("\n"));

            // Bảng chi tiết
            Table table = new Table(6, true);

            // Thêm header
            string[] headers = { "Tên sản phẩm", "Tên loại sản phẩm", "Số lượng", "Tên DVT", "Đơn giá mặt hàng", "Thành tiền" };
            foreach (string header in headers)
            {
                Cell cell = new Cell().Add(new Paragraph(header).SetFont(font));
                cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                cell.SetTextAlignment(TextAlignment.CENTER);
                table.AddHeaderCell(cell);
            }

            query = String.Format("select SANPHAM.TenSP as N'Tên sản phẩm', LOAISANPHAM.TenLSP as 'Loại sản phẩm', CTPBH.SoLuong as N'Số lượng', DVT.TenDVT as N'Đơn vị tính', CTPBH.DonGiaBH as N'Đơn giá', CTPBH.ThanhTien as N'Thành tiền' from CTPBH, SANPHAM, LOAISANPHAM, DVT where SoPhieu = '{0}' and CTPBH.MaSP = SANPHAM.MaSP and SANPHAM.MaLSP = LOAISANPHAM.MaLSP and LOAISANPHAM.MaDVT = DVT.MaDVT", soPhieuSelected);
            x = DataProvider.Instance.ExecuteQuery(query);

            if (x.Rows.Count == 0)
            {
                for (int k = 0; k < x.Columns.Count; k++)
                {
                    Cell cell = new Cell().Add(new Paragraph("\n").SetFont(font));
                    table.AddCell(cell);
                }
            }
            else
            {
                for (int i = 0; i < x.Rows.Count; i++)
                {
                    for (int j = 0; j < x.Columns.Count; j++)
                    {
                        Cell cell = new Cell().Add(new Paragraph(x.Rows[i][j].ToString()).SetFont(font));
                        table.AddCell(cell);
                    }
                }
            }
            document.Add(table);

            // Tổng thanh toán
            Paragraph total = new Paragraph("Tổng tiền: " + tongTien)
               .SetFont(font)
               .SetTextAlignment(TextAlignment.RIGHT);
            document.Add(total);

            document.Close();

        }

        private void OpenPDF(string path)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open PDF: " + ex.Message);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = "BangPhieuBanHang.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                DataTable dataTable = GetDataTableFromDataGridView(dtgvPBH); // Hàm lấy dữ liệu từ DataGridView
                ExportToExcel(dataTable, filePath);
            }
        }

        public void ExportToExcel(DataTable dataTable, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(dataTable, "Phieu Ban Hang");
                workbook.SaveAs(filePath);
            }

            // Mở file sau khi lưu
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }


        private DataTable GetDataTableFromDataGridView(DataGridView dgv)
        {
            var dt = new DataTable();
            for (int colIndex = 1; colIndex < dgv.Columns.Count; colIndex++)
            {
                DataGridViewColumn column = dgv.Columns[colIndex];
                dt.Columns.Add(column.HeaderText);
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int colIndex = 1; colIndex < dgv.Columns.Count; colIndex++)
                    {
                        DataGridViewColumn column = dgv.Columns[colIndex];
                        if (column.HeaderText == "Ngày lập")
                        {
                            dataRow[colIndex - 1] = Convert.ToDateTime(row.Cells[colIndex].Value).ToString("dd/MM/yyyy");
                            continue;
                        }
                        dataRow[colIndex - 1] = row.Cells[colIndex].Value;
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }

        private void picBoxLogo_Click(object sender, EventArgs e)
        {

        }

        private void DashboardTrangChu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {

        }

        private void TrangChu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgvPBH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textTimKiem_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
