using Warehouse.DTO;
using Warehouse.Factories;
using Warehouse.Services;
using Warehouse.Services.Exceptions;
using Warehouse.Services.Interfaces.Services;

namespace Warehouse.APP
{
    public partial class ProductAddForm : Form
    {
        public ProductAddForm()
        {
            InitializeComponent();
            PopulateCategories();
        }

        private void ClearAllBtn(object sender, EventArgs e)
        {
            cboCategory.SelectedIndex = -1; ;
            txtName.Text = "";
            txtBarcode.Text = "";
            txtLength.Text = "";
            txtHeight.Text = "";
            txtWidth.Text = "";
            txtWeight.Text = "";
            txtDescription.Text = "";
        }

        private void saveAndExit_Click(object sender, EventArgs e)
        {
            IProductService productService = ProductServiceFactory.Create();

            Executor.Execute<LoginException>(() =>
            {
                var product = new Product
                {
                    CategoryId = (int)cboCategory.SelectedValue,
                    Barcode = txtBarcode.Text,
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Dimensions = $"{txtLength.Text}x{txtWidth.Text}x{txtHeight.Text}",
                    Weight = float.Parse(txtWeight.Text),
                };

                productService.AddProduct(product);
                DialogResult = DialogResult.OK;
            }, ex => this.ShowWarning(ex.Message));



        }

        private void height_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox)!.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void width_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox)!.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox)!.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox)!.Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }

        }
        private void PopulateCategories()
        {
            IProductService productService = ProductServiceFactory.Create();

            var categories = productService.GetCategories().ToList();

            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "CategoryId";
            cboCategory.SelectedIndex = -1;
        }

    }
}
