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
using System.Drawing.Printing;

namespace DB
{
    enum RowState1 //Методы"Модификаторы".................................................................................................................................................
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class polz : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;
        private string result = "";
        public polz()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void polz_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
        private void CreateColumns()//Метод создания....................................................................................................................................
        {
            dataGridView1.Columns.Add("code ", "code ");
            dataGridView1.Columns.Add("name ", "Инициалы");
            dataGridView1.Columns.Add("Phone_number ", "Телефоный номер");
            dataGridView1.Columns.Add("Work_experience ", "Стаж работы");
            dataGridView1.Columns.Add("Type_name ", "Название занятий");
            dataGridView1.Columns.Add("Payment ", "Оплата ");
            dataGridView1.Columns.Add("Name_of_the_department ", "Название отделения");
            dataGridView1.Columns.Add("Lesson_topic ", "Тема занятий");
            dataGridView1.Columns.Add("Number_of_hours ", "Кол-во часов");
            dataGridView1.Columns.Add("Group_number ", "Номер группы");
            dataGridView1.Columns.Add("Topic_name ", "Тип урока");
            dataGridView1.Columns.Add("ttems_name ", "Оплата за час");
            dataGridView1.Columns.Add("Payment_per_hour ", "Название предмета"); 
            dataGridView1.Columns.Add("Name_of_the_specialty ", "Название специальности");
            dataGridView1.Columns.Add("Names ", "Иницалы");
            dataGridView1.Columns.Add("Phone_numbers ", "Телефоный номер");
            dataGridView1.Columns.Add("Address ", "Адресс");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dqw, IDataRecord record)//Тип данных......................................................................................................
        {
            dqw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7), record.GetString(8), record.GetString(9), record.GetString(10), record.GetString(11), record.GetString(12), record.GetString(13), record.GetString(14), record.GetString(15), record.GetString(16), RowState.ModifiedNew);
        }
       
        private void RefreshDataGrid(DataGridView dqw)//Обновление.......................................................................................................................
        {
            dqw.Rows.Clear();
            string queryString = $"select * from polz    ";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dqw, reader);
            }
            reader.Close();
        }
        private void Search(DataGridView dgw)//Метод поиска.............................................................................................................................
        {
            dgw.Rows.Clear();

            string searchString = $"select * from polz where concat (code,name,Phone_number,Work_experience,Type_name,Payment,Name_of_the_department,Lesson_topic,Number_of_hours,Group_number,Topic_name,ttems_name,Payment_per_hour,Name_of_the_specialty,Names,Phone_numbers,Address ) like '%" + textBox6.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection()); // поиск

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }
        private void Update()//Добавление...............................................................................................................................................
        {
            dataBase.openConnection();
            for (int index = 0; index < dataGridView1.Rows.Count; index++) //code Last_name Name Middle_name Phone_number Address
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[17].Value;
                if (rowState == RowState.Existed)
                    continue;
// code,name,Phone_number,Work_experience,Type_name,Payment,Name_of_the_department,Lesson_topic,Number_of_hours,Group_number,Topic_name,ttems_name,Payment_per_hour,Name_of_the_specialty,Names,Phone_numbers,Address
                if (rowState == RowState.Deleted)
                {
                    var code = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value); //добавление
                    var DeleteQuery = $"delete from polz where code = {code}";
                    var command = new SqlCommand(DeleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState.Modified)
                {
                    var code = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var Phone_number = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var Work_experience = dataGridView1.Rows[index].Cells[3].Value.ToString(); 
                    var Type_name = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var Payment = dataGridView1.Rows[index].Cells[5].Value.ToString();
                    var Name_of_the_department = dataGridView1.Rows[index].Cells[6].Value.ToString();
                    var Lesson_topic = dataGridView1.Rows[index].Cells[7].Value.ToString(); 
                    var Number_of_hours = dataGridView1.Rows[index].Cells[8].Value.ToString();
                    var Group_number = dataGridView1.Rows[index].Cells[9].Value.ToString();
                    var Topic_name = dataGridView1.Rows[index].Cells[10].Value.ToString();
                    var ttems_name = dataGridView1.Rows[index].Cells[11].Value.ToString();
                    var Payment_per_hour = dataGridView1.Rows[index].Cells[12].Value.ToString();
                    var Name_of_the_specialty = dataGridView1.Rows[index].Cells[13].Value.ToString();
                    var Names = dataGridView1.Rows[index].Cells[14].Value.ToString();
                    var Phone_numbers = dataGridView1.Rows[index].Cells[15].Value.ToString();
                    var Address = dataGridView1.Rows[index].Cells[16].Value.ToString();// code,name,Phone_number,Work_experience,Type_name,Payment,Name_of_the_department,Lesson_topic,Number_of_hours,Group_number,Topic_name,ttems_name,Payment_per_hour,Name_of_the_specialty,Names,Phone_numbers,Address

                    var changeQuery = $"update polz set name = '{name}',Phone_number = '{Phone_number}',Work_experience = '{Work_experience}',Type_name = '{Type_name}',Payment = '{Payment}',Name_of_the_department = '{Name_of_the_department}',Lesson_topic = '{Lesson_topic}',Number_of_hours = '{Number_of_hours}',Group_number = '{Group_number}',Topic_name = '{Topic_name}',ttems_name = '{ttems_name}',Payment_per_hour = '{Payment_per_hour}',Name_of_the_specialty = '{Name_of_the_specialty}',Names = '{Names}',Phone_numbers = '{Phone_numbers}',Address = '{Address}' where code='{code}' ";
                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }
        private void deleteRow()//Удаление.............................................................................................................................................
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;
            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[17].Value = RowState.Deleted; //удаление
                return;
            }
            dataGridView1.Rows[index].Cells[17].Value = RowState.Deleted;

        }
        private void Chande()//Изменение................................................................................................................................................
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;//code Last_name Name Middle_name Phone_number Address
            var code = textBox1.Text;
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

            //изменение

            dataGridView1.Rows[selectedRowIndex].SetValues(code, name, Phone_number, Work_experience, Type_name, Payment, Name_of_the_department, Lesson_topic, Number_of_hours, Group_number, Topic_name, ttems_name, Payment_per_hour, Name_of_the_specialty, Names, Phone_numbers, Address);
            dataGridView1.Rows[selectedRowIndex].Cells[17].Value = RowState.Modified;
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void button54_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox7.Text = row.Cells[5].Value.ToString();
                textBox8.Text = row.Cells[6].Value.ToString();
                textBox9.Text = row.Cells[7].Value.ToString();
                textBox10.Text = row.Cells[8].Value.ToString();
                textBox11.Text = row.Cells[9].Value.ToString();
                textBox12.Text = row.Cells[10].Value.ToString();
                textBox14.Text = row.Cells[11].Value.ToString();
                textBox13.Text = row.Cells[12].Value.ToString();
                textBox15.Text = row.Cells[13].Value.ToString();
                textBox16.Text = row.Cells[14].Value.ToString();
                textBox17.Text = row.Cells[15].Value.ToString();
                textBox19.Text = row.Cells[16].Value.ToString();
                


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 addTP = new Form3();
            addTP.Show();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Chande();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button71_Click(object sender, EventArgs e)
        {
            // задаем текст для печати
            result = " ID: " + textBox1.Text + "\r\n";
            result += " Инициалы: " + textBox2.Text + "\r\n";
            result += " Номер телефона : " + textBox3.Text + "\r\n";
            result += " Стаж работы : " + textBox4.Text + "\r\n";
            result += " Название занятий: " + textBox5.Text + "\r\n";
            result += " Оплата: " + textBox7.Text + "\r\n";
            result += " Название отделения: " + textBox8.Text + "\r\n";
            result += " Тема занятий : " + textBox9.Text + "\r\n";
            result += " Кол-во часов : " + textBox10.Text + "\r\n";
            result += " Номер группы : " + textBox11.Text + "\r\n";
            result += " Тип урока : " + textBox12.Text + "\r\n";
            result += " Название предмета: " + textBox13.Text + "\r\n";
            result += " Оплата за час: " + textBox14.Text + "\r\n";
            result += " Название спецальности: " + textBox15.Text + "\r\n";
            result += " Инициалы студента : " + textBox16.Text + "\r\n";
            result += " Телефон : " + textBox17.Text + "\r\n";
            result += " Адресс : " + textBox19.Text + "\r\n";

           
            

            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler1;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }

        // обработчик события печати
        void PrintPageHandler1(object sender, PrintPageEventArgs e)
        {
            // печать строки result
            e.Graphics.DrawString(result, new Font("Arial", 14), Brushes.Black, 0, 0);
        }
    }
    }

