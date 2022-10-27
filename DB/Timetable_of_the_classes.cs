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
    public partial class Timetable_of_the_classes : Form
    {
        DataBase dataBase = new DataBase();
        public Timetable_of_the_classes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataBase.openConnection();


                var ID = textBox11.Text;
                var ID_The_audience = textBox12.Text;
                var ID_Teacher = textBox13.Text;
                var ID_Groups = textBox14.Text;
                var Time = textBox15.Text;


                var addQuery = $"insert into Timetable_of_the_classes values ('{ID}','{ID_The_audience}','{ID_Teacher}','{ID_Groups}','{Time}')";
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
