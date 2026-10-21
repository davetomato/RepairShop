using System;
using System.Windows.Forms;
using FixFlow.Services;

namespace FixFlow.Forms
{
    public partial class ReportsForm : Form
    {
        private readonly ReportService _reportService = new ReportService();

        public ReportsForm()
        {
            InitializeComponent();
            InitializeReportTypes();
        }

        private void InitializeReportTypes()
        {
            cmbReportType.Items.AddRange(new string[] {
                "Repairs Per Status",
                "Income Per Month",
                "Parts Usage",
                "Technician Workload",
                "Unpaid Balances"
            });
            cmbReportType.SelectedIndex = 0;
        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedReport = cmbReportType.SelectedItem.ToString();
                switch (selectedReport)
                {
                    case "Repairs Per Status":
                        dgvReport.DataSource = _reportService.RepairsPerStatus();
                        break;
                    case "Income Per Month":
                        dgvReport.DataSource = _reportService.IncomePerMonth();
                        break;
                    case "Parts Usage":
                        dgvReport.DataSource = _reportService.PartsUsage();
                        break;
                    case "Technician Workload":
                        dgvReport.DataSource = _reportService.TechnicianWorkload();
                        break;
                    case "Unpaid Balances":
                        dgvReport.DataSource = _reportService.UnpaidBalances();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Access Denied / Report Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}