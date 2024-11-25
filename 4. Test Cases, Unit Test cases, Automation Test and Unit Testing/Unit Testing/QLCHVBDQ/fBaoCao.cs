
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHVBDQ
{
    public partial class fBaoCao : Form
    {
        public fBaoCao()
        {
            InitializeComponent();
            LoadBaoCao();
        }
        public static string Thang_selected = "1";
        public static string Nam_selected = "2023";

        public void LoadBaoCao()
        {
            textUserName.Text = fLogin.userName;
            dtgvBCTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvBCTonKho.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
            dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            dtgvBCTonKho.Columns.Add(IconCol);
            IconCol.Image = Properties.Resources.More_Vertical;
            foreach (DataGridViewColumn item in dtgvBCTonKho.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtgvBCTonKho.Columns[2].Width = 40;
            dtgvBCTonKho.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private void btnThemBCTK_Click(object sender, EventArgs e)
        {
            string Thang = dateTimePicker1.Value.Month.ToString();
            string Nam = dateTimePicker1.Value.Year.ToString();
            string query = "delete from BCTONKHO";
            DataProvider.Instance.ExecuteQuery(query);

            query = "select MaSP, TenSP, TonKho from SANPHAM";
            DataTable Ma_Ten_TonKho_SP = DataProvider.Instance.ExecuteQuery(query);
            int countSanPham = Ma_Ten_TonKho_SP.Rows.Count;
            for (int i = 0; i < countSanPham; i++)
            {
                string MaSP = Ma_Ten_TonKho_SP.Rows[i][0].ToString();
                string TenSP = Ma_Ten_TonKho_SP.Rows[i][1].ToString();
                string TonDau = Ma_Ten_TonKho_SP.Rows[i][2].ToString();

                query = String.Format("select sum(CTPMH.SoLuong) from PHIEUMUAHANG, CTPMH where PHIEUMUAHANG.SoPhieu = CTPMH.SoPhieu and MONTH(PHIEUMUAHANG.NgayLap) = {0} and Year(PHIEUMUAHANG.NgayLap) = {1} and MaSP = '{2}'", Thang, Nam, MaSP);
                string MuaVao = DataProvider.Instance.ExecuteScalar(query).ToString();
                if (MuaVao == "") MuaVao = "0";

                query = String.Format("select sum(CTPBH.SoLuong) from PHIEUBANHANG, CTPBH where PHIEUBANHANG.SoPhieu = CTPBH.SoPhieu and MONTH(PHIEUBANHANG.NgayLap) = {0} and Year(PHIEUBANHANG.NgayLap) = {1} and MaSP =  '{2}'", Thang, Nam, MaSP);
                string BanRa = DataProvider.Instance.ExecuteScalar(query).ToString();
                if (BanRa == "") BanRa = "0";

                string TonCuoi = (int.Parse(TonDau) + int.Parse(MuaVao) - int.Parse(BanRa)).ToString();
                query = String.Format("insert into BCTONKHO values ({0}, {1}, '{2}', {3}, {4}, {5}, {6})", Thang, Nam, MaSP, TonDau, MuaVao, BanRa, TonCuoi);
                int data = DataProvider.Instance.ExecuteNonQuery(query);
            }
            MessageBox.Show("Đã tạo báo cáo thành công");
            query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
            dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvBCTonKho.SelectedRows.Count;
            if (count > 0)
            {
                string query = "";
                if (MessageBox.Show("Bạn có thật sự muốn xóa các báo cáo đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvBCTonKho.SelectedRows[i].Index;
                        string Thang = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
                        string Nam = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();

                        query = String.Format("delete from BAOCAO where Thang = {0} and Nam = {1}", Thang, Nam);
                        int temp1 = DataProvider.Instance.ExecuteNonQuery(query);

                        query = String.Format("delete from BCTONKHO where Thang = {0} and Nam = {1}", Thang, Nam);
                        int temp = DataProvider.Instance.ExecuteNonQuery(query);
                        if (temp != -1) data = data + 1;
                    }
                    query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
                    dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} báo cáo đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }
        private void dtgvBCTonKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string r = e.RowIndex.ToString();
            //string c = e.ColumnIndex.ToString();
            ////MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            int rowIndex = dtgvBCTonKho.SelectedRows[0].Index;
            string Thang = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
            string Nam = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();
            //dtgvPBH.CurrentRow.Selected = true;
            panelMoreOption.Location = dtgvBCTonKho.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
            panelMoreOption.Left = panelMoreOption.Left - 50;
            panelMoreOption.Top = Math.Min(panelMoreOption.Top + panelMoreOption.Height + 150, 720);
            panelMoreOption.Visible = true;
            //MessageBox.Show("Bạn đang chọn hàng" + s);
            //dtgvPBH.CurrentRow.Selected = true;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvBCTonKho.SelectedRows[0].Index;
            string Thang = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
            string Nam = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn xóa báo cáo đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string query = String.Format("delete from BAOCAO where Thang = {0} and Nam = {1}", Thang, Nam);
                int data = DataProvider.Instance.ExecuteNonQuery(query);
                panelMoreOption.Visible = false;
                query = String.Format("delete from BAOCAO where Thang = {0} and Nam = {1}", Thang, Nam);
                int temp1 = DataProvider.Instance.ExecuteNonQuery(query);
                query = String.Format("delete from BCTONKHO where Thang = {0} and Nam = {1}", Thang, Nam);
                data = DataProvider.Instance.ExecuteNonQuery(query);
                if (data != -1)
                {
                    query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
                    dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show("Đã xóa báo cáo thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Xóa báo cáo không thành công", "Thông báo", MessageBoxButtons.OK);
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvBCTonKho.SelectedRows[0].Index;
            string Thang = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
            string Nam = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn làm mới báo cáo đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {

                panelMoreOption.Visible = false;

                string query = String.Format("delete from BCTONKHO where Thang = {0} and Nam = {1}", Thang, Nam);
                int data = DataProvider.Instance.ExecuteNonQuery(query);

                query = "select MaSP, TenSP, TonKho from SANPHAM";
                DataTable Ma_Ten_TonKho_SP = DataProvider.Instance.ExecuteQuery(query);
                int countSanPham = Ma_Ten_TonKho_SP.Rows.Count;
                for (int i = 0; i < countSanPham; i++)
                {
                    string MaSP = Ma_Ten_TonKho_SP.Rows[i][0].ToString();
                    string TenSP = Ma_Ten_TonKho_SP.Rows[i][1].ToString();
                    string TonCuoi = Ma_Ten_TonKho_SP.Rows[i][2].ToString();
                    // Số lượng mua vào
                    query = String.Format("select sum(CTPMH.SoLuong) from PHIEUMUAHANG, CTPMH where PHIEUMUAHANG.SoPhieu = CTPMH.SoPhieu and MONTH(PHIEUMUAHANG.NgayLap) = {0} and Year(PHIEUMUAHANG.NgayLap) = {1} and MaSP = '{2}'", Thang, Nam, MaSP);
                    string MuaVao = DataProvider.Instance.ExecuteScalar(query).ToString();
                    if (MuaVao == "") MuaVao = "0";

                    query = String.Format("select sum(CTPBH.SoLuong) from PHIEUBANHANG, CTPBH where PHIEUBANHANG.SoPhieu = CTPBH.SoPhieu and MONTH(PHIEUBANHANG.NgayLap) = {0} and Year(PHIEUBANHANG.NgayLap) = {1} and MaSP =  '{2}'", Thang, Nam, MaSP);
                    string BanRa = DataProvider.Instance.ExecuteScalar(query).ToString();
                    if (BanRa == "") BanRa = "0";
                    string TonDau = (int.Parse(TonCuoi) + int.Parse(BanRa) - int.Parse(MuaVao)).ToString();
                    query = String.Format("insert into BCTONKHO values ({0}, {1}, '{2}', {3}, {4}, {5}, {6})", Thang, Nam, MaSP, TonDau, MuaVao, BanRa, TonCuoi);
                    data = DataProvider.Instance.ExecuteNonQuery(query);
                }

                if (data != -1)
                {
                    query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
                    dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show("Đã làm mới báo cáo thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Làm mới báo cáo không thành công", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void dtgvBCTonKho_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1)
            {
                if (e.ColumnIndex != 0 || e.RowIndex != dtgvBCTonKho.CurrentCell.RowIndex)
                {
                    panelMoreOption.Visible = false;
                }
            }
        }
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvBCTonKho.SelectedRows[0].Index;
            Thang_selected = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
            Nam_selected = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();
            fCTBCTK f = new fCTBCTK();
            f.ShowDialog();
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
            if (dtgvBCTonKho.SelectedRows.Count == 0)
            {
                MessageBox.Show("Không có phiếu báo cáo tồn kho được chọn");
            }
            else if (dtgvBCTonKho.SelectedRows.Count == 1)
            {

                MessageBox.Show("Có 1 phiếu báo cáo tồn kho được chọn");
                DataGridViewRow hangSelected = dtgvBCTonKho.SelectedRows[0];
                string thangSelected = hangSelected.Cells[1].Value.ToString().Trim();
                string namSelected = hangSelected.Cells[2].Value.ToString().Trim();

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save an PDF File";
                saveFileDialog.FileName = "BCTonKho_T" + thangSelected + "-" + namSelected + ".pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    CreatePDF(path, thangSelected, namSelected);
                    MessageBox.Show("Ghi file thành công: " + path);
                    OpenPDF(path);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 phiếu báo cáo tồn kho để xuất PDF.");
            }
        }
        private void CreatePDF(string path, string thangSelected, string namSelected)
        {
            DataTable x;
            string query = String.Format("select BCTONKHO.MaSP as N'Mã sản phẩm', SANPHAM.TenSP as N'Tên sản phẩm', TonDau as N'Tồn đầu', SLMua as N'Số lượng mua vào' , SLBan as N'Số lượng bán ra', TonCuoi as N'Tồn cuối', DVT.TenDVT as N'Đơn vị tính' from BCTONKHO, SANPHAM, DVT, LOAISANPHAM where BCTONKHO.Thang = {0} and BCTONKHO.Nam = {1} and SANPHAM.MaSP = BCTONKHO.MaSP and SANPHAM.MaLSP = LOAISANPHAM.MaLSP and LOAISANPHAM.MaDVT = DVT.MaDVT", thangSelected, namSelected);
            x = DataProvider.Instance.ExecuteQuery(query);

            string thangPDF = thangSelected;
            string namPDF = namSelected;

            // Đường dẫn đến file font .ttf của bạn
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

            FileStream stream = new FileStream(path, FileMode.Create);
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Đặt tiêu đề
            Paragraph title = new Paragraph("BÁO CÁO TỒN KHO")
               .SetFont(font)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);
            document.Add(title);

            // Thêm khoảng trống
            document.Add(new Paragraph("\n"));

            Paragraph info = new Paragraph("Tháng " + thangPDF + " Năm " + namPDF)
               .SetFont(font)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);
            document.Add(info);

            // Thêm khoảng trống
            document.Add(new Paragraph("\n"));

            // Bảng chi tiết
            Table table = new Table(7, true);

            // Thêm header
            string[] headers = { "Mã sản phẩm", "Tên sản phẩm", "Tồn đầu", "Số lượng mua vào", "Số lượng bán ra", "Tồn cuối", "Đơn vị tính" };
            foreach (string header in headers)
            {
                Cell cell = new Cell().Add(new Paragraph(header).SetFont(font));
                cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                cell.SetTextAlignment(TextAlignment.CENTER);
                table.AddHeaderCell(cell);
            }

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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            DateTime currentDate = DateTime.Now;

            // Chỉ lấy phần tháng và năm để so sánh
            if (selectedDate.Year > currentDate.Year ||
                (selectedDate.Year == currentDate.Year && selectedDate.Month > currentDate.Month))
            {
                // Nếu người dùng chọn vượt quá tháng và năm hiện tại, đặt lại giá trị thành tháng và năm hiện tại
                dateTimePicker1.Value = new DateTime(currentDate.Year, currentDate.Month, 1);
            }
        }
    }
}
