using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
namespace User_Registration
{
    public partial class UserRegistration : Form
    {
        private readonly UserRepository _repo = new UserRepository();
        public UserRegistration()
        {
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                string value = "";

                if (cmbDivision.SelectedIndex <= 0)  // first row is "-- Select --"
                {
                    MessageBox.Show("Please select a Project (Branch).");
                    cmbDivision.DroppedDown = true;
                    return;
                }

                var selectedId = (int)cmbDivision.SelectedValue;                // BRANCH_ID
                var selectedName = ((BranchItem)cmbDivision.SelectedItem).PRIMARY_NAME; // PRIMARY_NAME

               //////////////////////////////////////////////////////////
                if (cmbProcessStep.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(Convert.ToString(cmbProcessStep.SelectedValue)))
                {
                    MessageBox.Show("Please select a Process.");
                    cmbProcessStep.DroppedDown = true;
                    return;
                }

                var processCode = Convert.ToString(cmbProcessStep.SelectedValue);            // Code
                var processName = ((ProcessItem)cmbProcessStep.SelectedItem).Name;           // Name (optional)
               /////////////////////////////////////////////////////////////////////////////

                if (CmbITsrep.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(Convert.ToString(CmbITsrep.SelectedValue)))
                {
                    MessageBox.Show("Please select a Process.");
                    CmbITsrep.DroppedDown = true;
                    return;
                }

                var ItsRepCode = Convert.ToString(CmbITsrep.SelectedValue);            // Code
                var ItsRepName = ((ITSRep)CmbITsrep.SelectedItem).Name;           // Name (optional)

                //////////////////////////////////////////////////////////////////////////////


                // Basic validations (add more as per NOT NULL constraints)
                //if (string.IsNullOrWhiteSpace(txtEmpid.Text))
                //{
                //    MessageBox.Show("EmpID is required.");
                //    txtEmpid.Focus();
                //    return;
                //}
                //if (string.IsNullOrWhiteSpace(txtEmpName.Text))
                //{
                //    MessageBox.Show("EmpName is required.");
                //    txtEmpName.Focus();
                //    return;
                //}
               // Parse nullable int helper
                //int? ToNullableInt(string s)
                //    => int.TryParse(s, out var v) ? v : (int?)null;
                // Collect selected IDs (long) and Names (string) if needed
                var selected = lstProjects.SelectedItems.Cast<BranchItem>().ToList();

                // if mandatory:
                if (selected.Count == 0) { MessageBox.Show("Please select at least one project."); return; }

                // join IDs with comma
                var idCsv = string.Join(",", selected.Select(x => x.BRANCH_ID));
                // (optional) join names too
                //  var nameCsv = string.Join(",", selected.Select(x => x.PRIMARY_NAME));



                // Validate status selected (in case both unchecked)
                if (!radioActive.Checked && !radioInactive.Checked)
                {
                    MessageBox.Show("Please select a Status (Active or Inactive).");
                    radioActive.Focus();
                    return;
                }
                 string statusValue = radioActive.Checked ? "Active" : "Inactive";


                var email = txtMailid.Text.Trim();
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Please enter a valid email address.");
                    txtMailid.Focus();
                    return;
                }



                var record = new UserRecord
                {
                    EmpID = txtEmpid.Text.Trim(),
                    EmpName = txtEmpName.Text.Trim(),
                    EmpDesign = txtEmpDesignation.Text.Trim(),

                    // <-- Save both ID and Name from ComboBox
                    EmpProjID = selectedId,           // int? → stores BRANCH_ID
                    EmpProjName = selectedName,         // string → stores PRIMARY_NAME
                    EmpMailID = email,
                    EmpPassword = txtpaswrd.Text, // TODO: hash before save
                    EmpSKMailID = txtskmailid.Text.Trim(),
                    EmpSKName = txtSkName.Text.Trim(),
                    EmpRemarks = txtRemarks.Text.Trim(),
                    EmpStatus = statusValue, // e.g., "Active"/"Inactive"
                    Xpe_Proj_ID = idCsv,
                    Xpe_Proj_StoreID = txtProjectStoreID.Text,
                    ProcessStep = processCode,
                    ProcessOrder = 1,
                    Reporting = txtReporting.Text.Trim(),
                    SubReporting = txtSubReporting.Text.Trim(),
                    XpeUser = value,
                    ITSRep = ItsRepCode,
                    MenuGroup = value,
                    Mobile = value,
                    FS_Folder = value,
                    FS_UserLevel = value,
                    FS_DEPT = value
                    // ProcessOrder = ToNullableInt(txtProcessOrder.Text),
             };

                // Insert
                var rows = await _repo.InsertAsync(record);
                if (rows == 1)
                    MessageBox.Show("Saved successfully.");
                else
                    MessageBox.Show("Nothing inserted (0 rows). Check inputs.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void UserRegistration_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            txtEmpid.Focus();
            lstProjects.ClearSelected();
            txtProjectStoreID.Text = string.Empty;

            //txtEmpid.Enabled = true;
            //txtEmpid.TabStop = true;
            await LoadBranchesToComboAsync();
            await LoadProcessComboAsync();
            await LoadITSRepoComboAsync();


            var branches = await _repo.GetBranchesAsync();
            lstProjects.DataSource = branches;
            lstProjects.DisplayMember = nameof(BranchItem.PRIMARY_NAME);
            lstProjects.ValueMember = nameof(BranchItem.BRANCH_ID);
            lstProjects.SelectionMode = SelectionMode.MultiSimple;
            // clear default selection
            lstProjects.ClearSelected();
            txtProjectStoreID.Text = string.Empty;

        }

        private async Task LoadBranchesToComboAsync()
        {
            var branches = await _repo.GetBranchesAsync();

            // Optional: insert first "Select..." row
            branches.Insert(0, new BranchItem { BRANCH_ID = 0, PRIMARY_NAME = "-- Select Project --" });

            cmbDivision.DisplayMember = nameof(BranchItem.PRIMARY_NAME);
            cmbDivision.ValueMember = nameof(BranchItem.BRANCH_ID);
            cmbDivision.DataSource = branches;
        }

        private async Task LoadProcessComboAsync()
        {
            var processes = await _repo.GetProcessesAsync();

            // Optional: default "Select"
            processes.Insert(0, new ProcessItem { Code = "", Name = "-- Select Process --" });

            cmbProcessStep.DisplayMember = nameof(ProcessItem.Name);
            cmbProcessStep.ValueMember = nameof(ProcessItem.Code);
            cmbProcessStep.DataSource = processes;
            cmbProcessStep.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }


    
        private async Task LoadITSRepoComboAsync()
        {
            var processes = await _repo.GetITSRepAsync();
            // Optional: default "Select"
            processes.Insert(0, new ITSRep { Code = "", Name = "-- Select Process --" });

            CmbITsrep.DisplayMember = nameof(ITSRep.Name);
            CmbITsrep.ValueMember = nameof(ITSRep.Code);
            CmbITsrep.DataSource = processes;
            CmbITsrep.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }


        private void All_Click(object sender, EventArgs e)
        {
            lstProjects.BeginUpdate();
            for (int i = 0; i < lstProjects.Items.Count; i++)
                lstProjects.SetSelected(i, true);
            lstProjects.EndUpdate();
        }
       private void Clear_Click(object sender, EventArgs e)
        {

            lstProjects.ClearSelected();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool IsValidEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            var email = input.Trim();

            // quick rejects
            if (email.Contains(" ")) return false;         // no spaces
            if (!email.Contains("@")) return false;        // must have @

            try
            {
                var addr = new MailAddress(email);
                // Ensure no hidden normalization changed it
                return addr.Address.Equals(email, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDivision.SelectedIndex > 0) // skip the "-- Select Project --" row
            {
                var selectedBranch = (BranchItem)cmbDivision.SelectedItem;

                // Clear previous selections
                lstProjects.ClearSelected();

                // Loop through listbox items and select the matching one
                for (int i = 0; i < lstProjects.Items.Count; i++)
                {
                    var branch = (BranchItem)lstProjects.Items[i];
                    if (branch.BRANCH_ID == selectedBranch.BRANCH_ID)
                    {
                        lstProjects.SetSelected(i, true);
                        break;
                    }
                }
            }

        }

        private void lstProjects_SelectedValueChanged(object sender, EventArgs e)
        {
            var selected = lstProjects.SelectedItems.Cast<BranchItem>().ToList();
            var idCsv = string.Join(",", selected.Select(x => x.BRANCH_ID));

            // show in textbox
            txtProjectStoreID.Text = idCsv;

        }

        private void txtProjectStoreID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    