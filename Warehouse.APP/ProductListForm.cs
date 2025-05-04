using Warehouse.APP.Interfaces;

namespace Warehouse.APP;

public partial class ProductListForm : Form, IListForm
{
    public ProductListForm()
    {
        InitializeComponent();
    }

    public void Add()
    {
        ProductDetailsForm form = new ProductDetailsForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            // Assuming you have a method to refresh the product list
        }
    }

    public void Edit()
    {

    }

    public void Delete()
    {

    }

    private void ProductListForm_Load(object sender, EventArgs e)
    {

    }
}
