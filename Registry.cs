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
using System;
using System.Collections.Generic;
using System.ComponentModel; // BindingList
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace User_Registration
{
    public partial class Registry : Form
    {
        // BindingList so the grid updates automatically on add/remove/edit
        //private BindingList<UserRecord> _records = new BindingList<UserRecord>();

        //public Registry()
        //{
        //    InitializeComponent();
        //    // Bind the list to the DataGridView
        //    Dgrid.AutoGenerateColumns = true; // You can also design columns manually
        //    Dgrid.DataSource = _records;

        //    // Enable inline editing and row delete
        //    Dgrid.ReadOnly = false;
        //    Dgrid.AllowUserToAddRows = false;
        //    Dgrid.AllowUserToDeleteRows = true;

        //    // Nice to have: autosize and select full row
        //    Dgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    Dgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    Dgrid.MultiSelect = false;

        //    Dgrid.AutoGenerateColumns = true;                 // or manually define columns
        //    Dgrid.ReadOnly = false;                           // allows typing to edit
        //    Dgrid.AllowUserToAddRows = false;                 // optional
        //    Dgrid.AllowUserToDeleteRows = true;               // allows Delete
        //    Dgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    Dgrid.MultiSelect = false;
        //    Dgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //    // Wire up CellContentClick so button column works (do this here
        //    // OR in Designer: select grid → Events → CellContentClick → Dgrid_CellContentClick)
        //    //Dgrid.CellContentClick += Dgrid_CellContentClick;
        //}

        //private void Dgrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    // If needed: validate changes here per cell/row
        //    // For BindingList<T>, edits are already tracked in the object.
        //}


        //private void Registry_Load(object sender, EventArgs e)
        //{

        //    Dgrid.AutoGenerateColumns = true;
        //    Dgrid.DataSource = _records;
        //    Dgrid.ReadOnly = false;
        //    Dgrid.AllowUserToAddRows = false;
        //    Dgrid.AllowUserToDeleteRows = true;

        //    Dgrid.CellContentClick += Dgrid_CellContentClick;

        //    AddRowActionButtons();

        //}
        //private void Dgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0) return; // ignore header row
        //    var grid = (DataGridView)sender;
        //    var colName = grid.Columns[e.ColumnIndex].Name;

        //    // Bound object for this row
        //    var record = grid.Rows[e.RowIndex].DataBoundItem as UserRecord;
        //    if (record == null) return;

        //    if (colName == "colEdit")
        //    {
        //        // Move focus to first editable cell and begin edit
        //        // Find first non-readonly, non-button, visible column
        //        for (int c = 0; c < grid.Columns.Count; c++)
        //        {
        //            var col = grid.Columns[c];
        //            if (col.ReadOnly) continue;
        //            if (col is DataGridViewButtonColumn) continue;
        //            if (!col.Visible) continue;

        //            grid.CurrentCell = grid.Rows[e.RowIndex].Cells[c];
        //            grid.BeginEdit(true);
        //            break;
        //        }
        //    }
        //    else if (colName == "colDelete")
        //    {
        //        var confirm = MessageBox.Show(
        //            "Delete this record?",
        //            "Confirm",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Question);

        //        if (confirm == DialogResult.Yes)
        //        {
        //            _records.Remove(record); // BindingList will update grid automatically
        //        }
        //    }
        //}
        //private string GetFilePath()
        //{
        //    return FileStorage.GetRegistrationsFile("UserRegister");
        //}

        //private void AddRowActionButtons()
        //    {


        //    // Avoid duplicates if called multiple times
        //    if (Dgrid.Columns["colEdit"] == null)
        //    {
        //        var colEdit = new DataGridViewButtonColumn();
        //        colEdit.Name = "colEdit";
        //        colEdit.HeaderText = "Edit";
        //        colEdit.Text = "Edit";
        //        colEdit.UseColumnTextForButtonValue = true;
        //        colEdit.Width = 60;
        //        Dgrid.Columns.Add(colEdit);
        //    }

        //    if (Dgrid.Columns["colDelete"] == null)
        //    {
        //        var colDelete = new DataGridViewButtonColumn();
        //        colDelete.Name = "colDelete";
        //        colDelete.HeaderText = "Delete";
        //        colDelete.Text = "Delete";
        //        colDelete.UseColumnTextForButtonValue = true;
        //        colDelete.Width = 70;
        //        Dgrid.Columns.Add(colDelete);
        //    }

        //    // Optional: keep action columns at the end
        //    Dgrid.Columns["colEdit"].DisplayIndex = Dgrid.Columns.Count - 2;
        //    Dgrid.Columns["colDelete"].DisplayIndex = Dgrid.Columns.Count - 1;


        //    //// Avoid duplicate columns if called multiple times
        //    //if (Dgrid.Columns["colDelete"] == null)
        //    //{
        //    //    var colDelete = new DataGridViewButtonColumn();
        //    //    colDelete.Name = "colDelete";
        //    //    colDelete.HeaderText = "Action";
        //    //    colDelete.Text = "Delete";
        //    //    colDelete.UseColumnTextForButtonValue = true;
        //    //    colDelete.Width = 80;
        //    //    Dgrid.Columns.Add(colDelete);
        //    //}
        //}


        //private void btnDeleteSelected_Click(object sender, EventArgs e)
        //    {
        //        if (Dgrid.CurrentRow != null && Dgrid.CurrentRow.Index >= 0)
        //        {
        //            var toRemove = Dgrid.CurrentRow.DataBoundItem as UserRecord;
        //            if (toRemove != null)
        //            {
        //                var confirm = MessageBox.Show("Delete this record?", "Confirm", MessageBoxButtons.YesNo);
        //                if (confirm == DialogResult.Yes)
        //                    _records.Remove(toRemove);
        //            }
        //        }
        //    }

        // Handle delete button inside the grid
        //private void Dgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0) return; // header
        //    var grid = (DataGridView)sender;

        //    if (grid.Columns[e.ColumnIndex].Name == "colDelete")
        //    {
        //        var record = grid.Rows[e.RowIndex].DataBoundItem as UserRecord;
        //        if (record != null)
        //        {
        //            var confirm = MessageBox.Show("Delete this record?", "Confirm", MessageBoxButtons.YesNo);
        //            if (confirm == DialogResult.Yes)
        //                _records.Remove(record);
        //        }
        //    }
        //}

        //private void btnload_Click_1(object sender, EventArgs e)
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
        //        {
        //            if (current != null)
        //                current.SecondName = line.Substring("Second Name:".Length).Trim();
        //        }
        //        else if (line.StartsWith("Email:"))
        //        {
        //            if (current != null)
        //                current.Email = line.Substring("Email:".Length).Trim();
        //        }
        //        else if (line.StartsWith("Username:"))
        //        {
        //            if (current != null)
        //                current.Username = line.Substring("Username:".Length).Trim();
        //        }
        //        else if (line.StartsWith("Password:"))
        //        {
        //            if (current != null)
        //                current.Password = line.Substring("Password:".Length).Trim();
        //        }
        //        else if (line.StartsWith("Division:"))
        //        {
        //            if (current != null)
        //                current.Division = line.Substring("Division:".Length).Trim();
        //        }
        //        else if (line.StartsWith("Subdivisions:"))
        //        {
        //            if (current != null)
        //                current.Subdivisions = line.Substring("Subdivisions:".Length).Trim();
        //        }
        //        else if (line.StartsWith("Status:"))
        //        {
        //            if (current != null)
        //                current.Status = line.Substring("Status:".Length).Trim();
        //        }
        //        else if (line.StartsWith("----------------"))
        //        {
        //            if (current != null)
        //                _records.Add(current);

        //            current = null;
        //        }
        //    }

        //    // Ensure the action column exists (safe-call)
        //    AddRowActionButtons();
        //}

        //private void btnsave_Click_1(object sender, EventArgs e)
        //{
        //    string path = GetFilePath();

        //    // Optional: guard if empty
        //    if (_records.Count == 0)
        //    {
        //        var ask = MessageBox.Show(
        //            "No records in the grid. This will create an empty file. Continue?",
        //            "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //        if (ask != DialogResult.Yes)
        //            return;
        //    }

        //    using (var sw = new StreamWriter(path, false))
        //    {
        //        foreach (var r in _records)
        //        {
        //            sw.WriteLine($"First Name: {r.FirstName}");
        //            sw.WriteLine($"Second Name: {r.SecondName}");
        //            sw.WriteLine($"Email: {r.Email}");
        //            sw.WriteLine($"Username: {r.Username}");
        //            sw.WriteLine($"Password: {r.Password}");
        //            sw.WriteLine($"Division: {r.Division}");
        //            sw.WriteLine($"Subdivisions: {r.Subdivisions}");
        //            sw.WriteLine($"Status: {r.Status}");
        //            sw.WriteLine("------------------------------");
        //        }
        //    }

        //    MessageBox.Show("File updated successfully!");
        //}
        //}
    }
}
 
    
