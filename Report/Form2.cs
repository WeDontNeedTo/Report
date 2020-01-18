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
    
    public partial class Form2 : Form
    {
        string s,sid;
        ReportInsert reportInsert = new ReportInsert();
        FormDelete formDelete = new FormDelete();
        public Form2(Form1 f1)
        {
            InitializeComponent();
            s = f1.textBox2.Text;



        }
        string ConnStr = @"Data Source =DESKTOP-SAGJ9G3\SQLEXPRESS;Initial Catalog = Report; Integrated Security = True";
        

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode =
      DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            dataGridView1.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(
           DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            FillReport();

            FillInfo();


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

        private void FillReport()
        {

            string SqlText = "SELECT Date, Name_of_sub, Score, FIOTeach, ControlName, Report_ID FROM  Report, Subject, Teachers, Control WHERE Student_ID = ";
            SqlText += s;
            SqlText += " AND Report.Subject_ID=Subject.Subject_ID AND Report.Teacher_ID=Teachers.Teacher_ID AND Report.Control_ID=Control.Control_ID";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();


            da.Fill(ds, "Report");
            dataGridView1.DataSource = ds.Tables["Report"].DefaultView;

            

            dataGridView1.Columns[0].HeaderText = "Дата";
            dataGridView1.Columns[1].HeaderText = "Предмет";
            dataGridView1.Columns[2].HeaderText = "Оценка";
            dataGridView1.Columns[3].HeaderText = "Преподаватель";
            dataGridView1.Columns[4].HeaderText = "Тип аттестации";
            dataGridView1.Columns[5].Visible = false;


        }

        public void FillInfo()
        {
            string SqlInfo = "SELECT FIO, CONVERT(varchar, Birth_date, 105), Course, RecordCard, GroupNumber, Specialty, Specialty_key, Student_ID FROM Students, Groups WHERE Student_ID = ";
            SqlInfo += s;
            SqlInfo += "AND Students.Group_ID=Groups.Group_ID";



            SqlDataAdapter sql = new SqlDataAdapter(SqlInfo, ConnStr);
            DataSet set = new DataSet();


            sql.Fill(set, "Students");
            dataGridView2.DataSource = set.Tables["Students"].DefaultView;

            label2.Text = dataGridView2[3, 0].Value.ToString();
            label3.Text = dataGridView2[0, 0].Value.ToString();
            label6.Text += dataGridView2[1, 0].Value.ToString();
            label5.Text += dataGridView2[2, 0].Value.ToString();
            label7.Text += dataGridView2[4, 0].Value.ToString() + " " + dataGridView2[5, 0].Value.ToString() + " " + dataGridView2[6, 0].Value.ToString();
            sid = dataGridView2[7, 0].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index, n;
            string ID;
            string name, birth, course;
            string SqlText = "DELETE FROM [Report] WHERE [Report].Report_ID = ";

            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            ID = Convert.ToString(dataGridView1[5, index].Value);

            SqlText +=ID;

           
            formDelete.label2.Text = " ";


            if (formDelete.ShowDialog() == DialogResult.OK)
            {
                MyExecuteNonQuery(SqlText);
                FillReport();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index, n;
            string SqlText = "UPDATE [Report] SET ";
            string RID,date, sub, score, fio, control;
            
            


            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            RID = dataGridView1[5, index].Value.ToString();
            date = dataGridView1[0, index].Value.ToString();
            sub = reportInsert.textBox2.Text;
            score = dataGridView1[2, index].Value.ToString();
            fio = reportInsert.textBox4.Text;
            control = reportInsert.textBox3.Text;

            



            reportInsert.dateTimePicker1.Value  = DateTime.Parse(date);
            reportInsert.comboBox1.SelectedItem = dataGridView1[1, index].Value.ToString(); 
            reportInsert.comboBox2.SelectedItem= dataGridView1[4, index].Value.ToString();
            reportInsert.comboBox3.SelectedItem = score;
            reportInsert.comboBox4.SelectedItem = dataGridView1[3, index].Value.ToString();


            if (reportInsert.ShowDialog() == DialogResult.OK)
            {

             
                date = reportInsert.dateTimePicker1.Value.ToShortDateString();
                sub = reportInsert.textBox2.Text;
                score = reportInsert.comboBox3.Text;
                fio = reportInsert.textBox4.Text;
                control = reportInsert.textBox3.Text;
                SqlText += "Date = \'" + date + "\', Subject_ID = '" + sub + "\', Control_ID = '" + control + "\', Teacher_ID = '" + fio + "\', Score = '" + score + "\'";
                    SqlText += "WHERE [Report].Report_ID = " + RID;

                    MyExecuteNonQuery(SqlText);
                    FillReport();
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          

            string SqlText = "";



            if (reportInsert.ShowDialog() == DialogResult.OK)
            {
                SqlText = "INSERT INTO Report ([Date], [Subject_ID], [Control_ID], [Score],[Teacher_ID], [Student_ID])  VALUES (";
                SqlText = SqlText + "\'" + reportInsert.dateTimePicker1.Value + "\',";
                SqlText = SqlText + "\'" + reportInsert.textBox2.Text + "\',";
                SqlText = SqlText + "\'" + reportInsert.textBox3.Text + "\',";
                SqlText = SqlText + "\'" + reportInsert.comboBox3.Text + "\',";
                SqlText = SqlText + "\'" + reportInsert.textBox4.Text + "\',";
                SqlText = SqlText + "\'" + sid + "\')";
               

                MyExecuteNonQuery(SqlText);
                FillReport();
              
            }
        }
    }



    
}
