using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class LoaiMon
    {
        private string ma;
        private string ten;
        public string Ma
        {
            get
            {
                return ma;
            }

            set
            {
                ma = value;
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

        public LoaiMon(string ma, string ten)
        {
            this.Ma = ma;
            this.Ten = ten;
        }
        public LoaiMon(DataRow row)
        {
            this.Ma = row["MaLoaiMon"].ToString();
            this.Ten = row["TenLoaiMon"].ToString();
        }
    }

    public class LoaiMonBLL
    {
        DAL db;
        public LoaiMonBLL()
        {
            db = new DAL();
        }
        public List<LoaiMon> LayDSLoaiMon()
        {
            List<LoaiMon> dsLoaiMon = new List<LoaiMon>();
            DataTable data = db.ExecuteQueryDataTable("select * from LOAIMON", CommandType.Text, null);
            foreach (DataRow item in data.Rows)
            {
                LoaiMon loaiMon = new LoaiMon(item);
                dsLoaiMon.Add(loaiMon);
            }
            return dsLoaiMon;
        }
    }
}
