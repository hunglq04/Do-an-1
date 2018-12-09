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
    public partial class ucThongKe : UserControl
    {
        HoaDonBLL dbHoaDon;
        public ucThongKe()
        {
            InitializeComponent();
            dbHoaDon = new HoaDonBLL();
            LoadData();
        }
        void LoadData()
        {
            lbTongKhachThang.Text = dbHoaDon.TKTongKhachThang() + " lượt";
            lbTongTienThang.Text = dbHoaDon.TKTongTienThang();
            lbTongTienNam.Text = dbHoaDon.TKTongTienNam(); ;
            lbTongKhachNam.Text = dbHoaDon.TKTongKhachNam() + " lượt";

            chart1.DataSource = dbHoaDon.TKDoanhThuTungNgay();
            chart1.Series["Series1"].XValueMember = "Ngay";
            chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chart1.Series["Series1"].YValueMembers = "TongTien";
            chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            chart2.DataSource = dbHoaDon.TKTop5MonThang();
            chart2.Series["Series1"].XValueMember = "TenMon";
            chart2.Series["Series1"].YValueMembers = "SoLuong";

            chart3.DataSource = dbHoaDon.TKLuotKhachNgay();
            chart3.Series["Series1"].XValueMember = "Ngay";
            chart3.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chart3.Series["Series1"].YValueMembers = "SoLuong";
            chart3.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            chart4.DataSource = dbHoaDon.TKDoanhThuTungThang();
            chart4.Series["Series1"].XValueMember = "Thang";
            chart4.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chart4.Series["Series1"].YValueMembers = "TongTien";
            chart4.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            chart6.DataSource = dbHoaDon.TKTop5MonNam();
            chart6.Series["Series1"].XValueMember = "TenMon";
            chart6.Series["Series1"].YValueMembers = "SoLuong";

            chart5.DataSource = dbHoaDon.TKLuotKhachThang();
            chart5.Series["Series1"].XValueMember = "Thang";
            chart5.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chart5.Series["Series1"].YValueMembers = "SoLuong";
            chart5.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
        }
    }
}
