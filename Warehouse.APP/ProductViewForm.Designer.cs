using Warehouse.DTO;

namespace Warehouse.APP
{
    partial class ProductViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductViewForm));
            dgvProducts = new DataGridView();
            btnSearchProduct = new Button();
            txtProductName = new TextBox();
            txtCaregoryName = new TextBox();
            btnEditProduct = new Button();
            btnDeleteProduct = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(12, 63);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.Size = new Size(988, 462);
            dgvProducts.TabIndex = 0;
            // 
            // btnSearchProduct
            // 
            btnSearchProduct.Font = new Font("Segoe UI", 12F);
            btnSearchProduct.Location = new Point(12, 12);
            btnSearchProduct.Name = "btnSearchProduct";
            btnSearchProduct.Size = new Size(120, 45);
            btnSearchProduct.TabIndex = 1;
            btnSearchProduct.Text = "Search";
            btnSearchProduct.UseVisualStyleBackColor = true;
            btnSearchProduct.Click += btnSearch_Click;
            // 
            // txtProductName
            // 
            txtProductName.Font = new Font("Segoe UI", 10F);
            txtProductName.Location = new Point(164, 22);
            txtProductName.Name = "txtProductName";
            txtProductName.PlaceholderText = "Product Name";
            txtProductName.Size = new Size(250, 30);
            txtProductName.TabIndex = 2;
            // 
            // txtCaregoryName
            // 
            txtCaregoryName.Font = new Font("Segoe UI", 10F);
            txtCaregoryName.Location = new Point(446, 22);
            txtCaregoryName.Name = "txtCaregoryName";
            txtCaregoryName.PlaceholderText = "Category Name";
            txtCaregoryName.Size = new Size(250, 30);
            txtCaregoryName.TabIndex = 3;
            // 
            // btnEditProduct
            // 
            btnEditProduct.Font = new Font("Segoe UI", 12F);
            btnEditProduct.Location = new Point(728, 12);
            btnEditProduct.Name = "btnEditProduct";
            btnEditProduct.Size = new Size(120, 45);
            btnEditProduct.TabIndex = 4;
            btnEditProduct.Text = "Edit";
            btnEditProduct.UseVisualStyleBackColor = true;
            // 
            // btnDeleteProduct
            // 
            btnDeleteProduct.Font = new Font("Segoe UI", 12F);
            btnDeleteProduct.Location = new Point(880, 12);
            btnDeleteProduct.Name = "btnDeleteProduct";
            btnDeleteProduct.Size = new Size(120, 45);
            btnDeleteProduct.TabIndex = 5;
            btnDeleteProduct.Text = "Delete";
            btnDeleteProduct.UseVisualStyleBackColor = true;
            // 
            // ProductViewForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1012, 537);
            Controls.Add(btnDeleteProduct);
            Controls.Add(btnEditProduct);
            Controls.Add(txtCaregoryName);
            Controls.Add(txtProductName);
            Controls.Add(btnSearchProduct);
            Controls.Add(dgvProducts);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ProductViewForm";
            Text = "View Product(s)";
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProducts;
        private Button btnSearchProduct;
        private TextBox txtProductName;
        private TextBox txtCaregoryName;
        private Button btnEditProduct;
        private Button btnDeleteProduct;
    }
}