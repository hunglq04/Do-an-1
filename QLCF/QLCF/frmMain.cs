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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            pnlSetting.Visible = false;
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
    }
}
