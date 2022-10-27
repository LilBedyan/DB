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
    public partial class Teachers : Form
    {
        DataBase dataBase = new DataBase();
        public Teachers()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataBase.openConnection();

                var ID = textBox20.Text;
                var ID_Academic_degree = textBox21.Text;
                var Surname = textBox22.Text;
                var Name = textBox23.Text;
                var Patronymic = textBox24.Text;
                var Experience = textBox19.Text;
                var Phone_number = textBox5.Text;


                var addQuery = $"insert into Teachers values ('{ID}','{ID_Academic_degree}','{Surname}','{Name}','{Patronymic}','{Experience}','{Phone_number}')";
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
