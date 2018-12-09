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
    public partial class frmChinhSua : Form
    {
        HoaDonBLL dbHoaDon;
        public frmChinhSua()
        {
            InitializeComponent();
            dbHoaDon = new HoaDonBLL();
        }

        private void frmChinhSua_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = frmMain.dsGoiMon;
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show("Chỉ nhập vào số!");
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string err = "";
            if(dbHoaDon.XoaCTHD(frmMain.maHD, ref err))
            {
                foreach(GoiMon mon in frmMain.dsGoiMon)
                {
                    if(dbHoaDon.GoiMon(frmMain.maHD, mon.TenMon, mon.SoLuong.ToString(), ref err))
                    {

                    }
                    else
                    {
                        MessageBox.Show(err, "Đã có lỗi xãy ra!");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show(err, "Đã xãy ra lỗi!");
            }
            this.Close();
        }
        
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.UpdateCellValue(e.ColumnIndex, e.RowIndex);
            frmMain.dsGoiMon = dataGridView1.DataSource as List<BusinessLogicLayer.GoiMon>;
        }
    }
}
