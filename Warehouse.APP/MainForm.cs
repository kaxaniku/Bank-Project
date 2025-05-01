using Warehouse.APP.Interfaces;

namespace Warehouse.APP
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void searchToolStripMenuItem1_Click(object sender, EventArgs e)
            => ShowListForm<ProductListForm>();

        private void searchToolStripMenuItem2_Click(object sender, EventArgs e)
            => ShowListForm<UserListForm>();

        private void toolStripButton1_Click(object sender, EventArgs e)
            => (ActiveMdiChild as IListForm)?.Add();

        private void ShowListForm<T>() where T : Form, new()
        {
            var form = new T();
            form.MdiParent = this;
            form.Show();
        }
    }
}
