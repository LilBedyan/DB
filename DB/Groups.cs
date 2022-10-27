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
    public partial class Groups : Form
    {
        DataBase dataBase = new DataBase();
        public Groups()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataBase.openConnection();

                var ID = textBox1.Text;
                var ID_Specialty = textBox2.Text;
                var Number = textBox3.Text;
                var Number_of_student = textBox4.Text;


                var addQuery = $"insert into Groups values ('{ID}','{ID_Specialty}','{Number}','{Number_of_student}')";
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
