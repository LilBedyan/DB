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
    public partial class The_audience : Form
    {
        DataBase dataBase = new DataBase();
        public The_audience()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataBase.openConnection();


                var ID = textBox34.Text;
                var Number = textBox35.Text;
                var Capacity = textBox40.Text;


                var addQuery = $"insert into The_audience values ('{ID}','{Number}','{Capacity}')";
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
