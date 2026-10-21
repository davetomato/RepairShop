using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FixFlow.Models;
using FixFlow.Services;

namespace FixFlow.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Session.IsLoggedIn)
            {
                MessageBox.Show("No active session found. Please log in.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            lblUserSession.Text = $"Logged in as: {Session.CurrentUser.Username} ({Session.CurrentUser.Role})";

            pnlSidebar.Controls.Clear();

            IList<string> allowedMenus = Session.CurrentUser.GetAllowedMenus();

            int topPosition = 20;
            foreach (string menuName in allowedMenus)
            {
                Button btn = new Button();
                btn.Text = menuName;
                btn.Width = pnlSidebar.Width - 20; 
                btn.Height = 40;
                btn.Left = 10;
                btn.Top = topPosition;
                btn.UseVisualStyleBackColor = true;
                btn.Tag = menuName; 
                btn.Click += MenuButton_Click;

                pnlSidebar.Controls.Add(btn);
                topPosition += 50; 
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string menu = btn.Tag.ToString();

            Form targetForm = null;
            switch (menu)
            {
                case "Repair Board":
                    targetForm = new RepairBoardForm();
                    break;
                case "Customers":
                    targetForm = new CustomersForm();
                    break;
                case "Devices":
                    targetForm = new DevicesForm();
                    break;
                case "Parts":
                    targetForm = new PartsForm();
                    break;
                case "Payments":
                    targetForm = new PaymentsForm();
                    break;
                case "Reports":
                    targetForm = new ReportsForm();
                    break;
                case "Users":
                    targetForm = new UsersForm();
                    break;
                case "Service Rates":
                    targetForm = new ServiceRatesForm();
                    break;
            }

            if (targetForm != null)
            {
                pnlContainer.Controls.Clear();
                targetForm.TopLevel = false;
                targetForm.FormBorderStyle = FormBorderStyle.None;
                targetForm.Dock = DockStyle.Fill;
                pnlContainer.Controls.Add(targetForm);
                targetForm.Show();
            }
            else
            {
                MessageBox.Show($"The form for '{menu}' is not yet linked.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}