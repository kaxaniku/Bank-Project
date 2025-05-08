namespace Warehouse.APP
{
    partial class ProductDetailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductDetailsForm));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            category = new ComboBox();
            height = new TextBox();
            description = new TextBox();
            barcode = new TextBox();
            width = new TextBox();
            name = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            weight = new TextBox();
            saveAndExit = new Button();
            discard = new Button();
            clearAll = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ActiveCaption;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 50);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaption;
            label1.Font = new Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(58, 5);
            label1.Name = "label1";
            label1.Size = new Size(181, 40);
            label1.TabIndex = 1;
            label1.Text = "Add Product";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.Window;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(52, 50);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // category
            // 
            category.Cursor = Cursors.Hand;
            category.FormattingEnabled = true;
            category.Location = new Point(12, 77);
            category.Name = "category";
            category.Size = new Size(128, 23);
            category.TabIndex = 3;
            category.DropDown += pictureBox3_Click;
            // 
            // height
            // 
            height.Cursor = Cursors.Hand;
            height.Location = new Point(12, 121);
            height.Name = "height";
            height.PlaceholderText = "Height";
            height.Size = new Size(69, 23);
            height.TabIndex = 4;
            // 
            // description
            // 
            description.Cursor = Cursors.Hand;
            description.Location = new Point(10, 209);
            description.MaximumSize = new Size(524, 100);
            description.MinimumSize = new Size(524, 25);
            description.Name = "description";
            description.Size = new Size(524, 25);
            description.TabIndex = 5;
            // 
            // barcode
            // 
            barcode.Cursor = Cursors.Hand;
            barcode.Location = new Point(408, 77);
            barcode.Name = "barcode";
            barcode.Size = new Size(128, 23);
            barcode.TabIndex = 6;
            // 
            // width
            // 
            width.Cursor = Cursors.Hand;
            width.Location = new Point(121, 121);
            width.Name = "width";
            width.PlaceholderText = "Width";
            width.Size = new Size(69, 23);
            width.TabIndex = 7;
            // 
            // name
            // 
            name.Cursor = Cursors.Hand;
            name.Location = new Point(205, 77);
            name.Name = "name";
            name.Size = new Size(128, 23);
            name.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 59);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 9;
            label2.Text = "Category";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(10, 191);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 10;
            label3.Text = "Description:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 147);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 11;
            label4.Text = "Weight:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(12, 103);
            label5.Name = "label5";
            label5.Size = new Size(73, 15);
            label5.TabIndex = 12;
            label5.Text = "Dimensions:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(408, 59);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 13;
            label6.Text = "Barcode";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(205, 59);
            label7.Name = "label7";
            label7.Size = new Size(39, 15);
            label7.TabIndex = 14;
            label7.Text = "Name";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(91, 124);
            label8.Name = "label8";
            label8.Size = new Size(19, 15);
            label8.TabIndex = 15;
            label8.Text = "✖";
            // 
            // weight
            // 
            weight.Cursor = Cursors.Hand;
            weight.Location = new Point(12, 165);
            weight.Name = "weight";
            weight.Size = new Size(128, 23);
            weight.TabIndex = 16;
            // 
            // saveAndExit
            // 
            saveAndExit.BackColor = SystemColors.ActiveCaption;
            saveAndExit.Cursor = Cursors.Hand;
            saveAndExit.FlatStyle = FlatStyle.Popup;
            saveAndExit.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            saveAndExit.Location = new Point(408, 240);
            saveAndExit.Name = "saveAndExit";
            saveAndExit.Size = new Size(126, 27);
            saveAndExit.TabIndex = 17;
            saveAndExit.Text = "Save and Exit";
            saveAndExit.UseVisualStyleBackColor = false;
            saveAndExit.Click += saveAndExit_Click;
            // 
            // discard
            // 
            discard.BackColor = SystemColors.ActiveCaption;
            discard.Cursor = Cursors.Hand;
            discard.FlatStyle = FlatStyle.Popup;
            discard.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            discard.Location = new Point(12, 240);
            discard.Name = "discard";
            discard.Size = new Size(126, 27);
            discard.TabIndex = 18;
            discard.Text = "Discard";
            discard.UseVisualStyleBackColor = false;
            // 
            // clearAll
            // 
            clearAll.BackColor = SystemColors.ActiveCaption;
            clearAll.Cursor = Cursors.Hand;
            clearAll.FlatStyle = FlatStyle.Popup;
            clearAll.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            clearAll.Location = new Point(214, 240);
            clearAll.Name = "clearAll";
            clearAll.Size = new Size(119, 27);
            clearAll.TabIndex = 19;
            clearAll.Text = "Clear All";
            clearAll.UseVisualStyleBackColor = false;
            clearAll.Click += ClearAllBtn;
            // 
            // ProductDetailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(553, 284);
            Controls.Add(clearAll);
            Controls.Add(discard);
            Controls.Add(saveAndExit);
            Controls.Add(weight);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(name);
            Controls.Add(width);
            Controls.Add(barcode);
            Controls.Add(description);
            Controls.Add(height);
            Controls.Add(category);
            Controls.Add(pictureBox2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "ProductDetailsForm";
            Text = "Add Products";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
        private ComboBox comboBox1;
        private void Init()
        {
            List<Item> items = new List<Item>();
            items.Add(new Item() { Text = "displayText1", Value = "ValueText1" });
            items.Add(new Item() { Text = "displayText2", Value = "ValueText2" });
            items.Add(new Item() { Text = "displayText3", Value = "ValueText3" });

            comboBox1.DataSource = items;
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

        }

        public class Item
        {
            public Item() { }

            public string Value { set; get; }
            public string Text { set; get; }
        }

        private ComboBox category;
        private TextBox height;
        private TextBox description;
        private TextBox barcode;
        private TextBox width;
        private TextBox name;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox weight;
        private Button saveAndExit;
        private Button discard;
        private Button clearAll;
    }
}