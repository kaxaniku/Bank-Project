using Warehouse.Repositories;

namespace Warehouse.APP
{
    public partial class WarehouseMainForm : Form
    {
        public WarehouseMainForm()
        {
            InitializeComponent();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ProductAddForm addProductForm = new ProductAddForm();
            if (addProductForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
        }
        private void btnViewProducts_Click(object sender, EventArgs e)
        {
            ProductViewForm productViewForm = new ProductViewForm();
            productViewForm.Show();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {

        }

    }
}
