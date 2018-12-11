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
    public partial class frmHoaDon : Form
    {
        HoaDonBLL dbHoaDon;
        public bool thanhToan;
        public frmHoaDon()
        {
            InitializeComponent();
            dbHoaDon = new HoaDonBLL();
            thanhToan = false;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            dgvChiTietHD.DataSource = dbHoaDon.LayChiTietHD(frmMain.maHD);
            lbNgayLap.Text = DateTime.Today.ToShortDateString();
            lbMaNVTN.Text = frmMain.maNVTN;
            lbSoHD.Text = frmMain.maHD;
            lbPhuThu.Text = dbHoaDon.LayTienPhuThu(frmMain.maHD);
            lbTongTien.Text = dbHoaDon.LayTongTien(frmMain.maHD);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string err = "";
            if(dbHoaDon.ThanhToan(frmMain.maHD, ref err))
            {
                MessageBox.Show("Thanh toán thành công!");
                thanhToan = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(err, "Đã có lỗi xãy ra!");
            }
        }
    }
}
