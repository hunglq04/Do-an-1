using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DAL
    {
        SqlConnection cnn;
        SqlCommand cmd;
        SqlDataAdapter adp;

        string strConnect =
            "Data Source=192.168.1.26; Initial Catalog=QLCF; User ID=admin; Password=123" ;

        public DAL()
        {
            cnn = new SqlConnection(strConnect);
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
            cnn.Open();
            cmd = cnn.CreateCommand();
        }
        public DataTable ExecuteQueryDataTable(
             string strSQL, CommandType ct, params SqlParameter[] param)
        {
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.Parameters.Clear();
            if (param != null)
                foreach (SqlParameter p in param)
                    cmd.Parameters.Add(p);
            adp.Fill(dt);
            return dt;
        }
        //Hàm chạy phương thức ExecuteNonQuery thực hiện lệnh insert, update, delete và trả về kiểu bool
        public bool MyExecuteNonQuery(string strSQL, CommandType ct, ref string error,
            params SqlParameter[] param)
        {
            bool f = false;
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
            cnn.Open();
            cmd.Parameters.Clear();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            foreach (SqlParameter p in param)
                cmd.Parameters.Add(p);
            try
            {
                cmd.ExecuteNonQuery();
                f = true;
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                cnn.Close();
            }
            return f;
        }
        //Hàm chạy phương thức ExecuteScalar và trả về 1 giá trị cụ thể
        public object MyExecuteScalar(string strSQL, CommandType ct, params SqlParameter[] param)
        {
            object o = new object();
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
            cnn.Open();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            cmd.Parameters.Clear();
            if (param != null)
                foreach (SqlParameter p in param)
                    cmd.Parameters.Add(p);
            o = cmd.ExecuteScalar();
            cnn.Close();
            return o;
        }
    }
}
