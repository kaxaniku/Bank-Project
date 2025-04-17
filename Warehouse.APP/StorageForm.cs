using System;
using System.Windows.Forms;

namespace WarehouseApp
{
    public partial class StorageForm : Form
    {
        public StorageForm()
        {
            InitializeComponent();
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            // Add logic for adding item (stubbed for now)
            MessageBox.Show("Add Item clicked");
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            // Add logic for editing item (stubbed for now)
            MessageBox.Show("Edit Item clicked");
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            // Add logic for deleting item (stubbed for now)
            MessageBox.Show("Delete Item clicked");
        }
    }
}
