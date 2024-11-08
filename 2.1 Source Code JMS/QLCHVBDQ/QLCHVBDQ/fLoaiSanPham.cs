using ClosedXML.Excel;
using QLCHVBDQ.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHVBDQ
{
    public partial class fLoaiSanPham : Form
    {
        public fLoaiSanPham()
        {
            InitializeComponent();
            LoadLoaiSP();
        }

        void LoadLoaiSP()
        {
            textUserName.Text = fLogin.userName;
            dtgvLoaiSP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvLoaiSP.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM";
            dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
            if (dtgvLoaiSP.Columns.Count != 6)
            {
                DataGridViewImageColumn IconEditCol = new DataGridViewImageColumn();
                IconEditCol.Image = Properties.Resources.icon_edit; // Hình ảnh biểu tượng "Sửa"
                dtgvLoaiSP.Columns.Add(IconEditCol);

                DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
                dtgvLoaiSP.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvLoaiSP.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                //dtgvLDV.Columns[dtgvLDV.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
        private void btnThemLSP_Click(object sender, EventArgs e)
        {
            fThemLoaiSP f = new fThemLoaiSP();
            f.ShowDialog();
            string query = "select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM";
            dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvLoaiSP.SelectedRows.Count;
            //MessageBox.Show(String.Format("Bạn đang chọn {0} hàng ", count));            //MessageBox.Show("Bạn đang chọn " + total);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các loại sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvLoaiSP.SelectedRows[i].Index;
                        int colIndex = 1;
                        string MaLSP = dtgvLoaiSP.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = LoaiSanPhamDAO.Instance.Delete_LSP(MaLSP);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM";
                    dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} loại sản phẩm đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void dtgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) // Cột biểu tượng "sửa"
            {
                int rowIndex = e.RowIndex;
                string MaLSP = dtgvLoaiSP.Rows[rowIndex].Cells[2].Value.ToString();
                fSuaLSP f = new fSuaLSP(MaLSP);
                f.ShowDialog();
                LoadLoaiSP();

            }

            if (e.ColumnIndex == 1)
            {
                //dtgvChiTietPDV.CurrentRow.Selected = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
                int rowIndex = e.RowIndex;
                string MaLDV = dtgvLoaiSP.Rows[rowIndex].Cells[2].Value.ToString();
                if (MessageBox.Show("Bạn có thật sự muốn xóa loại sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = LoaiDichVuDAO.Instance.Delete_LDV(MaLDV);
                    if (data != -1)
                    {
                        string query = "select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM";
                        dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
                        MessageBox.Show("Đã xóa loại sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa loại sản phẩm không thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
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

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = "BangLoaiSanPham.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                DataTable dataTable = GetDataTableFromDataGridView(dtgvLoaiSP); // Hàm lấy dữ liệu từ DataGridView
                ExportToExcel(dataTable, filePath);
            }

        }


        public void ExportToExcel(DataTable dataTable, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(dataTable, "Loai San Pham");
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
                        dataRow[colIndex - 1] = row.Cells[colIndex].Value;
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (textTimKiem.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đúng giá trị tìm kiếm");
            }
            else
            {
                textDsLSP.Text = "Kết quả tra cứu loại sản phẩm";
                btnDeletes.Visible = false;
                btnThemLSP.Visible = false;
                string query;
                query = String.Format("select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM where MaLSP = '{0}'", textTimKiem.Text);
                dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
                btnThoatTraCuu.Visible = true;
            }
        }



        private void btnThoatTraCuu_Click(object sender, EventArgs e)
        {
            textDsLSP.Text = "Danh sách loại sản phẩm";
            btnDeletes.Visible = true;
            btnThemLSP.Visible = true;
            btnThoatTraCuu.Visible = false;
            string query = String.Format("select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM");
            dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void textTimKiem_MouseHover(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            TB.BorderStyle = BorderStyle.FixedSingle;

            ToolTip tt = new ToolTip();
            tt.Active = true;
            tt.SetToolTip(TB, "Vui lòng nhập mã loại sản phẩm cần tìm");
        }
    }
}
