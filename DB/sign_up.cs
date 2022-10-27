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
    public partial class sign_up : Form
    {
        DataBase dataBase = new DataBase();


        public sign_up()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase();
            var login = textBox_login2.Text;
            var password = textBox_password2.Text;

            string querystrng = $"insert into register(login_user, password_user) values('{login}', '{password}')";

            SqlCommand command = new SqlCommand(querystrng, dataBase.getConnection());

            dataBase.openConnection();
            if (checkUser())

            {
                return;
            }
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан", "Успех");
                log_in frm_login = new log_in();
                this.Hide();
                frm_login.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан");
            }
            dataBase.closeConnection();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {


        }
        private Boolean checkUser()
        {
            DataBase dataBase = new DataBase();
            var loginUser = textBox_login2.Text;
            var passUser = textBox_password2.Text;

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user,login_user,password_user from register where login_user = '{loginUser}'and password_user = '{passUser}'";
            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользовательн уже существует");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
