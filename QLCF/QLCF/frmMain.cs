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
        HoaDonBLL dbHoaDon;
        List<Ban> dsBan;
        string tinhTrang;
        public static string maHD;
        public static string maNVTN;
        public static List<GoiMon> dsGoiMon;
        ucThongKe ucThongKe1;
        public frmMain()
        {
            InitializeComponent();
            dbBan = new BanBLL();
            dbHoaDon = new HoaDonBLL();
            maNVTN = lbMaNVTN.Text;
            btnNV.Visible = false;
            btnMon.Visible = false;
            LoadBan();

        }
        
        #region Methods
        void LoadBan()
        {
            dsBan = dbBan.LayDanhSachBan();
            foreach (Ban ban in dsBan)
            {
                Button btn = new Button() { Width = 90, Height = 90 };
                btn.Text = ban.MaBan;
                btn.ForeColor = Color.Black;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.Click += btn_Click;
                btn.Tag = ban.TinhTrang;

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
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            tinhTrang = btn.Tag.ToString();
            lbMaBan.Text = btn.Text;
            lbMaHD.Text = dbHoaDon.MaHDChuaTT(btn.Text);
            dgvGoiMon.DataSource = dbHoaDon.LayDSGoiMon(btn.Text);
            dgvGoiMon.CurrentCell = null;
        }
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
        private void btnGoiMon_Click(object sender, EventArgs e)
        {
            if(lbMaBan.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn bàn cần gọi món!");
                return;
            }
            if(tinhTrang.Equals("Trống"))
            {
                string err = "";
                if(dbHoaDon.ThemHoaDon(lbMaNVTN.Text, lbMaBan.Text, ref err))
                {
                    maHD = lbMaHD.Text = dbHoaDon.MaHDChuaTT(lbMaBan.Text);
                    frmGoiMon fGoiMon = new frmGoiMon();
                    fGoiMon.ShowDialog();
                    dgvGoiMon.DataSource = dbHoaDon.LayDSGoiMon(lbMaBan.Text);
                    fpnlPhongLanh.Controls.Clear();
                    fpnlSanVuon.Controls.Clear();
                    LoadBan();
                }
                else
                {
                    MessageBox.Show(err, "Đã xảy ra lỗi khi thêm hóa đơn!");
                    return;
                }
            }
            else
            {
                maHD = lbMaHD.Text;
                frmGoiMon fGoiMon = new frmGoiMon();
                fGoiMon.ShowDialog();
                dgvGoiMon.DataSource = dbHoaDon.LayDSGoiMon(lbMaBan.Text);
                fpnlPhongLanh.Controls.Clear();
                fpnlSanVuon.Controls.Clear();
                LoadBan();
            }
        }
        

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if(lbMaHD.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn bàn đã gọi món để thanh toán!");
                return; 
            }
            maHD = lbMaHD.Text;
            frmHoaDon fHoaDon = new frmHoaDon();
            fHoaDon.ShowDialog();
            if(fHoaDon.thanhToan)
            {
                dgvGoiMon.DataSource = ucMon.dsGoiMon;
                lbMaBan.Text = "";
                lbMaHD.Text = "";
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<Ban> dsBan1 = dbBan.LayDanhSachBan();
            bool check = true;
            string status;
            foreach(Ban ban in dsBan1)
            {
                status = ban.TinhTrang;
                var tinhTrang = from t in dsBan where t.MaBan == ban.MaBan select t.TinhTrang;
                foreach(var t in tinhTrang)
                {
                    status = t;
                }
                if(status != ban.TinhTrang)
                {
                    check = false;
                    break;
                }
            }
            if (!check)
            {
                fpnlPhongLanh.Controls.Clear();
                fpnlSanVuon.Controls.Clear();
                LoadBan();
            }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            maHD = lbMaHD.Text;
            if(lbMaHD.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn bàn đã gọi món để chỉnh sửa!");
                return;
            }
            //dgvGoiMon.Rows[0].Cells[1].Selected = true;
            dsGoiMon = dgvGoiMon.DataSource as List<GoiMon>;
            frmChinhSua fChinhSua = new frmChinhSua();
            fChinhSua.ShowDialog();
            dgvGoiMon.DataSource = dbHoaDon.LayDSGoiMon(lbMaBan.Text);
        }
        #endregion

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            
            if (btnNV.Visible == true)
            {
                btnNV.Visible = false;
                btnMon.Visible = false;
            }
            else
            {
                btnNV.Visible = true;
                btnMon.Visible = true;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            btnNV.Visible = false;
            btnMon.Visible = false;
            ucThongKe1.Visible = false;

        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            btnNV.Visible = false;
            btnMon.Visible = false;
            ucThongKe1 = new ucThongKe();
            //pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(ucThongKe1);
            ucThongKe1.BringToFront();
        }

        private void btnNV_Click(object sender, EventArgs e)
        {
            
        }

        private void btnMon_Click(object sender, EventArgs e)
        {
            
        }
    }
}
