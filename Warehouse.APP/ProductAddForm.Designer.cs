namespace Warehouse.APP
{
    partial class ProductAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductAddForm));
            pbBackground = new PictureBox();
            lblAddProduct = new Label();
            pbLogo = new PictureBox();
            cboCategory = new ComboBox();
            txtHeight = new TextBox();
            txtDescription = new TextBox();
            txtBarcode = new TextBox();
            txtWidth = new TextBox();
            txtName = new TextBox();
            lblCategory = new Label();
            lblDescription = new Label();
            lblWeight = new Label();
            lblDimensions = new Label();
            lblBarcode = new Label();
            lblName = new Label();
            lblSeparator1 = new Label();
            txtWeight = new TextBox();
            saveAndExit = new Button();
            btnCancel = new Button();
            clearAll = new Button();
            lblSeparator2 = new Label();
            txtLength = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pbBackground).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            SuspendLayout();
            // 
            // pbBackground
            // 
            pbBackground.BackColor = SystemColors.ActiveCaption;
            pbBackground.Location = new Point(0, 0);
            pbBackground.Margin = new Padding(3, 4, 3, 4);
            pbBackground.Name = "pbBackground";
            pbBackground.Size = new Size(550, 70);
            pbBackground.TabIndex = 0;
            pbBackground.TabStop = false;
            // 
            // lblAddProduct
            // 
            lblAddProduct.AutoSize = true;
            lblAddProduct.BackColor = SystemColors.ActiveCaption;
            lblAddProduct.Font = new Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAddProduct.ForeColor = SystemColors.HotTrack;
            lblAddProduct.Location = new Point(66, 7);
            lblAddProduct.Name = "lblAddProduct";
            lblAddProduct.Size = new Size(228, 50);
            lblAddProduct.TabIndex = 6;
            lblAddProduct.Text = "Edit Product";
            // 
            // pbLogo
            // 
            pbLogo.BackColor = SystemColors.Window;
            pbLogo.Image = (Image)resources.GetObject("pbLogo.Image");
            pbLogo.Location = new Point(0, 0);
            pbLogo.Margin = new Padding(3, 4, 3, 4);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(59, 67);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 2;
            pbLogo.TabStop = false;
            // 
            // cboCategory
            // 
            cboCategory.Cursor = Cursors.Hand;
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(153, 80);
            cboCategory.Margin = new Padding(3, 4, 3, 4);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(380, 28);
            cboCategory.TabIndex = 0;
            // 
            // txtHeight
            // 
            txtHeight.Cursor = Cursors.Hand;
            txtHeight.Location = new Point(437, 210);
            txtHeight.Margin = new Padding(3, 4, 3, 4);
            txtHeight.MaxLength = 10;
            txtHeight.Name = "txtHeight";
            txtHeight.PlaceholderText = "Height (m)";
            txtHeight.Size = new Size(100, 27);
            txtHeight.TabIndex = 5;
            txtHeight.KeyPress += height_KeyPress;
            // 
            // txtDescription
            // 
            txtDescription.Cursor = Cursors.Hand;
            txtDescription.Location = new Point(153, 300);
            txtDescription.Margin = new Padding(3, 4, 3, 4);
            txtDescription.MaxLength = 1000;
            txtDescription.MinimumSize = new Size(320, 100);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(380, 150);
            txtDescription.TabIndex = 7;
            // 
            // txtBarcode
            // 
            txtBarcode.Cursor = Cursors.Hand;
            txtBarcode.Location = new Point(153, 168);
            txtBarcode.Margin = new Padding(3, 4, 3, 4);
            txtBarcode.MaxLength = 100;
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(380, 27);
            txtBarcode.TabIndex = 2;
            // 
            // txtWidth
            // 
            txtWidth.Cursor = Cursors.Hand;
            txtWidth.Location = new Point(295, 210);
            txtWidth.Margin = new Padding(3, 4, 3, 4);
            txtWidth.MaxLength = 10;
            txtWidth.Name = "txtWidth";
            txtWidth.PlaceholderText = "Width (m)";
            txtWidth.Size = new Size(100, 27);
            txtWidth.TabIndex = 4;
            txtWidth.KeyPress += width_KeyPress;
            // 
            // txtName
            // 
            txtName.Cursor = Cursors.Hand;
            txtName.ImeMode = ImeMode.NoControl;
            txtName.Location = new Point(153, 124);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.MaxLength = 100;
            txtName.Name = "txtName";
            txtName.Size = new Size(380, 27);
            txtName.TabIndex = 1;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCategory.Location = new Point(10, 80);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(92, 28);
            lblCategory.TabIndex = 0;
            lblCategory.Text = "Category";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 12F);
            lblDescription.Location = new Point(10, 300);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(116, 28);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Description:";
            // 
            // lblWeight
            // 
            lblWeight.AutoSize = true;
            lblWeight.Font = new Font("Segoe UI", 12F);
            lblWeight.Location = new Point(10, 256);
            lblWeight.Name = "lblWeight";
            lblWeight.Size = new Size(79, 28);
            lblWeight.TabIndex = 4;
            lblWeight.Text = "Weight:";
            // 
            // lblDimensions
            // 
            lblDimensions.AutoSize = true;
            lblDimensions.Font = new Font("Segoe UI", 12F);
            lblDimensions.Location = new Point(10, 212);
            lblDimensions.Name = "lblDimensions";
            lblDimensions.Size = new Size(117, 28);
            lblDimensions.TabIndex = 3;
            lblDimensions.Text = "Dimensions:";
            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI", 12F);
            lblBarcode.Location = new Point(10, 168);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(83, 28);
            lblBarcode.TabIndex = 2;
            lblBarcode.Text = "Barcode";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 12F);
            lblName.Location = new Point(10, 124);
            lblName.Name = "lblName";
            lblName.Size = new Size(64, 28);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // lblSeparator1
            // 
            lblSeparator1.AutoSize = true;
            lblSeparator1.Location = new Point(259, 213);
            lblSeparator1.Name = "lblSeparator1";
            lblSeparator1.Size = new Size(30, 20);
            lblSeparator1.TabIndex = 15;
            lblSeparator1.Text = "✖";
            // 
            // txtWeight
            // 
            txtWeight.Cursor = Cursors.Hand;
            txtWeight.Location = new Point(153, 256);
            txtWeight.Margin = new Padding(3, 4, 3, 4);
            txtWeight.Name = "txtWeight";
            txtWeight.PlaceholderText = "Weight (kg)";
            txtWeight.Size = new Size(380, 27);
            txtWeight.TabIndex = 6;
            txtWeight.KeyPress += txtWeight_KeyPress;
            // 
            // saveAndExit
            // 
            saveAndExit.BackColor = SystemColors.ActiveCaption;
            saveAndExit.Cursor = Cursors.Hand;
            saveAndExit.FlatStyle = FlatStyle.Popup;
            saveAndExit.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            saveAndExit.Location = new Point(43, 469);
            saveAndExit.Margin = new Padding(3, 4, 3, 4);
            saveAndExit.Name = "saveAndExit";
            saveAndExit.Size = new Size(144, 36);
            saveAndExit.TabIndex = 8;
            saveAndExit.Text = "Save and Exit";
            saveAndExit.UseVisualStyleBackColor = false;
            saveAndExit.Click += saveAndExit_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = SystemColors.ActiveCaption;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCancel.Location = new Point(206, 469);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(144, 36);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // clearAll
            // 
            clearAll.BackColor = SystemColors.ActiveCaption;
            clearAll.Cursor = Cursors.Hand;
            clearAll.FlatStyle = FlatStyle.Popup;
            clearAll.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            clearAll.Location = new Point(369, 469);
            clearAll.Margin = new Padding(3, 4, 3, 4);
            clearAll.Name = "clearAll";
            clearAll.Size = new Size(136, 36);
            clearAll.TabIndex = 10;
            clearAll.Text = "Clear All";
            clearAll.UseVisualStyleBackColor = false;
            clearAll.Click += ClearAllBtn;
            // 
            // lblSeparator2
            // 
            lblSeparator2.AutoSize = true;
            lblSeparator2.Location = new Point(401, 213);
            lblSeparator2.Name = "lblSeparator2";
            lblSeparator2.Size = new Size(30, 20);
            lblSeparator2.TabIndex = 16;
            lblSeparator2.Text = "✖";
            // 
            // txtLength
            // 
            txtLength.Cursor = Cursors.Hand;
            txtLength.Location = new Point(153, 210);
            txtLength.Margin = new Padding(3, 4, 3, 4);
            txtLength.MaxLength = 10;
            txtLength.Name = "txtLength";
            txtLength.PlaceholderText = "Length (m)";
            txtLength.Size = new Size(100, 27);
            txtLength.TabIndex = 3;
            txtLength.KeyPress += txtLength_KeyPress;
            // 
            // ProductAddForm
            // 
            AcceptButton = saveAndExit;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(545, 523);
            Controls.Add(txtLength);
            Controls.Add(lblSeparator2);
            Controls.Add(clearAll);
            Controls.Add(btnCancel);
            Controls.Add(saveAndExit);
            Controls.Add(txtWeight);
            Controls.Add(lblSeparator1);
            Controls.Add(lblName);
            Controls.Add(lblBarcode);
            Controls.Add(lblDimensions);
            Controls.Add(lblWeight);
            Controls.Add(lblDescription);
            Controls.Add(lblCategory);
            Controls.Add(txtName);
            Controls.Add(txtWidth);
            Controls.Add(txtBarcode);
            Controls.Add(txtDescription);
            Controls.Add(txtHeight);
            Controls.Add(cboCategory);
            Controls.Add(pbLogo);
            Controls.Add(lblAddProduct);
            Controls.Add(pbBackground);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "ProductAddForm";
            Text = "Add Products";
            ((System.ComponentModel.ISupportInitialize)pbBackground).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbBackground;
        private Label lblAddProduct;
        private PictureBox pbLogo;

        public class Item
        {
            public Item() { }

            public string Value { set; get; }
            public string Text { set; get; }
        }

        private ComboBox cboCategory;
        private TextBox txtHeight;
        private TextBox txtDescription;
        private TextBox txtBarcode;
        private TextBox txtWidth;
        private TextBox txtName;
        private Label lblCategory;
        private Label lblDescription;
        private Label lblWeight;
        private Label lblDimensions;
        private Label lblBarcode;
        private Label lblName;
        private Label lblSeparator1;
        private TextBox txtWeight;
        private Button saveAndExit;
        private Button btnCancel;
        private Button clearAll;
        private Label lblSeparator2;
        private TextBox txtLength;
    }
}