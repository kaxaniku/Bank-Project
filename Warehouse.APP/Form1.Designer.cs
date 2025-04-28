namespace Warehouse.APP
{
    partial class WarehouseMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarehouseMainForm));
            btnAddProduct = new Button();
            btnViewProducts = new Button();
            SuspendLayout();
            // 
            // btnAddProduct
            // 
            btnAddProduct.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAddProduct.Image = (Image)resources.GetObject("btnAddProduct.Image");
            btnAddProduct.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddProduct.Location = new Point(25, 12);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(260, 80);
            btnAddProduct.TabIndex = 0;
            btnAddProduct.Text = "Add Product";
            btnAddProduct.TextAlign = ContentAlignment.MiddleRight;
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // btnViewProducts
            // 
            btnViewProducts.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnViewProducts.Image = (Image)resources.GetObject("btnViewProducts.Image");
            btnViewProducts.ImageAlign = ContentAlignment.MiddleLeft;
            btnViewProducts.Location = new Point(309, 12);
            btnViewProducts.Name = "btnViewProducts";
            btnViewProducts.Size = new Size(260, 80);
            btnViewProducts.TabIndex = 1;
            btnViewProducts.Text = "View Products";
            btnViewProducts.TextAlign = ContentAlignment.MiddleRight;
            btnViewProducts.UseVisualStyleBackColor = true;
            btnViewProducts.Click += btnViewProducts_Click;
            // 
            // WarehouseMainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 453);
            Controls.Add(btnViewProducts);
            Controls.Add(btnAddProduct);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WarehouseMainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Warehouse";
            ResumeLayout(false);
        }

        #endregion

        private Button btnAddProduct;
        private Button btnViewProducts;
    }
}
