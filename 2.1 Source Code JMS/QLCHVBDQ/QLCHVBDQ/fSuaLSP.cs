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

namespace QLCHVBDQ
{
    public partial class fSuaLSP : Form
    {
        public fSuaLSP(string MaLSP)
        {
            InitializeComponent();
            LoadLDVInfo(MaLSP);
        }
        void LoadLDVInfo(string MaLSP)
        {
            string query = $"SELECT * FROM LOAISANPHAM WHERE MaLSP = '{MaLSP}'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                textBoxMaLSP.Text = MaLSP;
                textBoxTenLSP.Text = row["TenLSP"].ToString();
                textBoxMaDVT.Text = row["MaDVT"].ToString();
                numUDPTLN.Value = Convert.ToDecimal(row["PhanTramLoiNhuan"]);
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            string MaLDV = textBoxMaLSP.Text;
            string TenLDV = textBoxTenLSP.Text;
            string MaDVT = textBoxMaDVT.Text;
            float PhanTramLoiNhuan = (float)numUDPTLN.Value;

            int result = LoaiSanPhamDAO.Instance.Update_LSP(MaLDV, TenLDV, PhanTramLoiNhuan);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật thông tin loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin loại sản phẩm không thành công!", "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}
