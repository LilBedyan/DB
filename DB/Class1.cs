using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DB
{
    class DataBase
    {
        SqlConnection con = new SqlConnection(@"Data Source = LAPTOP-VLAD\LILBEDYAN; Initial Catalog = Pracktika; Integrated Security = true");

        public void openConnection ()
        {
            if(con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();  
            }
        }

        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
