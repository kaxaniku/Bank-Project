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
        LoadData(_productService.GetProductsByName());
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

    public void LoadData(IEnumerable<object> data)
    {
        dataGridView.DataSource = data.ToList();
    }

    private void ProductListForm_Load(object sender, EventArgs e)
    {

    }

    private void SearchBar_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            string searchText = SearchBar.Text;

            var searchedItems = _productService.GetProductsByName()
                .Where(item => new[]
                {
                item.Barcode,
                item.Name,
                item.Description ?? string.Empty,
                item.Dimensions
                }.Any(property => property.Contains(searchText)));

            LoadData(searchedItems);
        }
    }

    private void SearchBar_Enter(object sender, EventArgs e)
    {
        SearchBar.Clear();
    }
}
