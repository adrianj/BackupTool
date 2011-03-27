This program is a simple tool for automating a backup process.

usage:

BackupTool <source directory> <destination directory> [options]

Source and destination directories must be fully qualified.

Default operation (no options) is to make the destination directory equivalent to the source directory. In other words, any files NOT present in source will be deleted from destination.  This may or may not be what you want.

At present there are no options, but these are what I have in mind:
[-g --gui] Runs a GUI
[-l <filename>] Logs output to a file given in filename
[--donotdelete] Does not delete files in destination

I guess it's not really a great "backup" tool as I'm not archiving multiple copies or even providing the ability to restore, but it should at least make cloning our iTunes music easier.
