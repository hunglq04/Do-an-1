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
    public partial class ucCacMon : UserControl
    {
        MonBLL monbll;
        LoaiMonBLL loaimonbll;
        bool them;
        public ucCacMon()
        {
            InitializeComponent();
            monbll = new MonBLL();
            loaimonbll = new LoaiMonBLL();
            EditDataGridView(dgvLoaiMon);
            EditDataGridView(dgvMon);

        }
        public void LoadMon()
        {

            List<LoaiMon> dsloaimon = loaimonbll.LayDSLoaiMon();
            this.groupBox1.Enabled = false;
            dgvLoaiMon.DataSource = dsloaimon;
            this.txtMLM.Enabled = false;
            this.txtTenLM.Enabled = false;

        }

        private void ucCacMon_Load(object sender, EventArgs e)
        {
            LoadMon();
            dgvMon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMon.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvMon.AllowUserToOrderColumns = true;
            dgvMon.AllowUserToResizeColumns = true;
        }


        private void dgvMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvMon.CurrentCell.RowIndex;
            string strMon =
        dgvMon.Rows[r].Cells[0].Value.ToString();
            txtMaMon.Text = dgvMon.Rows[r].Cells[0].Value.ToString();
            txtTenMon.Text = dgvMon.Rows[r].Cells[1].Value.ToString();
            txtGiaTien.Text = dgvMon.Rows[r].Cells[3].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            dgvMon_CellClick(null, null);
            this.txtMaMon.ResetText();
            this.txtTenMon.ResetText();
            this.txtGiaTien.ResetText();
            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.groupBox1.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            txtMaMon.Enabled = true;
            this.txtMaMon.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string error = "Có lỗi xảy ra!";
            int r = dgvMon.CurrentCell.RowIndex;
            string strMon =
            dgvMon.Rows[r].Cells[0].Value.ToString();
            DialogResult traloi;
            traloi = MessageBox.Show("Chắc xóa không?", "Trả lời",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traloi == DialogResult.Yes)
            {
                if (monbll.DeleteMon(strMon, ref error))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadMon();
                    dgvMon.DataSource = monbll.LayDanhSachMon(txtTenLM.Text);
                }
                else MessageBox.Show(error, "Thông báo");
            }
            else
            {
                MessageBox.Show("Không thực hiện việc xóa món!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            this.groupBox1.Enabled = true;
            dgvMon_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaMon.Enabled = false;
            this.txtTenMon.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.groupBox1.Enabled = false;
            this.txtMaMon.ResetText();
            this.txtTenMon.ResetText();
            this.txtGiaTien.ResetText();
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnLuu.Enabled = false;
            this.btnHuy.Enabled = false;
            dgvMon_CellClick(null, null);
            LoadMon();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string error = "Có lỗi xảy ra";
            if (them)
            {
                if (monbll.InsertMon(txtMaMon.Text, txtTenMon.Text, txtMLM.Text, float.Parse(txtGiaTien.Text), ref error))
                {
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMon();
                    dgvMon.DataSource = monbll.LayDanhSachMon(txtTenLM.Text);
                }
                else MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int r = dgvMon.CurrentCell.RowIndex;
                string strMon =
                dgvMon.Rows[r].Cells[0].Value.ToString();
                if (monbll.UpdateMon(strMon, txtTenMon.Text, float.Parse(txtGiaTien.Text), ref error))
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMon();
                    dgvMon.DataSource = monbll.LayDanhSachMon(txtTenLM.Text);
                }
                else MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.groupBox1.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnHuy.Enabled = false;
            this.btnLuu.Enabled = false;
        }

        private void dgvLoaiMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvLoaiMon.CurrentCell.RowIndex;
            txtMLM.Text = dgvLoaiMon.Rows[r].Cells[0].Value.ToString();
            txtTenLM.Text = dgvLoaiMon.Rows[r].Cells[1].Value.ToString();
            string strMon =
        dgvLoaiMon.Rows[r].Cells[1].Value.ToString();
            dgvMon.DataSource = monbll.LayDanhSachMon(strMon);

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
