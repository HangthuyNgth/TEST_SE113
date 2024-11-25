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
    public partial class fSuaNCC : Form
    {
        public fSuaNCC(string MaNCC)
        {
            InitializeComponent();
            LoadNCCInfo(MaNCC);
        }
        void LoadNCCInfo(string MaNCC)
        {
            string query = $"SELECT * FROM NHACUNGCAP WHERE MaNCC = '{MaNCC}'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                textBoxMaNCC.Text = MaNCC;
                textBoxTenNCC.Text = row["TenNCC"].ToString();
                textBoxDiaChi.Text = row["DiaChi"].ToString();
                textBoxSDT.Text = row["SDT"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string MaNCC = textBoxMaNCC.Text;
            string TenNCC = textBoxTenNCC.Text;
            string DiaChi = textBoxDiaChi.Text;
            string SDT = textBoxSDT.Text;

            int result = NhaCungCapDAO.Instance.Update_NCC(MaNCC, TenNCC, DiaChi, SDT);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật thông tin nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin nhà cung cấp không thành công!", "Thông báo", MessageBoxButtons.OK);
            }
        }

    }
}
