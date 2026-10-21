namespace FixFlow.Forms
{
    partial class ServiceRatesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvRates = new DataGridView();
            groupBox1 = new GroupBox();
            label1 = new Label();
            txtServiceName = new TextBox();
            label2 = new Label();
            numRate = new NumericUpDown();
            btnSaveRate = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRates).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRate).BeginInit();
            SuspendLayout();
            // 
            // dgvRates
            // 
            dgvRates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRates.Dock = DockStyle.Left;
            dgvRates.Location = new Point(0, 0);
            dgvRates.Name = "dgvRates";
            dgvRates.RowHeadersWidth = 51;
            dgvRates.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRates.Size = new Size(300, 450);
            dgvRates.TabIndex = 0;
            dgvRates.CellClick += this.dgvRates_CellClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnSaveRate);
            groupBox1.Controls.Add(numRate);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtServiceName);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(300, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(500, 450);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Edit Service Rate";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 62);
            label1.Name = "label1";
            label1.Size = new Size(100, 20);
            label1.TabIndex = 0;
            label1.Text = "Service Name";
            // 
            // txtServiceName
            // 
            txtServiceName.Location = new Point(228, 63);
            txtServiceName.Name = "txtServiceName";
            txtServiceName.ReadOnly = true;
            txtServiceName.Size = new Size(125, 27);
            txtServiceName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 115);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 2;
            label2.Text = "New Rate";
            // 
            // numRate
            // 
            numRate.DecimalPlaces = 2;
            numRate.Location = new Point(219, 113);
            numRate.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numRate.Name = "numRate";
            numRate.Size = new Size(150, 27);
            numRate.TabIndex = 3;
            // 
            // btnSaveRate
            // 
            btnSaveRate.Location = new Point(150, 176);
            btnSaveRate.Name = "btnSaveRate";
            btnSaveRate.Size = new Size(94, 29);
            btnSaveRate.TabIndex = 4;
            btnSaveRate.Text = "Save Rate";
            btnSaveRate.UseVisualStyleBackColor = true;
            btnSaveRate.Click += this.btnSaveRate_Click;
            // 
            // ServiceRatesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(dgvRates);
            Name = "ServiceRatesForm";
            Text = "Service Rates Form";
            ((System.ComponentModel.ISupportInitialize)dgvRates).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRate).EndInit();
            ResumeLayout(false);
        }

       

        #endregion

        private DataGridView dgvRates;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtServiceName;
        private Button btnSaveRate;
        private NumericUpDown numRate;
        private Label label2;
    }
}