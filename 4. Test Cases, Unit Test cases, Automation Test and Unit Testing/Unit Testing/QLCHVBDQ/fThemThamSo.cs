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
    public partial class fThemThamSo : Form
    {
        public float GiaTri { get; private set; }
        public fThemThamSo()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (float.TryParse(textBoxGiaTri.Text, out float giaTri))
            {
                GiaTri = giaTri;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Giá trị không hợp lệ!");
            }
        }
    }
}
