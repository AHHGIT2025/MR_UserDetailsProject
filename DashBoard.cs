using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Registration
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();  
            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        //    Registry registry = new Registry(); 
        //    registry.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormUserTableView formUserTableView = new FormUserTableView();  
            formUserTableView.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UserRegistration userRegistration = new UserRegistration(); 
            userRegistration.ShowDialog();
        }
    }
}
