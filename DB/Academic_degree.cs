using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DB
{
    public partial class Academic_degree : Form
    {
        DataBase dataBase = new DataBase();
        public Academic_degree()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataBase.openConnection();

                var ID = textBox27.Text;
                var Name = textBox28.Text;
                var Allowance = textBox29.Text;



                var addQuery = $"insert into Academic_degree values ('{ID}','{Name}','{Allowance}')";
                var command = new SqlCommand(addQuery, dataBase.getConnection());
                command.ExecuteNonQuery();
                this.Hide();

                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ОШИБКА: ID имеет числовой формат");
            }
        }
    }
}
