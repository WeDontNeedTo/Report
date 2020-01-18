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
namespace Report
{
    public partial class Students : Form
    {
        StudentInsert studentInsert = new StudentInsert();
        FormDelete formDelete = new FormDelete();
        public Students()
        {
            InitializeComponent();
        }

        string ConnStr = @"Data Source =DESKTOP-SAGJ9G3\SQLEXPRESS;Initial Catalog = Report; Integrated Security = True";

        private void Students_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode =
DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            dataGridView1.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(
           DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            FillStudents();
        }

        


        public void MyExecuteNonQuery(string SqlText)
        {
            SqlConnection cn;
            SqlCommand cmd;

            cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = SqlText;
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        private void FillStudents()
        {
            string SqlText = "SELECT Student_ID, FIO, Birth_date, Course, RecordCard, GroupNumber FROM  Students, Groups WHERE Groups.Group_ID=Students.Group_ID ";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();


            da.Fill(ds, "Students");
            dataGridView1.DataSource = ds.Tables["Students"].DefaultView;

            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Дата рождения";
            dataGridView1.Columns[3].HeaderText = "Курс";
            dataGridView1.Columns[4].HeaderText = "Зачетка";
            dataGridView1.Columns[5].HeaderText = "Группа";
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SqlText = "";



            if (studentInsert.ShowDialog() == DialogResult.OK)
            {

                if (studentInsert.textBox1.Text == null || studentInsert.textBox1.Text == " " || studentInsert.textBox1.Text == ""
                   || studentInsert.textBox2.Text == null || studentInsert.textBox2.Text == " " || studentInsert.textBox2.Text == "")
                {
                    MessageBox.Show("Поля не могут быть пустым. Введите, пожалуйста, данные снова");
                }
                else
                {
                    SqlText = "INSERT INTO Students ([FIO], [Birth_date], [Course], [RecordCard], [Group_ID]) VALUES (";
                    SqlText = SqlText + "\'" + studentInsert.textBox1.Text + "\',";
                    SqlText = SqlText + "\'" + studentInsert.dateTimePicker1.Value + "\',";
                    SqlText = SqlText + "\'" + studentInsert.numericUpDown1.Text + "\',";
                    SqlText = SqlText + "\'" + studentInsert.textBox2.Text + "\',";
                    SqlText = SqlText + "\'" + studentInsert.textBox3.Text + "\')";



                    MyExecuteNonQuery(SqlText);
                    FillStudents();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //studentInsert.FillID();
            int index, n;
            string SqlText = "UPDATE [Students] SET ";
            string StID, birth, name,  card,gc, GID;
            decimal course;


            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            StID = dataGridView1[0, index].Value.ToString();
            name = dataGridView1[1, index].Value.ToString();
            birth = dataGridView1[2, index].Value.ToString();
            course = Convert.ToDecimal(dataGridView1[3, index].Value);
            card = dataGridView1[4, index].Value.ToString();
            gc = dataGridView1[5, index].Value.ToString();
            GID = studentInsert.textBox3.Text;
           


            studentInsert.textBox1.Text = name;
            studentInsert.numericUpDown1.Value = course;
            studentInsert.textBox2.Text = card;
            studentInsert.dateTimePicker1.Value = Convert.ToDateTime(birth);
            studentInsert.comboBox1.SelectedItem = gc;
            studentInsert.textBox3.Text = GID; 


            if (studentInsert.ShowDialog() == DialogResult.OK)
            {

                if (studentInsert.textBox1.Text == null || studentInsert.textBox1.Text == " " || studentInsert.textBox1.Text == ""
                    || studentInsert.textBox2.Text == null || studentInsert.textBox2.Text == " " || studentInsert.textBox2.Text == "")
                {
                    MessageBox.Show("Поля не могут быть пустым. Введите, пожалуйста, данные снова");
                }
                else
                {
                    name = studentInsert.textBox1.Text;
                    course = studentInsert.numericUpDown1.Value;
                    card = studentInsert.textBox2.Text;
                    birth = studentInsert.dateTimePicker1.Value.ToString();
                   // gc = studentInsert.comboBox1.Text;
                    GID = studentInsert.textBox3.Text;
                    SqlText += "FIO = \'" + name + "\', Birth_date = '" + birth + "\', Course = '" + course.ToString() + "\', RecordCard = '" + card + "\', Group_ID = '" + GID + "\'";
                    SqlText += "WHERE [Students].Student_ID = " + StID;

                    MyExecuteNonQuery(SqlText);
                    FillStudents();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index, n;
            string StID;
            string name, birth, course;
            string SqlText = "DELETE FROM [Students] WHERE [Students].Student_ID = ";

            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            StID = Convert.ToString(dataGridView1[0, index].Value);

            SqlText += StID;

            name = Convert.ToString(dataGridView1[1, index].Value);
            birth = Convert.ToString(dataGridView1[2, index].Value);
            course = Convert.ToString(dataGridView1[3, index].Value);
            formDelete.label2.Text = " " + name;


            if (formDelete.ShowDialog() == DialogResult.OK)
            {
                MyExecuteNonQuery(SqlText);
                FillStudents();
            }
        }
    }
}
