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
    
    public partial class StudentInsert : Form
    {
        
       
        public StudentInsert()
        {
            InitializeComponent();
        }

        string ConnStr = @"Data Source =DESKTOP-SAGJ9G3\SQLEXPRESS;Initial Catalog = Report; Integrated Security = True";

        private void StudentInsert_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode =
DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            dataGridView1.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(
           DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            Fill();
            FillID();
            
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

        private void Fill()
        {
            string SqlText = "SELECT  GroupNumber  FROM  Groups";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Groups");
            dataGridView1.DataSource = ds.Tables["Groups"].DefaultView;

            string[] s= new string[dataGridView1.RowCount-1];
            //MessageBox.Show(dataGridView1.Rows[0].Cells["GroupNumber"].Value.ToString());
            

            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    if (dataGridView1.Rows[i].Cells["GroupNumber"].Value.ToString() != "") counter++;
            //}

            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
            s[i] = dataGridView1[0,i].Value.ToString();
                //MessageBox.Show(s[i]);   
            }
            comboBox1.Items.AddRange(s);
            comboBox1.SelectedIndex = comboBox1.FindString(s[0]);

            //comboBox2.Items.AddRange(x);


        }


        public void FillID()
        {
            string num = comboBox1.Text;
            string SqlText = "SELECT  Group_ID  FROM  Groups WHERE GroupNumber = \'"+num+ "\'";
            
           // SqlText += num ;
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "Groups");
            dataGridView1.DataSource = ds.Tables["Groups"].DefaultView;
           
           


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillID();
            textBox3.Text = dataGridView1[0, 0].Value.ToString();
           
        }

        private void StudentInsert_FormClosed(object sender, FormClosedEventArgs e)
        {
            comboBox1.Items.Clear();
        }
    }
}
