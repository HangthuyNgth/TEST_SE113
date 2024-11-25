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
    public partial class fLoaiDichVu : Form
    {
        public fLoaiDichVu()
        {
            InitializeComponent();
            LoadLDV();
        }
        void LoadLDV()
        {
            textUserName.Text = fLogin.userName;
            dtgvLDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvLDV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select MaLDV as N'Mã loại dịch vụ', TenLDV as N'Tên loại dịch vụ', DonGia as N'Đơn giá' from LOAIDICHVU";
            dtgvLDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            if (dtgvLDV.Columns.Count != 5)
            {
                DataGridViewImageColumn IconEditCol = new DataGridViewImageColumn();
                IconEditCol.Image = Properties.Resources.icon_edit; // Hình ảnh biểu tượng "Sửa"
                dtgvLDV.Columns.Add(IconEditCol);

                DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
                dtgvLDV.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvLDV.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                //dtgvLDV.Columns[dtgvLDV.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
        private void btnThemLDV_Click(object sender, EventArgs e)
        {
            fThemLDV f = new fThemLDV();
            f.ShowDialog();
            string query = "select MaLDV as N'Mã loại dịch vụ', TenLDV as N'Tên loại dịch vụ', DonGia as N'Đơn giá' from LOAIDICHVU";
            dtgvLDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvLDV.SelectedRows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các loại dịch vụ đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvLDV.SelectedRows[i].Index;
                        int colIndex = 1;
                        string MaLDV = dtgvLDV.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = LoaiDichVuDAO.Instance.Delete_LDV(MaLDV);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select MaLDV as N'Mã loại dịch vụ', TenLDV as N'Tên loại dịch vụ', DonGia as N'Đơn giá' from LOAIDICHVU";
                    dtgvLDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} loại dịch vụ đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void dtgvLDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int r = e.RowIndex;
            //int c = e.ColumnIndex;
            //MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);

            if (e.ColumnIndex == 0) // Cột biểu tượng "sửa"
            {
                int rowIndex = e.RowIndex;
                string MaLDV = dtgvLDV.Rows[rowIndex].Cells[2].Value.ToString();
                fSuaLDV f = new fSuaLDV(MaLDV);
                f.ShowDialog();
                LoadLDV();

            }

            if (e.ColumnIndex == 1)
            {
                //dtgvChiTietPDV.CurrentRow.Selected = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
                int rowIndex = e.RowIndex;
                string MaLDV = dtgvLDV.Rows[rowIndex].Cells[2].Value.ToString();
                if (MessageBox.Show("Bạn có thật sự muốn xóa loại dịch vụ đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = LoaiDichVuDAO.Instance.Delete_LDV(MaLDV);
                    if (data != -1)
                    {
                        string query = "select MaLDV as N'Mã loại dịch vụ', TenLDV as N'Tên loại dịch vụ', DonGia as N'Đơn giá' from LOAIDICHVU";
                        dtgvLDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                        MessageBox.Show("Đã xóa loại dịch vụ thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa loại dịch vụ không thành công", "Thông báo", MessageBoxButtons.OK);
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
            saveFileDialog.FileName = "BangLoaiDichVu.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                DataTable dataTable = GetDataTableFromDataGridView(dtgvLDV); // Hàm lấy dữ liệu từ DataGridView
                ExportToExcel(dataTable, filePath);
            }
        }

        public void ExportToExcel(DataTable dataTable, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(dataTable, "Loai Dich Vu");
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
                textDsLDV.Text = "Kết quả tra cứu loại dịch vụ";
                btnDeletes.Visible = false;
                btnThemLDV.Visible = false;
                string query;
                query = String.Format("select MaLDV as N'Mã loại dịch vụ', TenLDV as N'Tên loại dịch vụ', DonGia as N'Đơn giá' from LOAIDICHVU where MaLDV = '{0}'", textTimKiem.Text);
                dtgvLDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                btnThoatTraCuu.Visible = true;
            }
        }

        private void textTimKiem_MouseHover(object sender, EventArgs e)
        {
            textDsLDV.Text = "Danh sách loại dịch vụ";
            btnDeletes.Visible = true;
            btnThemLDV.Visible = true;
            btnThoatTraCuu.Visible = false;
            string query = String.Format("select MaLDV as N'Mã loại dịch vụ', TenLDV as N'Tên loại dịch vụ', DonGia as N'Đơn giá'");
            dtgvLDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
