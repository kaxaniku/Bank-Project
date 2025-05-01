using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse.APP.Interfaces;

namespace Warehouse.APP
{
    public partial class UserListForm: Form, IListForm
    {
        public UserListForm()
        {
            InitializeComponent();
        }

        public void Add()
        {
            UserDetailsForm form = new UserDetailsForm();
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
    }
}
