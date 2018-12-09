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
    public partial class frmGoiMon : Form
    {
        LoaiMonBLL dbLoaiMon;
        MonBLL dbMon;
        HoaDonBLL dbHoaDon;
        public frmGoiMon()
        {
            InitializeComponent();
            dbLoaiMon = new LoaiMonBLL();
            dbMon = new MonBLL();
            dbHoaDon = new HoaDonBLL();
        }
        private void frmGoiMon_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            List<LoaiMon> dsLoaiMon = dbLoaiMon.LayDSLoaiMon();
            foreach(LoaiMon lm in dsLoaiMon)
            {
                ucLoaiMon ucLoaiMon1 = new ucLoaiMon();
                ucLoaiMon1.tenLM = lm.Ten;
                flowLayoutPanel1.Controls.Add(ucLoaiMon1);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string err = "";
            foreach(GoiMon mon in ucMon.dsGoiMon)
            {
                if (dbHoaDon.GoiMon(frmMain.maHD, mon.TenMon, mon.SoLuong.ToString(), ref err))
                {

                }
                else
                {
                    MessageBox.Show(err, "Đã có lỗi xãy ra!");
                    return;
                }
            }
            ucMon.dsGoiMon.Clear();
            this.Close();
        }
    }
}
