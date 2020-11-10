using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fashison_eCommerce
{
    public class MY_DB
    {
        SqlConnection con = new SqlConnection(@"Data Source=SQL5097.site4now.net;Initial Catalog=DB_A6A231_DAQLTMDT;User Id=DB_A6A231_DAQLTMDT_admin;Password=nhom9123");

        //get the connection
        public SqlConnection getConnection
        {
            get { return con; }
        }

        //open the connection
        public void openConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        //Close the connection
        public void closeConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}