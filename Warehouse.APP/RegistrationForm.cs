using System;
using System.Windows.Forms;
using Warehouse.APP;

namespace WarehouseApp
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent(); // Call defined in Designer.cs
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Registration successful!");
            StorageForm storageForm = new StorageForm();
            storageForm.Show();
            this.Hide();
        }
    }
}
