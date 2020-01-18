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
    public partial class Teachers : Form
    {
        TeachersInsert teachersInsert = new TeachersInsert();
        FormDelete formDelete = new FormDelete();
        public Teachers()
        {
            InitializeComponent();
        }
        string ConnStr = @"Data Source =DESKTOP-SAGJ9G3\SQLEXPRESS;Initial Catalog = Report; Integrated Security = True";

        private void Teachers_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode =
      DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            dataGridView1.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(
           DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            FillTeachers();
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

        private void FillTeachers()
        {
            //
            string SqlText = "SELECT  * FROM  Teachers";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();

    
            da.Fill(ds, "Teachers");
            dataGridView1.DataSource = ds.Tables["Teachers"].DefaultView;

            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Должность";
            dataGridView1.Columns[0].Visible=false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SqlText = "";



            if (teachersInsert.ShowDialog() == DialogResult.OK)
            {
                if (teachersInsert.textBox1.Text == null || teachersInsert.textBox1.Text == " " || teachersInsert.textBox1.Text == "" || teachersInsert.textBox2.Text == null || teachersInsert.textBox2.Text == " " || teachersInsert.textBox2.Text == "")
                {
                    MessageBox.Show("Поля не могут быть пустым. Введите, пожалуйста, данные снова");
                }
                else
                {
                    SqlText = "INSERT INTO Teachers ([FIOTeach], [Position]) VALUES (";
                    SqlText = SqlText + "\'" + teachersInsert.textBox1.Text + "\',";
                    SqlText = SqlText + "\'" + teachersInsert.textBox2.Text + "\')";

                    MyExecuteNonQuery(SqlText);
                    FillTeachers();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index, n;
            string SqlText = "UPDATE [Teachers] SET ";
            string TeachID,  name, pos;


            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            TeachID = dataGridView1[0, index].Value.ToString();
            name = dataGridView1[1, index].Value.ToString();
            pos = dataGridView1[2, index].Value.ToString();
           

            teachersInsert.textBox1.Text = name;
            teachersInsert.textBox2.Text = pos;
            

            if (teachersInsert.ShowDialog() == DialogResult.OK)
            {
                if (teachersInsert.textBox1.Text == null || teachersInsert.textBox1.Text == " " || teachersInsert.textBox1.Text == "" || teachersInsert.textBox2.Text == null || teachersInsert.textBox2.Text == " " || teachersInsert.textBox2.Text == "")
                {
                    MessageBox.Show("Поля не могут быть пустым. Введите, пожалуйста, данные снова");
                }
                else
                {
                    name = teachersInsert.textBox1.Text;
                    pos = teachersInsert.textBox2.Text;
                    SqlText += "FIOTeach = \'" + name + "\', Position = '" + pos + "\'";
                    SqlText += "WHERE [Teachers].Teacher_ID = " + TeachID;

                    MyExecuteNonQuery(SqlText);
                    FillTeachers();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index, n;
            string TeachID;
            string name, pos;
            string SqlText = "DELETE FROM [Teachers] WHERE [Teachers].Teacher_ID = ";

            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            TeachID = Convert.ToString(dataGridView1[0, index].Value);

            SqlText += TeachID;

            name = Convert.ToString(dataGridView1[1, index].Value);
            pos = Convert.ToString(dataGridView1[2, index].Value);
            formDelete.label2.Text = " - " + name + " - " + pos;


            if (formDelete.ShowDialog() == DialogResult.OK)
            {
                MyExecuteNonQuery(SqlText);
                FillTeachers();
            }
        }
    }
}
