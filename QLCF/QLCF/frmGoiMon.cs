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
        List<GoiMon> dsGoiMon;
        public frmGoiMon()
        {
            InitializeComponent();
            dbLoaiMon = new LoaiMonBLL();
            dbMon = new MonBLL();
        }

        private void frmGoiMon_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            textBox1.Enabled = false;
            List<LoaiMon> dsLoaiMon = dbLoaiMon.LayDSLoaiMon();
            foreach(LoaiMon lm in dsLoaiMon)
            {
                ucLoaiMon ucLoaiMon1 = new ucLoaiMon();
                ucLoaiMon1.tenLM = lm.Ten;
                flowLayoutPanel1.Controls.Add(ucLoaiMon1);
                //ucLoaiMon1.LoadMon();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            List<Mon> dsMon = dbMon.TimKiemMon(textBox1.Text);
            foreach (Mon mon in dsMon)
            {
                ucMon ucMon1 = new ucMon();
                ucMon1.tenMA = mon.Ten;
                flowLayoutPanel1.Controls.Add(ucMon1);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            btnDone.Visible = false;
            btnSearch.Visible = true;
            textBox1.ResetText();
            flowLayoutPanel1.Controls.Clear();
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Visible = false;
            btnDone.Visible = true;
            textBox1.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
                dataGridView1.DataSource = null;
            dataGridView1.DataSource = ucMon.dsGoiMon;
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.DataSource = null;
            LoadData();
        }
    }
}
