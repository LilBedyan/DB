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
    enum RowState //Методы"Модификаторы".................................................................................................................................................
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Form1 : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;
        private string result = "";
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;//Стартовая позиция............................................................................................................
        }



        //Группа..........................................................................................................................................................................
        private void CreateColumns()//Метод создания....................................................................................................................................
        {
            dataGridView1.Columns.Add("ID", "id");
            dataGridView1.Columns.Add("ID_Specialty", "Код специальности");
            dataGridView1.Columns.Add("Number", "Номер группы");
            dataGridView1.Columns.Add("Number_of_student", "Количество студентов");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingleRow(DataGridView dqw, IDataRecord record)//Тип данных......................................................................................................
        {
            dqw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2), record.GetString(3), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dqw)//Метод Обновление.................................................................................................................
        {
            dqw.Rows.Clear();
            string queryString = $"SELECT * FROM Groups";
            
            SqlCommand command = new SqlCommand(queryString,dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dqw, reader);
            }
            reader.Close();
            button4.Visible = true;
        }
        private void Form1_Load(object sender, EventArgs e)//Форма.......................................................................................................................
        {
            CreateColumns();//1
            RefreshDataGrid(dataGridView1);//1
            CreateColumns2();//2
            RefreshDataGrid2(dataGridView2);//2
            CreateColumns3();//3
            RefreshDataGrid3(dataGridView3);//3
            CreateColumns4();//4
            RefreshDataGrid4(dataGridView4);//4
            CreateColumns5();//5
            RefreshDataGrid5(dataGridView5);//5
            CreateColumns6();//6
            RefreshDataGrid6(dataGridView6);//6
            CreateColumns9();//9
            RefreshDataGrid9(dataGridView9);//9
        }
       
        private void button1_Click(object sender, EventArgs e)//Кнопка Новой записи....................................................................................................... 
        {
            Groups addTP = new Groups();
            addTP.Show();
        }
        private void button37_Click(object sender, EventArgs e) //Кнопка обновления......................................................................................................
        {
            RefreshDataGrid(dataGridView1);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//Считывания данных с таблицы......................................................................
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
            }

        }
        private void textBox18_TextChanged(object sender, EventArgs e)//Поиск.............................................................................................................
        {
            Search(dataGridView1);
        }
        private void Search(DataGridView dgw)//Метод поиска................................................................................................................................
        {
            dgw.Rows.Clear();

            string searchString = $"SELECT * FROM Groups  WHERE CONCAT (ID,ID_Specialty,Number,Number_of_student) LIKE '%" + textBox18.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection()); // поиск

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }

        private void deleteRow()//Метод удаления.........................................................................................................................................
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;
            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[4].Value = RowState.Deleted; 
                return;
            }
            dataGridView1.Rows[index].Cells[4].Value = RowState.Deleted;
            button4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)//Кнопка удаления..........................................................................................................
        {
            deleteRow();

        }
        private void Update()//Метод сохранения.........................................................................................................................................
        {
            try
            {
                dataBase.openConnection();
                for (int index = 0; index < dataGridView1.Rows.Count; index++)
                {
                    var rowState = (RowState)dataGridView1.Rows[index].Cells[4].Value;
                    if (rowState == RowState.Existed)
                        continue;

                    if (rowState == RowState.Deleted)
                    {
                        var ID = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value); //добавление
                        var DeleteQuery = $"DELETE FROM Groups WHERE ID = {ID}";
                        var command = new SqlCommand(DeleteQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modified)
                    {
                        var ID = dataGridView1.Rows[index].Cells[0].Value.ToString();
                        var ID_Specialty = dataGridView1.Rows[index].Cells[1].Value.ToString();
                        var Number = dataGridView1.Rows[index].Cells[2].Value.ToString();
                        var Number_of_student = dataGridView1.Rows[index].Cells[3].Value.ToString();
                        var changeQuery = $"UPDATE Groups SET ID_Specialty = '{ID_Specialty}',Number = '{Number}',Number_of_student = '{Number_of_student}' WHERE ID = '{ID}' ";
                        var command = new SqlCommand(changeQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ОШИБКА: ID имеет числовой формат");
            }
            dataBase.closeConnection();
            button4.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)//Кнопка сохранения........................................................................................................
        {
            Update();
        }
        private void Chande()//Метод изменения
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var ID = textBox1.Text;
            var ID_Specialty = textBox2.Text;
            var Number = textBox3.Text;                 
            int Number_of_student;
            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                if (int.TryParse(textBox4.Text, out Number_of_student))
                {
                    dataGridView1.Rows[selectedRowIndex].SetValues(ID, ID_Specialty, Number, Number_of_student);
                    dataGridView1.Rows[selectedRowIndex].Cells[4].Value = RowState.Modified;

                }
                else
                {
                    MessageBox.Show("Числовой формат");
                }
            }
        }



        //Студенты................................................................................................................................................................................
        private void CreateColumns2()//Метод создания.................................................................................................................................... 
        {
            dataGridView2.Columns.Add("ID", "id");
            dataGridView2.Columns.Add("ID_Groups", "Код группы");//Cоздание колонки
            dataGridView2.Columns.Add("Surname", "Фамилия");
            dataGridView2.Columns.Add("Name", "Имя");
            dataGridView2.Columns.Add("Patronymic", "Отчество");//code Last_name Name Middle_name Phone_number Address
            dataGridView2.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow2(DataGridView dqw, IDataRecord record)//Тип данных.....................................................................................................
        {
            dqw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2), record.GetString(3), record.GetString(4), RowState.ModifiedNew);
        }
        private void RefreshDataGrid2(DataGridView dqw)//Метод обнавления..................................................................................................................
        {
            dqw.Rows.Clear();
            string queryString = $"select * from Students";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow2(dqw, reader);
            }
            reader.Close();
            button12.Visible = true;
        }





        private void label1_Click(object sender, EventArgs e)
        {

        }

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label74_Click(object sender, EventArgs e)
        {

        }

        private void label70_Click(object sender, EventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        

        

        private void button38_Click(object sender, EventArgs e)
        {

        }

       

       
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
        private void button4_Click(object sender, EventArgs e)//Кнопка изменения.........................................................................................................
        {
            Chande();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Students addTP = new Students();
            addTP.Show();
        }

        private void textBox25_TextChanged(object sender, EventArgs e)//Поиск..............................................................................................................
        {
            Search2(dataGridView2);
        }

        private void button40_Click(object sender, EventArgs e)//Обнавление................................................................................................................
        {
            RefreshDataGrid2(dataGridView2);
        }

        private void button11_Click(object sender, EventArgs e)//Удаление...................................................................................................................
        {
            deleteRow2();
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
           
        }
        private void Search2(DataGridView dgw)//Метод поиска...........................................................................................................................
        {
            dgw.Rows.Clear();

            string searchString = $"SELECT * FROM Students WHERE CONCAT (ID, ID_Groups, Surname, Name, Patronymic) LIKE '%" + textBox25.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection()); // поиск

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow2(dgw, read);
            }
            read.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)//Считывания данных с таблицы....................................................................
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
                textBox6.Text = row.Cells[0].Value.ToString();
                textBox7.Text = row.Cells[1].Value.ToString();
                textBox8.Text = row.Cells[2].Value.ToString();
                textBox9.Text = row.Cells[3].Value.ToString();
                textBox10.Text = row.Cells[4].Value.ToString();
            }
      
        }
        
        private void Update2()//Метод сохранения.........................................................................................................................................
        {
            try
            {
                dataBase.openConnection();
                for (int index = 0; index < dataGridView2.Rows.Count; index++) //code Last_name Name Middle_name Phone_number Address
                {
                    var rowState = (RowState)dataGridView2.Rows[index].Cells[5].Value;
                    if (rowState == RowState.Existed)
                        continue;

                    if (rowState == RowState.Deleted)
                    {
                        var ID = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value); //добавление
                        var DeleteQuery = $"DELETE FROM Students WHERE ID = {ID}";
                        var command = new SqlCommand(DeleteQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modified)
                    {
                        var ID = dataGridView2.Rows[index].Cells[0].Value.ToString();
                        var ID_Groups = dataGridView2.Rows[index].Cells[1].Value.ToString();
                        var Surname = dataGridView2.Rows[index].Cells[2].Value.ToString();
                        var Name = dataGridView2.Rows[index].Cells[3].Value.ToString();
                        var Patronymic = dataGridView2.Rows[index].Cells[4].Value.ToString();
                        var changeQuery = $"UPDATE Students SET ID_Groups = '{ID_Groups}', Surname = '{Surname}', Name = '{Name}', Patronymic = '{Patronymic}' where ID = '{ID}' ";
                        var command = new SqlCommand(changeQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ОШИБКА: ID имеет числовой формат");
            }
            dataBase.closeConnection();
            button12.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)//Сохранение...............................................................................................................
        {
            Update2();
        }
        private void deleteRow2()//Метод удаления........................................................................................................................................
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[index].Visible = false;
            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[5].Value = RowState.Deleted; //удаление
                return;
            }
            dataGridView2.Rows[index].Cells[5].Value = RowState.Deleted;
            button12.Visible = false;

        }
        private void Chande2()//Метод изменения...........................................................................................................................................
        {
            var selectedRowIndex = dataGridView2.CurrentCell.RowIndex;//code Last_name Name Middle_name Phone_number Address
            var ID = textBox6.Text;
            int ID_Groups;
            var Surname = textBox8.Text;                 //изменение
            var Name = textBox9.Text;
            var Patronymic = textBox10.Text;
            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                if (int.TryParse(textBox7.Text, out ID_Groups))
                {
                    dataGridView2.Rows[selectedRowIndex].SetValues(ID, ID_Groups, Surname, Name, Patronymic);
                    dataGridView2.Rows[selectedRowIndex].Cells[5].Value = RowState.Modified;

                }
                else
                {
                    MessageBox.Show("Числовой формат");
                }
            }
        }
        private void button12_Click(object sender, EventArgs e)//Изменение.................................................................................................................
        {
            Chande2();
        }

        //Расписание........................................................................................................................................................................
        private void CreateColumns3()//Метод создания.................................................................................................................................... 
        {
            dataGridView3.Columns.Add("ID", "id");
            dataGridView3.Columns.Add("ID_The_audience", "Код аудитории");//Cоздание колонки
            dataGridView3.Columns.Add("ID_Teacher", "Код преподавателя");
            dataGridView3.Columns.Add("ID_Groups", "Код группы");
            dataGridView3.Columns.Add("Time", "Время");//code Lesson_topic Number_of_hours Teacher Subject Task_type groups
            dataGridView3.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow3(DataGridView dqw, IDataRecord record)//Тип данных....................................................................................................
        {
            dqw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3), record.GetString(4), RowState.ModifiedNew);
        }
        private void RefreshDataGrid3(DataGridView dqw)//Метод обнавления.................................................................................................................
        {
            dqw.Rows.Clear();
            string queryString = $"SELECT * FROM Timetable_of_the_classes";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow3(dqw, reader);
            }
            reader.Close();
            button8.Visible = true;
        }
        private void Search3(DataGridView dgw)//Метод Поиска...........................................................................................................................
        {
            dgw.Rows.Clear();

            string searchString = $"SELECT * FROM Timetable_of_the_classes WHERE CONCAT (ID, ID_The_audience, ID_Teacher, ID_Groups, Time) LIKE '%" + textBox26.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection()); // поиск

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow3(dgw, read);
            }
            read.Close();
        }
        private void Update3()//Метод сохранения............................................................................................................................................
        {
            try
            {
                dataBase.openConnection();
                for (int index = 0; index < dataGridView3.Rows.Count; index++) //code Last_name Name Middle_name Phone_number Address
                {
                    var rowState = (RowState)dataGridView3.Rows[index].Cells[5].Value;
                    if (rowState == RowState.Existed)
                        continue;

                    if (rowState == RowState.Deleted)
                    {
                        var ID = Convert.ToInt32(dataGridView3.Rows[index].Cells[0].Value); //добавление
                        var DeleteQuery = $"delete from Timetable_of_the_classes where ID = {ID}";
                        var command = new SqlCommand(DeleteQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modified)
                    {
                        var ID = dataGridView3.Rows[index].Cells[0].Value.ToString();
                        var ID_The_audience = dataGridView3.Rows[index].Cells[1].Value.ToString();
                        var ID_Teacher = dataGridView3.Rows[index].Cells[2].Value.ToString();
                        var ID_Groups = dataGridView3.Rows[index].Cells[3].Value.ToString();
                        var Time = dataGridView3.Rows[index].Cells[4].Value.ToString();
                        var changeQuery = $"UPDATE Timetable_of_the_classes SET ID_The_audience = '{ID_The_audience}', ID_Teacher = '{ID_Teacher}', ID_Groups = '{ID_Groups}', Time = '{Time}' WHERE ID = '{ID}' ";
                        var command = new SqlCommand(changeQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ОШИБКА: ID имеет числовой формат");
            }
            dataBase.closeConnection();
            button8.Visible = true;
        }
        private void deleteRow3()//метод удаления..........................................................................................................................................
        {
            int index = dataGridView3.CurrentCell.RowIndex;
            dataGridView3.Rows[index].Visible = false;
            if (dataGridView3.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView3.Rows[index].Cells[5].Value = RowState.Deleted; //удаление
                return;
            }
            dataGridView3.Rows[index].Cells[5].Value = RowState.Deleted;
            button8.Visible = false;

        }
        private void Chande3()//Метод изменения...........................................................................................................................................
        {
            var selectedRowIndex = dataGridView3.CurrentCell.RowIndex;//code Last_name Name Middle_name Phone_number Address
            var ID = textBox11.Text;
            var ID_The_audience = textBox12.Text;
            var ID_Teacher = textBox13.Text;                 //изменение
            var ID_Groups = textBox14.Text;
            var Time = textBox15.Text;
            
            dataGridView3.Rows[selectedRowIndex].SetValues(ID, ID_The_audience, ID_Teacher, ID_Groups, Time);
            dataGridView3.Rows[selectedRowIndex].Cells[5].Value = RowState.Modified;
        }

        private void button6_Click(object sender, EventArgs e)//Добавление................................................................................................................
        {
            Timetable_of_the_classes addTP = new Timetable_of_the_classes();
            addTP.Show();
        }

        private void button7_Click(object sender, EventArgs e)//удаление..................................................................................................................
        {
            deleteRow3();
        }

        private void button8_Click(object sender, EventArgs e)//Изменение.................................................................................................................
        {
            Chande3();
        }

        private void button5_Click(object sender, EventArgs e)//Сохранение.................................................................................................................
        {
            Update3();
        }

        private void textBox26_TextChanged(object sender, EventArgs e)//Поиск.............................................................................................................
        {
            Search3(dataGridView3);
        }

        private void button42_Click(object sender, EventArgs e)//обнавление...............................................................................................................
        {
            RefreshDataGrid3(dataGridView3);
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)//Считывания данных с таблицы....................................................................
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[selectedRow];
                textBox11.Text = row.Cells[0].Value.ToString();
                textBox12.Text = row.Cells[1].Value.ToString();
                textBox13.Text = row.Cells[2].Value.ToString();
                textBox14.Text = row.Cells[3].Value.ToString();
                textBox15.Text = row.Cells[4].Value.ToString();
            }
        }


        //Преподователи..................................................................................................................................................................
        private void CreateColumns4()//Метод создания....................................................................................................................................
        {
            dataGridView4.Columns.Add("ID", "id");
            dataGridView4.Columns.Add("ID_Academic_degree", "Код ученой степени");//Cоздание колонки
            dataGridView4.Columns.Add("Surname ", "Фамилия");
            dataGridView4.Columns.Add("Name ", "Имя");
            dataGridView4.Columns.Add("Patronymic", "Отчество");//code Lesson_topic Number_of_hours Teacher Subject Task_type groups
            dataGridView4.Columns.Add("Experience", "Стаж работы");
            dataGridView4.Columns.Add("Phone_number", "Номер телефона");
            dataGridView4.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow4(DataGridView dqw, IDataRecord record)//Тип данных......................................................................................................
        {
            dqw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6),  RowState.ModifiedNew);
        }
        private void RefreshDataGrid4(DataGridView dqw)//Метод обнавления................................................................................................................
        {
            dqw.Rows.Clear();
            string queryString = $"select * from Teachers";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow4(dqw, reader);
            }
            reader.Close();
            button16.Visible = true;
        }
        private void Search4(DataGridView dgw)//Метод поиска..............................................................................................................................
        {
            dgw.Rows.Clear();

            string searchString = $"select * from Teachers where concat (ID, ID_Academic_degree, Surname , Name , Patronymic, Experience, Phone_number) like '%" + textBox30.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection()); // поиск

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow4(dgw, read);
            }
            read.Close();
        }
        private void Update4()//Метод сохранения........................................................................................................................................
        {
            try
            {


                dataBase.openConnection();
                for (int index = 0; index < dataGridView4.Rows.Count; index++) //code Last_name Name Middle_name Phone_number Address
                {
                    var rowState = (RowState)dataGridView4.Rows[index].Cells[7].Value;
                    if (rowState == RowState.Existed)
                        continue;

                    if (rowState == RowState.Deleted)
                    {
                        var ID = Convert.ToInt32(dataGridView4.Rows[index].Cells[0].Value); //добавление
                        var DeleteQuery = $"delete from Teachers where ID = {ID}";
                        var command = new SqlCommand(DeleteQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modified)
                    {
                        var ID = dataGridView4.Rows[index].Cells[0].Value.ToString();
                        var ID_Academic_degree = dataGridView4.Rows[index].Cells[1].Value.ToString();
                        var Surname = dataGridView4.Rows[index].Cells[2].Value.ToString();
                        var Name = dataGridView4.Rows[index].Cells[3].Value.ToString();
                        var Patronymic = dataGridView4.Rows[index].Cells[4].Value.ToString();
                        var Experience = dataGridView4.Rows[index].Cells[5].Value.ToString();
                        var Phone_number = dataGridView4.Rows[index].Cells[6].Value.ToString();
                        var changeQuery = $"update Teachers set ID_Academic_degree = '{ID_Academic_degree}', Surname = '{Surname}', Name = '{Name}', Patronymic = '{Patronymic}', Experience = '{Experience}', Phone_number='{Phone_number}' where ID='{ID}' ";
                        var command = new SqlCommand(changeQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ОШИБКА: ID имеет числовой формат");
            }
            dataBase.closeConnection();
            button16.Visible = true;
        }
        private void deleteRow4()//Метод удаления........................................................................................................................................
        {
            int index = dataGridView4.CurrentCell.RowIndex;
            dataGridView4.Rows[index].Visible = false;
            if (dataGridView4.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView4.Rows[index].Cells[7].Value = RowState.Deleted; //удаление
                return;
            }
            dataGridView4.Rows[index].Cells[7].Value = RowState.Deleted;
            button16.Visible = false;

        }
        private void Chande4()//Метод изменения..........................................................................................................................................
        {
            var selectedRowIndex = dataGridView4.CurrentCell.RowIndex;//code Last_name Name Middle_name Phone_number Address
            var ID = textBox20.Text;
            int ID_Academic_degree;
            var Surname = textBox22.Text;                 //изменение
            var Name = textBox23.Text;
            var Patronymic = textBox24.Text;
            var Experience = textBox19.Text;
            var Phone_number = textBox5.Text;
            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                if (int.TryParse(textBox21.Text, out ID_Academic_degree))
                {
                    dataGridView4.Rows[selectedRowIndex].SetValues(ID, ID_Academic_degree, Surname, Name, Patronymic, Experience, Phone_number);
                    dataGridView4.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;
                }
                else
                {
                    MessageBox.Show("Числовой формат");
                }
            }   
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)//Добавление.............................................................................................................
        {
            Teachers addTP = new Teachers();
            addTP.Show();
        }

        private void textBox30_TextChanged(object sender, EventArgs e)//Поиск............................................................................................................
        {
            Search4(dataGridView4);
        }

        private void button44_Click(object sender, EventArgs e)//Обнавление..............................................................................................................
        {
            RefreshDataGrid4(dataGridView4);
        }

        private void button15_Click(object sender, EventArgs e)//Удаление..............................................................................................................
        {
            deleteRow4();
        }

        private void button16_Click(object sender, EventArgs e)//Изменение..............................................................................................................
        {
            Chande4();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)//Считывание с таблицы..............................................................................................................
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[selectedRow];
                textBox20.Text = row.Cells[0].Value.ToString();
                textBox21.Text = row.Cells[1].Value.ToString();
                textBox22.Text = row.Cells[2].Value.ToString();
                textBox23.Text = row.Cells[3].Value.ToString();
                textBox24.Text = row.Cells[4].Value.ToString();
                textBox19.Text = row.Cells[5].Value.ToString();
                textBox5.Text = row.Cells[6].Value.ToString();
                
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Update4();
        }


        //Ученая степень......................................................................................................................................................................
        private void CreateColumns5()//Метод создания....................................................................................................................................
        {
            dataGridView5.Columns.Add("ID", "id");
            dataGridView5.Columns.Add("Name", "Название степени");//Cоздание колонки
            dataGridView5.Columns.Add("Allowance", "Зарплата");
            dataGridView5.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow5(DataGridView dqw, IDataRecord record)
        {
            dqw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RowState.ModifiedNew);
        }
        private void RefreshDataGrid5(DataGridView dqw)//Метод Обнавление..............................................................................................................
        {
            dqw.Rows.Clear();
            string queryString = $"select * from Academic_degree ";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow5(dqw, reader);
            }
            reader.Close();
            button20.Visible = true;
        }
        private void Search5(DataGridView dgw)//Метод поиска..............................................................................................................
        {
            dgw.Rows.Clear();

            string searchString = $"select * from Academic_degree  where concat (ID, Name, Allowance) like '%" + textBox31.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection()); // поиск

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow5(dgw, read);
            }
            read.Close();
        }
        private void Update5()//Метод Сохранения..............................................................................................................
        {
            try
            {
                dataBase.openConnection();
                for (int index = 0; index < dataGridView5.Rows.Count; index++) //code Last_name Name Middle_name Phone_number Address
                {
                    var rowState = (RowState)dataGridView5.Rows[index].Cells[3].Value;
                    if (rowState == RowState.Existed)
                        continue;

                    if (rowState == RowState.Deleted)
                    {
                        var ID = Convert.ToInt32(dataGridView5.Rows[index].Cells[0].Value); //добавление
                        var DeleteQuery = $"delete from Academic_degree  where ID = {ID}";
                        var command = new SqlCommand(DeleteQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modified)
                    {
                        var ID = dataGridView5.Rows[index].Cells[0].Value.ToString();
                        var Name = dataGridView5.Rows[index].Cells[1].Value.ToString();
                        var Allowance = dataGridView5.Rows[index].Cells[2].Value.ToString();

                        var changeQuery = $"update Academic_degree set Name= '{Name}', Allowance = '{Allowance}' where ID = '{ID}' ";
                        var command = new SqlCommand(changeQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ОШИБКА: ID имеет числовой формат");
            }
            dataBase.closeConnection();
            button20.Visible = true;
        }
        private void deleteRow5()//Метод удаления..............................................................................................................
        {
            int index = dataGridView5.CurrentCell.RowIndex;
            dataGridView5.Rows[index].Visible = false;
            if (dataGridView5.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView5.Rows[index].Cells[3].Value = RowState.Deleted; //удаление
                return;
            }
            dataGridView5.Rows[index].Cells[3].Value = RowState.Deleted;
            button20.Visible = false;

        }
        private void Chande5()//Метод изменения..............................................................................................................
        {
            var selectedRowIndex = dataGridView5.CurrentCell.RowIndex;//code Last_name Name Middle_name Phone_number Address
            var ID = textBox27.Text;
            var Name = textBox28.Text;
            var Allowance = textBox29.Text;                 //изменение
            
            dataGridView5.Rows[selectedRowIndex].SetValues(ID, Name, Allowance);
            dataGridView5.Rows[selectedRowIndex].Cells[3].Value = RowState.Modified;
        }

        private void button18_Click(object sender, EventArgs e)//Добавление..............................................................................................................
        {
            Academic_degree addTP = new Academic_degree();
            addTP.Show();
        }

        private void textBox31_TextChanged(object sender, EventArgs e)//Поиск..............................................................................................................
        {
            Search5(dataGridView5);
        }

        private void button46_Click(object sender, EventArgs e)//Обнавление..............................................................................................................
        {
            RefreshDataGrid5(dataGridView5);
        }

        private void button19_Click(object sender, EventArgs e)//Удаление..............................................................................................................
        {
            deleteRow5();
        }

        private void button20_Click(object sender, EventArgs e)//Изменение..............................................................................................................
        {
            Chande5();
        }

        private void button17_Click(object sender, EventArgs e)//Сохранение..............................................................................................................
        {
            Update5();
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)//Считывание с таблицы..............................................................................................................
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView5.Rows[selectedRow];
                textBox27.Text = row.Cells[0].Value.ToString();
                textBox28.Text = row.Cells[1].Value.ToString();
                textBox29.Text = row.Cells[2].Value.ToString();
                
                
                

            }
        }


        //Аудитории.......................................................................................................................
        private void CreateColumns6()//Метод создания....................................................................................................................................
        {
            dataGridView6.Columns.Add("ID", "id");
            dataGridView6.Columns.Add("Number", "Номер аудитории");//Cоздание колонки
            dataGridView6.Columns.Add("Capacity", "Вместимость аудитории");
            dataGridView6.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow6(DataGridView dqw, IDataRecord record)
        {
            dqw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), RowState.ModifiedNew);
        }
        private void RefreshDataGrid6(DataGridView dqw)//Метод обнавления..............................................................................................................
        {
            dqw.Rows.Clear();
            string queryString = $"SELECT * FROM The_audience";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow6(dqw, reader);
            }
            reader.Close();
            button24.Visible = true;
        }
        private void Search6(DataGridView dgw)//Метод поиска..............................................................................................................
        {
            dgw.Rows.Clear();

            string searchString = $"SELECT * FROM The_audience WHERE CONCAT (ID, Number, Capacity) LIKE '%" + textBox32.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection()); // поиск

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow6(dgw, read);
            }
            read.Close();
        }
        private void Update6()//Метод сохранения..............................................................................................................
        {
            try
            {
                dataBase.openConnection();
                for (int index = 0; index < dataGridView6.Rows.Count; index++) //code Last_name Name Middle_name Phone_number Address
                {
                    var rowState = (RowState)dataGridView6.Rows[index].Cells[3].Value;
                    if (rowState == RowState.Existed)
                        continue;

                    if (rowState == RowState.Deleted)
                    {
                        var ID = Convert.ToInt32(dataGridView6.Rows[index].Cells[0].Value); //добавление
                        var DeleteQuery = $"DELETE FROM The_audience WHERE ID = {ID}";
                        var command = new SqlCommand(DeleteQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modified)
                    {
                        var ID = dataGridView6.Rows[index].Cells[0].Value.ToString();
                        var Number = dataGridView6.Rows[index].Cells[1].Value.ToString();
                        var Capacity = dataGridView6.Rows[index].Cells[2].Value.ToString();
                        var changeQuery = $"update The_audience set Number = '{Number}', Capacity = '{Capacity}' where ID = '{ID}' ";
                        var command = new SqlCommand(changeQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ОШИБКА: ID имеет числовой формат");
            }
            dataBase.closeConnection();
            button24.Visible = true;
        }
        private void deleteRow6()//Метод удаления..............................................................................................................
        {
            int index = dataGridView6.CurrentCell.RowIndex;
            dataGridView6.Rows[index].Visible = false;
            if (dataGridView6.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView6.Rows[index].Cells[3].Value = RowState.Deleted; //удаление
                return;
            }
            dataGridView6.Rows[index].Cells[3].Value = RowState.Deleted;
            button24.Visible = false;

        }
        private void Chande6()//Метод изменения..............................................................................................................
        {
            try
            {
                var selectedRowIndex = dataGridView6.CurrentCell.RowIndex;//code Last_name Name Middle_name Phone_number Address
                var ID = textBox34.Text;
                var Number = textBox35.Text;
                var Capacity = textBox40.Text;


                dataGridView6.Rows[selectedRowIndex].SetValues(ID, Number, Capacity);
                dataGridView6.Rows[selectedRowIndex].Cells[3].Value = RowState.Modified;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ОШИБКА: числовой формат");
            }
        }

        private void textBox32_TextChanged(object sender, EventArgs e)//Поиск..............................................................................................................
        {
            Search6(dataGridView6);
        }

        private void button48_Click(object sender, EventArgs e)//Обнавление..............................................................................................................
        {
            RefreshDataGrid6(dataGridView6);
        }

        private void button22_Click(object sender, EventArgs e)//Добавление..............................................................................................................
        {
            The_audience addTP = new The_audience();
            addTP.Show();
        }

        private void button23_Click(object sender, EventArgs e)//Удаление..............................................................................................................
        {
            deleteRow6();
        }

        private void button24_Click(object sender, EventArgs e)//Изменение..............................................................................................................
        {
            Chande6();
        }

        private void button21_Click(object sender, EventArgs e)//Сохранение..............................................................................................................
        {
            Update6();
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)//Считывание данных..............................................................................................................
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView6.Rows[selectedRow];
                textBox34.Text = row.Cells[0].Value.ToString();
                textBox35.Text = row.Cells[1].Value.ToString();
                textBox40.Text = row.Cells[2].Value.ToString();
                
            }
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {

        }
                
        private void textBox33_TextChanged(object sender, EventArgs e)//Поиск..............................................................................................................
        {
            
        }

        private void button50_Click(object sender, EventArgs e)//Обнавление..............................................................................................................
        {
            
        }

        private void button26_Click(object sender, EventArgs e)//Добавление..............................................................................................................
        {
            
        }

        private void button27_Click(object sender, EventArgs e)//Удаление..............................................................................................................
        {
            
        }

        private void button28_Click(object sender, EventArgs e)//Изменение..............................................................................................................
        {
            
        }

        private void button25_Click(object sender, EventArgs e)//Сохранение..............................................................................................................
        {
            
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)//Считывание..............................................................................................................
        {
            
        }           

        private void textBox36_TextChanged(object sender, EventArgs e)
        {

        }

        private void button52_Click(object sender, EventArgs e)
        {
        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {
        }

        private void button32_Click(object sender, EventArgs e)
        {
        }

        private void button29_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)//Форма..........................................................................................
        {
            
        }
        //Специальность..................................................................................................................................................................
        private void CreateColumns9()//Метод создания....................................................................................................................................
        {
            dataGridView9.Columns.Add("ID", "id");
            dataGridView9.Columns.Add("Name", "Название специальности");//Cоздание колонки
            dataGridView9.Columns.Add("Training_period", "Срок обучения");
            dataGridView9.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow9(DataGridView dqw, IDataRecord record)
        {
            dqw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RowState.ModifiedNew);
        }
        private void RefreshDataGrid9(DataGridView dqw)//Обновление.......................................................................................................................
        {
            dqw.Rows.Clear();
            string queryString = $"SELECT * FROM Specialty";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow9(dqw, reader);
            }
            reader.Close();
            button36.Visible = true;
        }
        private void Search9(DataGridView dgw)//Метод поиска.............................................................................................................................
        {
            dgw.Rows.Clear();

            string searchString = $"SELECT * FROM Specialty WHERE CONCAT (ID, Name, Training_period) LIKE '%" + textBox37.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection()); // поиск

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow9(dgw, read);
            }
            read.Close();
        }
        private void Update9()//Добавление...............................................................................................................................................
        {
            try
            {
                dataBase.openConnection();
                for (int index = 0; index < dataGridView9.Rows.Count; index++) //code Last_name Name Middle_name Phone_number Address
                {
                    var rowState = (RowState)dataGridView9.Rows[index].Cells[3].Value;
                    if (rowState == RowState.Existed)
                        continue;

                    if (rowState == RowState.Deleted)
                    {
                        var ID = Convert.ToInt32(dataGridView9.Rows[index].Cells[0].Value); //добавление
                        var DeleteQuery = $"DELETE FROM Specialty WHERE ID = {ID}";
                        var command = new SqlCommand(DeleteQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modified)
                    {
                        var ID = dataGridView9.Rows[index].Cells[0].Value.ToString();
                        var Name = dataGridView9.Rows[index].Cells[1].Value.ToString();
                        var Training_period = dataGridView9.Rows[index].Cells[2].Value.ToString();
                        var changeQuery = $"update Specialty set Name = '{Name}', Training_period = '{Training_period}' where ID = '{ID}' ";
                        var command = new SqlCommand(changeQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ОШИБКА: ID имеет числовой формат");
            }
            dataBase.closeConnection();
            button36.Visible = true;
        }
        private void deleteRow9()//Удаление.............................................................................................................................................
        {
            int index = dataGridView9.CurrentCell.RowIndex;
            dataGridView9.Rows[index].Visible = false;
            if (dataGridView9.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView9.Rows[index].Cells[3].Value = RowState.Deleted; //удаление
                return;
            }
            dataGridView9.Rows[index].Cells[3].Value = RowState.Deleted;
            button36.Visible = false;

        }
        private void Chande9()//Изменение................................................................................................................................................
        {
            var selectedRowIndex = dataGridView9.CurrentCell.RowIndex;//code Last_name Name Middle_name Phone_number Address
            var ID = textBox55.Text;
            var Name = textBox56.Text;
            var Training_period = textBox43.Text;
            
            //изменение

            dataGridView9.Rows[selectedRowIndex].SetValues(ID, Name, Training_period);
            dataGridView9.Rows[selectedRowIndex].Cells[3].Value = RowState.Modified;
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            Search9(dataGridView9);
        }

        private void button54_Click(object sender, EventArgs e)
        {
            RefreshDataGrid9(dataGridView9);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            Specialty addTP = new Specialty();
            addTP.Show();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            deleteRow9();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            Chande9();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Update9();
        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)//Форма..........................................................................................
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView9.Rows[selectedRow];
                textBox55.Text = row.Cells[0].Value.ToString();
                textBox56.Text = row.Cells[1].Value.ToString();
                textBox43.Text = row.Cells[2].Value.ToString();
                

            }
        }

        private void button38_Click_1(object sender, EventArgs e)
        {
            polz addTP = new polz();
            addTP.Show();
        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void button55_Click(object sender, EventArgs e)
        {
            polz addTP = new polz();
            addTP.Show();
        }

        private void button56_Click(object sender, EventArgs e)
        {
            polz addTP = new polz();
            addTP.Show();
        }

        private void button57_Click(object sender, EventArgs e)
        {
            polz addTP = new polz();
            addTP.Show();
        }

        private void button58_Click(object sender, EventArgs e)
        {
            polz addTP = new polz();
            addTP.Show();
        }

        private void button59_Click(object sender, EventArgs e)
        {
            polz addTP = new polz();
            addTP.Show();
        }

        private void button60_Click(object sender, EventArgs e)
        {
            polz addTP = new polz();
            addTP.Show();
        }

        private void button61_Click(object sender, EventArgs e)
        {
            polz addTP = new polz();
            addTP.Show();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            polz addTP = new polz();
            addTP.Show();
        }
        //Группа..................................................................................................................................
        private void button63_Click(object sender, EventArgs e)
        {
            // задаем текст для печати
            result =" ID: "+ textBox1.Text+"\r\n"; 
            result += " ID Специальность: "+textBox2.Text + "\r\n"; 
            result += " Номер группы: "+textBox3.Text + "\r\n";
            result +=" Кол-во студентов: "+ textBox4.Text + "\r\n";

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
        //Студенты.................................................................................................................................
        private void button64_Click(object sender, EventArgs e)
        {
            // задаем текст для печати
            result = " ID: " + textBox6.Text + "\r\n";
            result += " ID Группы: " + textBox7.Text + "\r\n";
            result += " Фамилия: " + textBox8.Text + "\r\n";
            result += " Имя: " + textBox9.Text + "\r\n";
            result += " Отчество: " + textBox10.Text + "\r\n";

            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler2;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }

        // обработчик события печати
        void PrintPageHandler2(object sender, PrintPageEventArgs e)
        {
            // печать строки result
            e.Graphics.DrawString(result, new Font("Arial", 14), Brushes.Black, 0, 0);
        }
        //Расписание....................................................................................................................................
        private void button65_Click(object sender, EventArgs e)
        {
            // задаем текст для печати
            result = " ID: " + textBox11.Text + "\r\n";
            result += " ID Аудитории: " + textBox12.Text + "\r\n";
            result += " ID Преподавателя: " + textBox13.Text + "\r\n";
            result += " ID Группы: " + textBox14.Text + "\r\n";
            result += " Время: " + textBox15.Text + "\r\n";
            

            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler3;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }

        // обработчик события печати
        void PrintPageHandler3(object sender, PrintPageEventArgs e)
        {
            // печать строки result
            e.Graphics.DrawString(result, new Font("Arial", 14), Brushes.Black, 0, 0);
        }
        //Преподователи.................................................................................................................................
        private void button66_Click(object sender, EventArgs e)
        {
            // задаем текст для печати
            result = " ID: " + textBox20.Text + "\r\n";
            result += " ID Ученой степени: " + textBox21.Text + "\r\n";
            result += " Фамилия: " + textBox22.Text + "\r\n";
            result += " Имя: " + textBox23.Text + "\r\n";
            result += " Отчество: " + textBox24.Text + "\r\n";
            result += " Стаж работы: " + textBox19.Text + "\r\n";
            result += " Номер телефона: " + textBox5.Text + "\r\n";

            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler4;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }

        // обработчик события печати
        void PrintPageHandler4(object sender, PrintPageEventArgs e)
        {
            // печать строки result
            e.Graphics.DrawString(result, new Font("Arial", 14), Brushes.Black, 0, 0);
        }
        //Ученая степень...............................................................................................................................
        private void button67_Click(object sender, EventArgs e)
        {
            // задаем текст для печати
            result = " ID: " + textBox27.Text + "\r\n";
            result += " Название степени: " + textBox28.Text + "\r\n";
            result += " Зарплата: " + textBox29.Text + "\r\n";
            

            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler5;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }

        // обработчик события печати
        void PrintPageHandler5(object sender, PrintPageEventArgs e)
        {
            // печать строки result
            e.Graphics.DrawString(result, new Font("Arial", 14), Brushes.Black, 0, 0);
        }
        //Аудитории......................................................................................................................................
        private void button68_Click(object sender, EventArgs e)
        {
            // задаем текст для печати
            result = " ID: " + textBox34.Text + "\r\n";
            result += " Номер аудитории: " + textBox35.Text + "\r\n";
            result += " Вместимость: " + textBox40.Text + "\r\n";
            


            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler6;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }

        // обработчик события печати
        void PrintPageHandler6(object sender, PrintPageEventArgs e)
        {
            // печать строки result
            e.Graphics.DrawString(result, new Font("Arial", 14), Brushes.Black, 0, 0);
        }
    
        //Спецальность...............................................................................................................................
        private void button71_Click(object sender, EventArgs e)
        {
            // задаем текст для печати
            result = " ID: " + textBox55.Text + "\r\n";
            result += " Название: " + textBox56.Text + "\r\n";
            result += " Срок обучения: " + textBox43.Text + "\r\n";
            
            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler9;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }

        // обработчик события печати
        void PrintPageHandler9(object sender, PrintPageEventArgs e)
        {
            // печать строки result
            e.Graphics.DrawString(result, new Font("Arial", 14), Brushes.Black, 0, 0);
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }
    }
    }
    
    
    
    



