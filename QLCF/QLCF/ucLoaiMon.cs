using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;

namespace QLCF
{
    public partial class ucLoaiMon : UserControl
    {
        MonBLL dbMon;
        public string tenLM;
        public ucLoaiMon()
        {
            InitializeComponent();
            dbMon = new MonBLL();
        }
        public void LoadMon()
        {
            List<Mon> dsMon = dbMon.LayDanhSachMon(tenLM);
            foreach(Mon mon in dsMon)
            {
                ucMon ucMon1 = new ucMon();
                ucMon1.tenMA = mon.Ten;
                fpnlMon.Controls.Add(ucMon1);
            }
        }

        private void ucLoaiMon_Load(object sender, EventArgs e)
        {
            LoadMon();
            label1.Text = tenLM;
        }
    }
}
