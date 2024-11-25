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
    public partial class fThemPMH : Form
    {
        public fThemPMH()
        {
            InitializeComponent();
            dateNgayLapPhieu.MaxDate = DateTime.Now;
            LoadPMH();
        }

        public void LoadPMH()
        {
            string query = "select MaNCC from NHACUNGCAP";
            DataTable MaNCC = DataProvider.Instance.ExecuteQuery(query);
            if (comboBoxMaNCC.Items.Count == 0)
            {
                for (int i = 0; i < MaNCC.Rows.Count; i++)
                {
                    string s = MaNCC.Rows[i][0].ToString();
                    comboBoxMaNCC.Items.Add(s);
                }
            }
        }
        public void btnThem_Click(object sender, EventArgs e)
        {
            string SoPhieu = textBoxSoPhieu.Text.Trim();
            string NgayLap = dateNgayLapPhieu.Value.ToString("yyyy/MM/dd");
            string MaNCC = comboBoxMaNCC.Text;

            if (SoPhieu == "" || NgayLap == "" || MaNCC == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                int opt = PMHDAO.Instance.InsertPMH(SoPhieu, NgayLap, MaNCC);
                if (opt == 0)
                {
                    MessageBox.Show(String.Format("Số phiếu {0} phiếu đã tồn tại. Vui lòng nhập lại.", SoPhieu));
                }
                else if (opt == 1)
                {
                    MessageBox.Show(String.Format("Thêm phiếu mua hàng thành công"));
                }
                else
                {
                    MessageBox.Show(String.Format("Thêm phiếu không thành công"));
                }
            }


        }

        public void textThemPBH_Click(object sender, EventArgs e)
        {

        }

        public void textSoPhieu_Click(object sender, EventArgs e)
        {

        }

        public void textNgapLapPhieu_Click(object sender, EventArgs e)
        {

        }

        public void textTenNCC_Click(object sender, EventArgs e)
        {

        }

        public void comboBoxMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
