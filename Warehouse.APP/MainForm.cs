namespace Warehouse.APP
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductListForm form = new ProductListForm();
            form.MdiParent = this;
            form.Show();
        }
    }
}
