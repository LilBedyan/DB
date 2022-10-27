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
    public partial class Form3 : Form
    {
        DataBase dataBase = new DataBase();
        public Form3()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var name = textBox2.Text;
            var Phone_number = textBox3.Text;
            var Work_experience = textBox4.Text;
            var Type_name = textBox5.Text;
            var Payment = textBox7.Text;
            var Name_of_the_department = textBox8.Text;
            var Lesson_topic = textBox9.Text;
            var Number_of_hours = textBox10.Text;
            var Group_number = textBox11.Text;
            var Topic_name = textBox12.Text;
            var ttems_name = textBox13.Text;
            var Payment_per_hour = textBox14.Text;
            var Name_of_the_specialty = textBox15.Text;
            var Names = textBox16.Text;
            var Phone_numbers = textBox17.Text;
            var Address = textBox19.Text;


            var addQuery = $"insert into polz values ('{name}','{Phone_number}', '{Work_experience}', '{Type_name}', '{Payment}', '{Name_of_the_department}', '{Lesson_topic}', '{Number_of_hours}', '{Group_number}', '{Topic_name}', '{ttems_name}', '{Payment_per_hour}', '{Name_of_the_specialty}', '{Names}', '{Phone_numbers}', '{Address}')";
            var command = new SqlCommand(addQuery, dataBase.getConnection());
            command.ExecuteNonQuery();
            this.Hide();

            dataBase.closeConnection();
        }
    }
}
