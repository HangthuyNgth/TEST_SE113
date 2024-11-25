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
using ClosedXML.Excel;
using System.Diagnostics;

namespace QLCHVBDQ
{
    public partial class fSanPham : Form
    {
        public fSanPham()
        {
            InitializeComponent();
            LoadSP();
        }
        void LoadSP()
        {

            textUserName.Text = fLogin.userName;
            dtgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvSanPham.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM";
            dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
            if (dtgvSanPham.Columns.Count != 6)
            {
                DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
                dtgvSanPham.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvSanPham.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgvSanPham.Columns[dtgvSanPham.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
        public void btnThemSP_Click(object sender, EventArgs e)
        {
            fThemSP f = new fThemSP();
            f.ShowDialog();
            string query = "select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM";
            dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }


        public void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvSanPham.SelectedRows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvSanPham.SelectedRows[i].Index;
                        int colIndex = 1;
                        string MaSP = dtgvSanPham.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = SanPhamDAO.Instance.DeleteSP_MaSanPham(MaSP);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM";
                    dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} sản phẩm đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        public void dtgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowIndex = e.RowIndex;
                string MaSP = dtgvSanPham.Rows[rowIndex].Cells[1].Value.ToString();
                if (MessageBox.Show("Bạn có thật sự muốn xóa sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = SanPhamDAO.Instance.DeleteSP_MaSanPham(MaSP);
                    if (data != -1)
                    {
                        string query = "select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM";
                        dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
                        MessageBox.Show("Xóa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa sản phẩm không thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        public static implicit operator fSanPham(fDichVu v)
        {
            throw new NotImplementedException();
        }
        public void btnDichVu_Click(object sender, EventArgs e)
        {
            fDichVu f = new fDichVu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        public void btnLoaiSanPham_Click(object sender, EventArgs e)
        {
            fLoaiSanPham f = new fLoaiSanPham();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        public void btnLoaiDichVu_Click(object sender, EventArgs e)
        {
            fLoaiDichVu f = new fLoaiDichVu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        public void btnMuaHang_Click(object sender, EventArgs e)
        {
            fMuaHang f = new fMuaHang();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        public void btnBanHang_Click(object sender, EventArgs e)
        {
            fBanHang f = new fBanHang();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        public void btnNCC_Click(object sender, EventArgs e)
        {
            fNhaCungCap f = new fNhaCungCap();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        public void btnBaoCao_Click(object sender, EventArgs e)
        {
            fBaoCao f = new fBaoCao();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        public void btnTrangChu_Click(object sender, EventArgs e)
        {
            fTrangChu f = new fTrangChu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        public void btnThongTinTaiKhoan_Click(object sender, EventArgs e)
        {
            panelTTTK.Visible = false;
            fThongTinTaiKhoan f = new fThongTinTaiKhoan();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        public void buttonDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }

        }
        public void pictureBox3_Click(object sender, EventArgs e)
        {
            if (panelTTTK.Visible == true) panelTTTK.Visible = false;
            else panelTTTK.Visible = true;
        }

        public void dtgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = "BangSanPham.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                DataTable dataTable = GetDataTableFromDataGridView(dtgvSanPham); // Hàm lấy dữ liệu từ DataGridView
                ExportToExcel(dataTable, filePath);
            }
        }


            public void ExportToExcel(DataTable dataTable, string filePath)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(dataTable, "San Pham");
                    workbook.SaveAs(filePath);
                }

                // Mở file sau khi lưu
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        

        public DataTable GetDataTableFromDataGridView(DataGridView dgv)
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
                        dataRow[colIndex - 1] = row.Cells[colIndex].Value;
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {
            if (textTimKiem.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đúng giá trị tìm kiếm");
            }
            else
            {
                textDsSP.Text = "Kết quả tra cứu sản phẩm";
                btnDeletes.Visible = false;
                btnThemSP.Visible = false;
                string query;
                query = String.Format("select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM where MaSP = '{0}'", textTimKiem.Text);
                dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
                btnThoatTraCuu.Visible = true;
            }

        }

        public void textTimKiem_MouseHover(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            TB.BorderStyle = BorderStyle.FixedSingle;

            ToolTip tt = new ToolTip();
            tt.Active = true;
            tt.SetToolTip(TB, "Vui lòng nhập mã sản phẩm cần tìm");
        }

        public void btnThoatTraCuu_Click(object sender, EventArgs e)
        {
            textDsSP.Text = "Danh sách sản phẩm";
            btnDeletes.Visible = true;
            btnThemSP.Visible = true;
            btnThoatTraCuu.Visible = false;
            string query = String.Format("select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM");
            dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        public void picBoxLogo_Click(object sender, EventArgs e)
        {

        }

        public void btnSanPham_Click(object sender, EventArgs e)
        {

        }

        public void DashboardTrangChu_Paint(object sender, PaintEventArgs e)
        {

        }

        public void TrangChu_Paint(object sender, PaintEventArgs e)
        {

        }

        public void textDsSP_Click(object sender, EventArgs e)
        {

        }

        public void panelTTTK_Paint(object sender, PaintEventArgs e)
        {

        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void textUserName_Click(object sender, EventArgs e)
        {

        }

        public void panelUserName_Paint(object sender, PaintEventArgs e)
        {

        }

        public void textTimKiem_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
