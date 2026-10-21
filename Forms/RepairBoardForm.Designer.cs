namespace FixFlow.Forms
{
    partial class RepairBoardForm
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
            dgvOrders = new DataGridView();
            btnOpenOrder = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            // 
            // dgvOrders
            // 
            dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrders.Dock = DockStyle.Left;
            dgvOrders.Location = new Point(0, 0);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.RowHeadersWidth = 51;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new Size(300, 450);
            dgvOrders.TabIndex = 0;
            dgvOrders.CellDoubleClick += this.dgvOrders_CellDoubleClick;
            // 
            // btnOpenOrder
            // 
            btnOpenOrder.Location = new Point(451, 396);
            btnOpenOrder.Name = "btnOpenOrder";
            btnOpenOrder.Size = new Size(191, 29);
            btnOpenOrder.TabIndex = 1;
            btnOpenOrder.Text = "Open Selected Repair";
            btnOpenOrder.UseVisualStyleBackColor = true;
            btnOpenOrder.Click += this.btnOpenOrder_Click;
            // 
            // RepairBoardForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOpenOrder);
            Controls.Add(dgvOrders);
            Name = "RepairBoardForm";
            Text = "Repair Board Form";
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
        }

        

        #endregion

        private DataGridView dgvOrders;
        private Button btnOpenOrder;
    }
}