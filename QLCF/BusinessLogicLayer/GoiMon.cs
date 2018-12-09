using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class GoiMon
    {
        private string tenMon;
        private int soLuong;
        public string TenMon
        {
            get
            {
                return tenMon;
            }

            set
            {
                tenMon = value;
            }
        }
        public int SoLuong
        {
            get
            {
                return soLuong;
            }

            set
            {
                soLuong = value;
            }
        }

        public GoiMon(string tenMon, int soLuong)
        {
            this.TenMon = tenMon;
            this.SoLuong = soLuong;
        }
        public GoiMon(DataRow row)
        {
            TenMon = row["TenMon"].ToString();
            SoLuong = int.Parse(row["SoLuong"].ToString());
        }
    }
}
