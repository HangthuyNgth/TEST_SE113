using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using QLCHVBDQ.DAO;

namespace QLCHVBDQ
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }
        
        public static string userName = "Nguyen Van A";
        public static string userEmail = "email";
        public static string userType = "0";
  
        public static string storeUserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public void Email_Enter(object sender, EventArgs e)
        {
            if (Email.Text == "Email")
            {
                Email.Text = "";
                Email.ForeColor = Color.Black;
            }
        }

        public void Email_Leave(object sender, EventArgs e)
        {
            if (Email.Text == "")
            {
                Email.Text = "Email";
                Email.ForeColor = Color.Silver;
            }
        }

        public void textMatKhau_Enter(object sender, EventArgs e)
        {
            if (textMatKhau.Text == "Mật khẩu")
            {
                textMatKhau.Text = "";
                textMatKhau.ForeColor = Color.Black;
                textMatKhau.PasswordChar = '*';
            }
        }

        public void textMatKhau_Leave(object sender, EventArgs e)
        {
            if (textMatKhau.Text == "")
            {
                textMatKhau.PasswordChar = '\0';
                textMatKhau.Text = "Mật khẩu";
                textMatKhau.ForeColor = Color.Silver;
            }
        }

        public void btnDangNhap_Click(object sender, EventArgs e)  // backend phai kiem tra dang nhap thanh công hay chưa, ở đây mặc định là thành công
        {
            userEmail = Email.Text; 
            string email = Email.Text;
            string password = textMatKhau.Text;
            if (Email.ForeColor == Color.Silver) email = "";
            if (textMatKhau.ForeColor == Color.Silver) password = "";
            if(Login(email, password))
            {
                string query = String.Format("select LoaiNguoiDung from NGUOIDUNG where Email = '{0}'", email);
                DataTable x = DataProvider.Instance.ExecuteQuery(query);
                userType = x.Rows[0][0].ToString();
                userName = getUserName(email, password);

                fTrangChu f = new fTrangChu();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
            } 

        }
        public string getUserName(string email, string password)
        {
            return AccountDAO.Instance.getUserName(email, password);
        }
        public bool Login(string email, string password)
        {
            return AccountDAO.Instance.Login(email, password);
        }

        public void fLogin_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát ứng dụng?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
                //Application.Exit();
            }
        }

        public void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        public void btnDangKy_Click(object sender, EventArgs e)
        {
            fDangKy f = new fDangKy();
            f.Show();
        }

        public void fLogin_Load(object sender, EventArgs e)
        {

        }

        public void picBoxBackground_Click(object sender, EventArgs e)
        {

        }

        public void DangNhap_Click(object sender, EventArgs e)
        {

        }

        public void Email_TextChanged(object sender, EventArgs e)
        {

        }

        public void label1_Click(object sender, EventArgs e)
        {

        }

        public void textMatKhau_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
