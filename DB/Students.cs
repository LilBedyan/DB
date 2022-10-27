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
    
    public partial class Students : Form
    {
        DataBase dataBase = new DataBase();
        public Students()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                dataBase.openConnection();

                var ID = textBox6.Text;
                var ID_Groups = textBox7.Text;
                var Surname = textBox8.Text;
                var Name = textBox9.Text;
                var Patronymic = textBox10.Text;




                var addQuery = $"insert into Students values ('{ID}','{ID_Groups}','{Surname}','{Name}','{Patronymic}')";
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
