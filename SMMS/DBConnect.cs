using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMS
{
    internal class DBConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        private string con;
        public string myConnection()
        {
            con = @"Data Source=DESKTOP-VQLQM22;Initial Catalog=DBMarket;Integrated Security=True;Encrypt=False";
            return con;
        }

        public DataTable GetTable(string query)
        {
            cn.ConnectionString = myConnection();
            cmd = new SqlCommand(query, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
