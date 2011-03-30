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
        //private bool busy = false;

        public BackupGui(Program prog)
        {
            mProgram = prog;
            InitializeComponent();
            verboseCheck.Checked = mProgram.Options.Verbose;
            sourceBox.Text = mProgram.Options.SourceDirectory;
            destBox.Text = mProgram.Options.DestinationDirectory;
            logBox.Text = mProgram.Options.LogFilename;
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
                    sourceBox.Text = mProgram.Options.SourceDirectory; // will call updateCommandLine()
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                    destBox.Text = mProgram.Options.DestinationDirectory; // will call updateCommandLine()
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                logBox.Text = saveFileDialog.FileName; // will call updateCommandLine()
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
            {
                string[] args = splitCommand(commandText.Text);
                Program prog = new Program(args);
                progressBarControl1.StartWorker(prog.DoCopy);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateCommandLine() { updateCommandLine(null, null); }

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
                string log = logBox.Text;
                if (log.Length < 5 || !log.Substring(log.Length - 4).Equals(".txt")) log += ".txt";
                if (log.Contains(" ")) log = "\"" + log + "\"";
                s += log;
            }
            if (verboseCheck.Checked) s += " --verbose";
            if (doDeleteCheck.Checked) s += " --deleteNonSourceFiles";
            commandText.Text = s;
        }

        /// <summary>
        /// Splits a string into command line arguments, handling "string with spaces and quotes" appropriately.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
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
