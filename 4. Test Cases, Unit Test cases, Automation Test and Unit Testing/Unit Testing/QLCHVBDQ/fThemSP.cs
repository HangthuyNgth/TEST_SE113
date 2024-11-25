using Irony;
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
    public partial class fThemSP : Form
    {
        public fThemSP()
        {
            InitializeComponent();
            LoadThemSP();
        }
        public void LoadThemSP()
        {
            string query = "select MaLSP from LOAISANPHAM";
            DataTable LSP = DataProvider.Instance.ExecuteQuery(query);
            if (comboBoxMaLSP.Items.Count == 0)
            {
                for (int i = 0; i < LSP.Rows.Count; i++)
                {
                    string s = LSP.Rows[i][0].ToString();
                    comboBoxMaLSP.Items.Add(s);
                }
            }

            int data = DataProvider.Instance.ExecuteNonQuery(query);
            if (data > 0) // Kiểm tra số bản ghi được thêm vào
            {
                MessageBox.Show("Đã thêm sản phẩm thành công");
                errorLabel.Visible = false; // Ẩn thông báo lỗi nếu có
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ và đúng thông tin");
                errorLabel.Visible = true;
            }

        }

        public void btnThem_Click(object sender, EventArgs e)
        {
            string MaSP = textBoxMaSP.Text;
            string MaLSP = comboBoxMaLSP.Text;
            string TenSP = textBoxTenSP.Text;
            string TonKho = numUDTonKho.Value.ToString();
            string DonGia = numUDDonGia.Value.ToString();

            // Kiểm tra điều kiện đầu vào
            if (string.IsNullOrWhiteSpace(MaSP) || string.IsNullOrWhiteSpace(TenSP) ||
                string.IsNullOrWhiteSpace(MaLSP) || DonGia == "0" || TonKho == "0")
            {
                // Hiển thị thông báo lỗi
                errorLabel.Visible = true;
                return; // Ngăn không cho thêm sản phẩm
            }

            string query = String.Format("insert into SANPHAM values ('{0}', N'{1}', '{2}', {3}, {4})", MaSP, TenSP, MaLSP, DonGia, TonKho);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            if (data != -1)
            {
                MessageBox.Show("Đã thêm sản phẩm thành công");
                errorLabel.Visible = false; // Ẩn thông báo lỗi nếu có
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ và đúng thông tin");
                errorLabel.Visible = true;
            }
        }

        public void fThemSP_Load(object sender, EventArgs e)
        {

        }

        public void textThemSP_Click(object sender, EventArgs e)
        {

        }

        public void comboBoxMaLSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void numUDTonKho_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
