using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Warehouse.DTO;
using Warehouse.Factories;
using Warehouse.Services;
using Warehouse.Services.Exceptions;
using Warehouse.Services.Interfaces.Services;


namespace Warehouse.APP
{
    public partial class ProductViewForm : Form
    {
        public ProductViewForm()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
        }

        private void LoadProducts()
        {
            IProductService productService = ProductServiceFactory.Create();

            // Fetch categories and products
            var categories = productService.GetCategories().ToList();
            var products = productService.GetProductsByName().ToList();

            // Join products with categories to get category names
            var productData = from product in products
                              join category in categories
                              on product.CategoryId equals category.CategoryId
                              select new
                              {
                                  ProductId = product.ProductId,
                                  ProductName = product.Name,
                                  CategoryName = category.Name,
                                  ProductBarcode = product.Barcode,
                                  ProductWeight = product.Weight,
                                  ProductDimensions = product.Dimensions,
                                  ProductDescription = product.Description
                              };

            // Bind the data to the DataGridView
            dgvProducts.DataSource = productData.ToList();
        }
    }
}
