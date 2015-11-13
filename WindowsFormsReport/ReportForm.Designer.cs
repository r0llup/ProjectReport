namespace WindowsFormsReport
{
    partial class ReportForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.label8 = new System.Windows.Forms.Label();
            this.reportComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.addTitleTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addReportButton = new System.Windows.Forms.Button();
            this.typeTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.reportCheckBox = new System.Windows.Forms.CheckBox();
            this.deleteReportButton = new System.Windows.Forms.Button();
            this.deleteReportComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.modifyTitleTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.newTypeTextBox = new System.Windows.Forms.TextBox();
            this.newNameTextBox = new System.Windows.Forms.TextBox();
            this.oldNameTextBox = new System.Windows.Forms.TextBox();
            this.modifyReportButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.webBrowser);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 561);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Viewer";
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(3, 16);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(541, 542);
            this.webBrowser.TabIndex = 2;
            this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Choose a Report to view:";
            // 
            // reportComboBox
            // 
            this.reportComboBox.FormattingEnabled = true;
            this.reportComboBox.Location = new System.Drawing.Point(139, 16);
            this.reportComboBox.Name = "reportComboBox";
            this.reportComboBox.Size = new System.Drawing.Size(74, 21);
            this.reportComboBox.TabIndex = 1;
            this.reportComboBox.SelectedIndexChanged += new System.EventHandler(this.reportComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.addTitleTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.addReportButton);
            this.groupBox2.Controls.Add(this.typeTextBox);
            this.groupBox2.Controls.Add(this.nameTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 125);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add a Report";
            // 
            // addTitleTextBox
            // 
            this.addTitleTextBox.Location = new System.Drawing.Point(49, 17);
            this.addTitleTextBox.Name = "addTitleTextBox";
            this.addTitleTextBox.Size = new System.Drawing.Size(155, 20);
            this.addTitleTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Title:";
            // 
            // addReportButton
            // 
            this.addReportButton.Location = new System.Drawing.Point(67, 94);
            this.addReportButton.Name = "addReportButton";
            this.addReportButton.Size = new System.Drawing.Size(75, 23);
            this.addReportButton.TabIndex = 6;
            this.addReportButton.Text = "Add";
            this.addReportButton.UseVisualStyleBackColor = true;
            this.addReportButton.Click += new System.EventHandler(this.addReportButton_Click);
            // 
            // typeTextBox
            // 
            this.typeTextBox.Location = new System.Drawing.Point(49, 68);
            this.typeTextBox.Name = "typeTextBox";
            this.typeTextBox.Size = new System.Drawing.Size(155, 20);
            this.typeTextBox.TabIndex = 5;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(49, 43);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(155, 20);
            this.nameTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.reportCheckBox);
            this.groupBox3.Controls.Add(this.deleteReportButton);
            this.groupBox3.Controls.Add(this.deleteReportComboBox);
            this.groupBox3.Location = new System.Drawing.Point(6, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(219, 72);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Delete a Report";
            // 
            // reportCheckBox
            // 
            this.reportCheckBox.AutoSize = true;
            this.reportCheckBox.Location = new System.Drawing.Point(9, 48);
            this.reportCheckBox.Name = "reportCheckBox";
            this.reportCheckBox.Size = new System.Drawing.Size(113, 17);
            this.reportCheckBox.TabIndex = 8;
            this.reportCheckBox.Text = "Delete all Reports.";
            this.reportCheckBox.UseVisualStyleBackColor = true;
            // 
            // deleteReportButton
            // 
            this.deleteReportButton.Location = new System.Drawing.Point(138, 18);
            this.deleteReportButton.Name = "deleteReportButton";
            this.deleteReportButton.Size = new System.Drawing.Size(75, 23);
            this.deleteReportButton.TabIndex = 9;
            this.deleteReportButton.Text = "Delete";
            this.deleteReportButton.UseVisualStyleBackColor = true;
            this.deleteReportButton.Click += new System.EventHandler(this.deleteReportButton_Click);
            // 
            // deleteReportComboBox
            // 
            this.deleteReportComboBox.FormattingEnabled = true;
            this.deleteReportComboBox.Location = new System.Drawing.Point(9, 20);
            this.deleteReportComboBox.Name = "deleteReportComboBox";
            this.deleteReportComboBox.Size = new System.Drawing.Size(123, 21);
            this.deleteReportComboBox.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.modifyTitleTextBox);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.newTypeTextBox);
            this.groupBox4.Controls.Add(this.newNameTextBox);
            this.groupBox4.Controls.Add(this.oldNameTextBox);
            this.groupBox4.Controls.Add(this.modifyReportButton);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(6, 221);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(219, 152);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Modify a Report";
            // 
            // modifyTitleTextBox
            // 
            this.modifyTitleTextBox.Location = new System.Drawing.Point(69, 17);
            this.modifyTitleTextBox.Name = "modifyTitleTextBox";
            this.modifyTitleTextBox.Size = new System.Drawing.Size(135, 20);
            this.modifyTitleTextBox.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Title:";
            // 
            // newTypeTextBox
            // 
            this.newTypeTextBox.Location = new System.Drawing.Point(69, 93);
            this.newTypeTextBox.Name = "newTypeTextBox";
            this.newTypeTextBox.Size = new System.Drawing.Size(135, 20);
            this.newTypeTextBox.TabIndex = 13;
            // 
            // newNameTextBox
            // 
            this.newNameTextBox.Location = new System.Drawing.Point(69, 67);
            this.newNameTextBox.Name = "newNameTextBox";
            this.newNameTextBox.Size = new System.Drawing.Size(135, 20);
            this.newNameTextBox.TabIndex = 12;
            // 
            // oldNameTextBox
            // 
            this.oldNameTextBox.Location = new System.Drawing.Point(69, 41);
            this.oldNameTextBox.Name = "oldNameTextBox";
            this.oldNameTextBox.Size = new System.Drawing.Size(135, 20);
            this.oldNameTextBox.TabIndex = 11;
            // 
            // modifyReportButton
            // 
            this.modifyReportButton.Location = new System.Drawing.Point(69, 119);
            this.modifyReportButton.Name = "modifyReportButton";
            this.modifyReportButton.Size = new System.Drawing.Size(75, 23);
            this.modifyReportButton.TabIndex = 13;
            this.modifyReportButton.Text = "Modify";
            this.modifyReportButton.UseVisualStyleBackColor = true;
            this.modifyReportButton.Click += new System.EventHandler(this.modifyButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "New type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "New name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Old name:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox5.Location = new System.Drawing.Point(553, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(231, 561);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.reportComboBox);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Location = new System.Drawing.Point(6, 379);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(219, 49);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "View a Report";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReportForm";
            this.Text = "WindowsFormsReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportForm_FormClosing);
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox typeTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addReportButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button deleteReportButton;
        private System.Windows.Forms.ComboBox deleteReportComboBox;
        private System.Windows.Forms.CheckBox reportCheckBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button modifyReportButton;
        private System.Windows.Forms.TextBox newTypeTextBox;
        private System.Windows.Forms.TextBox newNameTextBox;
        private System.Windows.Forms.TextBox oldNameTextBox;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox addTitleTextBox;
        private System.Windows.Forms.TextBox modifyTitleTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox reportComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;


    }
}

