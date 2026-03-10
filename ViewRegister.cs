using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Registration
{
    public partial class ViewRegister : Form
    {
        private BindingList<UserRecord> _records = new BindingList<UserRecord>();
        public ViewRegister()
        {
            InitializeComponent();
            // 2) Bind the list to the DataGridView
            Dgrid.AutoGenerateColumns = true;         // or design your own columns
            Dgrid.DataSource = _records;

            // enable editing and deleting in the grid
            Dgrid.ReadOnly = false;
            Dgrid.AllowUserToAddRows = false;         // optional
            Dgrid.AllowUserToDeleteRows = true;       // allows delete key to remove
        }

        
      

      

        // Helper: get project root → SavedData → registrations.txt (no hard-code)
       private string GetFilePath()
        {
            return FileStorage.GetRegistrationsFile("UserRegister");
        }



        //string projectRoot = Path.GetFullPath(
        //        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\")
        //    );
        //    string folder = Path.Combine(projectRoot, "SavedData");
        //    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        //    return Path.Combine(folder, "registrations.txt");
        //}

        // 3) Load from Notepad → List → Grid
        //private void btnLoad_Click(object sender, EventArgs e)
        //{
        //    string path = GetFilePath();
        //    _records.Clear();

        //    if (!File.Exists(path))
        //    {
        //        MessageBox.Show("No file found. It will be created on save.");
        //        return;
        //    }

        //    var lines = File.ReadAllLines(path);
        //    UserRecord current = null;

        //    foreach (var line in lines)
        //    {
        //        if (line.StartsWith("First Name:"))
        //        {
        //            current = new UserRecord();
        //            current.FirstName = line.Substring("First Name:".Length).Trim();
        //        }
        //        else if (line.StartsWith("Second Name:"))
        //            current.SecondName = line.Substring("Second Name:".Length).Trim();
        //        else if (line.StartsWith("Email:"))
        //            current.Email = line.Substring("Email:".Length).Trim();
        //        else if (line.StartsWith("Username:"))
        //            current.Username = line.Substring("Username:".Length).Trim();
        //        else if (line.StartsWith("Password:"))
        //            current.Password = line.Substring("Password:".Length).Trim();
        //        else if (line.StartsWith("Division:"))
        //            current.Division = line.Substring("Division:".Length).Trim();
        //        else if (line.StartsWith("Subdivisions:"))
        //            current.Subdivisions = line.Substring("Subdivisions:".Length).Trim();
        //        else if (line.StartsWith("Status:"))
        //            current.Status = line.Substring("Status:".Length).Trim();
        //        else if (line.StartsWith("----------------"))
        //        {
        //            if (current != null) _records.Add(current);
        //            current = null;
        //        }
        //    }
        //}

        // 4) Save (rewrite entire file from the list/grid)
        private void btnSave_Click(object sender, EventArgs e)
        {
            string path = GetFilePath();

            using (var sw = new StreamWriter(path))
            {
                foreach (var r in _records)
                {
                    sw.WriteLine($"First Name: {r.FirstName}");
                    sw.WriteLine($"Second Name: {r.SecondName}");
                    sw.WriteLine($"Email: {r.Email}");
                    sw.WriteLine($"Username: {r.Username}");
                    sw.WriteLine($"Password: {r.Password}");
                    sw.WriteLine($"Division: {r.Division}");
                    sw.WriteLine($"Subdivisions: {r.Subdivisions}");
                    sw.WriteLine($"Status: {r.Status}");
                    sw.WriteLine("------------------------------");
                }
            }

            MessageBox.Show("File updated successfully!");
        }

        // 5) Optional: Delete selected row with a button
        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (Dgrid.CurrentRow != null && Dgrid.CurrentRow.Index >= 0)
            {
                var toRemove = Dgrid.CurrentRow.DataBoundItem as UserRecord;
                if (toRemove != null)
                {
                    _records.Remove(toRemove);
                }
            }
        }
        private bool _cellContentClickWired = false;
        private void ViewRegister_Load(object sender, EventArgs e)
        {

            Dgrid.AutoGenerateColumns = true;
            Dgrid.DataSource = _records;

            Dgrid.ReadOnly = false;
            Dgrid.AllowUserToAddRows = false;
            Dgrid.AllowUserToDeleteRows = true;
            Dgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dgrid.MultiSelect = false;
            Dgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (!_cellContentClickWired)
            {
                Dgrid.CellContentClick += Dgrid_CellContentClick;
                _cellContentClickWired = true;
            }

            AddRowActionButtons();
        }
        private bool _actionColumnsAdded = false;
        private void AddRowActionButtons()
        {
            // Avoid duplicate columns if called multiple times
            if (Dgrid.Columns["colDelete"] == null)
            {
                var colDelete = new DataGridViewButtonColumn();
                colDelete.Name = "colDelete";
                colDelete.HeaderText = "Action";
                colDelete.Text = "Delete";
                colDelete.UseColumnTextForButtonValue = true;
                colDelete.Width = 80;
                Dgrid.Columns.Add(colDelete);
            }
        }

        private void Dgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // header
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex].Name == "colDelete")
            {
                var record = grid.Rows[e.RowIndex].DataBoundItem as UserRecord;
                if (record != null)
                {
                    var confirm = MessageBox.Show("Delete this record?", "Confirm", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        _records.Remove(record);
                    }
                }
            }
        }

     

        private void btnload_Click_1(object sender, EventArgs e)
        {

            string path = GetFilePath();
            _records.Clear();

            if (!File.Exists(path))
            {
                MessageBox.Show("No file found. It will be created on save.\n\n" + path);
                return;
            }

            var lines = File.ReadAllLines(path);
            UserRecord current = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("First Name:"))
                {
                    current = new UserRecord();
                    current.FirstName = line.Substring("First Name:".Length).Trim();
                }
                else if (line.StartsWith("Second Name:"))
                    current.SecondName = line.Substring("Second Name:".Length).Trim();
                else if (line.StartsWith("Email:"))
                    current.Email = line.Substring("Email:".Length).Trim();
                else if (line.StartsWith("Username:"))
                    current.Username = line.Substring("Username:".Length).Trim();
                else if (line.StartsWith("Password:"))
                    current.Password = line.Substring("Password:".Length).Trim();
                else if (line.StartsWith("Division:"))
                    current.Division = line.Substring("Division:".Length).Trim();
                else if (line.StartsWith("Subdivisions:"))
                    current.Subdivisions = line.Substring("Subdivisions:".Length).Trim();
                else if (line.StartsWith("Status:"))
                    current.Status = line.Substring("Status:".Length).Trim();
                else if (line.StartsWith("----------------"))
                {
                    if (current != null) _records.Add(current);
                    current = null;
                }
            }

            _actionColumnsAdded = false; // force re-check
            AddRowActionButtons();

        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
