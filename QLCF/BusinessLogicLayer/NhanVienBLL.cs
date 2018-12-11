using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.SqlClient;
using System.Data;

namespace BusinessLogicLayer
{
    public class NhanVien
    {
        private string maNV;
        private string tenNV;
        private string cmnd;
        private string soDT;
        private string diaChi;
        private DateTime ngaySinh;
        private DateTime ngayVaoLam;
        public string MaNV
        {
            get
            {
                return maNV;
            }

            set
            {
                maNV = value;
            }
        }
        public string TenNV
        {
            get
            {
                return tenNV;
            }

            set
            {
                tenNV = value;
            }
        }
        public string CMND
        {
            get
            {
                return cmnd;
            }

            set
            {
                cmnd = value;
            }
        }
        public string SoDT
        {
            get
            {
                return soDT;
            }

            set
            {
                soDT = value;
            }
        }
        public string DiaChi
        {
            get
            {
                return diaChi;
            }

            set
            {
                diaChi = value;
            }
        }
        public DateTime NgaySinh
        {
            get
            {
                return ngaySinh;
            }

            set
            {
                ngaySinh = value;
            }
        }
        public DateTime NgayVaoLam
        {
            get
            {
                return ngayVaoLam;
            }

            set
            {
                ngayVaoLam = value;
            }
        }
        public NhanVien(string maNV, string tenNV, string cmnd, string soDT, string diaChi, DateTime ngaySinh, DateTime ngayVaoLam)
        {
            this.MaNV = maNV;
            this.TenNV = tenNV;
            this.CMND = cmnd;
            this.SoDT = soDT;
            this.DiaChi = diaChi;
            this.NgaySinh = ngaySinh;
            this.NgayVaoLam = ngayVaoLam;
        }
        public NhanVien(DataRow row)
        {
            this.MaNV = row["MaNV"].ToString();
            this.TenNV = row["TenNV"].ToString();
            this.CMND = row["CMND"].ToString();
            this.SoDT = row["SoDT"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.NgaySinh = DateTime.Parse(row["NgaySinh"].ToString());
            this.NgayVaoLam = DateTime.Parse(row["NgayVaoLam"].ToString());
        }
    }
    public class NhanVienBLL
    {
        DAL db;
        public NhanVienBLL()
        {
            db = new DAL();
        }
        public List<NhanVien> LayDanhSachNV()
        {
            List<NhanVien> dsNV = new List<NhanVien>();
            DataTable data = db.ExecuteQueryDataTable("USP_LoadNhanVien", CommandType.StoredProcedure);
            foreach (DataRow item in data.Rows)
            {
                NhanVien nv = new NhanVien(item);
                dsNV.Add(nv);
            }
            return dsNV;
        }
        public List<NhanVien> TimKiemNVTheoTen(string tenNV)
        {
            List<NhanVien> dsNV = new List<NhanVien>();
            DataTable data = db.ExecuteQueryDataTable("USP_TimKiemNVTheoTen", CommandType.StoredProcedure, new SqlParameter("@tenNV", tenNV));
            foreach (DataRow item in data.Rows)
            {
                NhanVien nv = new NhanVien(item);
                dsNV.Add(nv);
            }
            return dsNV;
        }
        public List<NhanVien> TimKiemNVTheoMaNV(string maNV)
        {
            List<NhanVien> dsNV = new List<NhanVien>();
            DataTable data = db.ExecuteQueryDataTable("USP_TimKiemNVTheoMa", CommandType.StoredProcedure, new SqlParameter("@maNV", maNV));
            foreach (DataRow item in data.Rows)
            {
                NhanVien nv = new NhanVien(item);
                dsNV.Add(nv);
            }
            return dsNV;
        }
        public bool InsertNV(string maNV, string tenNV, string cmnd, string soDT, string diaChi, DateTime ngaySinh, DateTime ngayVaoLam, ref string error)
        {
            return db.MyExecuteNonQuery("USP_ThemNV", CommandType.StoredProcedure, ref error, new SqlParameter("@maNV", maNV), new SqlParameter("@tenNV", tenNV), new SqlParameter("@cmnd", cmnd), new SqlParameter("@soDT", soDT),
               new SqlParameter("@diachi", diaChi), new SqlParameter("ngaysinh", ngaySinh), new SqlParameter("@ngayvaolam", ngayVaoLam));
        }
        public bool DeleteNV(string maNV, ref string error)
        {
            return db.MyExecuteNonQuery("USP_DeleteNV", CommandType.StoredProcedure, ref error, new SqlParameter("@maNV", maNV));
        }
        public bool UpdateNV(string maNV, string tenNV, string cmnd, string soDT, string diaChi, DateTime ngaySinh, DateTime ngayVaoLam, ref string error)
        {
            return db.MyExecuteNonQuery("USP_UpdateNV", CommandType.StoredProcedure, ref error, new SqlParameter("@maNV", maNV), new SqlParameter("@tenNV", tenNV), new SqlParameter("@cmnd", cmnd), new SqlParameter("@soDT", soDT),
               new SqlParameter("@diachi", diaChi), new SqlParameter("ngaysinh", ngaySinh), new SqlParameter("@ngayvaolam", ngayVaoLam));
        }
        public bool InsertNVPhucVu(string maNV, ref string error)
        {
            return db.MyExecuteNonQuery("USP_ThemNVPhucVu", CommandType.StoredProcedure, ref error, new SqlParameter("@maNV", maNV));
        }
        public bool InsertNVThuNgan(string maNV, ref string error)
        {
            return db.MyExecuteNonQuery("USP_ThemNVThuNgan", CommandType.StoredProcedure, ref error, new SqlParameter("@maNV", maNV));
        }
        public int CheckLoaiNV(string maNV)
        {
            return (int)db.MyExecuteScalar("USP_CheckLoaiNV", CommandType.StoredProcedure, new SqlParameter("@maNV", maNV));
        }
    }
}
