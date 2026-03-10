
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace User_Registration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      
        Dictionary<string, List<string>> divisionData = new Dictionary<string, List<string>>
        {
            { "IT",    new List<string> { "Networking", "Software", "Infrastructure" } },
            { "HR",    new List<string> { "Payroll", "Recruitment", "Training" } },
            { "Sales", new List<string> { "Domestic", "International", "Corporate" } }
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbDivision.Items.AddRange(divisionData.Keys.ToArray());
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlSubDivisions.Controls.Clear();  // 

            if (cmbDivision.SelectedItem == null)
                return;

            string selectedDivision = cmbDivision.SelectedItem.ToString();

            List<string> subs;
            if (!divisionData.TryGetValue(selectedDivision, out subs))
                return;

            foreach (string sub in subs)
            {
                CheckBox cb = new CheckBox();
                cb.Text = sub;
                cb.AutoSize = true;

                pnlSubDivisions.Controls.Add(cb);
            }
        }

        private static string GetProjectRoot(string projectFolderName = null)
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (dir != null)
            {
                //  pass "UserRegister", it will stop exactly at that folder
                if (!string.IsNullOrWhiteSpace(projectFolderName) &&
                    dir.Name.Equals(projectFolderName, StringComparison.OrdinalIgnoreCase))
                {
                    return dir.FullName;
                }

                // Alternatively, stop where the .csproj exists
                if (dir.GetFiles("*.csproj").Any())
                {
                    return dir.FullName;
                }

                dir = dir.Parent;
            }

          
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fName = txtFirstName.Text;
            string sName = txtSecondName.Text;
            string email = txtEmail.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string division = cmbDivision.SelectedItem != null ? cmbDivision.SelectedItem.ToString() : "";

            string status = "";

            if (radioActive.Checked)
                status = "Active";
            else if (radioInactive.Checked)
                status = "Inactive";
            else
                status = "Not Selected";

            List<string> selectedSubDivs = new List<string>();

            foreach (Control control in pnlSubDivisions.Controls)
            {
                CheckBox cb = control as CheckBox;
                if (cb != null && cb.Checked)
                {
                    selectedSubDivs.Add(cb.Text);
                }
            }

            string data =
                "First Name: " + fName + "\n" +
                "Second Name: " + sName + "\n" +
                "Email: " + email + "\n" +
                "Username: " + username + "\n" +
                "Password: " + password + "\n" +
                "Division: " + division + "\n" +
                "Subdivisions: " + string.Join(", ", selectedSubDivs) + "\n" +
                $"Status: {status}\n" +   // ← Added
                  "------------------------------\n";

            
             string projectRoot = GetProjectRoot("UserRegister");
             string folder = Path.Combine(projectRoot, "SavedData");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);


            string path = FileStorage.GetRegistrationsFile("UserRegister");
            File.AppendAllText(path, data);
            MessageBox.Show("Saved to: " + path);



            //string path = Path.Combine(folder, "registrations.txt");

            //File.AppendAllText(path, data);

            //MessageBox.Show("Saved to: " + path);
 

             // Documents folder (recommended)
            //string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //// string folder = Application.StartupPath;
            //string path = Path.Combine(folder, "registrations.txt");
            //string dir = Path.GetDirectoryName(path);
            //if (!Directory.Exists(dir))
            //{
            //    Directory.CreateDirectory(dir);
            //}
            //File.AppendAllText(path, data);
            //MessageBox.Show("Saved to Notepad successfully!\n\n" + path, "Success");
        }
    }
}
