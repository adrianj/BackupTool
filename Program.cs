using System;
using System.Collections.Generic;
using AdriansLib;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace BackupTool
{

    public class Program
    {
        public static Program[] ProgList = new Program[2];
        public static int ProgCount = 0;

        private int mFileCount = 0;
        private int mFileTotal = 1;
        public OptionClass Options;
        public bool Cancel { get; set; }
        public BackupGui gui {get;set;}
        public event ProgressEventHandler ProgressEvent;
        private ProgressEventArgs mProgress = new ProgressEventArgs();

        public void OnProgressEvent(ProgressEventArgs e)
        {
            if(ProgressEvent != null)
                ProgressEvent(this, e);
        }

        public static string[] ForbiddenDestinations = new string[] {
            "c:\\"
        };

        public static string [] ForbiddenDestinationStart = new string[] {
            "c:\\Program Files\\",
            "c:\\Windows\\",
            "c:\\Program Files (x86)\\",
        };


        [STAThread]
        public static void Main(string[] args)
        {
            if (Program.ProgCount == 0)
            {
                ProgCount = 1;
                ProgList[0] = new Program(args);
                ProgList[0].Show();
                if (!ProgList[0].Options.UseGui)
                {
                    ProgList[0].DoCopy();
                }
            }
            else if (Program.ProgCount == 1)
            {
                ProgList[1] = new Program(args);
                if (ProgList[0].gui != null)
                {
                    ProgList[1].ProgressEvent += ProgList[0].gui.Program_ProgressEvent;
                }
                Thread thread2 = new Thread(new ThreadStart(ProgList[1].DoCopy));
                thread2.Start();
            }
            else
                Console.WriteLine("No more than 2 threads");
        }

        private string[] mArgs;

        public Program(string [] args)
        {
            mArgs = args;
            Options = new OptionClass(args);
            try
            {
                Options.ReadAllOptions(args);
            }
            catch (Exception e)
            {
                Exit(e);
            }
        }

        public void Exit(Exception e)
        {
            if (Options != null)
            {
                if (e == null)
                {
                    if (Cancel)
                    {
                        mProgress.MessageL3 = "CANCELLED!";
                        mProgress.Code = ProgressEventArgs.ProgressCode.Cancelled;
                        OnProgressEvent(mProgress);
                    }
                    else
                    {

                        mProgress.MessageL3 = "DONE!";
                        mProgress.Code = ProgressEventArgs.ProgressCode.Done;
                        OnProgressEvent(mProgress);
                    }
                }
                else
                {
                    if (!Options.UseGui)
                    {
                        WriteLine("" + e.GetType() + "" + e + Environment.NewLine);
                        Console.WriteLine(Properties.Resources.Usage);
                    }
                    mProgress.MessageL3 = ""+e+Environment.NewLine+"ERROR!";
                    mProgress.Code = ProgressEventArgs.ProgressCode.Error;
                    OnProgressEvent(mProgress);
                }

                Options.CloseLog();
            }
            if (ProgList[0] != null && ProgList[0].Options != null && !ProgList[0].Options.UseGui)
            {
                if(Options != null && Options.PauseOnExit)
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                // Should really switch the exception here.
                Environment.Exit(1);
            }
        }

        public void Show()
        {
            if (Options.UseGui)
            {
                gui = new BackupGui(this);
                Application.Run(gui);
            }
        }

        public void PrintArgs()
        {
            string s = "Options:" + Environment.NewLine;
            if (mArgs.Length > 1) s += "\tSourceDirectory: " + mArgs[0] + Environment.NewLine;
            else s += "\tSourceDirectory: NULL" + Environment.NewLine;
            if (mArgs.Length > 2) s += "\tDestinationDirectory: " + mArgs[1] + Environment.NewLine;
            else s += "\tDestinationDirectory: NULL" + Environment.NewLine;
            s += "\tLogFile: " + Options.LogFilename + Environment.NewLine
                + "\tPauseOnExit: " + Options.PauseOnExit + Environment.NewLine
                + "\tVerbose: " + Options.Verbose + Environment.NewLine
                + "\tDeleteNonSourceFiles: " + Options.DeleteNonSourceFiles + Environment.NewLine
                + "\tUseGui: " + Options.UseGui;
            WriteLine(s);
        }

        public void DoCopy()
        {
            PrintArgs();
            mProgress.Code = ProgressEventArgs.ProgressCode.InProgress;
            try
            {
                if (Options.SourceDirectory == null)
                {
                    string m = "Could not read from Source: '";
                    if (mArgs.Length > 0) m += mArgs[0];
                    throw new ArgumentException(m + "'");
                }
                if (Options.DestinationDirectory == null)
                {
                    string m = "Could not write to Destination: '";
                    if (mArgs.Length > 1) m += mArgs[1];
                    throw new ArgumentException(m + "'");
                }
                mProgress.MessageL2 = "Counting Files...";
                mFileCount = 0;
                mFileTotal = 0;
                CountFiles("");
                CopyTree("", "");
                Exit(null);
            }
            catch (ArgumentException e)
            {
                Exit(e);
            }
        }

        private bool checkForCancel()
        {
            if (Cancel) return true;
            System.Threading.Thread.Sleep(0);
            return false;
        }

        public void CountFiles(string source)
        {
            if (checkForCancel()) return;
            string sourceDir = Options.SourceDirectory + source;
            mProgress.MessageL3 = "Directory '" + sourceDir + "'";
            OnProgressEvent(mProgress);
            foreach(string s in Directory.GetDirectories(sourceDir))
            {
                CountFiles(source+Path.GetFileName(s) + Path.DirectorySeparatorChar);
            }
            foreach (string s in Directory.GetFiles(sourceDir))
            {
                mFileTotal++;
            }
        }

        public void CopyTree(string source, string destination)
        {
            if (checkForCancel()) return;
            string sourceDir = Options.SourceDirectory + source;
            string targetDir = Options.DestinationDirectory + destination;
            mProgress.MessageL2 = "Copying directory '" + sourceDir + "' to '" + targetDir;
            WriteLine("Copying directory '" + sourceDir + "' to '" + targetDir);
            if (sourceDir[sourceDir.Length - 1] == Path.DirectorySeparatorChar)
            {
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
                string[] sourceDirs = Directory.GetDirectories(sourceDir);
                // go through and delete directories that shouldnt be there.
                if (Options.DeleteNonSourceFiles)
                {
                    foreach (string tt in Directory.GetDirectories(targetDir))
                    {
                        string t = Path.GetFileName(tt);
                        bool delete = true;
                        foreach (string ss in sourceDirs)
                        {
                            string s = Path.GetFileName(ss);
                            if (s.Equals(t, StringComparison.InvariantCultureIgnoreCase)) delete = false;
                        }
                        if (delete)
                        {
                            DeleteTree(tt);
                        }
                    }
                }

                foreach (string sourceD in sourceDirs)
                {
                    string s = source + Path.GetFileName(sourceD) + Path.DirectorySeparatorChar;
                    CopyTree(s, s);
                }
                if (checkForCancel()) return;
                string[] sourceFiles = Directory.GetFiles(sourceDir);
                // First go through and delete files that shouldnt be there.
                if (Options.DeleteNonSourceFiles)
                {
                    foreach (string tt in Directory.GetFiles(targetDir))
                    {
                        string t = Path.GetFileName(tt);
                        bool delete = true;
                        foreach (string ss in sourceFiles)
                        {
                            string s = Path.GetFileName(ss);
                            if (s.Equals(t, StringComparison.InvariantCultureIgnoreCase)) delete = false;
                        }
                        if (delete)
                        {
                            mProgress.MessageL3 = "Deleting file '" + tt + "'";
                            OnProgressEvent(mProgress);
                            WriteLine("Deleting file '" + tt + "'");
                            File.Delete(tt);
                        }
                    }
                }
                foreach (string sourceFile in sourceFiles)
                {
                    if (checkForCancel()) return;
                    string targetFile = targetDir + Path.GetFileName(sourceFile);

                    FileInfo fs = new FileInfo(sourceFile);
                    FileInfo ft = new FileInfo(targetFile);
                    if (ft.Exists && ft.LastWriteTime.Ticks <= fs.LastWriteTime.Ticks)
                    {
                        string m = "("+(++mFileCount)+" / "+mFileTotal+") Ignoring file '" + sourceFile + "'";
                        WriteLine(m);
                        mProgress.MessageL3 = m;
                        OnProgressEvent(mProgress);
                    }
                    else
                    {
                        string m = "(" + (++mFileCount) + " / " + mFileTotal + ") Copying file '" + sourceFile + "' to '" + targetFile + "'";
                        WriteLine(m);
                        mProgress.MessageL3 = m;
                        OnProgressEvent(mProgress);
                        File.Copy(sourceFile, targetFile, true);
                    }
                }
            }
        }

        public void DeleteTree(string dirPath)
        {
            WriteLine("Deleting directory '" + dirPath+"'");
            foreach(string subdir in Directory.GetDirectories(dirPath))
            {
                DeleteTree(subdir);
            }
            foreach (string file in Directory.GetFiles(dirPath))
            {
                WriteLine("Deleting file '" + file + "'");
                File.Delete(file);
            }
            Directory.Delete(dirPath);
        }

        public void WriteLine(string text)
        {
            if (Options.Verbose)
                Console.WriteLine(text);
            if (!Options.UseGui && Options.Log != null)
            {
                Options.Log.WriteLine(text);
            }
        }

        public void Error(Exception e)
        {
            WriteLine("" + e);
        }
    }


    /// <summary>
    /// Processes command line arguments.
    /// </summary>
    public class OptionClass
    {
        //public bool Cancel { get; set; }
        public bool PauseOnExit { get; set; }
        public bool DeleteNonSourceFiles { get; set; }
        private string mSourceDirectory = null;
        public string SourceDirectory {
            get { return mSourceDirectory;}
            set
            {
                string val = trim(value);
                if (checkPath(val, false,true))
                {
                    if (value.Equals(DestinationDirectory))
                        throw new ArgumentException("Source and Destination directories cannot be the same");
                    mSourceDirectory = val;
                }
                else
                    throw new ArgumentException("Invalid Source Directory: " + val + ". Is it a fully qualified path?");
            }
        }
        private string mDestinationDirectory = null;
        public string DestinationDirectory
        {
            get { return mDestinationDirectory; }
            set
            {
                string val = trim(value);
                if (checkPath(val, true,true))
                {
                    if (val.Equals(SourceDirectory))
                        throw new ArgumentException("Source and Destination directories cannot be the same");
                    // Check that destination is not forbidden
                    foreach (string f in Program.ForbiddenDestinations)
                    {
                        if (f.Equals(val, StringComparison.InvariantCultureIgnoreCase))
                            throw new ArgumentException("Writing to "+f+" is forbidden and crazy. Choose another destination.");
                    }
                    foreach (string f in Program.ForbiddenDestinationStart)
                    {
                        if (val.Length >= f.Length)
                        {
                            if (f.Equals(val.Substring(0, f.Length), StringComparison.InvariantCultureIgnoreCase))
                                throw new ArgumentException("Writing to " + val + " is forbidden and crazy. Choose another destination.");
                        }
                    }
                    mDestinationDirectory = value;
                }
                else
                    throw new ArgumentException("Invalid Destination Directory: "+val+". Is it a fully qualified path?");
            }
        }
        
        public bool UseGui { get; set; }

        private string mLogFile = null;
        public string LogFilename { get { return mLogFile; }  }
        private StreamWriter mLog = null;
        public StreamWriter Log { get { return mLog; }  }
        public void OpenLog(string logfilename)
        {
            if (!UseGui)
                mLog = new StreamWriter(trim(logfilename));
            else if(CheckWrite(trim(logfilename)))
                throw new ArgumentException("Invalid Log file: " + logfilename + ".");
            mLogFile = logfilename;
        }
        public void CloseLog()
        {
            if (mLog != null)
            {
                mLog.Flush();
                mLog.Close();
                mLog.Dispose();
                mLog = null;
                mLogFile = null;
                Thread.Sleep(10);
            }
        }

        public bool Verbose { get; set; }

        private string trim(string textWithQuotes)
        {
            if (textWithQuotes.Length < 2) return textWithQuotes;
            string s = textWithQuotes;
            if (s[0] == '"') s = s.Substring(1);
            if (s[s.Length - 1] == '"') s = s.Substring(0, s.Length - 1);
            return s;
        }

        /// <summary>
        /// Creates the class from a set of command line arguments.
        /// First pass through only reads Verbose and PauseOnExit options
        /// </summary>
        /// <param name="args"></param>
        public OptionClass(string[] args)
        {
            // first check for pauseonexit - useful for debugging. Exceptions get caught and displayed.
            foreach (string s in args)
            {
                if (s.Equals("--pauseonexit", StringComparison.InvariantCultureIgnoreCase)) PauseOnExit = true;
                if (s.Equals("--verbose", StringComparison.InvariantCultureIgnoreCase)) Verbose = true;
                if (s.Equals("--gui", StringComparison.InvariantCultureIgnoreCase)) UseGui = true;
                if (s.Equals("--deletenonsourcefiles", StringComparison.InvariantCultureIgnoreCase)) DeleteNonSourceFiles = true;
            }
        }

        public void ReadAllOptions(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string s = args[i];
                if (!UseGui && s.Equals("--log", StringComparison.InvariantCultureIgnoreCase))
                    if (checkArgument(i + 1, args))
                        if (checkPath(trim(args[i + 1]), true, false)) OpenLog(trim(args[i + 1]));
                        else throw new ArgumentException("Could not write to log file: " + trim(args[i + 1]));
                    else throw new ArgumentException("Invalid log file syntax");
            }
            if (checkArgument(0, args)) SourceDirectory = trim(args[0]);
            if (checkArgument(1, args)) DestinationDirectory = trim(args[1]);
        }

        /// <summary>
        /// Checks if an argument of length > 0 exists at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool checkArgument(int index, string[] args)
        {
            if (args.Length > index && args[index].Length > 0) return true;
            return false;
        }
        
        public bool checkPath(string path, bool checkWrite, bool checkDir)
        {
            if (checkDir)
            {
                string dir = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar;
                // directory of length 0 is not acceptable.
                if (dir.Length > 0 && !dir.Equals(path)) return false;
            }
            if (checkWrite && !CheckWrite(path)) return false;
            return true;
        }



        public bool CheckWrite(string path)
        {
            try
            {
                // if its a directory then treat it differently.
                if (path[path.Length - 1] == Path.DirectorySeparatorChar)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        Directory.Delete(path);
                    }
                }
                else
                {
                    FileStream stream = new FileStream(path, FileMode.Create);
                    stream.Close();
                    FileInfo inf = new FileInfo(path);
                    if (inf.Length == 0) File.Delete(path);
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
    }
}
