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

namespace QLCF
{
    public partial class frmMain : Form
    {
        BanBLL dbBan;
        public frmMain()
        {
            InitializeComponent();
            dbBan = new BanBLL();
            LoadBan();

        }
        
        #region Methods
        void LoadBan()
        {
            List<Ban> dsBan = dbBan.LayDanhSachBan();
            foreach (Ban ban in dsBan)
            {
                Button btn = new Button() { Width = 90, Height = 90 };
                btn.Text = ban.MaBan;
                btn.ForeColor = Color.Black;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                if (ban.TinhTrang.Equals("Trống"))
                    btn.BackColor = Color.Transparent;
                else
                btn.BackColor = Color.Firebrick;

                if (ban.MaKV.Trim() == "KV1")
                    fpnlPhongLanh.Controls.Add(btn);
                else
                    fpnlSanVuon.Controls.Add(btn);
            }
        }
        #endregion
        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult traLoi = MessageBox.Show("Bạn có thật sự muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            if(traLoi == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (pnlSetting.Visible)
                pnlSetting.Visible = false;
            else pnlSetting.Visible = true;
        }

        private void pnlSetting_Leave(object sender, EventArgs e)
        {
            pnlSetting.Visible = false;
        }
        #endregion
    }
}
