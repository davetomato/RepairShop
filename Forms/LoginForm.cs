using FixFlow.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FixFlow.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AuthService authService = new AuthService();
            var user = authService.Login(txtUsername.Text, txtPassword.Text);

            if (user != null)  
            {
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                lblError.Text = "Invalid username or password.";
                lblError.ForeColor = Color.Red;
            }
        }

    }
}
