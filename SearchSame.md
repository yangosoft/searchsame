# Introduction #

SearchSame is an utility to search duplicated files across directories. You can specify multiple directories to search in.

# Installation #
## Windows ##
Ensure you have [Microsoft .NET Framework 3.5](http://www.microsoft.com/downloads/details.aspx?FamilyID=333325FD-AE52-4E35-B531-508D977D32A6) then put the downloaded executable in any place.

Run a console _Start -> Execute -> cmd.exe_ and then execute the program.

## Linux ##
Download Mono _sudo aptitude install mono_

# Command Line Options #

searchsame.exe [-e] directories

  * directory refers to full path of the directory: Example "C:\windows" or "/usr/src"
  * you can put multiple directories: _searchsame.exe "C:\windows" "C:\dir2" "C:\dir3"_
  * -e if you put this flag searchsame will do a md5 hash of each file of each directory. This will be very slow because will compare 1 to 1 each file.

# Output #

The output of the program is
` OriginalFileName = DuplicatedFileName = md5Hash `

  * OriginalFileName: is the first ocurrence of the duplicated file.
  * DuplicatedFileName: is the duplicated file found.
  * md5Hash: md5 hash of the file content.
  * = is a token to separate each column.

If -e flag is present the program will output all the md5 hashes of each file processed in a list like:
```
md5Hash FileName1
md5Hash FileName2
md5Hash FileName3
```