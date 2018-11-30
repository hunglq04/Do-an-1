using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class Ban
    {
        private string maBan;
        private string ten;
        private string maKV;
        private string tinhTrang;
        public string MaBan
        {
            get
            {
                return maBan;
            }

            set
            {
                maBan = value;
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
        public string MaKV
        {
            get
            {
                return maKV;
            }

            set
            {
                maKV = value;
            }
        }
        public string TinhTrang
        {
            get
            {
                return tinhTrang;
            }

            set
            {
                tinhTrang = value;
            }
        }

        public Ban(string maBan, string ten, string maKV, string tinhTrang)
        {
            this.MaBan = maBan;
            this.Ten = ten;
            this.MaKV = maKV;
            this.TinhTrang = tinhTrang;
        }
        public Ban(DataRow row)
        {
            this.MaBan = row["MaBan"].ToString();
            this.Ten = row["TenBan"].ToString();
            this.MaKV = row["MaKhuVuc"].ToString();
            this.TinhTrang = row["TrangThai"].ToString();
        }
    }
    public class BanBLL
    {
        DAL db;
        public BanBLL()
        {
            db = new DAL();
        }
        public List<Ban> LayDanhSachBan()
        {
            List<Ban> dsBan = new List<Ban>();
            DataTable data = db.ExecuteQueryDataTable("Select * from BAN", CommandType.Text, null);
            foreach(DataRow item in data.Rows)
            {
                Ban ban = new Ban(item);
                dsBan.Add(ban);
            }
            return dsBan;
        }
    }
}
