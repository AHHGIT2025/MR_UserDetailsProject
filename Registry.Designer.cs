namespace User_Registration
{
    partial class Registry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Dgrid = new System.Windows.Forms.DataGridView();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Dgrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgrid
            // 
            this.Dgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgrid.Location = new System.Drawing.Point(12, 61);
            this.Dgrid.Name = "Dgrid";
            this.Dgrid.RowHeadersWidth = 51;
            this.Dgrid.RowTemplate.Height = 24;
            this.Dgrid.Size = new System.Drawing.Size(1324, 377);
            this.Dgrid.TabIndex = 0;
            this.Dgrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgrid_CellContentClick);
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(168, 32);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(75, 23);
            this.btnsave.TabIndex = 4;
            this.btnsave.Text = "SAVE ALL";
            this.btnsave.UseVisualStyleBackColor = true;
            //this.btnsave.Click += new System.EventHandler(this.btnsave_Click_1);
            // 
            // btnload
            // 
            this.btnload.Location = new System.Drawing.Point(40, 32);
            this.btnload.Name = "btnload";
            this.btnload.Size = new System.Drawing.Size(75, 23);
            this.btnload.TabIndex = 3;
            this.btnload.Text = "LOAD";
            this.btnload.UseVisualStyleBackColor = true;
            //this.btnload.Click += new System.EventHandler(this.btnload_Click_1);
            // 
            // Registry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1368, 450);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.btnload);
            this.Controls.Add(this.Dgrid);
            this.Name = "Registry";
            this.Text = "Registry";
            //this.Load += new System.EventHandler(this.Registry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgrid;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnload;
    }
}