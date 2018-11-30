using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.SqlClient;

namespace BusinessLogicLayer
{
    public class Mon
    {
        private string maMon;
        private string ten;
        private string maLoaiMon;
        private float gia;

        public string MaMon
        {
            get
            {
                return maMon;
            }

            set
            {
                maMon = value;
            }
        }

        public string Ten
        {
            get
            {
                return ten;
            }

            set
            {
                ten = value;
            }
        }

        public string MaLoaiMon
        {
            get
            {
                return maLoaiMon;
            }

            set
            {
                maLoaiMon = value;
            }
        }

        public float Gia
        {
            get
            {
                return gia;
            }

            set
            {
                gia = value;
            }
        }

        public Mon(string maMon, string ten, string maLoaiMon, float gia)
        {
            this.MaMon = maMon;
            this.Ten = ten;
            this.MaLoaiMon = maLoaiMon;
            this.Gia = gia;
        }
        public Mon(DataRow row)
        {
            this.MaMon = row["MaMon"].ToString();
            this.Ten = row["TenMon"].ToString();
            this.MaLoaiMon = row["MaLoaiMon"].ToString();
            this.Gia = float.Parse(row["Gia"].ToString());
        }
    }
    public class MonBLL
    {
        DAL db;
        public MonBLL()
        {
            db = new DAL();
        }
        public List<Mon> LayDanhSachMon(string tenLM)
        {
            List<Mon> dsMon = new List<Mon>();
            DataTable data = db.ExecuteQueryDataTable("USP_LayMonTheoLM", CommandType.StoredProcedure, new SqlParameter("@tenLM", tenLM));
            foreach(DataRow item in data.Rows)
            {
                Mon mon = new Mon(item);
                dsMon.Add(mon);
            }
            return dsMon;
        }
        public List<Mon> TimKiemMon(string ten)
        {
            List<Mon> dsMon = new List<Mon>();
            DataTable data = db.ExecuteQueryDataTable("USP_TimKiemMon", CommandType.StoredProcedure, new SqlParameter("@ten", ten));
            foreach (DataRow item in data.Rows)
            {
                Mon mon = new Mon(item);
                dsMon.Add(mon);
            }
            return dsMon;
        }
    }
}
