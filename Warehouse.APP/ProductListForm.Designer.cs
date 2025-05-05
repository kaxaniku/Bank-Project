namespace Warehouse.APP
{
    partial class ProductListForm
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
            dataGridView = new DataGridView();
            ProductName = new DataGridViewTextBoxColumn();
            Barcode = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            Dimensions = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { ProductName, Barcode, Description, Dimensions });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(0, 0);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(800, 474);
            dataGridView.TabIndex = 0;
            // 
            // ProductName
            // 
            ProductName.DataPropertyName = "Name";
            ProductName.HeaderText = "Name";
            ProductName.MinimumWidth = 6;
            ProductName.Name = "ProductName";
            ProductName.ReadOnly = true;
            ProductName.Width = 125;
            // 
            // Barcode
            // 
            Barcode.DataPropertyName = "Barcode";
            Barcode.HeaderText = "Barcode";
            Barcode.MinimumWidth = 6;
            Barcode.Name = "Barcode";
            Barcode.ReadOnly = true;
            Barcode.Width = 125;
            // 
            // Description
            // 
            Description.DataPropertyName = "Description";
            Description.HeaderText = "Description";
            Description.MinimumWidth = 6;
            Description.Name = "Description";
            Description.ReadOnly = true;
            Description.Width = 125;
            // 
            // Dimensions
            // 
            Dimensions.DataPropertyName = "Dimensions";
            Dimensions.HeaderText = "Dimensions";
            Dimensions.MinimumWidth = 6;
            Dimensions.Name = "Dimensions";
            Dimensions.ReadOnly = true;
            Dimensions.Width = 125;
            // 
            // ProductListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 474);
            Controls.Add(dataGridView);
            Name = "ProductListForm";
            Text = "ProductListForm";
            Load += ProductListForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn ProductName;
        private DataGridViewTextBoxColumn Barcode;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn Dimensions;
    }
}