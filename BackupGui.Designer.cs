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
            this.goButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.logButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.TextBox();
            this.verboseCheck = new System.Windows.Forms.CheckBox();
            this.outputText = new System.Windows.Forms.TextBox();
            this.commandText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            // 
            // destBox
            // 
            this.destBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.destBox.Location = new System.Drawing.Point(110, 39);
            this.destBox.Name = "destBox";
            this.destBox.Size = new System.Drawing.Size(309, 20);
            this.destBox.TabIndex = 1;
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
            // goButton
            // 
            this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.goButton.Location = new System.Drawing.Point(10, 159);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(445, 23);
            this.goButton.TabIndex = 7;
            this.goButton.Text = "GO!";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
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
            // outputText
            // 
            this.outputText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.outputText.Location = new System.Drawing.Point(10, 188);
            this.outputText.Multiline = true;
            this.outputText.Name = "outputText";
            this.outputText.ReadOnly = true;
            this.outputText.Size = new System.Drawing.Size(445, 115);
            this.outputText.TabIndex = 12;
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
            // BackupGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 315);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.commandText);
            this.Controls.Add(this.outputText);
            this.Controls.Add(this.verboseCheck);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.goButton);
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
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.CheckBox verboseCheck;
        private System.Windows.Forms.TextBox outputText;
        private System.Windows.Forms.TextBox commandText;
        private System.Windows.Forms.Label label4;
    }
}