using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCF
{
    public partial class frmDangNhap : Form
    {
        DataAccessLayer.DAL db;
        public static string username = DataAccessLayer.DAL.user;
        public static string password = DataAccessLayer.DAL.pass;
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataAccessLayer.DAL.pass = txtPassword.Text;
            DataAccessLayer.DAL.user = txtUserName.Text;
            try
            {
                this.Hide();
                db = new DataAccessLayer.DAL();
                frmMain f = new frmMain();
                f.ShowDialog();
                this.Show();
            }
            catch
            {
                lblError.Visible = true;
                txtUserName.Focus();
            }
            
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                DataAccessLayer.DAL.pass = txtPassword.Text;
                DataAccessLayer.DAL.user = txtUserName.Text;
                try
                {
                    db = new DataAccessLayer.DAL();
                    frmMain f = new frmMain();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                catch
                {
                    lblError.Visible = true;
                    txtUserName.Focus();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn thoát?", "Thông báo",
               MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
                Application.Exit();
        }
    }
}
