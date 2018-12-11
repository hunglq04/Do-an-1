using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class Users
    {
        private string username;
        private string password;
        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }
        public Users(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public Users(DataRow row)
        {
            this.Username = row["UserName"].ToString();
            this.Password = row["Password"].ToString();
        }
    }
    public class UsersBLL
    {
        DAL db;
        public UsersBLL()
        {
            db = new DAL();
        }
        public bool UpdateUser(string username, string password, ref string error)
        {
            return db.MyExecuteNonQuery("USP_UpdateUser", CommandType.StoredProcedure, ref error, new SqlParameter("@username", username), new SqlParameter("@password", password));
        }
    }
}