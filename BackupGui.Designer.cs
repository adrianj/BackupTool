namespace BackupTool
{
    partial class BackupGui
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
            this.sourceBox = new System.Windows.Forms.TextBox();
            this.destBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sourceButton = new System.Windows.Forms.Button();
            this.destButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.doDeleteCheck = new System.Windows.Forms.CheckBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.logButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.TextBox();
            this.verboseCheck = new System.Windows.Forms.CheckBox();
            this.commandText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBarControl1 = new AdriansLib.ProgressBarControl();
            this.SuspendLayout();
            // 
            // sourceBox
            // 
            this.sourceBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceBox.Location = new System.Drawing.Point(110, 12);
            this.sourceBox.Name = "sourceBox";
            this.sourceBox.Size = new System.Drawing.Size(309, 20);
            this.sourceBox.TabIndex = 0;
            this.sourceBox.TextChanged += new System.EventHandler(this.updateCommandLine);
            // 
            // destBox
            // 
            this.destBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.destBox.Location = new System.Drawing.Point(110, 39);
            this.destBox.Name = "destBox";
            this.destBox.Size = new System.Drawing.Size(309, 20);
            this.destBox.TabIndex = 1;
            this.destBox.TextChanged += new System.EventHandler(this.updateCommandLine);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination Folder";
            // 
            // sourceButton
            // 
            this.sourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceButton.Location = new System.Drawing.Point(425, 12);
            this.sourceButton.Name = "sourceButton";
            this.sourceButton.Size = new System.Drawing.Size(30, 20);
            this.sourceButton.TabIndex = 4;
            this.sourceButton.Text = "...";
            this.sourceButton.UseVisualStyleBackColor = true;
            this.sourceButton.Click += new System.EventHandler(this.sourceButton_Click);
            // 
            // destButton
            // 
            this.destButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.destButton.Location = new System.Drawing.Point(425, 39);
            this.destButton.Name = "destButton";
            this.destButton.Size = new System.Drawing.Size(30, 20);
            this.destButton.TabIndex = 5;
            this.destButton.Text = "...";
            this.destButton.UseVisualStyleBackColor = true;
            this.destButton.Click += new System.EventHandler(this.destButton_Click);
            // 
            // doDeleteCheck
            // 
            this.doDeleteCheck.AutoSize = true;
            this.doDeleteCheck.Location = new System.Drawing.Point(10, 91);
            this.doDeleteCheck.Name = "doDeleteCheck";
            this.doDeleteCheck.Size = new System.Drawing.Size(212, 17);
            this.doDeleteCheck.TabIndex = 6;
            this.doDeleteCheck.Text = "Delete Destination Files Not In Source?";
            this.doDeleteCheck.UseVisualStyleBackColor = true;
            this.doDeleteCheck.CheckedChanged += new System.EventHandler(this.doDeleteCheck_CheckedChanged);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // logButton
            // 
            this.logButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logButton.Location = new System.Drawing.Point(425, 65);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(30, 20);
            this.logButton.TabIndex = 10;
            this.logButton.Text = "...";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Log File";
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Location = new System.Drawing.Point(110, 65);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(309, 20);
            this.logBox.TabIndex = 8;
            this.logBox.TextChanged += new System.EventHandler(this.updateCommandLine);
            // 
            // verboseCheck
            // 
            this.verboseCheck.AutoSize = true;
            this.verboseCheck.Location = new System.Drawing.Point(10, 109);
            this.verboseCheck.Name = "verboseCheck";
            this.verboseCheck.Size = new System.Drawing.Size(65, 17);
            this.verboseCheck.TabIndex = 11;
            this.verboseCheck.Text = "Verbose";
            this.verboseCheck.UseVisualStyleBackColor = true;
            this.verboseCheck.CheckedChanged += new System.EventHandler(this.doDeleteCheck_CheckedChanged);
            // 
            // commandText
            // 
            this.commandText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.commandText.Location = new System.Drawing.Point(134, 133);
            this.commandText.Name = "commandText";
            this.commandText.Size = new System.Drawing.Size(321, 20);
            this.commandText.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Command Line Options";
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.progressBarControl1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.progressBarControl1.EnableTextBox = true;
            this.progressBarControl1.Location = new System.Drawing.Point(10, 159);
            this.progressBarControl1.Maximum = 100;
            this.progressBarControl1.Minimum = 0;
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Result = null;
            this.progressBarControl1.Size = new System.Drawing.Size(445, 150);
            this.progressBarControl1.TabIndex = 15;
            this.progressBarControl1.Value = 0;
            this.progressBarControl1.ButtonClick += new System.EventHandler(this.goButton_Click);
            // 
            // BackupGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 307);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.commandText);
            this.Controls.Add(this.verboseCheck);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.doDeleteCheck);
            this.Controls.Add(this.destButton);
            this.Controls.Add(this.sourceButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.destBox);
            this.Controls.Add(this.sourceBox);
            this.Name = "BackupGui";
            this.Text = "Backup Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourceBox;
        private System.Windows.Forms.TextBox destBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sourceButton;
        private System.Windows.Forms.Button destButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox doDeleteCheck;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.CheckBox verboseCheck;
        private System.Windows.Forms.TextBox commandText;
        private System.Windows.Forms.Label label4;
        private AdriansLib.ProgressBarControl progressBarControl1;
    }
}