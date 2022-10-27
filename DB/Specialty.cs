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
    public partial class Specialty : Form
    {
        DataBase dataBase = new DataBase();
        public Specialty()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataBase.openConnection();

                var ID = textBox55.Text;
                var Name = textBox56.Text;
                var Training_period = textBox43.Text;



                var addQuery = $"insert into Specialty values ('{ID}','{Name}','{Training_period}')";
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
