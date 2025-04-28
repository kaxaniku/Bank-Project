using System.Windows.Forms.VisualStyles;
using Warehouse.Factories;
using Warehouse.Services.Exceptions;
using Warehouse.Services.Interfaces.Services;

namespace Warehouse.APP;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
#if DEBUG
        txtUsername.Text = "admin";
        txtPassword.Text = "admin123";
#endif
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        if (ValidateInputs()) return;
        
        IUserService userService = UserServiceFactory.Create();
        Executor.Execute<LoginException>(() =>
        {
            LocalStorage.UserId = userService.LoginUser(txtUsername.Text, txtPassword.Text);
            LocalStorage.Username = txtUsername.Text;
            DialogResult = DialogResult.OK;
        }, ex => this.ShowWarning(ex.Message));
    }

    private bool ValidateInputs()
    {
        if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
        {
            this.ShowInfo("Please enter username and password.");
            return true;
        }

        return false;
    }
}