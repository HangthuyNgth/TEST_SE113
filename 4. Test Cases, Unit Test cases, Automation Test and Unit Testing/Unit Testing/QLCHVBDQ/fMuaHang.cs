using QLCHVBDQ.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Layout.Borders;
using System.IO;
using iText.Kernel.Colors;
using iText.IO.Font;
using iText.Kernel.Font;
using ClosedXML.Excel;
using System.Diagnostics;

namespace QLCHVBDQ
{
    public partial class fMuaHang : Form
    {
        public fMuaHang()
        {
            InitializeComponent();
            LoadPhieuMH();
        }
        public static string SoPhieu_selected = "Phieu1";
        void LoadPhieuMH()
        {
            textUserName.Text = fLogin.userName;
            dtgvPMH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvPMH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
            dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            dtgvPMH.Columns.Add(IconCol);
            IconCol.Image = Properties.Resources.More_Vertical;
            foreach (DataGridViewColumn item in dtgvPMH.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtgvPMH.Columns[4].Width = 40;
            dtgvPMH.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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


        private void dtgvPMH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dtgvPMH.CurrentRow.Selected = true;
                panelMoreOption.Location = dtgvPMH.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
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
                if (e.ColumnIndex != 0 || e.RowIndex != dtgvPMH.CurrentCell.RowIndex)
                {
                    panelMoreOption.Visible = false;
                }
            }

        }
        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvPMH.SelectedRows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvPMH.SelectedRows[i].Index;
                        int colIndex = 1;
                        string SoPhieu = dtgvPMH.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = PMHDAO.Instance.DeletePMH(SoPhieu);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
                    dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} phiếu đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int rowIndex = dtgvPMH.SelectedRows[0].Index;
            string SoPhieu = dtgvPMH.Rows[rowIndex].Cells[1].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                panelMoreOption.Visible = false;
                int data = PMHDAO.Instance.DeletePMH(SoPhieu);
                if (data == 1)
                {
                    string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
                    dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
                    dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show("Đã xóa phiếu thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Xóa phiếu không thành công", "Thông báo", MessageBoxButtons.OK);
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPMH.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPMH.Rows[rowIndex].Cells[1].Value.ToString();
            fThemCTPMH f = new fThemCTPMH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
            dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnThemPMH_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            fThemPMH f = new fThemPMH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
            dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;

            // datagridview
            // Trả về chỉ số của hàng được chọn 
            int rowIndex = dtgvPMH.SelectedRows[0].Index;
            // Trả về ô thứ 1 của hàng được chọn (vì Cells[1])
            SoPhieu_selected = dtgvPMH.Rows[rowIndex].Cells[1].Value.ToString();

            fCTPMH f = new fCTPMH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
            dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
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

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            fBanHang f = new fBanHang();
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
            // xử lý xuất
            // kiểm tra có item nào được chọn không ? (1 phiếu tại 1 thời điểm)

            if (dtgvPMH.SelectedRows.Count == 0)
            {
                MessageBox.Show("Không có phiếu mua hàng được chọn");
            }
            else if (dtgvPMH.SelectedRows.Count == 1)
            {

                MessageBox.Show("Có 1 phiếu mua hàng được chọn");
                DataGridViewRow hangSelected = dtgvPMH.SelectedRows[0];
                string soPhieuSelected = hangSelected.Cells[1].Value.ToString().Trim();

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save an PDF File";
                saveFileDialog.FileName = "PMH_" + soPhieuSelected + ".pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    CreatePDF(path, soPhieuSelected);
                    MessageBox.Show("Ghi file thành công: " + path);
                    OpenPDF(path);
                }
            } else
            {
                MessageBox.Show("Vui lòng chọn 1 phiếu mua hàng để xuất PDF.");
            }
        }

        private void CreatePDF(string path, string soPhieuSelected)
        {
            DataTable x;
            string query = String.Format("select SoPhieu, NgayLap, TenNCC, NHACUNGCAP.SDT, NHACUNGCAP.DiaChi, TongTien  from PHIEUMUAHANG, NHACUNGCAP where SoPhieu = '{0}' and PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC", soPhieuSelected);
            x = DataProvider.Instance.ExecuteQuery(query);

            string soPhieuPDF = x.Rows[0][0].ToString();
            string ngayLapPhieuPDF = Convert.ToDateTime(x.Rows[0][1]).ToString("dd/MM/yyyy");
            string tenNCCPDF = x.Rows[0][2].ToString();
            string sdtPDF = x.Rows[0][3].ToString();
            string diaChiPDF = x.Rows[0][4].ToString();
            string tongTien = x.Rows[0][5].ToString();

            // Đường dẫn đến file font .ttf của bạn
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                FileStream stream = new FileStream(path, FileMode.Create);
            
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);              

                // Đặt tiêu đề
                Paragraph title = new Paragraph("THÔNG TIN PHIẾU MUA HÀNG")
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
                   .Add(new Text("\nTên nhà cung cấp: " + tenNCCPDF).SetFont(font))
                   .Add(new Text("\nSố điện thoại: " + sdtPDF).SetFont(font))
                   .Add(new Text("\nĐịa chỉ: " + diaChiPDF).SetFont(font));
                document.Add(info);
                
                
                // Thêm khoảng trống
                document.Add(new Paragraph("\n"));

                // Bảng chi tiết
                Table table = new Table(6, true);

                // Thêm header
                string[] headers = {"Tên sản phẩm", "Tên loại sản phẩm", "Số lượng", "Tên DVT", "Đơn giá mặt hàng", "Thành tiền"};
                foreach (string header in headers)
                {
                    Cell cell = new Cell().Add(new Paragraph(header).SetFont(font));
                    cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    table.AddHeaderCell(cell);
                }

                query = String.Format("select SANPHAM.TenSP, LOAISANPHAM.TenLSP, CTPMH.SoLuong, DVT.TenDVT, CTPMH.DonGiaMH, CTPMH.ThanhTien from CTPMH, SANPHAM, LOAISANPHAM, DVT where SoPhieu = '{0}' and CTPMH.MaSP = SANPHAM.MaSP and SANPHAM.MaLSP = LOAISANPHAM.MaLSP and LOAISANPHAM.MaDVT = DVT.MaDVT", soPhieuSelected);
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
            saveFileDialog.FileName = "BangPhieuMuaHang.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                DataTable dataTable = GetDataTableFromDataGridView(dtgvPMH); // Hàm lấy dữ liệu từ DataGridView
                ExportToExcel(dataTable, filePath);
            }
        }

        public void ExportToExcel(DataTable dataTable, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(dataTable, "Phieu Mua Hang");
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

        private void DashboardTrangChu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMuaHang_Click(object sender, EventArgs e)
        {

        }

        private void picBoxLogo_Click(object sender, EventArgs e)
        {

        }

        private void textDsPDV_Click(object sender, EventArgs e)
        {

        }

        private void textTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelTTTK_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void RefreshData()
        {
            string query = "SELECT SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', " +
                "TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' FROM PHIEUMUAHANG," +
                " NHACUNGCAP WHERE PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
            dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }


    }
}
