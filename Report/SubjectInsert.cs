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
    public partial class SubjectInsert : Form
    {
        
        Teachers teachers = new Teachers();
        public SubjectInsert()
        {
            
            InitializeComponent();

           
        }
        string ConnStr = @"Data Source =DESKTOP-SAGJ9G3\SQLEXPRESS;Initial Catalog = Report; Integrated Security = True";
        private void SubjectInsert_Load(object sender, EventArgs e)
        {

           

        }

       

     

       

        
    }
}
