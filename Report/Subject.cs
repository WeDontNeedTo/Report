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
    public partial class Subject : Form
    {
        SubjectInsert subjectInsert = new SubjectInsert();
        FormDelete formDelete = new FormDelete();
        public Subject()
        {
            InitializeComponent();
        }
        string ConnStr = @"Data Source =DESKTOP-SAGJ9G3\SQLEXPRESS;Initial Catalog = Report; Integrated Security = True";

        private void Subject_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode =
    DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            dataGridView1.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(
           DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            FillSubject();
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

        private void FillSubject()
        {
            string SqlText = "SELECT Subject_ID, Name_of_sub, Hours FROM  Subject ";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();


            da.Fill(ds, "Subject");
            dataGridView1.DataSource = ds.Tables["Subject"].DefaultView;

            dataGridView1.Columns[1].HeaderText = "Название";
            dataGridView1.Columns[2].HeaderText = "Часы";
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SqlText = "";

           

            


                if (subjectInsert.ShowDialog() == DialogResult.OK)
            {
                    if (subjectInsert.textBox1.Text == null || subjectInsert.textBox1.Text == " " || subjectInsert.textBox1.Text == "" || subjectInsert.textBox2.Text == null || subjectInsert.textBox2.Text == " " || subjectInsert.textBox2.Text == "")
                {
                    MessageBox.Show("Поля не могут быть пустым. Введите, пожалуйста, данные снова");
                }
                else
                {
                    SqlText = "INSERT INTO Subject ([Name_of_sub], [Hours]) VALUES (";
                    SqlText = SqlText + "\'" + subjectInsert.textBox1.Text + "\',";
                    SqlText = SqlText + "\'" + subjectInsert.textBox2.Text + "\')";

                    MyExecuteNonQuery(SqlText);
                    FillSubject();
                }
                    

                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int index, n;
            string SqlText = "UPDATE [Subject] SET ";
            string SubID, name, hours;


            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            SubID = dataGridView1[0, index].Value.ToString();
            name = dataGridView1[1, index].Value.ToString();
            hours = dataGridView1[2, index].Value.ToString();
          
         


            subjectInsert.textBox1.Text = name;
            subjectInsert.textBox2.Text = hours;
            

            if (subjectInsert.ShowDialog() == DialogResult.OK)
            {
                if (subjectInsert.textBox1.Text == null || subjectInsert.textBox1.Text == " " || subjectInsert.textBox1.Text == "" || subjectInsert.textBox2.Text == null || subjectInsert.textBox2.Text == " " || subjectInsert.textBox2.Text == "")
                {
                    MessageBox.Show("Поля не могут быть пустым. Введите, пожалуйста, данные снова");
                }
                else
                {
                    name = subjectInsert.textBox1.Text;
                    hours = subjectInsert.textBox2.Text;
                    //control = subjectInsert.comboBox1.Text;
                    SqlText += "Name_of_sub = \'" + name + "\', Hours = '"  + hours + "\'";
                    SqlText += "WHERE [Subject].Subject_ID = " + SubID;

                    MyExecuteNonQuery(SqlText);
                    FillSubject();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index, n;
            string SubID;
            string name, hours;
            string SqlText = "DELETE FROM [Subject] WHERE [Subject].Subject_ID = ";

            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            SubID = Convert.ToString(dataGridView1[0, index].Value);

            SqlText += SubID;

            name = Convert.ToString(dataGridView1[1, index].Value);
            hours = Convert.ToString(dataGridView1[2, index].Value);
            // control = Convert.ToString(dataGridView1[3, index].Value);
            formDelete.label2.Text = " " + name + "  " + hours + " "; //+ control;


            if (formDelete.ShowDialog() == DialogResult.OK)
            {
                MyExecuteNonQuery(SqlText);
                FillSubject();
            }
        }
    }
}
