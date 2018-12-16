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
            EditDataGridView(dgvChiTietHD);
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
        void EditDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan; //Color.FromArgb(238, 239, 249)
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 233, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14F, GraphicsUnit.Pixel);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 15.5F, GraphicsUnit.Pixel);
        }
    }
}
