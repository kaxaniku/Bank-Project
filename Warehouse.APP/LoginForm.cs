using Warehouse.Repositories;
using Warehouse.Services;
using Warehouse.Services.Interfaces.Services;

namespace Warehouse.APP
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //IUserService userService = new UserService(null);
            //int id = userService.LoginUser(txtUsername.Text, txtPassword.Text);
            DialogResult = DialogResult.OK;
        }
    }
}
