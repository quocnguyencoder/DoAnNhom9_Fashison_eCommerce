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
        SqlConnection con = new SqlConnection(@"Data Source=QUOCNGUYEN-LAPT;Initial Catalog=DA_QLTMDT;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

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