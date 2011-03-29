This program is a simple tool for automating a backup process.

Usage:

BackupTool <source directory> <destination directory> [options]

Source and destination directories must be fully qualified.

Default operation (no options) is to copy the files with most recent date modified from source directory to destination directory.

Options:

[--gui] Runs a GUI. Source/Destination directories may be blank. Other options can be modified.
[--log <filename>] Logs output to a file given in filename.
[--verbose] Prints out info to the Console.
[--bidirectional]  Copies files in both directions, always preferring most recently modified.
[--deleteNonSourceFiles] Delete files in destination directory that are not present in source.  Ignored when --bidirectional flag is set.

I guess it's not really a great "backup" tool as I'm not archiving multiple copies or even providing the ability to restore, but it should at least make cloning our iTunes music easier.

Todo:

* Modify progress bar to provide more information
   -  Make it multithreaded
   -  Add a cancel feature
   -  Have it listen to ProgressEvents
   -  Provide an optional information box.
* Count files to be copied to have an indication of progress.
* Add bidirectional feature.