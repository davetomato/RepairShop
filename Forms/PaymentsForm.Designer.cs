namespace FixFlow.Forms
{
    partial class PaymentsForm
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
            grpPayment = new GroupBox();
            label1 = new Label();
            txtOrderId = new TextBox();
            label2 = new Label();
            numAmount = new NumericUpDown();
            btnRecordPayment = new Button();
            grpPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
            SuspendLayout();
            // 
            // grpPayment
            // 
            grpPayment.Controls.Add(btnRecordPayment);
            grpPayment.Controls.Add(numAmount);
            grpPayment.Controls.Add(label2);
            grpPayment.Controls.Add(txtOrderId);
            grpPayment.Controls.Add(label1);
            grpPayment.Dock = DockStyle.Fill;
            grpPayment.Location = new Point(0, 0);
            grpPayment.Name = "grpPayment";
            grpPayment.Size = new Size(800, 450);
            grpPayment.TabIndex = 0;
            grpPayment.TabStop = false;
            grpPayment.Text = "Record Repair Payment";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(108, 90);
            label1.Name = "label1";
            label1.Size = new Size(113, 20);
            label1.TabIndex = 0;
            label1.Text = "Repair Order ID";
            // 
            // txtOrderId
            // 
            txtOrderId.Location = new Point(268, 87);
            txtOrderId.Name = "txtOrderId";
            txtOrderId.Size = new Size(125, 27);
            txtOrderId.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(108, 159);
            label2.Name = "label2";
            label2.Size = new Size(144, 20);
            label2.TabIndex = 2;
            label2.Text = "Payment Amount (P)";
            // 
            // numAmount
            // 
            numAmount.DecimalPlaces = 2;
            numAmount.Location = new Point(268, 157);
            numAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(150, 27);
            numAmount.TabIndex = 3;
            // 
            // btnRecordPayment
            // 
            btnRecordPayment.Location = new Point(173, 236);
            btnRecordPayment.Name = "btnRecordPayment";
            btnRecordPayment.Size = new Size(176, 29);
            btnRecordPayment.TabIndex = 4;
            btnRecordPayment.Text = "Record Payment";
            btnRecordPayment.UseVisualStyleBackColor = true;
            btnRecordPayment.Click += this.btnRecordPayment_Click;
            // 
            // PaymentsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(grpPayment);
            Name = "PaymentsForm";
            Text = "Payment Form";
            grpPayment.ResumeLayout(false);
            grpPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpPayment;
        private Button btnRecordPayment;
        private NumericUpDown numAmount;
        private Label label2;
        private TextBox txtOrderId;
        private Label label1;
    }
}