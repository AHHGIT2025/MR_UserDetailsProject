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
    public partial class FormUserTableView : Form
    {

         

        private readonly UserRepository _repo = new UserRepository();
        private BindingList<UserRecord> _rows = new BindingList<UserRecord>();
        private bool _cellContentClickWired = false;
        public FormUserTableView()
        {
           

            InitializeComponent();
            //dataGridView1.AutoGenerateColumns = false;
           
            //dataGridView1.DataSource = _rows;
            //dataGridView1.ReadOnly = false;
            //dataGridView1.AllowUserToAddRows = false; // keep controlled inserts
            //dataGridView1.AllowUserToDeleteRows = false; // delete via repo method
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void FormUserTableView_Load(object sender, EventArgs e)
        {

            // status filter defaults
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.AddRange(new object[] { "All", "Active", "Inactive" });
            cmbStatusFilter.SelectedIndex = 0;

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = _rows;


            dataGridView1.ReadOnly = false;               // allow inline edits
            dataGridView1.AllowUserToAddRows = false;     // control inserts yourself
            dataGridView1.AllowUserToDeleteRows = false;  // delete via button
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (!_cellContentClickWired)
            {
                dataGridView1.CellContentClick += dataGridView1_CellContentClick;
                _cellContentClickWired = true;
            }
          //  dataGridView1.Dock = DockStyle.Fill; // or Anchor = Top, Bottom, Left, Right
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            var term = txtSearch.Text?.Trim();
            var list = await _repo.SearchAsync(term);
            _rows = new BindingList<UserRecord>(list);
            dataGridView1.DataSource = _rows;

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {

            var list = await _repo.GetAllAsync();
            _rows = new BindingList<UserRecord>(list);
            dataGridView1.DataSource = _rows;

        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Guard against header clicks or invalid indices
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.ColumnIndex >= dataGridView1.Columns.Count) return;
            if (e.RowIndex >= dataGridView1.Rows.Count) return;

            var col = dataGridView1.Columns[e.ColumnIndex];
            var row = dataGridView1.Rows[e.RowIndex];
            if (col == null || row == null) return;

            var rec = row.DataBoundItem as UserRecord;
            if (rec == null) return;

            if (col.Name == "colEdit")
            {
                // Put the row into edit mode and focus first editable, visible data column
                for (int c = 0; c < dataGridView1.Columns.Count; c++)
                {
                    var dc = dataGridView1.Columns[c];
                    if (dc is DataGridViewButtonColumn) continue;
                    if (!dc.Visible || dc.ReadOnly) continue;

                    dataGridView1.CurrentCell = row.Cells[c];
                    dataGridView1.BeginEdit(true);
                    break;
                }
            }
            else if (col.Name == "colDelete")
            {
                if (string.IsNullOrWhiteSpace(rec.EmpID))
                {
                    MessageBox.Show("EmpID is required to delete.");
                    return;
                }

                var confirm = MessageBox.Show(
                    $"Delete EmpID {rec.EmpID}?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                var affected = await _repo.DeleteByEmpIDAsync(rec.EmpID);
                if (affected > 0)
                {
                    _rows.Remove(rec); // BindingList updates grid automatically
                }
                else
                {
                    MessageBox.Show("Delete failed in database.");
                }
            }

        }

        private async void Filter_Click(object sender, EventArgs e)
        {

            string selected = cmbStatusFilter.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(selected)) selected = "All";

            if (selected == "All")
            {
                var listAll = await _repo.GetAllAsync();
                _rows = new BindingList<UserRecord>(listAll);
                dataGridView1.DataSource = _rows;
            }
            else
            {
                var list = await _repo.GetByStatusAsync(selected);
                _rows = new BindingList<UserRecord>(list);
                dataGridView1.DataSource = _rows;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
