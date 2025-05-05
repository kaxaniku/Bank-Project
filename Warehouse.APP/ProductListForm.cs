using Warehouse.APP.Interfaces;
using Warehouse.Factories;
using Warehouse.Services.Interfaces.Services;

namespace Warehouse.APP;

public partial class ProductListForm : Form, IListForm
{
    private readonly IProductService _productService;

    public ProductListForm()
    {
        InitializeComponent();
        dataGridView.AutoGenerateColumns = false;

        _productService = ProductServiceFactory.Create();
        LoadData();
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

    public void LoadData()
    {
        var result = _productService.GetProductsByName();
        dataGridView.DataSource = result.ToList();
    }

    private void ProductListForm_Load(object sender, EventArgs e)
    {

    }
}
