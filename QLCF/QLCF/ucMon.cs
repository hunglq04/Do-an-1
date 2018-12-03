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
    public partial class ucMon : UserControl
    {
        public static List<GoiMon> dsGoiMon = new List<GoiMon>();
        public string tenMA;
        public ucMon()
        {
            InitializeComponent();
        }

        private void ucMon_Load(object sender, EventArgs e)
        {
            checkBox1.Text = tenMA;
            textBox1.Text = "0";
            textBox1.Enabled = false;
            if (dsGoiMon != null)
                dsGoiMon.Clear();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = true;
                textBox1.Focus();
            }
            else
            {
                textBox1.Enabled = false;
                textBox1.Text = "0";
                if (dsGoiMon.Exists(m => m.TenMon.Equals(checkBox1.Text)))
                {
                    var mon = dsGoiMon.Single(m => m.TenMon == checkBox1.Text);
                    dsGoiMon.Remove(mon);
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            GoiMon data = new GoiMon(checkBox1.Text, int.Parse(textBox1.Text));
            if (textBox1.Enabled == true && textBox1.Text != "0")
                dsGoiMon.Add(data);
            else
                dsGoiMon.Remove(data);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dsGoiMon.Exists(m => m.TenMon.Equals(checkBox1.Text)))
            {
                var mon = dsGoiMon.Single(m => m.TenMon == checkBox1.Text);
                dsGoiMon.Remove(mon);
            }
        }
    }
}
