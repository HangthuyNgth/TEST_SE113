using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QLCHVBDQ.DAO;
using ClosedXML.Excel;
using System.Diagnostics;
using System.IO;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;

namespace QLCHVBDQ
{
    public partial class fDichVu : Form
    {
        public fDichVu()
        {
            InitializeComponent();
            LoadPhieuDV();            
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

        public static string SoPhieu_selected = "Phieu1";
        void LoadPhieuDV()
        {
            textUserName.Text = fLogin.userName;
            dtgvPDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvPDV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
            dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            dtgvPDV.Columns.Add(IconCol);
            IconCol.Image = Properties.Resources.More_Vertical;
            foreach (DataGridViewColumn item in dtgvPDV.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtgvPDV.Columns[7].Width = 40;
            dtgvPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        } 



        private void dtgvPDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string r = e.RowIndex.ToString();
            string c = e.ColumnIndex.ToString();
            //MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            if (e.ColumnIndex == 0)
            {
                //dtgvPDV.CurrentRow.Selected = true;
                panelMoreOption.Location = dtgvPDV.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                panelMoreOption.Left = panelMoreOption.Left - 50;
                panelMoreOption.Top = Math.Min(panelMoreOption.Top + panelMoreOption.Height + 100, 820);
                panelMoreOption.Visible = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
            }
        }

        private void dtgvPDV_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex != -1)
            {
                if (e.ColumnIndex != 0 || e.RowIndex != dtgvPDV.CurrentCell.RowIndex)
                {
                    //dtgvPDV.CurrentRow.Selected = false;
                    panelMoreOption.Visible = false;
                }
            }
            
        }
        private void btnDelete(object sender, EventArgs e)
        {
            int count = dtgvPDV.SelectedRows.Count;
            //MessageBox.Show(String.Format("Bạn đang chọn {0} hàng ", count));            //MessageBox.Show("Bạn đang chọn " + total);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvPDV.SelectedRows[i].Index;
                        int colIndex = 1;
                        string SoPhieu = dtgvPDV.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = PDVDAO.Instance.DeletePDV(SoPhieu);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
                    dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} phiếu đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int rowIndex = dtgvPDV.SelectedRows[0].Index;
            string SoPhieu = dtgvPDV.Rows[rowIndex].Cells[1].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                panelMoreOption.Visible = false;
                int data = PDVDAO.Instance.DeletePDV(SoPhieu);
                if (data == 1)
                {
                    if (btnThoatTraCuu.Visible == true)
                    {
                        string query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where SoPhieu = '{0}'", textBoxSoPhieu.Text);
                        dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    }
                    else
                    {
                        string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
                        dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    }
                    MessageBox.Show("Đã xóa phiếu thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Xóa phiếu không thành công", "Thông báo", MessageBoxButtons.OK);
            }
                
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPDV.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPDV.Rows[rowIndex].Cells[1].Value.ToString();
            fThemCTPDV f = new fThemCTPDV();
            f.ShowDialog();
            if(btnThoatTraCuu.Visible == true)
            {
                string query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where SoPhieu = '{0}'", textBoxSoPhieu.Text);
                dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
                dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
            
        }

        private void btnThemPDV_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            fThemPDV f = new fThemPDV();
            f.ShowDialog();
            string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
            dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPDV.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPDV.Rows[rowIndex].Cells[1].Value.ToString();
            fChiTietPDV f = new fChiTietPDV();
            f.ShowDialog();
            if (btnThoatTraCuu.Visible == true)
            {
                string query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where SoPhieu = '{0}'", textBoxSoPhieu.Text);
                dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
                dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(comboBoxTieuChi.Text == "Tiêu chí")
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm");
            }
            else
            {
                if (textBoxSoPhieu.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập đúng giá trị tìm kiếm");
                }
                else
                {

                    textDsPDV.Text = "Kết quả tra cứu phiếu dịch vụ";
                    pictureBox4.Visible = false;
                    btnThemPDV.Visible = false;
                    string query;

                    if (comboBoxTieuChi.Text == "Số phiếu")
                    {
                        query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where SoPhieu = '{0}'", textBoxSoPhieu.Text);
                    }
                    else if ((comboBoxTieuChi.Text == "Ngày lập"))
                    {
                        query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where NgayLap = '{0}'", Convert.ToDateTime(textBoxSoPhieu.Text).ToString("yyyy/MM/dd"));
                    }
                    else
                    {
                        query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where TinhTrang = '{0}'", textBoxSoPhieu.Text);
                    }

                    dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    btnThoatTraCuu.Visible = true;                       
                }
            }
        }

        private void btnThoatTraCuu_Click(object sender, EventArgs e)
        {
            textDsPDV.Text = "Danh sách phiếu dịch vụ";
            pictureBox4.Visible = true;
            btnThemPDV.Visible = true;
            btnThoatTraCuu.Visible = false;

            string query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU");

            
            dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = "BangDichVu.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                DataTable dataTable = GetDataTableFromDataGridView(dtgvPDV); // Hàm lấy dữ liệu từ DataGridView
                ExportToExcel(dataTable, filePath);
            }
        }

        public void ExportToExcel(DataTable dataTable, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(dataTable, "Dich Vu");
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

        private void buttonXuatPDF_Click(object sender, EventArgs e)
        {
            if (dtgvPDV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Không có phiếu dịch vụ được chọn");
            }
            else if (dtgvPDV.SelectedRows.Count == 1)
            {

                MessageBox.Show("Có 1 phiếu dịch vụ được chọn");
                DataGridViewRow hangSelected = dtgvPDV.SelectedRows[0];
                string soPhieuSelected = hangSelected.Cells[1].Value.ToString().Trim();

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save an PDF File";
                saveFileDialog.FileName = "PDV_" + soPhieuSelected + ".pdf";

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
                MessageBox.Show("Vui lòng chọn 1 phiếu dịch vụ để xuất PDF.");
            }
        }
        private void CreatePDF(string path, string soPhieuSelected)
        {
            DataTable x;
            string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
            x = DataProvider.Instance.ExecuteQuery(query);

            string soPhieuPDF = x.Rows[0][0].ToString();
            string ngayLapPhieuPDF = Convert.ToDateTime(x.Rows[0][1]).ToString("dd/MM/yyyy");
            string maKHPDF = x.Rows[0][2].ToString();
            string tongTienPDF = x.Rows[0][3].ToString();
            string tongTTPDF = x.Rows[0][4].ToString();
            string tongConLaiPDF = x.Rows[0][5].ToString();
            string tinhTrangPDF = (x.Rows[0][6].ToString() == "True") ? "Đã hoàn thành" : "Chưa hoàn thành";

            // Đường dẫn đến file font .ttf của bạn
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

            FileStream stream = new FileStream(path, FileMode.Create);
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Đặt tiêu đề
            Paragraph title = new Paragraph("THÔNG TIN DỊCH VỤ")
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
               .Add(new Text("\nMã khách hàng: " + maKHPDF).SetFont(font))
               .Add(new Text("\nTổng tiền: " + tongTienPDF).SetFont(font))
               .Add(new Text("\nTổng trả trước: " + tongTTPDF).SetFont(font))
               .Add(new Text("\nTổng còn lại: " + tongConLaiPDF).SetFont(font))
               .Add(new Text("\nTình trạng: " + tinhTrangPDF).SetFont(font));
            document.Add(info);


            // Thêm khoảng trống
            document.Add(new Paragraph("\n"));

            // Bảng chi tiết
            Table table = new Table(8, true);

            // Thêm header
            string[] headers = { "Số phiếu", "Ngày lập", "Tên khách hàng", "Số điện thoại", "Tổng tiền", "Tổng trả trước", "Tổng còn lại", "Tình trạng" };
            foreach (string header in headers)
            {
                Cell cell = new Cell().Add(new Paragraph(header).SetFont(font));
                cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                cell.SetTextAlignment(TextAlignment.CENTER);
                table.AddHeaderCell(cell);
            }

            query = String.Format("select SoPhieu, NgayLap, KHACHHANG.TenKH, KHACHHANG.SDT, TongTien, TongTraTruoc, TongConLai, TinhTrang  from PHIEUDICHVU, KHACHHANG where SoPhieu = '{0}' and KHACHHANG.MaKH = PHIEUDICHVU.MaKH", soPhieuPDF);
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
                        // duyệt qua cột "Tình trạng" 
                        string columnName = x.Columns[j].ColumnName;
                        object columnValue = x.Rows[i][j];

                        if (columnName.ToString() == "TinhTrang")
                        {
                            if (columnValue.ToString() == "True")
                            {
                                columnValue = "Đã hoàn thành";
                            }
                            else
                            {
                                columnValue = "Chưa hoàn thành";
                            }
                        } else if (columnName == "NgayLap")
                        {   
                            columnValue = Convert.ToDateTime(columnValue).ToString("dd/MM/yyyy");
                        }

                        // Add the cell with the column value to the table.
                        Cell cell = new Cell().Add(new Paragraph(columnValue.ToString()).SetFont(font));
                        table.AddCell(cell);
                    }
                }
            }
            document.Add(table);
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

        private void TrangChu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {

        }

        private void picBoxLogo_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSoPhieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}
  