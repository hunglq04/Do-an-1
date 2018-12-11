using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using DataAccessLayer;
using System.Data.SqlClient;

namespace QLCF
{
    public partial class frmDoiMatKhau : Form
    {
        UsersBLL ubll;
        public frmDoiMatKhau()
        {
            InitializeComponent();
            ubll = new UsersBLL();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string err = "";
            if(txtCurrentPass.Text != DAL.pass)
            {
                MessageBox.Show("Mật khẩu không chính xác!");
                return;
            }
            if(txtNewPass.Text == "")
            {
                MessageBox.Show("Mật khẩu không hợp lệ!");
                return;
            }
            if(txtRetype.Text != txtNewPass.Text)
            {
                MessageBox.Show("Nhập lại mật khẩu không chính xác!");
                return;
            }
            if (ubll.UpdateUser(DAL.user, txtNewPass.Text.Trim(), ref err))
            {
                MessageBox.Show("Đổi mật khẩu thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show(err, "Có lỗi xảy ra!");
            }

        }
    }
}
