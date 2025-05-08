using System.Windows.Forms.VisualStyles;
using Warehouse.Factories;
using Warehouse.Services.Exceptions;
using Warehouse.Services.Interfaces.Services;

namespace Warehouse.APP;

public partial class ProductDetailsForm : Form
{
    public ProductDetailsForm()
    {
        InitializeComponent();
    }

    private void pictureBox3_Click(object sender, EventArgs e)
    {
        try
        {
            IProductService productService = ProductServiceFactory.Create();
            var categories = productService.GetCategories(); // List<Category>

            foreach (var cat in categories)
            {
                category.Items.Add(cat.Name);
            }
        }
        catch (Exception ex)
        {
            this.ShowWarning(ex.Message);
        }
            
    }

    private void ClearAllBtn(object sender, EventArgs e)
    {
        category.Text = "";
        name.Text = "";
        barcode.Text = "";
        height.Text = "";
        width.Text = "";
        weight.Text = "";
        description.Text = "";
    }

    private void saveAndExit_Click(object sender, EventArgs e)
    {

    }
}
