using QLCHVBDQ.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHVBDQ
{
    public partial class fSuaLDV : Form
    {
        public fSuaLDV(string MaLDV)
        {
            InitializeComponent();
            LoadLDVInfo(MaLDV);
        }
        void LoadLDVInfo(string MaLDV)
        {
            string query = $"SELECT * FROM LOAIDICHVU WHERE MaLDV = '{MaLDV}'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                textBoxMaLDV.Text = MaLDV;
                textBoxTenLDV.Text = row["TenLDV"].ToString();
                numUDDonGia.Value = Convert.ToDecimal(row["DonGia"]);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string MaLDV = textBoxMaLDV.Text;
            string TenLDV = textBoxTenLDV.Text;
            float DonGia = (float)numUDDonGia.Value;

            int result = LoaiDichVuDAO.Instance.Update_LDV(MaLDV, TenLDV, DonGia);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật thông tin loại dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin loại dịch vụ không thành công!", "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}
