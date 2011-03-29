using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using AdriansLib;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BackupTool
{

    

    public partial class BackupGui : Form
    {
        private Program mProgram;
        private bool busy = false;

        public BackupGui(Program prog)
        {
            mProgram = prog;
            InitializeComponent();
            mProgram.ProgressEvent += new ProgressEventHandler(Program_ProgressEvent);
            verboseCheck.Checked = mProgram.Options.Verbose;
            sourceBox.Text = mProgram.Options.SourceDirectory;
            destBox.Text = mProgram.Options.DestinationDirectory;
            logBox.Text = mProgram.Options.LogFilename;
        }

        // thread safe events...
        public void Program_ProgressEvent(object sender, ProgressEventArgs e)
        {
            string text = e.PrintOutlinedMessage();
            if (e.Code != ProgressEventArgs.ProgressCode.InProgress) setDone("GO!");
        }

        delegate void setTextCallback(string text);
        delegate void jobDoneCallback(string text);

        private void setDone(string text)
        {
            if (goButton.InvokeRequired)
            {
                jobDoneCallback d = new jobDoneCallback(setDone);
                this.Invoke(d, new object[] { text });
                busy = false;
            }
            else
            {
                goButton.Text = text;
                busy = false;
            }
        }

        private void sourceButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
                string f = folderBrowserDialog.SelectedPath;
            if (f != null && f.Length > 0)
            {
                if (f[f.Length - 1] != Path.DirectorySeparatorChar) f += Path.DirectorySeparatorChar;
                try
                {
                    mProgram.Options.SourceDirectory = f;
                    sourceBox.Text = mProgram.Options.SourceDirectory;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //updateCommandLine();
            }
        }

        private void destButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();

            string f = folderBrowserDialog.SelectedPath;
            if (f != null && f.Length > 0)
            {
                if (f[f.Length - 1] != Path.DirectorySeparatorChar) f += Path.DirectorySeparatorChar;
                try
                {
                    mProgram.Options.DestinationDirectory = f;
                    destBox.Text = mProgram.Options.DestinationDirectory;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //updateCommandLine();
            }
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                //mProgram.Options.OpenLog(saveFileDialog.FileName);
                logBox.Text = saveFileDialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //updateCommandLine();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void doDeleteCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == doDeleteCheck && doDeleteCheck.Checked)
            {
                DialogResult r = MessageBox.Show("Are you sure?" + Environment.NewLine + "This option could potentially delete a lot of stuff in the destination directory", "Delete Destination Contents?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (r == DialogResult.No) doDeleteCheck.Checked = false;
            }
            updateCommandLine();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (!busy)
            {
                try
                {
                    string[] args = splitCommand(commandText.Text);
                    // Quick way to check source/dest are ok
                    mProgram.Options.SourceDirectory = args[0];
                    mProgram.Options.DestinationDirectory = args[1];
                    busy = true;
                    goButton.Text = "Cancel";
                    Program.Main(args);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (Program.ProgList[1] != null)
                {
                    Program.ProgList[1].Cancel = true;
                }
            }
        }

        private void updateCommandLine()
        {
            updateCommandLine(null, null);
        }
        private void updateCommandLine(object sender, EventArgs e)
        {
            string src = sourceBox.Text;
            if(sourceBox.Text.Contains(" ")) src = "\""+sourceBox.Text+"\"";
            string dst = destBox.Text;
            if (destBox.Text.Contains(" "))  dst = "\"" + destBox.Text + "\"";
            
            string s = src +" "+dst;

            if (logBox.Text != null && logBox.Text.Length > 0)
            {
                s += " --log ";
                if (logBox.Text.Contains(" ")) s += "\"" + logBox.Text + "\"";
                else s += logBox.Text;
            }
            if (verboseCheck.Checked) s += " --verbose";
            if (doDeleteCheck.Checked) s += " --deleteNonSourceFiles";
            commandText.Text = s;
        }

        private string [] splitCommand(string command)
        {
            string[] ss = command.Split(new char[] { ' ' });
            List<string> res = new List<string>();
            bool q = false;
            string qs = "";
            foreach (string s in ss)
            {
                if (s.Length < 1) continue;
                if (!q && s[0] == '"')
                {
                    if (s[s.Length - 1] == '"')
                    {
                        res.Add(s);
                    }
                    else
                    {
                        q = true;
                        qs = s;
                    }
                }
                else if (q && s[s.Length - 1] == '"')
                {
                    qs += " "+s;
                    res.Add(qs);
                    q = false;
                }
                else if (q) qs += " "+s;
                else res.Add(s);
            }
            return res.ToArray();
        }

    }
}
