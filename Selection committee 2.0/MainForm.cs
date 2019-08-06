using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Selection_committee_2._0
{
    public partial class MainForm : Form
    {
        static string connectionString = "server=localhost; user=TheSecondChance; database=selection_committee_database; password={My_Database};";
        MySqlConnection sqlConnection = new MySqlConnection(connectionString);
        DataTable dataTable, deleteDataTable;
        int amountOfSpecialties = 1;
        int amountOfAchievements = 1;
        string Surname;
        bool isFourExams = false;

        public MainForm()
        {
            InitializeComponent();
        }

        public string ConvertDateToMySQLFormat(DateTimePicker timePicker)
        {
            string[] temp = new string[3];
            string date;

            date = timePicker.Value.ToString();
            date = date.Substring(0, 10);
            temp = date.Split('.');
            date = temp[2] + '-' + temp[1] + '-' + temp[0];

            return date;
        }

        public void ClearPageControls(TabPage tabPage)
        {
            foreach (Control control in tabPage.Controls) // Очистка элементов окна
            {
                if (control.GetType() == typeof(TextBox))
                    control.Text = string.Empty;
                if (control.GetType() == typeof(DateTimePicker))
                    control.Text = DateTime.Today.ToString();
                if (control.GetType() == typeof(ComboBox))
                    control.Text = string.Empty;
            }
        }

        public void ClearPanelControls(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                    control.Text = string.Empty;
                if (control.GetType() == typeof(DateTimePicker))
                    control.Text = DateTime.Today.ToString();
                if (control.GetType() == typeof(ComboBox))
                    control.Text = string.Empty;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();

            InitializeMainPage();
            InitializeDeletePage();
            InitializeEnrolleeAddPage1();
            InitiallizeEnrolleePage2();
        }

        public void RefreshPages()
        {
            InitializeMainPage();
            InitializeDeletePage();
        }

        public void UpdateDataGridView(DataGridView gridView, ref DataTable dataTable, string sqlRequest)
        {

            MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            try
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                dataAdapter.SelectCommand = sqlCommand;
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                BindingSource bindingSource = new BindingSource();
                gridView.DataSource = dataTable;
                dataAdapter.Update(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void InitializeMainPage()
        {
            string sqlRequest = "select * from mainpage_information;";
            UpdateDataGridView(dataGridView_MainPage, ref dataTable, sqlRequest);

            dataGridView_MainPage.Columns[1].Width = 275;
            dataGridView_MainPage.Columns[2].Width = 390;
            dataGridView_MainPage.Columns[3].Width = 125;
            dataGridView_MainPage.Columns[5].Width = 200;

            for (int i = 0; i < dataGridView_MainPage.ColumnCount; i++)
            {
                comboBox_Search.Items.Add(dataGridView_MainPage.Columns[i].HeaderText);
            }
            comboBox_Search.SelectedItem = "Полное имя";
        }

        private void toolStripMenuItem_MainPage_Click(object sender, EventArgs e)
        {
            tabControl_Application.SelectedTab = tabMainPage;
        }

        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(dataTable);
            string searchColumn;
            searchColumn = comboBox_Search.Text;
            if (textBox_Search.Text != string.Empty)
            {
                if (searchColumn == "ID абитуриента")
                {
                    dataView.RowFilter = string.Format("`" + searchColumn + "` = {0}", textBox_Search.Text);
                    dataGridView_MainPage.DataSource = dataView;
                }
                else if (searchColumn == "Сумма баллов")
                {

                    dataView.RowFilter = string.Format("`" + searchColumn + "` > {0}", textBox_Search.Text);
                    dataGridView_MainPage.DataSource = dataView;
                }
                else
                {
                    dataView.RowFilter = string.Format("`" + searchColumn + "` LIKE '%{0}%'", textBox_Search.Text);
                    dataGridView_MainPage.DataSource = dataView;
                }
            }
        }

        private void comboBox_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Search.Text == "Сумма баллов")
            {
                label_Find.Text = "Сумма баллов больше:";
            }
            else if (comboBox_Search.Text == "Дата рождения")
            {
                textBox_Search.Enabled = false;
            }
            else
            {
                label_Find.Text = "Найти:";
                textBox_Search.Enabled = true;
            }
        }

        private void dateTimePicker_DateSearch_ValueChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(dataTable);
            string searchColumn;
            searchColumn = comboBox_Search.Text;
            if (searchColumn == "Дата рождения")
            {
                dataView.RowFilter = string.Format("`" + searchColumn + "` <= #{0}#", ConvertDateToMySQLFormat(dateTimePicker_DateSearch));
                dataGridView_MainPage.DataSource = dataView;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_amountOfRows.Text = "Отобрано записей: " + dataGridView_MainPage.RowCount.ToString();
            label_amountOfRowsInDelete.Text = "Отобрано записей: " + dataGridView_DeletePage.RowCount.ToString();
        }

        public void InitializeEnrolleeAddPage1()
        {
            if (comboBox_Nationality.Text == "Российская Федерация")
            {
                textBox_PassportSeries.MaxLength = 4;
                textBox_PassportNumber.MaxLength = 6;
                textBox_PhoneNumber.MaxLength = 11;
                textBox_EduDocNumber.MaxLength = 14;
            }

            button_ToSpecialtyAndExamPage.Text = "Сохранить данные и продолжить " + char.ConvertFromUtf32(0x2192);

            string sqlRequest = "select Nationality from enrollee group by Nationality;";
            MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            MySqlDataReader dataReader = sqlCommand.ExecuteReader();
            AutoCompleteStringCollection Nationality_stringCollection = new AutoCompleteStringCollection();

            comboBox_Nationality.Items.Clear();
            while (dataReader.Read())
            {
                Nationality_stringCollection.Add(dataReader[0].ToString());
                comboBox_Nationality.Items.Add(dataReader[0]);
            }
            comboBox_Nationality.AutoCompleteCustomSource = Nationality_stringCollection;
            dataReader.Close();

            sqlRequest = "select substring(Address, 1, instr(Address, ',')-1) as Street from enrollee group by Street;";
            sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            dataReader = sqlCommand.ExecuteReader();
            AutoCompleteStringCollection Street_stringCollection = new AutoCompleteStringCollection();

            comboBox_Street.Items.Clear();
            while (dataReader.Read())
            {
                Street_stringCollection.Add(dataReader[0].ToString());
                comboBox_Street.Items.Add(dataReader[0]);
            }
            comboBox_Street.AutoCompleteCustomSource = Street_stringCollection;
            dataReader.Close();

            sqlRequest = "select Issued_by from enrollee group by Issued_by;";
            sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            dataReader = sqlCommand.ExecuteReader();
            AutoCompleteStringCollection Issued_by_stringCollection = new AutoCompleteStringCollection();

            while (dataReader.Read())
            {
                Issued_by_stringCollection.Add(dataReader[0].ToString());
            }
            textBox_IssuedBy.AutoCompleteCustomSource = Issued_by_stringCollection;
            dataReader.Close();

            sqlRequest = "select Edu_doc_name from enrollee group by Edu_doc_name;";
            sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            dataReader = sqlCommand.ExecuteReader();
            AutoCompleteStringCollection EduDocName_stringCollection = new AutoCompleteStringCollection();

            comboBox_EduDocName.Items.Clear();
            while (dataReader.Read())
            {
                EduDocName_stringCollection.Add(dataReader[0].ToString());
                comboBox_EduDocName.Items.Add(dataReader[0]);
            }
            comboBox_EduDocName.AutoCompleteCustomSource = EduDocName_stringCollection;
            dataReader.Close();
        }

        public void InitiallizeEnrolleePage2()
        {
            string sqlRequest = "select Specialty_name from specialty order by Specialty_name;";
            MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            MySqlDataReader dataReader = sqlCommand.ExecuteReader();
            AutoCompleteStringCollection FirstSpecialty_stringCollection = new AutoCompleteStringCollection();

            while (dataReader.Read())
            {
                FirstSpecialty_stringCollection.Add(dataReader[0].ToString());
                comboBox_FirstSpecialty.Items.Add(dataReader[0]);
            }
            comboBox_FirstSpecialty.AutoCompleteCustomSource = FirstSpecialty_stringCollection;
            dataReader.Close();

            sqlRequest = "select Achievement_name from selection_committee_database.achievement order by Achievement_name;";
            sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            dataReader = sqlCommand.ExecuteReader();
            AutoCompleteStringCollection Achievements_stringCollection = new AutoCompleteStringCollection();

            while (dataReader.Read())
            {
                Achievements_stringCollection.Add(dataReader[0].ToString());
                comboBox_FirstAchievement.Items.Add(dataReader[0]);
            }
            comboBox_FirstAchievement.AutoCompleteCustomSource = Achievements_stringCollection;
            dataReader.Close();
        }

        public void SendFirstEnrolleAddPageDataToDatabese()
        {
            string[] temp = new string[3];
            string fullName, surname, name, patronymicName = null;
            string dateOfBirth;
            char gender;
            string nationality;
            string address, street, houseNumber;
            string phoneNumber, EmailAddress;
            string passportSeries, passportNumber, passportDateOfIssue, passportIssuedBy;
            string eduDocName, eduDocNumber, eduDocDateOfIssue;
            string eduDocType;

            fullName = textBox_FullName.Text;
            temp = fullName.Split(' ');
            temp = temp.Where(x => x != "").ToArray(); // Убираем лишние пробелы при их наличии
            surname = temp[0];
            Surname = surname; // Глобальная переменная для передачи в другую функцию
            name = temp[1];
            if (temp.Length == 3)
            {
                patronymicName = temp[2];
            }

            dateOfBirth = ConvertDateToMySQLFormat(dateTimePicker_DateOfBirth);

            if (radioButton_MaleGender.Checked) gender = 'M';
            else gender = 'Ж';

            nationality = comboBox_Nationality.Text;

            street = comboBox_Street.Text;
            houseNumber = textBox_HouseNumber.Text;
            address = street + ", " + houseNumber;

            phoneNumber = textBox_PhoneNumber.Text;
            EmailAddress = textBox_EMail.Text;

            passportSeries = textBox_PassportSeries.Text;
            passportNumber = textBox_PassportNumber.Text;
            passportDateOfIssue = ConvertDateToMySQLFormat(dateTimePicker_PassportDateOfIssue);
            passportIssuedBy = textBox_IssuedBy.Text;

            eduDocName = comboBox_EduDocName.Text;
            eduDocNumber = textBox_EduDocNumber.Text;
            eduDocDateOfIssue = ConvertDateToMySQLFormat(dateTimePicker_EduDocDateOfIssue);
            if (radioButton_EduDocTypeOriginal.Checked) eduDocType = "оригинал";
            else eduDocType = "копия";

            string sqlRequest = "insert into enrollee (Surname, Name, Patronymic_name, Date_of_birth, Gender, Nationality, Address, E_mail, Phone_number, " +
                "Pass_series, Pass_number, Pass_date_of_issue, Issued_by, Edu_doc_name, Edu_doc_type, Edu_doc_number, Edu_doc_date_of_issue)" +
                "values ('" + surname + "','" + name + "','" + patronymicName + "','" + dateOfBirth + "','" + gender + "','" + nationality + "','" + address + "','" + EmailAddress + "','" +
                "" + phoneNumber + "','" + passportSeries + "','" + passportNumber + "','" + passportDateOfIssue + "','" + passportIssuedBy + "','" +
                "" + eduDocName + "','" + eduDocType + "','" + eduDocNumber + "','" + eduDocDateOfIssue + "');";

            MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public void SendSecondEnrolleAddPageDataToDatabese()
        {
            string enrollee_id;
            string sqlRequest = "select id from enrollee where Surname = '" + Surname + "';";
            MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            MySqlDataReader dataReader;
            enrollee_id = sqlCommand.ExecuteScalar().ToString();

            // Данные о предпочтительных специальностях
            List<string> specialtyCode = new List<string>();
            List<string> specialtyName = new List<string>();

            specialtyName.Add(comboBox_FirstSpecialty.Text);
            specialtyName.Add(comboBox_SecondSpecialty.Text);
            specialtyName.Add(comboBox_ThirdSpecialty.Text);
            specialtyName = specialtyName.Where(x => x != string.Empty).ToList();

            if (comboBox_FirstSpecialty.Text == string.Empty)
                MessageBox.Show("Не все поля заполнены!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (comboBox_SecondSpecialty.Text == string.Empty && comboBox_ThirdSpecialty.Text == string.Empty)
                amountOfSpecialties = 1;
            else if (comboBox_SecondSpecialty.Text == string.Empty || comboBox_ThirdSpecialty.Text == string.Empty)
                amountOfSpecialties = 2;

            if (amountOfSpecialties == 1)
            {
                sqlRequest = "select Specialty_code from specialty where Specialty_name = '" + specialtyName[0] + "';";
                sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                specialtyCode.Add(sqlCommand.ExecuteScalar().ToString());

                sqlRequest = "insert into chosen_specialties (Enrollee_id, Chosen_specialty_code) values ('" + enrollee_id + "', '" + specialtyCode[0] + "');";
                sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            else
            {
                if (amountOfSpecialties == 2)
                    sqlRequest = "select Specialty_code from specialty where(Specialty_name = '" + specialtyName[0] + "' || Specialty_name = '" + specialtyName[1] + "');";
                else
                    sqlRequest = "select Specialty_code from specialty where(Specialty_name = '" + specialtyName[0] + "' || Specialty_name = '" + specialtyName[1] + "' || Specialty_name = '" + specialtyName[2] + "');";
                sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    specialtyCode.Add(dataReader[0].ToString());
                }
                dataReader.Close();

                foreach (string code in specialtyCode)
                {
                    sqlRequest = "insert into chosen_specialties (Enrollee_id, Chosen_specialty_code) values ('" + enrollee_id + "', '" + code + "');";
                    sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }

            }

            // Данные об индивидуальных достижениях
            List<string> achievementName = new List<string>();
            List<string> achievementId = new List<string>();
            List<string> achievementDate = new List<string>();

            achievementName.Add(comboBox_FirstAchievement.Text);
            achievementName.Add(comboBox_SecondAchievement.Text);
            achievementName.Add(comboBox_ThirdAchievement.Text);

            achievementDate.Add(ConvertDateToMySQLFormat(dateTimePicker_FirstAchievement));
            achievementDate.Add(ConvertDateToMySQLFormat(dateTimePicker_SecondAchievement));
            achievementDate.Add(ConvertDateToMySQLFormat(dateTimePicker_ThirdAchievement));

            achievementName = achievementName.Where(x => x != string.Empty).ToList();

            if (achievementName.Count != 0)
            {
                achievementDate = achievementDate.Where(x => x != string.Empty).ToList();
                if (amountOfAchievements == 1)
                {
                    sqlRequest = "select id from achievement where Achievement_name = '" + achievementName[0] + "';";
                    sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                    achievementId.Add(sqlCommand.ExecuteScalar().ToString());

                    sqlRequest = "insert into enrollee_achievements (Enrollee_id, Achievement_id, Date_of_issue) values ('" + enrollee_id + "', '" + achievementId[0] + "', '" + achievementDate[0] + "');";
                    sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    if (amountOfAchievements == 2)
                        sqlRequest = "select id from achievement where(Achievement_name = '" + achievementName[0] + "' || Achievement_name = '" + achievementName[1] + "');";
                    else
                        sqlRequest = "select id from achievement where(Achievement_name = '" + achievementName[0] + "' || Achievement_name = '" + achievementName[1] + "' || Achievement_name = '" + achievementName[2] + "');";
                    sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                    dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        achievementId.Add(dataReader[0].ToString());
                    }
                    dataReader.Close();

                    foreach (var tuple in achievementId.Zip(achievementDate, Tuple.Create))
                    {
                        sqlRequest = "insert into enrollee_achievements (Enrollee_id, Achievement_id, Date_of_issue) values ('" + enrollee_id + "', '" + tuple.Item1 + "', '" + tuple.Item2 + "');";
                        sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }

            // Данные об экзаменах
            int amountOfExams;
            if (isFourExams) amountOfExams = 4;
            else amountOfExams = 3;

            string[] examName = new string[amountOfExams];
            string[] examId = new string[amountOfExams];
            string[] dateOfExam = new string[amountOfExams];
            string[] amountOfPoints = new string[amountOfExams];

            examName[0] = comboBox_FirstExamName.Text;
            dateOfExam[0] = ConvertDateToMySQLFormat(dateTimePicker_FirstExamDate);
            amountOfPoints[0] = textBox_FirstExamPoints.Text;
            examName[1] = comboBox_SecondExamName.Text;
            dateOfExam[1] = ConvertDateToMySQLFormat(dateTimePicker_SecondExamDate);
            amountOfPoints[1] = textBox_SecondExamPoints.Text;
            examName[2] = comboBox_ThirdExamName.Text;
            dateOfExam[2] = ConvertDateToMySQLFormat(dateTimePicker_ThirdExamDate);
            amountOfPoints[2] = textBox_ThirdExamPoints.Text;
            if (isFourExams)
            {
                examName[3] = comboBox_FourthExamName.Text;
                dateOfExam[3] = ConvertDateToMySQLFormat(dateTimePicker_FourthExamDate);
                amountOfPoints[3] = textBox_FourthExamPoints.Text;
            }

            for (int i = 0; i < examId.Length; i++)
            {
                sqlRequest = "select id from exam where Exam_name = '" + examName[i] + "';";
                sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                examId[i] = sqlCommand.ExecuteScalar().ToString();
            }

            for (int i = 0; i < amountOfExams; i++)
            {
                sqlRequest = "insert into enrollee_exams (Enrollee_id, Exam_id, Date_of_exam, Amount_of_points) values ('" + enrollee_id + "', '" + examId[i] + "', '" + dateOfExam[i] + "', '" + amountOfPoints[i] + "');";
                sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            amountOfSpecialties = 0;
            amountOfAchievements = 0;
            label_SecondSpecialty.Visible = false;
            label_ThirdSpecialty.Visible = false;
            comboBox_SecondSpecialty.Visible = false;
            comboBox_ThirdSpecialty.Visible = false;
            panel_SecondAchievement.Visible = false;
            panel_ThirdAchievement.Visible = false;

        }

        private void EnrolleeAdditionMenuItem_Click(object sender, EventArgs e)
        {
            tabControl_Application.SelectedTab = tabPageEnrolleeAdd1;
            comboBox_Nationality.Text = "Российская Федерация";
        }

        public void InitializeDeletePage()
        {
            string sqlRequest = "select * from mainpage_information;";
            UpdateDataGridView(dataGridView_DeletePage, ref deleteDataTable, sqlRequest);

            int temp;
            temp = dataGridView_DeletePage.ColumnCount;
            dataGridView_DeletePage.Columns[1].Width = 275;
            dataGridView_DeletePage.Columns[2].Width = 390;
            dataGridView_DeletePage.Columns[3].Width = 125;
            dataGridView_DeletePage.Columns[5].Width = 200;

            for (int i = 0; i < dataGridView_DeletePage.ColumnCount; i++)
            {
                comboBox_SearchToDelete.Items.Add(dataGridView_DeletePage.Columns[i].HeaderText);
            }
            comboBox_SearchToDelete.SelectedItem = "Полное имя";
        }

        private void textBox_SearchToDelete_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(deleteDataTable);
            string searchColumn;
            searchColumn = comboBox_SearchToDelete.Text;
            if (comboBox_SearchToDelete.Text != string.Empty)
            {
                if (searchColumn == "ID абитуриента")
                {
                    dataView.RowFilter = string.Format("`" + searchColumn + "` = {0}", textBox_SearchToDelete.Text);
                    dataGridView_DeletePage.DataSource = dataView;
                }
                else if (searchColumn == "Сумма баллов")
                {

                    dataView.RowFilter = string.Format("`" + searchColumn + "` > {0}", textBox_SearchToDelete.Text);
                    dataGridView_DeletePage.DataSource = dataView;
                }
                else
                {
                    dataView.RowFilter = string.Format("`" + searchColumn + "` LIKE '%{0}%'", textBox_SearchToDelete.Text);
                    dataGridView_DeletePage.DataSource = dataView;
                }
            }
        }

        private void comboBox_SearchToDelete_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_SearchToDelete.Text == "Сумма баллов")
            {
                label_FindToDelete.Text = "Сумма баллов больше:";
            }
            else if (comboBox_SearchToDelete.Text == "Дата рождения")
            {
                textBox_SearchToDelete.Enabled = false;
            }
            else
            {
                label_FindToDelete.Text = "Найти:";
                textBox_SearchToDelete.Enabled = true;
            }
        }

        private void dateTimePicker_DateSearchToDelete_ValueChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(deleteDataTable);
            string searchColumn;
            searchColumn = comboBox_SearchToDelete.Text;
            if (searchColumn == "Дата рождения")
            {
                dataView.RowFilter = string.Format("`" + searchColumn + "` <= #{0}#", ConvertDateToMySQLFormat(dateTimePicker_DateSearchToDelete));
                dataGridView_DeletePage.DataSource = dataView;
            }
        }

        private void button_DeleteEnrollee_Click(object sender, EventArgs e)
        {
            DialogResult result;
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon warningIcon = MessageBoxIcon.Warning;
            MessageBoxIcon informationIcon = MessageBoxIcon.Information;
            string enrolleeId, full_name;

            if (dataGridView_DeletePage.RowCount != 1)
            {
                MessageBox.Show("Необходимо выбрать одного абитуриента для удаления!", "Ошибка", button, warningIcon);
            }
            else
            {
                enrolleeId = dataGridView_DeletePage[0, 0].Value.ToString();
                full_name = dataGridView_DeletePage[1, 0].Value.ToString();

                result = MessageBox.Show("Вы действительно хотите безвозвратно удалить абитуриента " + full_name + "?", "Удаление", buttons);
                if (result == DialogResult.Yes)
                {
                    string sqlRequest = "delete from enrollee where id = '" + enrolleeId + "';";
                    MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Все данные о " + full_name + " были успешно удалены!", "Успешное удаление", button, informationIcon);
                    RefreshPages();
                    tabControl_Application.SelectedTab = tabMainPage;
                }
            }
        }

        private void ToolStripMenuItem_DeleteEnrollee_Click(object sender, EventArgs e)
        {
            tabControl_Application.SelectedTab = tabPageEnrolleeDelete;
        }

        private void button_ToSpecialtyAndExamPage_Click(object sender, EventArgs e)
        {
            SendFirstEnrolleAddPageDataToDatabese();

            tabControl_Application.SelectedTab = tabPageEnrolleeAdd2;

            // Очистка элементов окна
            ClearPageControls(tabPageEnrolleeAdd1);
            if (radioButton_MaleGender.Checked) radioButton_MaleGender.Checked = false;
            else radioButton_FemaleGender.Checked = false;
            if (radioButton_EduDocTypeOriginal.Checked) radioButton_EduDocTypeOriginal.Checked = false;
            else radioButton_EduDocTypeCopy.Checked = false;
        }

        private void button_AddSpecialty_Click(object sender, EventArgs e)
        {
            amountOfSpecialties++;
            string FirstSpecialty = comboBox_FirstSpecialty.Text;
            Point buttonAddSpecialtyPosition;
            Point buttonCheckExamsPosition;
            if (amountOfSpecialties == 2)
            {
                buttonAddSpecialtyPosition = new Point(430, 235);
                buttonCheckExamsPosition = new Point(478, 235);
                label_SecondSpecialty.Visible = true;
                comboBox_SecondSpecialty.Visible = true;
                button_AddSpecialty.Location = buttonAddSpecialtyPosition;
                button_CheckExams.Location = buttonCheckExamsPosition;

                string sqlRequest = "select Specialty_name from specialty where Specialty_name != '" + FirstSpecialty + "' order by Specialty_name;";
                MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();
                AutoCompleteStringCollection SecondSpecialty_stringCollection = new AutoCompleteStringCollection();

                while (dataReader.Read())
                {
                    SecondSpecialty_stringCollection.Add(dataReader[0].ToString());
                    comboBox_SecondSpecialty.Items.Add(dataReader[0]);
                }
                comboBox_SecondSpecialty.AutoCompleteCustomSource = SecondSpecialty_stringCollection;
                dataReader.Close();
            }
            else
            {
                string SecondSpecialty = comboBox_SecondSpecialty.Text;
                button_AddSpecialty.Visible = false;
                label_ThirdSpecialty.Visible = true;
                comboBox_ThirdSpecialty.Visible = true;
                buttonCheckExamsPosition = new Point(430, 317);
                button_CheckExams.Location = buttonCheckExamsPosition;

                string sqlRequest = "select Specialty_name from specialty where (Specialty_name != '" + FirstSpecialty + "' && Specialty_name != '" + SecondSpecialty + "') order by Specialty_name;";
                MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();
                AutoCompleteStringCollection ThirdSpecialty_stringCollection = new AutoCompleteStringCollection();

                while (dataReader.Read())
                {
                    ThirdSpecialty_stringCollection.Add(dataReader[0].ToString());
                    comboBox_ThirdSpecialty.Items.Add(dataReader[0]);
                }
                comboBox_ThirdSpecialty.AutoCompleteCustomSource = ThirdSpecialty_stringCollection;
                dataReader.Close();
            }
        }

        private void button_AddAchievement_Click(object sender, EventArgs e)
        {
            amountOfAchievements++;
            string FirstAchievement = comboBox_FirstAchievement.Text;
            Point buttonAddAchievementPosition;
            if (amountOfAchievements == 2)
            {
                buttonAddAchievementPosition = new Point(543, 519);
                panel_SecondAchievement.Visible = true;
                button_AddAchievement.Location = buttonAddAchievementPosition;

                string sqlRequest = "select Achievement_name from achievement where Achievement_name != '" + FirstAchievement + "'  order by Achievement_name;";
                MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();
                AutoCompleteStringCollection SecondAchievement_stringCollection = new AutoCompleteStringCollection();

                while (dataReader.Read())
                {
                    SecondAchievement_stringCollection.Add(dataReader[0].ToString());
                    comboBox_SecondAchievement.Items.Add(dataReader[0]);
                }
                comboBox_SecondAchievement.AutoCompleteCustomSource = SecondAchievement_stringCollection;
                dataReader.Close();
            }
            else
            {
                string SecondAchievement = comboBox_SecondAchievement.Text;
                panel_ThirdAchievement.Visible = true;
                button_AddAchievement.Visible = false;

                string sqlRequest = "select Achievement_name from achievement where (Achievement_name != '" + FirstAchievement + "' && Achievement_name != '" + SecondAchievement + "') order by Achievement_name;";
                MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();
                AutoCompleteStringCollection ThirdAchievement_stringCollection = new AutoCompleteStringCollection();

                while (dataReader.Read())
                {
                    ThirdAchievement_stringCollection.Add(dataReader[0].ToString());
                    comboBox_ThirdAchievement.Items.Add(dataReader[0]);
                }
                comboBox_ThirdAchievement.AutoCompleteCustomSource = ThirdAchievement_stringCollection;
                dataReader.Close();
            }
        }

        private void button_CheckExams_Click(object sender, EventArgs e)
        {
            comboBox_FirstExamName.Items.Clear();
            comboBox_SecondExamName.Items.Clear();
            comboBox_ThirdExamName.Items.Clear();
            comboBox_FourthExamName.Items.Clear();

            string FirstSpecialty, SecondSpecialty, ThirdSpecialty;
            List<string> suggestedExams = new List<string>();
            List<string> placedExams = new List<string>();
            FirstSpecialty = comboBox_FirstSpecialty.Text;
            SecondSpecialty = comboBox_SecondSpecialty.Text;
            ThirdSpecialty = comboBox_ThirdSpecialty.Text;


            panel_FourthExam.Visible = false;
            isFourExams = false;
            if (FirstSpecialty == "Дизайн" || FirstSpecialty == "Архитектура" || FirstSpecialty == "Дизайн архитектурной среды")
            {
                panel_FourthExam.Visible = true;
                isFourExams = true;
            }

            string sqlRequest = "select Exam_name from specialty join specialty_exams on (specialty.Specialty_code = specialty_exams.Specialty_exam_code)" +
                " join exam on (exam.id = specialty_exams.Exam_id) where Specialty_name = '" + FirstSpecialty + "';";
            MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            MySqlDataReader dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                suggestedExams.Add(dataReader[0].ToString());
            }
            dataReader.Close();
            placedExams = suggestedExams;

            sqlRequest = "select Exam_name from specialty join specialty_exams on (specialty.Specialty_code = specialty_exams.Specialty_exam_code)" +
                " join exam on (exam.id = specialty_exams.Exam_id) where Specialty_name = '" + SecondSpecialty + "';";
            sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                suggestedExams.Add(dataReader[0].ToString());
            }
            dataReader.Close();

            sqlRequest = "select Exam_name from specialty join specialty_exams on (specialty.Specialty_code = specialty_exams.Specialty_exam_code)" +
                " join exam on (exam.id = specialty_exams.Exam_id) where Specialty_name = '" + ThirdSpecialty + "';";
            sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                suggestedExams.Add(dataReader[0].ToString());
            }
            dataReader.Close();

            suggestedExams = suggestedExams.Distinct().ToList();

            AutoCompleteStringCollection stringCollection_Exams = new AutoCompleteStringCollection();
            foreach (string element in suggestedExams)
            {
                comboBox_FirstExamName.Items.Add(element);
                stringCollection_Exams.Add(element);
            }
            comboBox_FirstExamName.AutoCompleteCustomSource = stringCollection_Exams;

            foreach (string element in suggestedExams)
            {
                comboBox_SecondExamName.Items.Add(element);
            }
            comboBox_SecondExamName.AutoCompleteCustomSource = stringCollection_Exams;

            foreach (string element in suggestedExams)
            {
                comboBox_ThirdExamName.Items.Add(element);
            }
            comboBox_ThirdExamName.AutoCompleteCustomSource = stringCollection_Exams;

            if (isFourExams)
            {
                foreach (string element in suggestedExams)
                {
                    comboBox_FourthExamName.Items.Add(element);
                }
                comboBox_FourthExamName.AutoCompleteCustomSource = stringCollection_Exams;
            }

            comboBox_FirstExamName.Text = placedExams[0];
            comboBox_SecondExamName.Text = placedExams[1];
            comboBox_ThirdExamName.Text = placedExams[2];
            if (isFourExams) comboBox_FourthExamName.Text = placedExams[3];
        }

        private void button_EnrolleeAddExit_Click(object sender, EventArgs e)
        {

            DialogResult result = CheckEnrolleePoints();

            if (result == DialogResult.None)
            {
                DialogResult isSendToDatabase;
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                isSendToDatabase = MessageBox.Show("Завершить ввод данных об абитуриенте?", "Завершение ввода", buttons);
                if (isSendToDatabase == DialogResult.Yes)
                {
                    tabControl_Application.SelectedTab = tabMainPage;
                    SendSecondEnrolleAddPageDataToDatabese(); // Отправление данных в базу
                    RefreshPages();
                    MessageBox.Show("Информация была успешно внесена в базу данных!", "Успешный ввод", button, icon);
                    // Очистка элементов окна
                    ClearPageControls(tabPageEnrolleeAdd2);
                    ClearPanelControls(panel_FourthExam);
                    ClearPanelControls(panel_SecondAchievement);
                    ClearPanelControls(panel_ThirdAchievement);
                }
            }
        }

        private void ToolStripMenuItem_Report_Click(object sender, EventArgs e)
        {
            tabControl_Application.SelectedTab = tabPageCrystalReport;
        }

        private void tabPageCrystalReport_Enter(object sender, EventArgs e)
        {
            string sqlRequest = "select Specialty_name, concat(enrollee.Surname, ' ', enrollee.Name,' ', enrollee.Patronymic_name) as full_name, enrollee_total_points.`Общий балл абитуриента` as total_points, enrollee.Edu_doc_type as edu_doc_type from enrollee join enrollee_total_points on (enrollee_total_points.ID = enrollee.id) join chosen_specialties on (chosen_specialties.Enrollee_id = enrollee.id) join specialty on (specialty.Specialty_code = chosen_specialties.Chosen_specialty_code) order by total_points desc;";
            MySqlCommand sqlCommand = new MySqlCommand(sqlRequest, sqlConnection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataSet myData = new DataSet();
            
            try
            {
                sqlCommand.CommandText = sqlRequest;
                sqlCommand.Connection = sqlConnection;
                dataAdapter.SelectCommand = sqlCommand;
                dataAdapter.Fill(myData);
                myData.WriteXml(@"C:\Users\roenk\OneDrive\Documents\Visual Studio 2017\Projects\Selection committee 2.0\XML_datasets\dataset.xml", XmlWriteMode.WriteSchema);
                enrolleeReport.Load(@".\EnrolleeReport.rpt");
                enrolleeReport.SetDataSource(myData);
                ReportViewer.ReportSource = enrolleeReport;

            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Отчёт не может быть создан", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void comboBox_Nationality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Nationality.Text != "Российская Федерация")
            {
                textBox_PassportSeries.MaxLength = 25;
                textBox_PassportNumber.MaxLength = 25;
                textBox_PhoneNumber.MaxLength = 25;
                textBox_EduDocNumber.MaxLength = 25;
            }
            else
            {
                textBox_PassportSeries.MaxLength = 4;
                textBox_PassportNumber.MaxLength = 6;
                textBox_PhoneNumber.MaxLength = 11;
                textBox_EduDocNumber.MaxLength = 14;
            }
        }
     
        public DialogResult CheckEnrolleePoints()
        {
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            DialogResult result = DialogResult.None;


            if (textBox_FirstExamPoints.Text == string.Empty || textBox_SecondExamPoints.Text == string.Empty || textBox_ThirdExamPoints.Text == string.Empty)
            {
                return result = MessageBox.Show("Не все поля заполнены!", "Предупреждение", button, icon);
            }
            else if (isFourExams)
            {
                if (textBox_FourthExamPoints.Text == string.Empty)
                {
                    return result = MessageBox.Show("Не все поля заполнены!", "Предупреждение", button, icon);
                }
            }

            if (Convert.ToInt16(textBox_FirstExamPoints.Text) > 100 || Convert.ToInt16(textBox_FirstExamPoints.Text) < 0)
                result = MessageBox.Show("Число баллов принимает значение от 0 до 100!", "Предупреждение", button, icon);
            else if (Convert.ToInt16(textBox_SecondExamPoints.Text) > 100 || Convert.ToInt16(textBox_SecondExamPoints.Text) < 0)
                result = MessageBox.Show("Число баллов принимает значение от 0 до 100!", "Предупреждение", button, icon);
            else if (Convert.ToInt16(textBox_ThirdExamPoints.Text) > 100 || Convert.ToInt16(textBox_ThirdExamPoints.Text) < 0)
                result = MessageBox.Show("Число баллов принимает значение от 0 до 100!", "Предупреждение", button, icon);
            else if (isFourExams)
            {
                if (Convert.ToInt16(textBox_FourthExamPoints.Text) > 100 || Convert.ToInt16(textBox_FourthExamPoints.Text) < 0)
                    result = MessageBox.Show("Число баллов принимает значение от 0 до 100!", "Предупреждение", button, icon);
                else if (comboBox_FirstSpecialty.Text == "Архитектура" || comboBox_FirstSpecialty.Text == "Дизайн архитектурной среды")
                {
                    if (Convert.ToInt16(textBox_FirstExamPoints.Text) < 36 || Convert.ToInt16(textBox_SecondExamPoints.Text) < 27 || Convert.ToInt16(textBox_ThirdExamPoints.Text) < 30 || Convert.ToInt16(textBox_FourthExamPoints.Text) < 30)
                        result = MessageBox.Show("У абитуриента недостаточно баллов для поступления на данную специальность!", "Предупреждение", button, icon);
                }
                else if (comboBox_FirstSpecialty.Text == "Дизайн")
                {
                    if (Convert.ToInt16(textBox_FirstExamPoints.Text) < 36 || Convert.ToInt16(textBox_SecondExamPoints.Text) < 42 || Convert.ToInt16(textBox_ThirdExamPoints.Text) < 30 || Convert.ToInt16(textBox_FourthExamPoints.Text) < 30)
                        result = MessageBox.Show("У абитуриента недостаточно баллов для поступления на данную специальность!", "Предупреждение", button, icon);
                }
            }
            else
            {
                if (comboBox_FirstSpecialty.Text == "Информационная безопасность" || comboBox_FirstSpecialty.Text == "Программная инженерия")
                {
                    if (Convert.ToInt16(textBox_FirstExamPoints.Text) < 40 || Convert.ToInt16(textBox_SecondExamPoints.Text) < 40 || Convert.ToInt16(textBox_ThirdExamPoints.Text) < 40)
                        result = MessageBox.Show("У абитуриента недостаточно баллов для поступления на данную специальность!", "Предупреждение", button, icon);
                }
                else if (comboBox_FirstSpecialty.Text == "Экономика" || comboBox_FirstSpecialty.Text == "Менеджмент" || comboBox_FirstSpecialty.Text == "Государственное и муниципальное управление" || comboBox_FirstSpecialty.Text == "Бизнес-информатика")
                {
                    if (Convert.ToInt16(textBox_FirstExamPoints.Text) < 36 || Convert.ToInt16(textBox_SecondExamPoints.Text) < 27 || Convert.ToInt16(textBox_ThirdExamPoints.Text) < 42)
                        result = MessageBox.Show("У абитуриента недостаточно баллов для поступления на данную специальность!", "Предупреждение", button, icon);
                }
                else if (comboBox_FirstSpecialty.Text == "Химическая технология")
                {
                    if (Convert.ToInt16(textBox_FirstExamPoints.Text) < 36 || Convert.ToInt16(textBox_SecondExamPoints.Text) < 27 || Convert.ToInt16(textBox_ThirdExamPoints.Text) < 36)
                        result = MessageBox.Show("У абитуриента недостаточно баллов для поступления на данную специальность!", "Предупреждение", button, icon);
                }
                else
                {
                    if (Convert.ToInt16(textBox_FirstExamPoints.Text) < 36 || Convert.ToInt16(textBox_SecondExamPoints.Text) < 27 || Convert.ToInt16(textBox_ThirdExamPoints.Text) < 36)
                        result = MessageBox.Show("У абитуриента недостаточно баллов для поступления на данную специальность!", "Предупреждение", button, icon);
                }
            }
            return result;
        }

        

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Вы действительно хотите выйти из программы?";
            string caption = "   Завершение работы";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(text, caption, buttons);

            if (result == DialogResult.Yes)
            {
                sqlConnection.Close();
                this.Close();
            }

        }
    }
}
