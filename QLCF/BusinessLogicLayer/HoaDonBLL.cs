using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicLayer
{
    public class HoaDonBLL
    {
        DAL dbs = new DAL();
        public HoaDonBLL()
        {
            dbs = new DAL();
        }
        //Tạo hóa đơn mới
        public bool ThemHoaDon(string maNVTN, string maBan, ref string err)
        {
            return dbs.MyExecuteNonQuery("USP_ThemHoaDon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@maNVTN", maNVTN), new SqlParameter("@maBan", maBan));
        }
        //Gọi món
        public bool GoiMon(string maHD, string tenMon, string soLuong, ref string err)
        {
            return dbs.MyExecuteNonQuery("USP_GoiMon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@maHD", maHD), new SqlParameter("@tenMon", tenMon), new SqlParameter("@soLuong", soLuong));
        }
        //Lấy mã hóa đơn chưa thanh toán
        public string MaHDChuaTT(string maBan)
        {
            try
            {
                return dbs.MyExecuteScalar("USP_LayMaHDChuaTT", CommandType.StoredProcedure, new SqlParameter("@maBan", maBan)).ToString();
            }
            catch (NullReferenceException)
            {
                return "";
            }

        }
        //Lấy danh sách gọi món
        public List<GoiMon> LayDSGoiMon(string maBan)
        {
            List<GoiMon> dsGoiMon = new List<BusinessLogicLayer.GoiMon>();
            DataTable data = dbs.ExecuteQueryDataTable("USP_LayDSGoiMon", CommandType.StoredProcedure, new SqlParameter("@maBan", maBan));
            foreach (DataRow item in data.Rows)
            {
                GoiMon mon = new BusinessLogicLayer.GoiMon(item);
                dsGoiMon.Add(mon);
            }
            return dsGoiMon;
        }
        //Lấy chi tiết hóa đơn
        public DataTable LayChiTietHD(string maHD)
        {
            return dbs.ExecuteQueryDataTable("USP_LayChiTietHD", CommandType.StoredProcedure, new SqlParameter("@maHD", maHD));
        }
        //Lấy tiền phụ thu
        public string LayTienPhuThu(string maHD)
        {
            float kq = float.Parse(dbs.MyExecuteScalar("USP_LayTienPhuThu", CommandType.StoredProcedure, new SqlParameter("@maHD", maHD)).ToString());
            //Ép kiểu về tiền Việt
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            var rep = new { Find = ",00", rep = " " };
            return string.Format(info, "{0:c}", kq).Replace(rep.Find, rep.rep);
        }
        //Lấy tổng tiền
        public string LayTongTien(string maHD)
        {
            float kq = float.Parse(dbs.MyExecuteScalar("USP_LayTongTien", CommandType.StoredProcedure, new SqlParameter("@maHD", maHD)).ToString());
            string phuThu = LayTienPhuThu(maHD).Substring(0, LayTienPhuThu(maHD).Length - 1);
            float tong = kq + float.Parse(phuThu) * 1000;
            //Ép kiểu về tiền Việt
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            var rep = new { Find = ",00", rep = " " };
            return string.Format(info, "{0:c}", tong).Replace(rep.Find, rep.rep);
        }
        //Thanh toán
        public bool ThanhToan(string maHD, ref string err)
        {
            return dbs.MyExecuteNonQuery("USP_ThanhToan", CommandType.StoredProcedure, ref err, new SqlParameter("@maHD", maHD));
        }
        //Xóa tất cả món của 1 hóa đơn
        public bool XoaCTHD(string maHD, ref string err)
        {
            return dbs.MyExecuteNonQuery("USP_XoaCTHD", CommandType.StoredProcedure, ref err, new SqlParameter("@maHD", maHD));
        }

        #region Thống kê
        //Doanh thu từng ngày trong tháng
        public DataTable TKDoanhThuTungNgay()
        {
            return dbs.ExecuteQueryDataTable("USP_TKTongTienTungNgay", CommandType.StoredProcedure, null);
        }
        //Tổng tiền tháng
        public string TKTongTienThang()
        {
            float kq = float.Parse(dbs.MyExecuteScalar("USP_TKTongTienThang", CommandType.StoredProcedure,  null).ToString());
            //Ép kiểu về tiền Việt
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            var rep = new { Find = ",00", rep = " " };
            return string.Format(info, "{0:c}", kq).Replace(rep.Find, rep.rep);
        }
        //Tổng lượt khách tháng
        public string TKTongKhachThang()
        {
            return dbs.MyExecuteScalar("USP_TKTongKhachThang", CommandType.StoredProcedure, null).ToString();
        }
        //Top 5 món được gọi nhiều nhất
        public DataTable TKTop5MonThang()
        {
            return dbs.ExecuteQueryDataTable("USP_TKTop5MonThang", CommandType.StoredProcedure, null);
        }
        //Lượt khách từng ngày trong tháng
        public DataTable TKLuotKhachNgay()
        {
            return dbs.ExecuteQueryDataTable("USP_TKLuotKhachTungNgay", CommandType.StoredProcedure, null);
        }

        //Doanh thu từng thang trong năm
        public DataTable TKDoanhThuTungThang()
        {
            return dbs.ExecuteQueryDataTable("USP_TKTongTienTungThang", CommandType.StoredProcedure, null);
        }
        //Tổng tiền năm
        public string TKTongTienNam()
        {
            float kq = float.Parse(dbs.MyExecuteScalar("USP_TKTongTienNam", CommandType.StoredProcedure, null).ToString());
            //Ép kiểu về tiền Việt
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            var rep = new { Find = ",00", rep = " " };
            return string.Format(info, "{0:c}", kq).Replace(rep.Find, rep.rep);
        }
        //Tổng lượt khách năm
        public string TKTongKhachNam()
        {
            return dbs.MyExecuteScalar("USP_TKTongKhachNam", CommandType.StoredProcedure, null).ToString();
        }
        //Top 5 món được gọi nhiều nhất năm
        public DataTable TKTop5MonNam()
        {
            return dbs.ExecuteQueryDataTable("USP_TKTop5MonNam", CommandType.StoredProcedure, null);
        }
        //Lượt khách từng thang trong năm
        public DataTable TKLuotKhachThang()
        {
            return dbs.ExecuteQueryDataTable("USP_TKTongKhachTungThang", CommandType.StoredProcedure, null);
        }
        #endregion
    }
}
