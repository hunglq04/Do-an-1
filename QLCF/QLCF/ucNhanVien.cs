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
    public partial class ucNhanVien : UserControl
    {
        NhanVienBLL nvbll;
        bool them;
        public ucNhanVien()
        {
            InitializeComponent();
            nvbll = new NhanVienBLL();
            LoadNV();
            groupBox1.Enabled = false;
            EditDataGridView(dgvNV);
        }
        public void LoadNV()
        {
            List<NhanVien> dsnv = nvbll.LayDanhSachNV();
            dgvNV.DataSource = dsnv;
            this.txtTimNV.Enabled = false;
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvNV.CurrentCell.RowIndex;
            string strNV =
        dgvNV.Rows[r].Cells[0].Value.ToString();
            txtMaNV.Text = dgvNV.Rows[r].Cells[0].Value.ToString();
            txtTenNV.Text = dgvNV.Rows[r].Cells[1].Value.ToString();
            txtCMND.Text = dgvNV.Rows[r].Cells[2].Value.ToString();
            txtSoDT.Text = dgvNV.Rows[r].Cells[3].Value.ToString();
            txtDiaChi.Text = dgvNV.Rows[r].Cells[4].Value.ToString();
            dtpNgaySinh.Text = dgvNV.Rows[r].Cells[5].Value.ToString();
            dtpNgayVaoLam.Text = dgvNV.Rows[r].Cells[6].Value.ToString();
            if (nvbll.CheckLoaiNV(strNV) == 1)
            {
                cboxLNV.SelectedIndex = 0;
            }
            if (nvbll.CheckLoaiNV(strNV) == 0)
            {
                cboxLNV.SelectedIndex = 1;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            dgvNV_CellClick(null, null);
            this.txtMaNV.ResetText();
            this.txtTenNV.ResetText();
            this.txtCMND.ResetText();
            this.txtSoDT.ResetText();
            this.txtDiaChi.ResetText();
            this.dtpNgaySinh.ResetText();
            this.dtpNgayVaoLam.ResetText();
            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.groupBox1.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            txtMaNV.Enabled = true;
            this.txtMaNV.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string error = "Có lỗi xảy ra";
            if (them)
            {
                if (nvbll.InsertNV(txtMaNV.Text,txtTenNV.Text,txtCMND.Text,txtSoDT.Text, txtDiaChi.Text, DateTime.Parse(dtpNgaySinh.Text), DateTime.Parse(dtpNgayVaoLam.Text),ref error))
                {
                    if (cboxLNV.SelectedIndex == 0)
                    {
                        if (nvbll.InsertNVPhucVu(txtMaNV.Text, ref error))
                        {
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadNV();
                        }
                        else MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (cboxLNV.SelectedIndex == 1)
                    {
                        if (nvbll.InsertNVThuNgan(txtMaNV.Text, ref error))
                        {
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadNV();
                        }
                        else MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int r = dgvNV.CurrentCell.RowIndex;
                string strNV =
                dgvNV.Rows[r].Cells[0].Value.ToString();
                if (nvbll.UpdateNV(strNV, txtTenNV.Text, txtCMND.Text, txtSoDT.Text, txtDiaChi.Text, DateTime.Parse(dtpNgaySinh.Text), DateTime.Parse(dtpNgayVaoLam.Text), ref error))
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNV();
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            this.groupBox1.Enabled = true;
            this.cboxLNV.Enabled = false;
            dgvNV_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaNV.Enabled = false;
            this.txtMaNV.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.groupBox1.Enabled = false;
            this.txtMaNV.ResetText();
            this.txtTenNV.ResetText();
            this.txtCMND.ResetText();
            this.txtSoDT.ResetText();
            this.txtDiaChi.ResetText();
            this.dtpNgaySinh.ResetText();
            this.dtpNgayVaoLam.ResetText();
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnLuu.Enabled = false;
            this.btnHuy.Enabled = false;
            dgvNV_CellClick(null, null);
            LoadNV();
        }


        private void cboxTimTheo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtTimNV.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string error = "Có lỗi xảy ra!";
            int r = dgvNV.CurrentCell.RowIndex;
            string strNV =
            dgvNV.Rows[r].Cells[0].Value.ToString();
            DialogResult traloi;
            traloi = MessageBox.Show("Chắc xóa không?", "Trả lời",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traloi == DialogResult.Yes)
            {
                if (nvbll.DeleteNV(strNV, ref error))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadNV();
                }
                else MessageBox.Show(error, "Thông báo");
            }
            else
            {
                MessageBox.Show("Không thực hiện việc xóa nhân viên!");
            }
        }

        private void txtTimNV_TextChanged(object sender, EventArgs e)
        {
            if (cboxTimTheo.SelectedIndex == 0)
            {
                if (txtTimNV.Text == null)
                {
                    MessageBox.Show("Bạn chưa nhập mã nhân viên muốn tìm!!! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else dgvNV.DataSource = nvbll.TimKiemNVTheoMaNV(txtTimNV.Text);
            }
            if (cboxTimTheo.SelectedIndex == 1)
            {
                if (txtTimNV.Text == null)
                {
                    MessageBox.Show("Bạn chưa nhập tên nhân viên muốn tìm!!! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else dgvNV.DataSource = nvbll.TimKiemNVTheoTen(txtTimNV.Text);
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
