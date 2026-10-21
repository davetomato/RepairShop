namespace FixFlow.Forms
{
    partial class PartsForm
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
            dgvParts = new DataGridView();
            txtName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            numPrice = new NumericUpDown();
            label3 = new Label();
            numStock = new NumericUpDown();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvParts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
            SuspendLayout();
            // 
            // dgvParts
            // 
            dgvParts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvParts.Dock = DockStyle.Left;
            dgvParts.Location = new Point(0, 0);
            dgvParts.MultiSelect = false;
            dgvParts.Name = "dgvParts";
            dgvParts.RowHeadersWidth = 51;
            dgvParts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParts.Size = new Size(300, 450);
            dgvParts.TabIndex = 0;
            dgvParts.CellClick += dgvParts_CellClick;
            // 
            // txtName
            // 
            txtName.Location = new Point(472, 69);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(497, 31);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 2;
            label1.Text = "Part Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(434, 144);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 3;
            label2.Text = "Price";
            // 
            // numPrice
            // 
            numPrice.DecimalPlaces = 2;
            numPrice.Location = new Point(492, 137);
            numPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(150, 27);
            numPrice.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(434, 203);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.TabIndex = 5;
            label3.Text = "Stock";
            // 
            // numStock
            // 
            numStock.Location = new Point(492, 196);
            numStock.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numStock.Name = "numStock";
            numStock.Size = new Size(150, 27);
            numStock.TabIndex = 6;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(476, 262);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add Part";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(463, 321);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(121, 34);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Update Part";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(476, 387);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete Part";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // PartsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(833, 450);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(numStock);
            Controls.Add(label3);
            Controls.Add(numPrice);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtName);
            Controls.Add(dgvParts);
            Name = "PartsForm";
            Text = "PartsForm";
            ((System.ComponentModel.ISupportInitialize)dgvParts).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvParts;
        private TextBox txtName;
        private Label label1;
        private Label label2;
        private NumericUpDown numPrice;
        private Label label3;
        private NumericUpDown numStock;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}