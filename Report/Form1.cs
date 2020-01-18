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
    
    public partial class Form1 : Form
    {
        
        

        public Form1()
        {
            
            InitializeComponent();
            ToolTip t = new ToolTip();
            t.SetToolTip(textBox1, "Доступные номера зачеток 1011,1027,3014,2013,5001");
            this.KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.button1.Click += new System.EventHandler(this.button1_Click);
        }
        string ConnStr = @"Data Source =DESKTOP-SAGJ9G3\SQLEXPRESS;Initial Catalog = Report; Integrated Security = True";

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
        private void Form1_Load(object sender, EventArgs e)
        {
            FillStudents();
        }

        //textBox1.Text ="";
        //string SqlText= "SELECT Student_ID FROM  Students WHERE RecordCard LIKE '%"+textBox1.Text+"'";
        //MyExecuteNonQuery(SqlText);
        //FillStudents();
        //int id = Convert.ToInt32(dataGridView1[0, 0].Value);
        //form2.Show();
        //textBox1.Text = id.ToString();
        //break;

        private void FillStudents()
        {
            string SqlText = "SELECT RecordCard FROM  Students ";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Students");
            dataGridView1.DataSource = ds.Tables["Students"].DefaultView;
        
        }

        private void FillStudentsID()
        {
            string SqlText = "SELECT Student_ID FROM  Students WHERE RecordCard LIKE '%" + textBox1.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Students");
            dataGridView1.DataSource = ds.Tables["Students"].DefaultView;

        }
        
        //private void CreateOrShowReport()
        //{
        //    Form2 form2 = new Form2(this);
        //    if (form2 == null || form2.IsDisposed)
        //    {
        //        form2 = new Form2(this);
        //        form2.FormClosed += Form2_FormClosed;
        //    }
        //    form2.Show();


        //}


        //private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Form2 form2 = new Form2(this);
        //    form2.FormClosed -= Form2_FormClosed;
        //    form2 = null;
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == textBox1.Text)
                        {
                           
                           FillStudentsID();
                           int id = Convert.ToInt32(dataGridView1[0, 0].Value);
                           // CreateOrShowReport();
                            textBox1.Text = "";
                            textBox2.Text = id.ToString();
                            Form2 form2 = new Form2(this);
                            form2.Show();
                            // this.textBox2.Text = form2.textBox1.Text;

                            FillStudents();
                            break;
                        }
                        //else
                        //{
                        //    MessageBox.Show("К сожалению, нет такой зачетки. Попробуйте ввести другой номер");
                            
                        //}
                        
                    }
               
            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1.PerformClick();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void преподавателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Teachers teachers = new Teachers();
            teachers.Show();
        }

        private void предметыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Subject subject = new Subject();
            subject.Show();
        }

        private void группыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Groups groups = new Groups();
            groups.Show();
        }

        private void студентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Students students = new Students();
            students.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }
}
