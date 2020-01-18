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
    public partial class ReportInsert : Form
    {
        public ReportInsert()
        {

            InitializeComponent();
        }
        string ConnStr = @"Data Source =DESKTOP-SAGJ9G3\SQLEXPRESS;Initial Catalog = Report; Integrated Security = True";

        private void ReportInsert_Load(object sender, EventArgs e)
        {

            dataGridView1.AutoSizeColumnsMode =
DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            dataGridView1.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(
           DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            //methods ...
            Fill1();
            FillID1();


            Fill2();
            FillID2();

            Score();

            Fill3();
            FillID3();

            dateTimePicker1.Format = DateTimePickerFormat.Short;
            

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

        private void Fill1()
        {
            string SqlText = "SELECT  Name_of_sub  FROM  Subject";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Subject");
            dataGridView1.DataSource = ds.Tables["Subject"].DefaultView;

            string[] s = new string[dataGridView1.RowCount - 1];
            //MessageBox.Show(dataGridView1.Rows[0].Cells["GroupNumber"].Value.ToString());


            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    if (dataGridView1.Rows[i].Cells["GroupNumber"].Value.ToString() != "") counter++;
            //}

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                s[i] = dataGridView1[0, i].Value.ToString();
                //MessageBox.Show(s[i]);   
            }
            comboBox1.Items.AddRange(s);
            comboBox1.SelectedIndex = comboBox1.FindString(s[0]);
            //comboBox1.SelectedIndex = 0;

            //comboBox2.Items.AddRange(x);


        }

        public void FillID1()
        {
            string num = comboBox1.Text;
            string SqlText = "SELECT  Subject_ID  FROM  Subject WHERE Name_of_sub = \'" + num + "\'";

            // SqlText += num ;
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Teachers");
            dataGridView1.DataSource = ds.Tables["Teachers"].DefaultView;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillID1();
            textBox2.Text = dataGridView1[0, 0].Value.ToString();
        }






        private void Fill2()
        {
            string SqlText = "SELECT  ControlName  FROM  Control";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Control");
            dataGridView2.DataSource = ds.Tables["Control"].DefaultView;

            string[] s = new string[dataGridView2.RowCount - 1];
            //MessageBox.Show(dataGridView1.Rows[0].Cells["GroupNumber"].Value.ToString());


            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    if (dataGridView1.Rows[i].Cells["GroupNumber"].Value.ToString() != "") counter++;
            //}

            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                s[i] = dataGridView2[0, i].Value.ToString();
                //MessageBox.Show(s[i]);   
            }
            comboBox2.Items.AddRange(s);
            comboBox2.SelectedIndex = comboBox2.FindString(s[0]);
            //comboBox1.SelectedIndex = 0;

            //comboBox2.Items.AddRange(x);


        }

        public void FillID2()
        {
            string num = comboBox2.Text;
            string SqlText = "SELECT  Control_ID  FROM  Control WHERE ControlName = \'" + num + "\'";

            // SqlText += num ;
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Control");
            dataGridView2.DataSource = ds.Tables["Control"].DefaultView;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillID2();
            Score();
            textBox3.Text = dataGridView2[0, 0].Value.ToString();
        }


        public void Score()
        {

            if (comboBox2.SelectedIndex == comboBox2.FindString("Зачет"))
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("зачет");
                comboBox3.Items.Add("незачет");
                comboBox3.SelectedIndex = comboBox3.FindString("зачет");

            }

            else
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("5");
                comboBox3.Items.Add("4");
                comboBox3.Items.Add("3");
                comboBox3.Items.Add("2");
                comboBox3.SelectedIndex = comboBox3.FindString("5");
            }
        }


        private void Fill3()
        {
            string SqlText = "SELECT  FIOTeach  FROM  Teachers";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Teachers");
            dataGridView3.DataSource = ds.Tables["Teachers"].DefaultView;

            string[] s = new string[dataGridView3.RowCount - 1];
            //MessageBox.Show(dataGridView1.Rows[0].Cells["GroupNumber"].Value.ToString());


            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    if (dataGridView1.Rows[i].Cells["GroupNumber"].Value.ToString() != "") counter++;
            //}

            for (int i = 0; i < dataGridView3.RowCount - 1; i++)
            {
                s[i] = dataGridView3[0, i].Value.ToString();
                //MessageBox.Show(s[i]);   
            }
            comboBox4.Items.AddRange(s);
            comboBox4.SelectedIndex = comboBox4.FindString(s[0]);
            //comboBox1.SelectedIndex = 0;

            //comboBox2.Items.AddRange(x);


        }

        public void FillID3()
        {
            string num = comboBox4.Text;
            string SqlText = "SELECT  Teacher_ID  FROM  Teachers WHERE FIOTeach = \'" + num + "\'";

            // SqlText += num ;
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Teachers");
            dataGridView3.DataSource = ds.Tables["Teachers"].DefaultView;

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillID3();
            
            textBox4.Text = dataGridView3[0, 0].Value.ToString();
        }
        private void ReportInsert_FormClosing(object sender, FormClosingEventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
        }

        
    }
}
