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
    public partial class Groups : Form
    {
        GroupInsert groupInsert = new GroupInsert();
        FormDelete formDelete = new FormDelete();

        public Groups()
        {
            InitializeComponent();
        }
        string ConnStr = @"Data Source =DESKTOP-SAGJ9G3\SQLEXPRESS;Initial Catalog = Report; Integrated Security = True";

        private void Groups_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode =
    DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            dataGridView1.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(
           DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            FillGroups();
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

        private void FillGroups()
        {
            string SqlText = "SELECT *  FROM  Groups";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();


            da.Fill(ds, "Groups");
            dataGridView1.DataSource = ds.Tables["Groups"].DefaultView;

            dataGridView1.Columns[1].HeaderText = "Специльность";
            dataGridView1.Columns[2].HeaderText = "Код специальности";
            dataGridView1.Columns[3].HeaderText = "Номер группы";
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SqlText = "";



            if (groupInsert.ShowDialog() == DialogResult.OK)
            {
                if (groupInsert.textBox1.Text == null || groupInsert.textBox1.Text == " " || groupInsert.textBox1.Text == ""
                    || groupInsert.textBox2.Text == null || groupInsert.textBox2.Text == " " || groupInsert.textBox2.Text == ""
                    || groupInsert.textBox3.Text == null || groupInsert.textBox3.Text == " " || groupInsert.textBox3.Text == "")

                {
                    MessageBox.Show("Поля не могут быть пустым. Введите, пожалуйста, данные снова");
                }
                else
                {
                    SqlText = "INSERT INTO Groups ([Specialty], [Specialty_key], [GroupNumber]) VALUES (";
                    SqlText = SqlText + "\'" + groupInsert.textBox1.Text + "\',";
                    SqlText = SqlText + "\'" + groupInsert.textBox2.Text + "\',";
                    SqlText = SqlText + "\'" + groupInsert.textBox3.Text + "\')";

                    MyExecuteNonQuery(SqlText);
                    FillGroups();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index, n;
            string SqlText = "UPDATE [Groups] SET ";
            string GrID, name, key, num;


            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            GrID = dataGridView1[0, index].Value.ToString();
            name = dataGridView1[1, index].Value.ToString();
            key = dataGridView1[2, index].Value.ToString();
            num = dataGridView1[3, index].Value.ToString();


            groupInsert.textBox1.Text = name;
            groupInsert.textBox2.Text = key;
            groupInsert.textBox3.Text = num;


            if (groupInsert.ShowDialog() == DialogResult.OK)
            {
                if (groupInsert.textBox1.Text == null || groupInsert.textBox1.Text == " " || groupInsert.textBox1.Text == ""
                    || groupInsert.textBox2.Text == null || groupInsert.textBox2.Text == " " || groupInsert.textBox2.Text == ""
                    || groupInsert.textBox3.Text == null || groupInsert.textBox3.Text == " " || groupInsert.textBox3.Text == "")

                {
                    MessageBox.Show("Поля не могут быть пустым. Введите, пожалуйста, данные снова");
                }
                else
                {
                    name = groupInsert.textBox1.Text;
                    key = groupInsert.textBox2.Text;
                    num = groupInsert.textBox3.Text;
                    SqlText += "Specialty = \'" + name + "\', Specialty_key = '" + key + "\', GroupNumber = '" + num + "\'";
                    SqlText += "WHERE [Groups].Group_ID = " + GrID;

                    MyExecuteNonQuery(SqlText);
                    FillGroups();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index, n;
            string GrID;
            string name, key, num;
            string SqlText = "DELETE FROM [Groups] WHERE [Groups].Group_ID = ";

            n = dataGridView1.Rows.Count;
            if (n == 1) return;

            index = dataGridView1.CurrentRow.Index;
            GrID = Convert.ToString(dataGridView1[0, index].Value);

            SqlText += GrID;

            name = Convert.ToString(dataGridView1[1, index].Value);
            key = Convert.ToString(dataGridView1[2, index].Value);
            num = Convert.ToString(dataGridView1[3, index].Value);
            formDelete.label2.Text = " " + name + "  " + key + " " + num;


            if (formDelete.ShowDialog() == DialogResult.OK)
            {
                MyExecuteNonQuery(SqlText);
                FillGroups();
            }
        }
    }
}
