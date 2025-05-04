namespace Warehouse.APP;

public partial class ProductDetailsForm : Form
{
    public ProductDetailsForm()
    {
        InitializeComponent();
    }

    private void pictureBox3_Click(object sender, EventArgs e)
    {

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
