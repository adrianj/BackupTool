using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace BackupTool
{

    public delegate void CopyEventHandler(object sender, CopyEventArgs e);

    public class CopyEventArgs : EventArgs
    {
        public bool Finished { get; set; }
        public string Message { get; set; }
        public CopyEventArgs(string arg)
        {
            Message = arg;
            Finished = false;
        }
        public CopyEventArgs(string arg,bool finished)
        {
            Message = arg;
            Finished = finished;
        }
    }


    public class Program
    {
        public static Program[] ProgList = new Program[2];
        public static int ProgCount = 0;

        public bool Finished { get; set; }

        public OptionClass Options;
        public BackupGui gui {get;set;}
        public event CopyEventHandler CopyEvent;
        public void OnCopyEvent(CopyEventArgs e)
        {
            CopyEvent(null, e);
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
            try
            {
                if (Program.ProgCount == 0)
                {
                    ProgCount = 1;
                    Program prog = new Program(args);
                    ProgList[0] = prog;
                    prog.Show();
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
                        ProgList[1].CopyEvent += ProgList[0].gui.Program_CopyEvent;
                    }
                    Thread thread2 = new Thread(new ThreadStart(ProgList[1].DoCopy));
                    thread2.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("" + e);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        

        public Program(string [] args)
        {
            Options = new OptionClass(args);
            try
            {
                Options.ReadAllOptions(args);
            }
            catch (ArgumentNullException e)
            {
                throw;
                //Error(e);
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

        public void DoCopy()
        {
            Finished = false;

            WriteLine("Options:" + Environment.NewLine
                + "\tDeleteNonSourceFiles: " + Options.DeleteNonSourceFiles + Environment.NewLine
                + "\tSourceDirectory: " + Options.SourceDirectory + Environment.NewLine
                + "\tDestinationDirectory: " + Options.DestinationDirectory + Environment.NewLine
                + "\tPauseOnExit: " + Options.PauseOnExit + Environment.NewLine
                + "\tVerbose: " + Options.Verbose + Environment.NewLine
                + "\tLogFile: " + Options.Log + Environment.NewLine
                + "\tUseGui: " + Options.UseGui);
            try
            {
                if (Options.DestinationDirectory == null)
                {
                    throw new ArgumentException("Could not write to Destination: '" + Options.DestinationDirectory + "'");
                }
                if (Options.SourceDirectory == null)
                {
                    throw new ArgumentException("Could not read Source: '" + Options.SourceDirectory + "'");
                }
                CopyTree("", "");
            }
            catch (ArgumentNullException e)
            {
                throw;
                //Error(e);
            }
            Options.CloseLog();
            if (Options.Cancel)
                OnCopyEvent(new CopyEventArgs("CANCELLED!", true));
            else
                OnCopyEvent(new CopyEventArgs("DONE!", true));
            if (Options.PauseOnExit)
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

            Finished = true;
        }

        private bool checkForCancel()
        {
            if (Options.Cancel) return true;
            System.Threading.Thread.Sleep(0);
            return false;
        }

        public void CopyTree(string source, string destDirectory)
        {
            if (checkForCancel()) return;
            string sourceDir = Options.SourceDirectory + source;
            string targetDir = Options.DestinationDirectory + destDirectory;
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
                        WriteLine("Ignoring file '" + sourceFile+"'");
                    else
                    {
                        WriteLine("Copying file '" + sourceFile + "' to '" + targetFile + "'");
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
            OnCopyEvent(new CopyEventArgs(text));
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
        public bool Cancel { get; set; }
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
            else
                CheckWrite(trim(logfilename));
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
            }
        }

        public void ReadAllOptions(string[] args)
        {
            for(int i = 0; i < args.Length; i++)
            {
                string s = args[i];
                if (s.Equals("--deletenonsourcefiles",StringComparison.InvariantCultureIgnoreCase)) DeleteNonSourceFiles = true;
                if (!UseGui && s.Equals("--log", StringComparison.InvariantCultureIgnoreCase))
                    if (checkArgument(i + 1, args))
                        if (checkPath(args[i + 1], true, false)) OpenLog(args[i + 1]);
                        else throw new ArgumentException("Could not write to log file: " + args[i + 1]);
                    else throw new ArgumentException("Invalid log file syntax");
            }
            // If not using GUI, then source and destination (1st and 2nd args) must already be great.
            try
            {
                if (checkArgument(0, args)) SourceDirectory = args[0];
                if (checkArgument(1, args)) DestinationDirectory = args[1];
            }
            catch(ArgumentException e)
            {
                if(!UseGui) throw;
            }
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
            catch (IOException ioe)
            {
                return false;
            }
            return true;
        }
    }
}
