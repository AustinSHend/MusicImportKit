﻿using Microsoft.Office.Interop.Excel;
using Microsoft.WindowsAPICodePack.Dialogs;
using MusicImportKit.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace MusicImportKit
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            // Initialize user settings at program load
            ApplyUserSettings();
        }

        // Applies user settings and dynamically enables features
        private void ApplyUserSettings()
        {
            if (Settings.Default.DefaultInput != "")
            {
                InputPathBox.Text = Settings.Default.DefaultInput;
                InputPathBox.ForeColor = Color.Black;
            }
            else
            {
                InputPathBox.Text = "Input Folder";
                InputPathBox.ForeColor = SystemColors.GrayText;
            }

            if (Settings.Default.DefaultTemp != "")
            {
                TempPathBox.Text = Settings.Default.DefaultTemp;
                TempPathBox.ForeColor = Color.Black;
            }
            else
            {
                TempPathBox.Text = "Temp Folder";
                TempPathBox.ForeColor = SystemColors.GrayText;
            }

            if (Settings.Default.DefaultRedacted == true)
            {
                RedactedButton.Visible = true;
            }
            else
            {
                RedactedButton.Visible = false;
            }

            // Reset Artist/Album textboxes
            ArtistTextBox.Text = "Artist";
            ArtistTextBox.ForeColor = SystemColors.GrayText;
            AlbumTextBox.Text = "Album";
            AlbumTextBox.ForeColor = SystemColors.GrayText;

            if (Settings.Default.DefaultOutput != "")
            {
                OutputPathTextBox.Text = Settings.Default.DefaultOutput;
                OutputPathTextBox.ForeColor = Color.Black;
            }
            else
            {
                OutputPathTextBox.Text = "Output Folder (base path)";
                OutputPathTextBox.ForeColor = SystemColors.GrayText;
            }

            if (Settings.Default.DefaultParse != "")
            {
                OutputNamingSyntaxTextBox.Text = Settings.Default.DefaultParse;
                OutputNamingSyntaxTextBox.ForeColor = Color.Black;
            }
            else
            {
                OutputNamingSyntaxTextBox.Text = "Output Folder+File Name (syntax in tooltip)";
                OutputNamingSyntaxTextBox.ForeColor = SystemColors.GrayText;
            }

            ReplayGainCheckbox.Checked = Settings.Default.DefaultRG;

            if (Settings.Default.DefaultSpecificFiletypeText != "")
            {
                CopyFileTypesTextBox.Text = Settings.Default.DefaultSpecificFiletypeText;
                CopyFileTypesTextBox.ForeColor = Color.Black;
            }
            else
            {
                CopyFileTypesTextBox.Text = "e.g. *.jpg; *.log; *.cue; *.pdf";
                CopyFileTypesTextBox.ForeColor = SystemColors.GrayText;
            }

            // Check specific file copy setting and enable or disable file types textbox with it
            CopyContentsCheckbox.Checked = Settings.Default.DefaultSpecificFiletypes;
            if (CopyContentsCheckbox.Checked == true)
            {
                CopyFileTypesTextBox.Enabled = true;
                RenameLogCueCheckbox.Enabled = true;
            }
            else
            {
                CopyFileTypesTextBox.Enabled = false;
                RenameLogCueCheckbox.Enabled = false;
                StripImageMetadataCheckbox.Enabled = false;
            }

            if (Settings.Default.ExcelSheetLocation != "")
            {
                ExcelExportCheckbox.Checked = Settings.Default.DefaultExcelExport;
                ExcelExportCheckbox.Enabled = true;
                ExcelExportCheckbox.Text = "Append parsed data to Excel sheet";
                ExcelLogScoreTextBox.Visible = true;
                ExcelNotesTextBox.Visible = true;
            }
            else
            {
                ExcelExportCheckbox.Checked = false;
                ExcelExportCheckbox.Enabled = false;
                ExcelExportCheckbox.Text = "Append parsed data to Excel sheet (requires Excel sheet be set)";
                ExcelLogScoreTextBox.Visible = false;
                ExcelNotesTextBox.Visible = false;
            }

            // Miscellaneous other settings
            DeleteTempFolderCheckbox.Checked = Settings.Default.DefaultDeleteTemp;
            RenameLogCueCheckbox.Checked = Settings.Default.DefaultRenameLogCue;
            ConvertOpenFolderCheckbox.Checked = Settings.Default.DefaultFolderOpen;

            // Dynamically enable functionality based on available .exes
            if (Settings.Default.AADLocation != "")
            {
                AADButton.Enabled = true;
            }
            else
            {
                AADButton.Enabled = false;
            }

            StripImageMetadataCheckbox.Text = "Strip image metadata (bmp, gif, jpg, png) and compress .pngs";
            if (CopyContentsCheckbox.Checked == true)
            {
                StripImageMetadataCheckbox.Enabled = true;
                StripImageMetadataCheckbox.Checked = Settings.Default.DefaultStripImageMetadata;
            }

            if (Settings.Default.Mp3tagLocation != "")
            {
                Mp3tagButton.Enabled = true;
            }
            else
            {
                Mp3tagButton.Enabled = false;
            }

            if (Settings.Default.SpekLocation != "")
            {
                SpectrogramsButton.Enabled = true;
            }
            else
            {
                SpectrogramsButton.Enabled = false;
            }

            // Dynamic converter detection
            // First remove all entries
            ConvertToComboBox.Items.Clear();
            PresetComboBox.Items.Clear();

            // Add FLAC entries if flac.exe detected
            if (Settings.Default.FLACLocation != "")
            {
                AutoWavConvertCheckbox.Checked = Settings.Default.DefaultAutoWAVConvert;
                AutoWavConvertCheckbox.Enabled = true;
                AutoWavConvertCheckbox.Text = "Convert input .wav files to .flac";

                ConvertToComboBox.Items.Add("FLAC");
                PresetComboBox.Items.Add("Standard");

                // SoX is required for proper downsampling and dithering
                if (Settings.Default.SoXLocation != "")
                {
                    PresetComboBox.Items.Add("Force 16-bit");
                    PresetComboBox.Items.Add("Force 44.1kHz/48kHz");
                    PresetComboBox.Items.Add("Force 16-bit and 44.1/48kHz");
                }
            }
            else
            {
                AutoWavConvertCheckbox.Checked = false;
                AutoWavConvertCheckbox.Enabled = false;
                AutoWavConvertCheckbox.Text = "Convert input .wav files to .flac (requires flac.exe)";
            }

            // Add MP3 entries if lame.exe and flac.exe are detected
            if (Settings.Default.LAMELocation != "" && Settings.Default.FLACLocation != "")
            {
                ConvertToComboBox.Items.Add("MP3");
                PresetComboBox.Items.Add("245kbps VBR (V0)");
                PresetComboBox.Items.Add("225kbps VBR (V1)");
                PresetComboBox.Items.Add("190kbps VBR (V2)");
                PresetComboBox.Items.Add("175kbps VBR (V3)");
                PresetComboBox.Items.Add("165kbps VBR (V4)");
                PresetComboBox.Items.Add("130kbps VBR (V5)");
                PresetComboBox.Items.Add("115kbps VBR (V6)");
                PresetComboBox.Items.Add("100kbps VBR (V7)");
                PresetComboBox.Items.Add("85kbps VBR (V8)");
                PresetComboBox.Items.Add("65kbps VBR (V9)");
                PresetComboBox.Items.Add("320kbps CBR");
                PresetComboBox.Items.Add("256kbps CBR");
                PresetComboBox.Items.Add("192kbps CBR");
                PresetComboBox.Items.Add("128kbps CBR");
                PresetComboBox.Items.Add("64kbps CBR");
            }

            // Add Opus entries if opusenc.exe detected
            if (Settings.Default.OpusLocation != "")
            {
                ConvertToComboBox.Items.Add("Opus");
                PresetComboBox.Items.Add("192kbps VBR");
                PresetComboBox.Items.Add("160kbps VBR");
                PresetComboBox.Items.Add("128kbps VBR");
                PresetComboBox.Items.Add("96kbps VBR");
                PresetComboBox.Items.Add("64kbps VBR");
                PresetComboBox.Items.Add("32kbps VBR");
            }

            // Set the user-preferred default
            if (ConvertToComboBox.Items.Contains(Settings.Default.DefaultConvertFormat) && Settings.Default.DefaultConvertFormat != "")
            {
                // Set the comboboxes to the user's preference
                ConvertToComboBox.Text = Settings.Default.DefaultConvertFormat;
                PresetComboBox.Text = Settings.Default.DefaultConvertPreset;
            }
        }

        // Used for cleaning a string of invalid file name characters
        private string CleanString(string input, string ignoredChars = "")
        {
            if (input == null)
            {
                return input;
            }

            // First string holds characters to replace, second holds the replacement. Replacements are done serially so make sure they line up
            string replaceableIllegalChars = "\\/:*?\"“”<>|";
            string fullWidthReplacements = "＼／：＊？＂＂＂＜＞｜";

            // For every character in first string
            for (int i = 0; i < replaceableIllegalChars.Length; i++)
            {
                // If the character is not ignored via parameter
                if (!ignoredChars.Contains(replaceableIllegalChars[i]))
                {
                    // Replace the character with its replacement in the input string
                    input = input.Replace(replaceableIllegalChars[i], fullWidthReplacements[i]);
                }
            }

            // For each character that exists in the invalid file name chars string
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                // If the character is not ignored via paramter
                if (!ignoredChars.Contains(c))
                {
                    // Replace the illegal character with a '-'. Note that this runs after the initial fullwidth replacement so this will only catch characters we don't prettify
                    input = input.Replace(c, '-');
                }
            }

            return input;
        }

        private string GetRealImageFormat(Stream inStream)
        {
            string realFormat = "";

            // Read first 4 bytes into header
            byte[] imageHeader = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                imageHeader[i] = (byte)inStream.ReadByte();
            }

            // Reset position of stream
            inStream.Position -= 4;


            // If file has a magic BMP header ("BM")
            if (imageHeader[0] == 0x42 && imageHeader[1] == 0x4d)
            {
                realFormat = "BMP";
            }
            // Else if file has a magic GIF header ("GIF")
            else if (imageHeader[0] == 0x47 && imageHeader[1] == 0x49 && imageHeader[2] == 0x46)
            {
                realFormat = "GIF";
            }
            // Else if file has a magic JPG/JPEG header ("ÿØ")
            else if (imageHeader[0] == 0xff && imageHeader[1] == 0xd8)
            {
                realFormat = "JPG";
            }
            // Else if file has a magic PNG header (".PNG")
            else if (imageHeader[0] == 0x89 && imageHeader[1] == 0x50 && imageHeader[2] == 0x4e && imageHeader[3] == 0x47)
            {
                realFormat = "PNG";
            }

            return realFormat;
        }

        // Compress and remove metadata from PNG files
        private void CompressPng(string inputPNG, bool strip = false)
        {
            // Initialize oxipng.exe and compress in place, stripping metadata on the way
            System.Diagnostics.Process oxiPngProcess = new System.Diagnostics.Process();
            // Only use the 64-bit version of oxipng on 64-bit systems
            if (Environment.Is64BitOperatingSystem)
            {
                oxiPngProcess.StartInfo.FileName = "Redist\\oxipng64.exe";
            }
            else
            {
                oxiPngProcess.StartInfo.FileName = "Redist\\oxipng86.exe";
            }
            oxiPngProcess.StartInfo.UseShellExecute = false;
            oxiPngProcess.StartInfo.CreateNoWindow = true;
            // -o 4 sets the iteration level to 4. Possible values are 0->6, but anything above 4 takes a long time for little gain
            oxiPngProcess.StartInfo.Arguments = "-o 4";

            // If metadata stripping is enabled
            if (strip)
            {
                // Add argument to --strip safe (only strips metadata which will not impact viewing of the image)
                oxiPngProcess.StartInfo.Arguments += " --strip safe";
            }

            // Add input file argument
            oxiPngProcess.StartInfo.Arguments += " \"" + inputPNG + "\"";

            // Start and wait
            oxiPngProcess.Start();
            oxiPngProcess.WaitForExit();

            return;
        }

        // Remove metadata from bmp/gif files
        private void StripBmpGifMetadata(string inputFile)
        {
            Bitmap tempBitmap = new Bitmap(inputFile);
            // For every property (metadata)
            foreach (PropertyItem currentProperty in tempBitmap.PropertyItems)
            {
                // Initialize a temporary property
                PropertyItem tempProperty = currentProperty;
                // Nullify temporary property
                tempProperty.Value = new byte[] { 0 };
                // Set original property to temporary property
                tempBitmap.SetPropertyItem(tempProperty);
            }
            // Save changed bitmap to original file
            tempBitmap.Save(inputFile + ".tmp");
            // Free resources
            tempBitmap.Dispose();
            // Delete original and rename .tmp file to original
            File.Delete(inputFile);
            File.Move(inputFile + ".tmp", inputFile);

            return;
        }

        // Remove EXIF data from jpg/jpeg files
        private Stream StripJpegExif(Stream inStream, Stream outStream)
        {
            // Read first 2 bytes into header (should be 0xff and 0xd8 aka magic jpeg header)
            byte[] jpegHeader = new byte[2];
            jpegHeader[0] = (byte)inStream.ReadByte();
            jpegHeader[1] = (byte)inStream.ReadByte();

            // If file has a magic jpeg header
            if (jpegHeader[0] == 0xff && jpegHeader[1] == 0xd8)
            {
                // Skip through its header section
                SkipAppHeaders(inStream);

                // Write manual magic jpeg header to the beginning of outStream
                outStream.WriteByte(0xff);
                outStream.WriteByte(0xd8);
            }
            // Else if file is masquerading as a jpeg
            else
            {
                MessageBox.Show("False JPEG file detected. Make sure the file isn't another format renamed to .jpg/.jpeg.");
                // Back up two bytes in stream in preparation to move the entire file to outStream
                inStream.Position -= 2;
            }

            // Copy the rest of the file's payload from inStream to outStream
            int readCount;
            byte[] readBuffer = new byte[4096];
            while ((readCount = inStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
            {
                outStream.Write(readBuffer, 0, readCount);
            }

            return outStream;
        }

        // Helper function for StripJpegExif that skips all "APP" sections at the beginning of a JPEG file and returns control once it hits the payload
        private void SkipAppHeaders(Stream inStream)
        {
            // Read next two bytes into header variable (should be 0xff and 0xe0 to denote the first (APP0) section)
            byte[] header = new byte[2];
            header[0] = (byte)inStream.ReadByte();
            header[1] = (byte)inStream.ReadByte();

            // While we're still in an APP section, skip forward until we're not (0xef denotes the last possible APP section)
            while (header[0] == 0xff && header[1] >= 0xe0 && header[1] <= 0xef)
            {
                // Read next byte into appLength (contains data for the length of the current APP section)
                int appLength = inStream.ReadByte();

                // Shift appLength 8 bits and inclusive or the next byte to it (ultimately reading two bytes into the int)
                appLength <<= 8;
                appLength |= inStream.ReadByte();
                // Literal length data for current section is included in itself (and we've already read it) so subtract those 2 bytes
                appLength -= 2;

                // Move the stream forward by the calculated appLength
                inStream.Seek(appLength, SeekOrigin.Current);

                // Move next two bytes from inStream to header variable (should be 0xff and the next APP section marker)
                header[0] = (byte)inStream.ReadByte();
                header[1] = (byte)inStream.ReadByte();
            }
            // Skip back two bytes (these two bytes could have been an APP header but it was determined it was part of the payload instead,
            // so back up 2 bytes and send control back to StripJpegExif
            inStream.Position -= 2;
        }

        // Strips metadata from images in the input list (and compresses .png files)
        private void StripImages(List<string> inputFiles)
        {
            // List that contains pending images to be checked and moved
            List<string> pendingImages = new List<string>();

            // Add supported image formats to list
            pendingImages.AddRange(inputFiles.FindAll(x => x.EndsWith(".bmp")));
            pendingImages.AddRange(inputFiles.FindAll(x => x.EndsWith(".gif")));
            pendingImages.AddRange(inputFiles.FindAll(x => x.EndsWith(".jpg")));
            pendingImages.AddRange(inputFiles.FindAll(x => x.EndsWith(".jpeg")));
            pendingImages.AddRange(inputFiles.FindAll(x => x.EndsWith(".png")));

            // Lists to hold the images as defined by their magic headers, not their formats
            List<string> pendingBMP = new List<string>();
            List<string> pendingGIF = new List<string>();
            List<string> pendingJPG = new List<string>();
            List<string> pendingPNG = new List<string>();

            // For every file in pendingImages
            foreach (string currentImage in pendingImages)
            {
                // Open input filestream (currentImage)
                using (FileStream sourceIMGStream = File.Open(currentImage, FileMode.Open))
                {
                    // Send it to GetRealImageFormat to determine its format by its magic header
                    string realFormat = GetRealImageFormat(sourceIMGStream);

                    // Depending on its realFormat, send it to appropriate list
                    if (realFormat == "BMP")
                    {
                        pendingBMP.Add(currentImage);
                    }
                    else if (realFormat == "GIF")
                    {
                        pendingGIF.Add(currentImage);
                    }
                    else if (realFormat == "JPG")
                    {
                        pendingJPG.Add(currentImage);
                    }
                    else if (realFormat == "PNG")
                    {
                        pendingPNG.Add(currentImage);
                    }
                }
            }

            // For every image in pendingBMP
            // Parallelized for measureable improvement in speed.
            Parallel.ForEach(pendingBMP, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (currentBMP) => {
                // String that holds the location of the BMP file. Will be modified if the file is renamed.
                string location = currentBMP;

                // If the current file is a BMP but does not end with the right extension
                if (!currentBMP.EndsWith(".bmp"))
                {
                    // Rename to correct extension
                    location = Path.GetDirectoryName(currentBMP) + "\\" + Path.GetFileNameWithoutExtension(currentBMP) + ".bmp";
                    File.Move(currentBMP, location);
                }

                // Strip the file of its metadata
                StripBmpGifMetadata(location);
            });

            // For every image in pendingGIF
            // Parallelized for measureable improvement in speed.
            Parallel.ForEach(pendingGIF, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (currentGIF) => {
                // String that holds the location of the GIF file. Will be modified if the file is renamed.
                string location = currentGIF;

                // If the current file is a GIF but does not end with the right extension
                if (!currentGIF.EndsWith(".gif"))
                {
                    // Rename to correct extension
                    location = Path.GetDirectoryName(currentGIF) + "\\" + Path.GetFileNameWithoutExtension(currentGIF) + ".gif";
                    File.Move(currentGIF, location);
                }

                // Strip the file of its metadata
                StripBmpGifMetadata(location);
            });

            // For every image in pendingJPG
            // Paralellized but little to no improvement in speed. Unparallelize at will.
            Parallel.ForEach(pendingJPG, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (currentJPG) => {
                // String that holds the location of the JPG file. Will be modified if the file is renamed.
                string location = currentJPG;

                // If the current file is a JPG/JPEG but does not end with the right extension
                if (!currentJPG.EndsWith(".jpg") && !currentJPG.EndsWith(".jpeg"))
                {
                    // Rename to correct extension
                    location = Path.GetDirectoryName(currentJPG) + "\\" + Path.GetFileNameWithoutExtension(currentJPG) + ".jpg";
                    File.Move(currentJPG, location);
                }

                // Open input filestream (currentImage)
                using (FileStream sourceJPGStream = File.Open(location, FileMode.Open))
                {
                    // Open output filestream (currentImage + ".tmp")
                    using (FileStream outputJPGStream = File.Open(location + ".tmp", FileMode.Create))
                    {
                        // Strip EXIF data from input filestream and feed it into output filestream
                        StripJpegExif(sourceJPGStream, outputJPGStream);
                    }
                }

                // Delete original and rename .tmp file to original
                File.Delete(location);
                File.Move(location + ".tmp", location);
            });

            // For every image in pendingPNG
            // OxiPNG is already multi-threaded so we defer parallelization to it
            foreach (string currentPNG in pendingPNG)
            {
                // String that holds the location of the PNG file. Will be modified if the file is renamed.
                string location = currentPNG;

                // If the current file is a PNG but does not end with the right extension
                if (!currentPNG.EndsWith(".png"))
                {
                    // Rename to correct extension
                    location = Path.GetDirectoryName(currentPNG) + "\\" + Path.GetFileNameWithoutExtension(currentPNG) + ".png";
                    File.Move(currentPNG, location);
                }

                // Compress the currentPNG and pass a parameter to strip the metadata as well
                CompressPng(location, true);
            }
        }

        // Adds information to the next available row in an Excel spreadsheet and sorts it
        private void AddToExcel(string lastFolder, string excelLogScore, string excelNotes)
        {
            // Initialization for Excel functionality
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook currentWorkbook = excel.Workbooks.Open(Settings.Default.ExcelSheetLocation);
            // Uses first worksheet by default
            Worksheet currentWorksheet = excel.Worksheets[1];
            Range usedRange = currentWorksheet.UsedRange;
            int numRows = usedRange.Rows.Count;

            // Insert output folder name, log score, and notes in columns 1, 2, and 3 respectively
            currentWorksheet.Cells[numRows + 1, 1] = lastFolder;

            // Check if log score and notes are empty before writing
            if (excelLogScore != "Log score" && excelLogScore != "")
            {
                currentWorksheet.Cells[numRows + 1, 2] = excelLogScore;
            }
            if (excelNotes != "Notes" && excelNotes != "")
            {
                currentWorksheet.Cells[numRows + 1, 3] = excelNotes;
            }

            // Update the usedRange to account for new row
            usedRange = currentWorksheet.UsedRange;

            // Re-sort worksheet to account for new row
            usedRange.Sort(usedRange.Columns[1], XlSortOrder.xlAscending, Type.Missing, Type.Missing, XlSortOrder.xlAscending, Type.Missing,
                XlSortOrder.xlAscending, XlYesNoGuess.xlGuess,
                Type.Missing, Type.Missing,
                XlSortOrientation.xlSortColumns, XlSortMethod.xlPinYin);

            // Save workbook and quit
            currentWorkbook.Close(true);
            excel.Quit();
        }

        // Renames .logs and .cues in an input file list to the standard naming scheme used by EAC (%artist% - %album%.log and %album%.cue)
        // Fails if there are multiple .logs or multiple .cues in the input list, as this means there are multiple discs in the album and we cannot determine what their ordering is
        private void RenameLogCue(List<string> inputFiles, string outputFolder, string artist, string album)
        {
            // Find .logs and .cues that were copied
            List<string> logList = new List<string>();
            logList.AddRange(inputFiles.FindAll(x => x.EndsWith(".log")));
            List<string> cueList = new List<string>();
            cueList.AddRange(inputFiles.FindAll(x => x.EndsWith(".cue")));

            // If there are 2 or more .cues/.logs in the output, alert the user to rename manually (not possible to detect which .cue is CD1/CD2/etc)
            if (cueList.Count() >= 2 || logList.Count() >= 2)
            {
                MessageBox.Show("More than one .cue/.log file detected in output folder. Rename manually.");
                // Opens output path in explorer
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = outputFolder,
                    UseShellExecute = true,
                });
            }
            else
            {
                // If there's only one .cue, rename it (only if it's not already named properly)
                if (cueList.Count() == 1 && !File.Exists(outputFolder + album + ".cue"))
                {
                    if (File.Exists(cueList[0]))
                    {
                        File.Move(cueList[0], outputFolder + album + ".cue");
                    }
                }
                // If there's only one .log, rename it (only if it's not already named properly)
                if (logList.Count() == 1 && !File.Exists(outputFolder + artist + " - " + album + ".log"))
                {
                    if (File.Exists(logList[0]))
                    {
                        File.Move(logList[0], outputFolder + artist + " - " + album + ".log");
                    }
                }
            }
        }

        // Adds a "\" to the end of a path if it doesn't already have one
        private string NormalizePath(string path)
        {
            return path.EndsWith("\\") ? path : path + "\\";
        }

        // Gets a list of all non-system directories that are recursively nested under the input directory.
        // Mainly used as a helper function for GetRecursiveFilesSafe
        private string[] GetRecursiveDirectoriesSafe(string initialPath)
        {
            // List to hold the directories we find for eventual return
            List<string> outputDirectories = new List<string>();

            // For each directory that's one level below us
            foreach (string currentDir in Directory.GetDirectories(initialPath))
            {
                // try; we're expecting errors if we run into illegal folders
                try
                {
                    DirectoryInfo currentDirInfo = new DirectoryInfo(currentDir);
                    // If the file is not marked as a system folder (i.e. will throw errors) OR if the folder is a root folder. Root folders are technically system folders but they are safe to use
                    if (!currentDirInfo.Attributes.HasFlag(FileAttributes.System) || currentDirInfo.Root.FullName.Equals(currentDirInfo.FullName))
                    {
                        outputDirectories.AddRange(GetRecursiveDirectoriesSafe(currentDir));
                    }
                }
                // blackhole errors. Each error is a folder we don't want. We shouldn't ever be getting errors due to !system.folder conditional above but this is a just in case.
                catch { }
            }

            // Add the input path to the list
            outputDirectories.Add(initialPath);

            return outputDirectories.ToArray();
        }

        // Gets a list of all files that are recursively nested under the input directory (ignoring restricted folders), and optionally allows filtering with second parameter
        private string[] GetRecursiveFilesSafe(string initialPath, string searchPattern = "*.*")
        {
            // Holds the eventual file list
            List<string> files = new List<string>();
            // Holds the eventual list of safe directories as determined by GetRecursiveDirectoriesSafe
            List<string> directories = new List<string>();

            // Add all safe directories to directories list
            directories.AddRange(GetRecursiveDirectoriesSafe(initialPath));

            // For each directory in the safe directory list
            foreach (string currentDir in directories)
            {
                // Add all files within the directory to the file list, and searches with a user-specified pattern (default is "*.*")
                files.AddRange(Directory.GetFiles(currentDir, searchPattern));
            }

            return files.ToArray();
        }

        // Recursively copy a folder's contents into another folder. Selectively copies certain files if 3rd parameter is specified.
        // Fourth parameter prevents .flacs from being copied (for preventing overwriting of newly converted .flacs with the old .flacs)
        private string[] RecursiveFolderCopy(string fromPath, string toPath, string specificFiletypeText = "*.*", bool noFlac = false)
        {
            List<string> pendingFileTypes = new List<string>();
            string originalFiletypeText = specificFiletypeText;

            // List to hold all files created by this function (for returning)
            List<string> copiedFiles = new List<string>();

            if (specificFiletypeText != "")
            {
                // Convert common wildcards into regex
                specificFiletypeText = specificFiletypeText.Replace(@".", @"\.");
                specificFiletypeText = specificFiletypeText.Replace(@"*", @".*");
                specificFiletypeText = specificFiletypeText.Replace(@"?", @".");

                // pendingFileTypes contains strings of each (regex) phrase to match, e.g. ".*\.jpg" and "?png"
                pendingFileTypes = specificFiletypeText.Split(';').Select(x => x.Trim()).ToList();
                pendingFileTypes.RemoveAll(str => String.IsNullOrEmpty(str));
            }

            // If either of the paths don't end with "\", add "\" to them
            fromPath = NormalizePath(fromPath);
            toPath = NormalizePath(toPath);

            // Create a list of pending files
            List<string> pendingFiles = Directory.GetFiles(fromPath).ToList();

            if (noFlac)
            {
                // Remove all .flacs from the pending files (presumed to be converted)
                pendingFiles.RemoveAll(str => str.EndsWith(".flac"));
            }

            // For each file in the pending files
            foreach (string currentFile in pendingFiles)
            {
                FileInfo curFileInfo = new FileInfo(currentFile);
                // If file is not going to be copied to itself
                if (specificFiletypeText != "" && currentFile != toPath + curFileInfo.Name)
                {
                    // For every file type (e.g. *.jpg, *.png)
                    foreach (string currentFileType in pendingFileTypes)
                    {
                        // Check if the file matches this file type via Regex
                        Match match = Regex.Match("^" + curFileInfo.Name + "$", currentFileType);
                        if (match.Success && File.Exists(curFileInfo.FullName))
                        {
                            // Creates a directory of toPath, inherently checks if it exists before creation
                            Directory.CreateDirectory(toPath);
                            File.Copy(curFileInfo.FullName, toPath + curFileInfo.Name, true);
                            // Add copied file to outputFile list
                            copiedFiles.Add(toPath + curFileInfo.Name);
                        }
                    }
                }
                // If file is not going to be copied to itself
                else if (currentFile != toPath + curFileInfo.Name)
                {
                    if (File.Exists(curFileInfo.FullName))
                    {
                        // Creates a directory of toPath, inherently checks if it exists before creation
                        Directory.CreateDirectory(toPath);
                        File.Copy(curFileInfo.FullName, toPath + curFileInfo.Name, true);
                        // Add copied file to outputFile list
                        copiedFiles.Add(toPath + curFileInfo.Name);
                    }
                }
            }

            // Recurse each folder in the fromPath into this function again, so that they may create new folders and copy
            foreach (string currentFolder in Directory.GetDirectories(fromPath))
            {
                DirectoryInfo curFolderInfo = new DirectoryInfo(currentFolder);
                copiedFiles.AddRange(RecursiveFolderCopy(currentFolder, toPath + curFolderInfo.Name, originalFiletypeText, noFlac));
            }

            return copiedFiles.ToArray();
        }

        // Parses custom syntax (e.g. %tag% and &codec&) and returns a string based on the metadata/tags of a file
        private string ParseNamingSyntax(string syntax, string codec, string preset, string filename, int futureBPS = -1, int futureSampleRate = -1)
        {
            string parsedString = "";
            string formattedString = "";
            int nextMarkerIndex = 0;

            // Replace UNIX folder delimiters with Windows folder delimiters
            syntax = syntax.Replace('/', '\\');

            // Clean the input syntax of illegal file characters before starting, and ignore folder delimiters in this process
            syntax = CleanString(syntax, "\\");

            // Get tags of (flac) file
            TagLib.File tagFile = TagLib.File.Create(filename);
            TagLib.Ogg.XiphComment tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

            // Prettier metadata to use for folder/filenames
            // MP3
            if (codec == "MP3")
            {
                switch (preset)
                {
                    case "245kbps VBR (V0)":
                        preset = "V0";
                        break;
                    case "225kbps VBR (V1)":
                        preset = "V1";
                        break;
                    case "190kbps VBR (V2)":
                        preset = "V2";
                        break;
                    case "175kbps VBR (V3)":
                        preset = "V3";
                        break;
                    case "165kbps VBR (V4)":
                        preset = "V4";
                        break;
                    case "130kbps VBR (V5)":
                        preset = "V5";
                        break;
                    case "115kbps VBR (V6)":
                        preset = "V6";
                        break;
                    case "100kbps VBR (V7)":
                        preset = "V7";
                        break;
                    case "85kbps VBR (V8)":
                        preset = "V8";
                        break;
                    case "65kbps VBR (V9)":
                        preset = "V9";
                        break;
                    case "320kbps CBR":
                        preset = "320";
                        break;
                    case "256kbps CBR":
                        preset = "256";
                        break;
                    case "192kbps CBR":
                        preset = "192";
                        break;
                    case "128kbps CBR":
                        preset = "128";
                        break;
                    case "64kbps CBR":
                        preset = "64";
                        break;
                }
            }
            // Opus
            else if (codec == "Opus")
            {
                switch (preset)
                {
                    case "192kbps VBR":
                        preset = "192";
                        break;
                    case "160kbps VBR":
                        preset = "160";
                        break;
                    case "128kbps VBR":
                        preset = "128";
                        break;
                    case "96kbps VBR":
                        preset = "96";
                        break;
                    case "64kbps VBR":
                        preset = "64";
                        break;
                    case "32kbps VBR":
                        preset = "32";
                        break;
                }
            }

            // Move through the input string, matching syntax, pushing its equivalent into a formatted string, then deleting the matched portion of the original and loop
            while (syntax.Length > 0)
            {
                // If we are at a % and there is a matching %
                if (syntax[0] == '%' && syntax.IndexOf('%') != syntax.LastIndexOf('%'))
                {
                    // Find the matching percent marker, e.g. %artist"%"
                    nextMarkerIndex = syntax.IndexOf('%', 1);

                    // Retrieve parsed tag from the file, and clean it of illegal characters on the way
                    parsedString = CleanString(tagMap.GetFirstField(syntax.Substring(1, nextMarkerIndex - 1)));

                    // Add parsed element to the eventual output string
                    formattedString += parsedString;

                    // Remove the matched portion from the input string
                    syntax = syntax.Remove(0, nextMarkerIndex + 1);
                }
                // If we are at a & and there is a matching &
                else if (syntax[0] == '&' && syntax.IndexOf('&') != syntax.LastIndexOf('&'))
                {
                    // Find the matching ampersand, e.g. &codec"&"
                    nextMarkerIndex = syntax.IndexOf('&', 1);

                    // Bit-depth e.g. 16, 24
                    if (syntax.Substring(1, nextMarkerIndex - 1) == "bps")
                    {
                        // Manual BPS insertion
                        if (futureBPS != -1)
                        {
                            formattedString += futureBPS;
                        }
                        else
                        {
                            formattedString += tagFile.Properties.BitsPerSample.ToString();
                        }
                    }

                    // Lossless smartbit, uses bit-depth + "-" + a short sample rate e.g. 16-44, 24-96
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "smartbit" && (codec == "FLAC"))
                    {
                        // Manual BPS and sample rate insertion
                        if (futureBPS != -1 && futureSampleRate != -1)
                        {
                            formattedString += futureBPS + "-" + futureSampleRate.ToString().Substring(0, 2);
                        }
                        else if (futureBPS != -1)
                        {
                            formattedString += futureBPS + "-" + tagFile.Properties.AudioSampleRate.ToString().Substring(0, 2);
                        }
                        else if (futureSampleRate != -1)
                        {
                            formattedString += tagFile.Properties.BitsPerSample.ToString() + "-" + futureSampleRate;
                        }
                        else
                        {
                            formattedString += tagFile.Properties.BitsPerSample.ToString() + "-" + tagFile.Properties.AudioSampleRate.ToString().Substring(0, 2);
                        }
                    }

                    // Lossy smartbit, uses bitrate/preset e.g. 320
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "smartbit")
                    {
                        formattedString += preset;
                    }

                    // Sample rate e.g. 44100, 48000, 96000
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "samplerate")
                    {
                        // Manual sample rate insertion
                        if (futureSampleRate != -1)
                        {
                            formattedString += futureSampleRate;
                        }
                        else
                        {
                            formattedString += tagFile.Properties.AudioSampleRate.ToString();
                        }
                    }

                    // Shortened sample rate, e.g. 44, 48, 96
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "short-samplerate")
                    {
                        // Manual sample rate insertion
                        if (futureSampleRate != -1)
                        {
                            formattedString += futureSampleRate.ToString().Substring(0, 2);
                        }
                        else
                        {
                            formattedString += tagFile.Properties.AudioSampleRate.ToString().Substring(0, 2);
                        }
                    }

                    // Codec
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "codec")
                    {
                        formattedString += codec;
                    }

                    // Bitrate
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "bitrate")
                    {
                        formattedString += preset;
                    }

                    // Padded track number logic
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "paddedtracknumber")
                    {
                        // Retrieve parsed tag from the file, and clean it of illegal characters on the way
                        parsedString = CleanString(tagMap.GetFirstField("tracknumber"));

                        // Pad the track number if it's 1-9
                        if (Int32.Parse(parsedString) >= 1 && Int32.Parse(parsedString) <= 9)
                        {
                            parsedString = parsedString.TrimStart('0').Insert(0, "0");
                        }

                        // If 0, set to 00
                        else if (Int32.Parse(parsedString) == 0)
                        {
                            parsedString = "00";
                        }

                        formattedString += parsedString;
                    }

                    // Remove the matched portion from the input string
                    syntax = syntax.Remove(0, nextMarkerIndex + 1);
                }
                else
                {
                    if (syntax[0] == '\\')
                    {
                        // Remove spaces and periods from the end of a folder name; not allowed
                        formattedString = formattedString.TrimEnd(' ', '.');
                    }

                    // Pass non-matching characters into the formattedString
                    formattedString += syntax[0];
                    syntax = syntax.Remove(0, 1);
                }
            }

            return formattedString;
        }

        // For a given list of input FLACs, convert them to a certain codec (FLAC, MP3, etc.) in a specific preset (16-bit resample, 192kbps, V0, etc.)
        // Returns a list of output files
        private List<string> ConvertToFormat(List<string> inputFLACs, string outputPath, string syntax, string codec, string preset)
        {
            // Future lists of resultant output files. Note that this list will be randomly ordered due to parallelization.
            List<string> outputFiles = new List<string>();

            // Used partially in guesswork, pulls tag/file data from first .flac file
            TagLib.File tempTagFile = TagLib.File.Create(inputFLACs[0]);
            TagLib.Ogg.XiphComment tempTagMap = (TagLib.Ogg.XiphComment)tempTagFile.GetTag(TagLib.TagTypes.Xiph);

            // Disambiguate folder names later on, putting lower samplerates and lower BPS into higher folders
            int highestSampleRate = tempTagFile.Properties.AudioSampleRate;
            int highestBPS = tempTagFile.Properties.BitsPerSample;
            // Holds the base sample rate for disambiguating folder names later on
            int highestBaseSampleRate = 0;

            // Find the highest BPS and samplerate in the input files
            foreach (string currentFLAC in inputFLACs)
            {
                TagLib.File tempLoopTagFile = TagLib.File.Create(currentFLAC);
                TagLib.Ogg.XiphComment tempLoopTagMap = (TagLib.Ogg.XiphComment)tempLoopTagFile.GetTag(TagLib.TagTypes.Xiph);

                if (tempLoopTagFile.Properties.AudioSampleRate > highestSampleRate)
                {
                    highestSampleRate = tempLoopTagFile.Properties.AudioSampleRate;
                }

                if (tempLoopTagFile.Properties.BitsPerSample > highestBPS)
                {
                    highestBPS = tempLoopTagFile.Properties.BitsPerSample;
                }
            }

            // Holds the highest base sample rate, aka 44100 for CD audio or 48000 for digital
            // This will result in 48000 for 192kHz, 44100 for 88.2kHz etc.
            if (highestSampleRate % 44100 == 0)
            {
                highestBaseSampleRate = 44100;
            }
            else if (highestSampleRate % 48000 == 0)
            {
                highestBaseSampleRate = 48000;
            }

            // Normalizes the outputPath for consistency
            NormalizePath(outputPath);
            // FLAC Conversion
            if (codec == "FLAC")
            {
                // Initialize a variable for input into ParseNamingSyntax, disambiguating output
                int futureBPS = highestBPS;
                // If other files are going to reduce bit depth, change the futureBPS accordingly
                if (preset == "Force 16-bit" || preset == "Force 16-bit and 44.1/48kHz")
                {
                    futureBPS = 16;
                }

                // Initialize a variable for input into ParseNamingSyntax, disambiguating output
                int futureSampleRate = highestSampleRate;
                // If other files are going to resample, change the futureSampleRate accordingly
                if (preset == "Force 44.1kHz/48kHz" || preset == "Force 16-bit and 44.1/48kHz")
                {
                    futureSampleRate = highestBaseSampleRate;
                }

                // Parallel convert files
                Parallel.ForEach(inputFLACs, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (currentFLAC) => {
                    // Get a tagmap of the current file
                    TagLib.File tagFile = TagLib.File.Create(currentFLAC);
                    TagLib.Ogg.XiphComment tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

                    // If bit depth reduction/resampling is selected and the file actually needs it
                    if ((preset == "Force 16-bit" || preset == "Force 44.1kHz/48kHz" || preset == "Force 16-bit and 44.1/48kHz")
                                    && (tagFile.Properties.BitsPerSample >= 24 || (tagFile.Properties.AudioSampleRate != 44100 && tagFile.Properties.AudioSampleRate != 48000)))
                    {
                        // Set up parse strings, faking futureBPS and futureSampleRate to the function
                        string parsedFileSyntax = ParseNamingSyntax(syntax, codec, preset, currentFLAC, futureBPS, futureSampleRate);
                        string parsedFolderSyntax = "";
                        // If the path denotes there is a folder
                        if (parsedFileSyntax.LastIndexOf('\\') != -1)
                        {
                            parsedFolderSyntax = parsedFileSyntax.Substring(0, parsedFileSyntax.LastIndexOf('\\'));
                        }

                        // In this section we initialize SoX and attempt to use it to reduce bit-depth/downsample.
                        // If this fails (SoX throws errors on crazy ASCII filenames and filepaths), we will instead copy the file into %temp% with a randomly generated safe name for SoX to work with

                        // Future output file name. Will be changed to the correct file if SoX throws errors and needs to be worked around.
                        string outputFile = Path.GetDirectoryName(currentFLAC) + "\\" + Path.GetFileNameWithoutExtension(currentFLAC) + "downsampled" + ".flac";

                        // Initialize sox.exe
                        System.Diagnostics.Process soxProcess = new System.Diagnostics.Process();
                        soxProcess.StartInfo.FileName = Settings.Default.SoXLocation;
                        soxProcess.StartInfo.UseShellExecute = false;
                        soxProcess.StartInfo.CreateNoWindow = true;

                        // Add intial arguments to SoX (guarding)
                        soxProcess.StartInfo.Arguments = "\"" + currentFLAC + "\" -G";

                        if (preset == "Force 16-bit" || preset == "Force 16-bit and 44.1/48kHz")
                        {
                            soxProcess.StartInfo.Arguments += " -b 16";
                        }

                        // Add output file information to SoX
                        soxProcess.StartInfo.Arguments += " \"" + outputFile + "\" rate -v -L";

                        // Add resampling arguments to SoX, depending on their base sample rate
                        if (preset == "Force 44.1kHz/48kHz" || preset == "Force 16-bit and 44.1/48kHz")
                        {
                            if (tagFile.Properties.AudioSampleRate % 44100 == 0)
                            {
                                soxProcess.StartInfo.Arguments += " 44100";
                            }
                            else if (tagFile.Properties.AudioSampleRate % 48000 == 0)
                            {
                                soxProcess.StartInfo.Arguments += " 48000";
                            }
                        }
                        // If we're only reducing bit depth, specify the audio sample rate to use
                        else if (preset == "Force 16-bit")
                        {
                            soxProcess.StartInfo.Arguments += " " + tagFile.Properties.AudioSampleRate.ToString();
                        }

                        // Add dither to argument list
                        soxProcess.StartInfo.Arguments += " dither";

                        // Start SoX process
                        soxProcess.Start();
                        soxProcess.WaitForExit();

                        // If SoX did not complete sucessfully, begin a workaround
                        // This section copies the file into %temp% and uses a guid as a filename, creating a totally safe file path+name for SoX to use
                        // SoX throws errors on both high ASCII filenames and filepaths, meaning even a containing folder with high ASCII will cause SoX to fail
                        if (!File.Exists(Path.GetDirectoryName(currentFLAC) + "\\" + Path.GetFileNameWithoutExtension(currentFLAC) + "downsampled" + ".flac"))
                        {
                            // Used so Sox doesn't throw errors at wacky filenames
                            // Temp path + random guid string + .flac. Used to create a safe filename for SoX to use
                            string soxSafeName = Path.GetTempPath() + "SoXTemp\\" + Guid.NewGuid().ToString() + ".flac";

                            // Update the outputFile string to point to this new eventual "safe" file
                            outputFile = Path.GetDirectoryName(soxSafeName) + "\\" + Path.GetFileNameWithoutExtension(soxSafeName) + "downsampled" + ".flac";
                            // Directory for the new file
                            Directory.CreateDirectory(Path.GetTempPath() + "SoXTemp\\");

                            // Copy input FLAC into the soxSafeName position
                            if (File.Exists(currentFLAC))
                            {
                                File.Copy(currentFLAC, soxSafeName);
                            }

                            // Add intial arguments to SoX (guarding)
                            soxProcess.StartInfo.Arguments = "\"" + soxSafeName + "\" -G";

                            if (preset == "Force 16-bit" || preset == "Force 16-bit and 44.1/48kHz")
                            {
                                soxProcess.StartInfo.Arguments += " -b 16";
                            }

                            // Add output file information to SoX
                            soxProcess.StartInfo.Arguments += " \"" + outputFile + "\" rate -v -L";

                            // Add resampling arguments to SoX, depending on their base sample rate
                            if (preset == "Force 44.1kHz/48kHz" || preset == "Force 16-bit and 44.1/48kHz")
                            {
                                if (tagFile.Properties.AudioSampleRate % 44100 == 0)
                                {
                                    soxProcess.StartInfo.Arguments += " 44100";
                                }
                                else if (tagFile.Properties.AudioSampleRate % 48000 == 0)
                                {
                                    soxProcess.StartInfo.Arguments += " 48000";
                                }
                            }
                            // If we're only reducing bit depth, specify the audio sample rate to use
                            else if (preset == "Force 16-bit")
                            {
                                soxProcess.StartInfo.Arguments += " " + tagFile.Properties.AudioSampleRate.ToString();
                            }

                            // Add dither to argument list
                            soxProcess.StartInfo.Arguments += " dither";

                            // Start SoX process
                            soxProcess.Start();
                            soxProcess.WaitForExit();

                            // Remove non-downsampled file
                            if (File.Exists(soxSafeName))
                            {
                                File.Delete(soxSafeName);
                            }
                        }

                        // Create a directory to move the file to
                        Directory.CreateDirectory(outputPath + parsedFolderSyntax);
                        // Move the output downsampled file to the standard output directory, renaming in the process
                        if (File.Exists(outputFile))
                        {
                            // Delete file if it already exists. Combined with below command simulates an overwrite
                            if (File.Exists(outputPath + parsedFileSyntax + ".flac"))
                            {
                                File.Delete(outputPath + parsedFileSyntax + ".flac");
                            }

                            File.Move(outputFile, outputPath + parsedFileSyntax + ".flac");
                        }

                        // Remove temp directory if it exists and is empty
                        if (Directory.Exists(Path.GetTempPath() + "SoXTemp\\") && GetRecursiveFilesSafe(Path.GetTempPath() + "SoXTemp\\").Count() == 0)
                        {
                            Directory.Delete(Path.GetTempPath() + "SoXTemp\\");
                        }

                        // Add resultant file to output file list
                        outputFiles.Add(outputPath + parsedFileSyntax + ".flac");
                    }
                    // Plain FLAC re-encoding if bit depth reduction/resampling is not selected or not needed
                    else
                    {
                        // Set up parse strings, faking futureBPS and futureSampleRate to the function
                        string parsedFileSyntax = ParseNamingSyntax(syntax, codec, preset, currentFLAC, futureBPS, futureSampleRate);
                        string parsedFolderSyntax = "";
                        // If the path denotes there is a folder
                        if (parsedFileSyntax.LastIndexOf('\\') != -1)
                        {
                            parsedFolderSyntax = parsedFileSyntax.Substring(0, parsedFileSyntax.LastIndexOf('\\'));
                        }

                        // Path+Name of future file
                        string outputFile = outputPath + parsedFileSyntax + ".flac";
                        // Directory for new file
                        Directory.CreateDirectory(outputPath + parsedFolderSyntax);

                        // Initialize flac.exe and convert to the output+parsed syntax location, using V8 compression.
                        System.Diagnostics.Process flacProcess = new System.Diagnostics.Process();
                        flacProcess.StartInfo.FileName = Settings.Default.FLACLocation;
                        flacProcess.StartInfo.UseShellExecute = false;
                        flacProcess.StartInfo.CreateNoWindow = true;

                        // Add arguments to force Re-FLACing and use V8 compression
                        flacProcess.StartInfo.Arguments = "-f -V8 \"" + currentFLAC + "\" -o \"" + outputFile + "\"";

                        flacProcess.Start();
                        flacProcess.WaitForExit();

                        // Add resultant file to output file list
                        outputFiles.Add(outputPath + parsedFileSyntax + ".flac");
                    }
                });
            }

            // MP3 Conversion
            else if (codec == "MP3")
            {
                Parallel.ForEach(inputFLACs, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (currentFLAC) => {
                    // Get a tagmap of the current file
                    TagLib.File tagFile = TagLib.File.Create(currentFLAC);
                    TagLib.Ogg.XiphComment tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

                    // Set up parse strings, faking futureBPS and futureSampleRate to the function
                    string parsedFileSyntax = ParseNamingSyntax(syntax, codec, preset, currentFLAC);
                    string parsedFolderSyntax = "";
                    // If the path denotes there is a folder
                    if (parsedFileSyntax.LastIndexOf('\\') != -1)
                    {
                        parsedFolderSyntax = parsedFileSyntax.Substring(0, parsedFileSyntax.LastIndexOf('\\'));
                    }

                    // Path+Name of future file
                    string outputWAV = outputPath + parsedFileSyntax + ".wav";
                    string outputMP3 = outputPath + parsedFileSyntax + ".mp3";

                    // Create directory
                    Directory.CreateDirectory(outputPath + parsedFolderSyntax);

                    // Get all tags from input .flac and store them
                    List<string> pendingTagNames = new List<string>();
                    List<string> pendingTagData = new List<string>();
                    foreach (string currentField in tagMap)
                    {
                        pendingTagNames.Add(currentField);
                        pendingTagData.Add(tagMap.GetFirstField(currentField));
                    }

                    // Initialize flac.exe to decode to WAV
                    System.Diagnostics.Process deflacProcess = new System.Diagnostics.Process();
                    deflacProcess.StartInfo.FileName = Settings.Default.FLACLocation;
                    deflacProcess.StartInfo.UseShellExecute = false;
                    deflacProcess.StartInfo.CreateNoWindow = true;

                    // Add arguments to decode FLAC to WAV
                    deflacProcess.StartInfo.Arguments = "-d \"" + currentFLAC + "\" -o \"" + outputWAV + "\"";

                    // Start and wait
                    deflacProcess.Start();
                    deflacProcess.WaitForExit();

                    // Initialize lame.exe
                    System.Diagnostics.Process lameProcess = new System.Diagnostics.Process();
                    lameProcess.StartInfo.FileName = Settings.Default.LAMELocation;
                    lameProcess.StartInfo.UseShellExecute = false;
                    lameProcess.StartInfo.CreateNoWindow = true;

                    // Add initial arguments (high quality mode)
                    lameProcess.StartInfo.Arguments = "-h";

                    // Determine which preset to use
                    switch (preset)
                    {
                        case "245kbps VBR (V0)":
                            lameProcess.StartInfo.Arguments += " -V 0";
                            break;
                        case "225kbps VBR (V1)":
                            lameProcess.StartInfo.Arguments += " -V 1";
                            break;
                        case "190kbps VBR (V2)":
                            lameProcess.StartInfo.Arguments += " -V 2";
                            break;
                        case "175kbps VBR (V3)":
                            lameProcess.StartInfo.Arguments += " -V 3";
                            break;
                        case "165kbps VBR (V4)":
                            lameProcess.StartInfo.Arguments += " -V 4";
                            break;
                        case "130kbps VBR (V5)":
                            lameProcess.StartInfo.Arguments += " -V 5";
                            break;
                        case "115kbps VBR (V6)":
                            lameProcess.StartInfo.Arguments += " -V 6";
                            break;
                        case "100kbps VBR (V7)":
                            lameProcess.StartInfo.Arguments += " -V 7";
                            break;
                        case "85kbps VBR (V8)":
                            lameProcess.StartInfo.Arguments += " -V 8";
                            break;
                        case "65kbps VBR (V9)":
                            lameProcess.StartInfo.Arguments += " -V 9";
                            break;
                        case "320kbps CBR":
                            lameProcess.StartInfo.Arguments += " -b 320";
                            break;
                        case "256kbps CBR":
                            lameProcess.StartInfo.Arguments += " -b 256";
                            break;
                        case "192kbps CBR":
                            lameProcess.StartInfo.Arguments += " -b 192";
                            break;
                        case "128kbps CBR":
                            lameProcess.StartInfo.Arguments += " -b 128";
                            break;
                        case "64kbps CBR":
                            lameProcess.StartInfo.Arguments += " -b 64";
                            break;
                    }

                    // Add ending arguments
                    lameProcess.StartInfo.Arguments += " \"" + outputWAV + "\" \"" + outputMP3 + "\"";

                    // Start and wait
                    lameProcess.Start();
                    lameProcess.WaitForExit();

                    // Remove .wav files
                    if (File.Exists(outputWAV))
                    {
                        File.Delete(outputWAV);
                    }

                    // Add resultant file to output file list
                    outputFiles.Add(outputPath + parsedFileSyntax + ".mp3");

                    // Map out the resultant .mp3 file in preparation to migrate tags back
                    TagLib.File outputTagFile = TagLib.File.Create(outputMP3);
                    TagLib.Id3v2.Tag outputTagMap = (TagLib.Id3v2.Tag)outputTagFile.GetTag(TagLib.TagTypes.Id3v2);

                    // Lists to hold multi-value tags
                    List<string> albumArtistList = new List<string>();
                    List<string> composerList = new List<string>();
                    List<string> genreList = new List<string>();
                    List<string> performerList = new List<string>();

                    // Migrate standard tags from the flac's taglist to the mp3's tag map
                    for (int i = 0; i < pendingTagNames.Count(); i++)
                    {
                        // Determine what the current tag should be mapped to
                        switch (pendingTagNames[i].ToLower())
                        {
                            case "album":
                                outputTagFile.Tag.Album = pendingTagData[i];
                                break;
                            case "albumsort":
                                outputTagFile.Tag.AlbumSort = pendingTagData[i];
                                break;
                            case "albumartist":
                            case "album artist":
                                albumArtistList.Add(pendingTagData[i]);
                                break;
                            case "beatsperminute":
                            case "bpm":
                                outputTagFile.Tag.BeatsPerMinute = Convert.ToUInt32(pendingTagData[i]);
                                break;
                            case "description":
                            case "comment":
                                outputTagFile.Tag.Comment = pendingTagData[i];
                                break;
                            case "composer":
                                composerList.Add(pendingTagData[i]);
                                break;
                            case "copyright":
                                outputTagFile.Tag.Copyright = pendingTagData[i];
                                break;
                            case "conductor":
                                outputTagFile.Tag.Conductor = pendingTagData[i];
                                break;
                            case "disc":
                            case "discnumber":
                                outputTagFile.Tag.Disc = Convert.ToUInt32(pendingTagData[i]);
                                break;
                            case "disccount":
                                outputTagFile.Tag.DiscCount = Convert.ToUInt32(pendingTagData[i]);
                                break;
                            case "genre":
                                genreList.Add(pendingTagData[i]);
                                break;
                            case "grouping":
                                outputTagFile.Tag.Grouping = pendingTagData[i];
                                break;
                            case "lyrics":
                                outputTagFile.Tag.Lyrics = pendingTagData[i];
                                break;
                            case "musicbrainzartistid":
                                outputTagFile.Tag.MusicBrainzArtistId = pendingTagData[i];
                                break;
                            case "musicbrainzdiscid":
                                outputTagFile.Tag.MusicBrainzDiscId = pendingTagData[i];
                                break;
                            case "musicbrainzreleaseartistid":
                                outputTagFile.Tag.MusicBrainzReleaseArtistId = pendingTagData[i];
                                break;
                            case "musicbrainzreleasecountry":
                                outputTagFile.Tag.MusicBrainzReleaseCountry = pendingTagData[i];
                                break;
                            case "musicbrainzreleaseid":
                                outputTagFile.Tag.MusicBrainzReleaseId = pendingTagData[i];
                                break;
                            case "musicbrainzreleasestatus":
                                outputTagFile.Tag.MusicBrainzReleaseStatus = pendingTagData[i];
                                break;
                            case "musicbrainzreleasetype":
                                outputTagFile.Tag.MusicBrainzReleaseType = pendingTagData[i];
                                break;
                            case "musicbrainztrackid":
                                outputTagFile.Tag.MusicBrainzTrackId = pendingTagData[i];
                                break;
                            case "musicipid":
                                outputTagFile.Tag.MusicIpId = pendingTagData[i];
                                break;
                            case "performer":
                            case "artist":
                                performerList.Add(pendingTagData[i]);
                                break;
                            case "title":
                                outputTagFile.Tag.Title = pendingTagData[i];
                                break;
                            case "titlesort":
                                outputTagFile.Tag.TitleSort = pendingTagData[i];
                                break;
                            case "track":
                            case "tracknumber":
                                outputTagFile.Tag.Track = Convert.ToUInt32(pendingTagData[i]);
                                break;
                            case "trackcount":
                                outputTagFile.Tag.TrackCount = Convert.ToUInt32(pendingTagData[i]);
                                break;
                            case "year":
                            case "date":
                                outputTagFile.Tag.Year = Convert.ToUInt32(pendingTagData[i]);
                                break;
                            // If none of the above match, it's a custom field
                            default:
                                // Add the custom tag data to a special string array (required to be able to set it to tFrame.Text)
                                String[] customTagText = { pendingTagData[i] };

                                // Creates an empty frame for the custom tag
                                TagLib.Id3v2.UserTextInformationFrame tFrame = TagLib.Id3v2.UserTextInformationFrame.Get(outputTagMap, pendingTagNames[i], true);

                                // Set frame's data to our custom data
                                tFrame.Text = customTagText;

                                // Replaces empty frame with our own frame
                                outputTagMap.ReplaceFrame(TagLib.Id3v2.UserTextInformationFrame.Get(outputTagMap, pendingTagNames[i], false), tFrame);
                                break;
                        }
                    }

                    // Migrate pictures from .flac to .mp3
                    if (tagFile.Tag.Pictures != null)
                    {
                        outputTagFile.Tag.Pictures = tagFile.Tag.Pictures;
                    }

                    // Migrate string arrays
                    outputTagFile.Tag.AlbumArtists = albumArtistList.ToArray();
                    outputTagFile.Tag.Composers = composerList.ToArray();
                    outputTagFile.Tag.Genres = genreList.ToArray();
                    outputTagFile.Tag.Performers = performerList.ToArray();

                    // Save tags to file
                    outputTagFile.Save();
                });
            }

            // Opus Conversion
            else if (codec == "Opus")
            {
                Parallel.ForEach(inputFLACs, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (currentFLAC) => {
                    string parsedFileSyntax = ParseNamingSyntax(syntax, codec, preset, currentFLAC);
                    string parsedFolderSyntax = "";
                    // If the path denotes there is a folder
                    if (parsedFileSyntax.LastIndexOf('\\') != -1)
                    {
                        parsedFolderSyntax = parsedFileSyntax.Substring(0, parsedFileSyntax.LastIndexOf('\\'));
                    }

                    // Output Path+Name of future file
                    string outputOpus = outputPath + parsedFileSyntax + ".opus";
                    // Create directory
                    Directory.CreateDirectory(outputPath + parsedFolderSyntax);

                    // Initialize opusenc.exe
                    System.Diagnostics.Process opusProcess = new System.Diagnostics.Process();
                    opusProcess.StartInfo.FileName = Settings.Default.OpusLocation;
                    opusProcess.StartInfo.UseShellExecute = false;
                    opusProcess.StartInfo.CreateNoWindow = true;

                    // Add initial arguments (quiet mode)
                    opusProcess.StartInfo.Arguments = "--quiet";

                    // Determine which preset to use
                    switch (preset)
                    {
                        case "192kbps VBR":
                            opusProcess.StartInfo.Arguments += " --bitrate 192 --vbr";
                            break;
                        case "160kbps VBR":
                            opusProcess.StartInfo.Arguments += " --bitrate 160 --vbr";
                            break;
                        case "128kbps VBR":
                            opusProcess.StartInfo.Arguments += " --bitrate 128 --vbr";
                            break;
                        case "96kbps VBR":
                            opusProcess.StartInfo.Arguments += " --bitrate 96 --vbr";
                            break;
                        case "64kbps VBR":
                            opusProcess.StartInfo.Arguments += " --bitrate 64 --vbr";
                            break;
                        case "32kbps VBR":
                            opusProcess.StartInfo.Arguments += " --bitrate 32 --vbr";
                            break;
                    }

                    // Add ending arguments
                    opusProcess.StartInfo.Arguments += " --ignorelength \"" + currentFLAC + "\" \"" + outputOpus + "\"";

                    // Start and wait
                    opusProcess.Start();
                    opusProcess.WaitForExit();

                    // Add resultant file to output file list
                    outputFiles.Add(outputPath + parsedFileSyntax + ".opus");
                });
            }

            return outputFiles;
        }

        // Calculates ReplayGain on a list of input files.
        // Uses the input list as a single album in album mode.
        private void CalculateReplayGain(List<string> inputFLACs)
        {
            // Sort the inputFLACs
            inputFLACs.Sort();

            // Initialize bs1770gain.exe
            System.Diagnostics.Process replayGainProcess = new System.Diagnostics.Process();
            // Only use the 64-bit version of bs1770gain on 64-bit systems
            if (Environment.Is64BitOperatingSystem)
            {
                replayGainProcess.StartInfo.FileName = @"Redist\bs1770gain64\bs1770gain.exe";
            }
            else
            {
                replayGainProcess.StartInfo.FileName = @"Redist\bs1770gain86\bs1770gain.exe";
            }
            replayGainProcess.StartInfo.UseShellExecute = false;
            replayGainProcess.StartInfo.CreateNoWindow = true;

            // Initialize commands onto bs1770gain
            // --ebu: sets reference loudness to -23.00 dB
            // -t: calculates true peaks
            // --unit=dB: sets measurement unit to dB
            // --format flac: force output format to flac (if this option isn't set, the presence of cover art will trigger an mkv container instead)
            replayGainProcess.StartInfo.Arguments = "--ebu -t --unit=dB --format flac";
            // Add each input file into the argument string
            foreach (string currentFLAC in inputFLACs)
            {
                replayGainProcess.StartInfo.Arguments += " \"" + currentFLAC + "\"";
            }

            // Set the output directory to .\R128
            replayGainProcess.StartInfo.Arguments += " -f \"" + Path.GetDirectoryName(inputFLACs[0]) + "\\TempReplayGainData.txt\"";
            // Start and wait
            replayGainProcess.Start();
            replayGainProcess.WaitForExit();

            // Read in the resultant text file
            using (StreamReader reader = File.OpenText(Path.GetDirectoryName(inputFLACs[0]) + "\\TempReplayGainData.txt"))
            {
                // String to hold current parsed line
                string line = "";

                // Skip first line "analyzing ..."
                reader.ReadLine();

                // List to split up elements of a parsed line into
                List<string> elements = new List<string>();

                foreach (string currentFLAC in inputFLACs)
                {
                    // Tag containers for the input file
                    TagLib.File originTagFile = TagLib.File.Create(currentFLAC);
                    TagLib.Ogg.XiphComment originTagMap = (TagLib.Ogg.XiphComment)originTagFile.GetTag(TagLib.TagTypes.Xiph);

                    // Skip identifying track line.
                    reader.ReadLine();
                    // Read in first line (integrated)
                    line = reader.ReadLine();
                    // Separate elements based on whitespace
                    elements.Clear();
                    elements.AddRange(line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries));
                    // Copy relevant ReplayGain tags from the file we generated back into our original input file
                    originTagMap.SetField("REPLAYGAIN_TRACK_GAIN", elements[4] + " dB");
                    // Read in next line (true peak)
                    line = reader.ReadLine();
                    // Separate elements based on whitespace
                    elements.Clear();
                    elements.AddRange(line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries));
                    originTagMap.SetField("REPLAYGAIN_TRACK_PEAK", elements[5]);
                    // We manually insert the correct reference loudness, which needs to be correct for Opus's RG calculation (matches other scanners' format as well)
                    // Formula to get this number is "107 dB + Reference Loudness." In our case we are using EBU reference loudness which is -23.00 dB.
                    originTagMap.SetField("REPLAYGAIN_REFERENCE_LOUDNESS", "84.00 dB");

                    // Save changes to original file
                    originTagFile.Save();
                }

                // Variables to hold the parsed album gain and peak, as we're going to apply it to multiple files
                double albumGain = 0;
                double albumPeak = 0;

                // Skip identifying "[ALBUM]:" line.
                reader.ReadLine();
                // Read in first line (integrated)
                line = reader.ReadLine();
                // Separate elements based on whitespace
                elements.Clear();
                elements.AddRange(line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries));
                // Set album gain variable
                albumGain = double.Parse(elements[4]);
                // Read in next line (true peak)
                line = reader.ReadLine();
                // Separate elements based on whitespace
                elements.Clear();
                elements.AddRange(line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries));
                // Set album peak variable
                albumPeak = double.Parse(elements[5]);

                foreach (string currentFLAC in inputFLACs)
                {
                    // Tag containers for the input file
                    TagLib.File originTagFile = TagLib.File.Create(currentFLAC);
                    TagLib.Ogg.XiphComment originTagMap = (TagLib.Ogg.XiphComment)originTagFile.GetTag(TagLib.TagTypes.Xiph);

                    // Copy relevant ReplayGain tags from the file we generated back into our original input file
                    originTagMap.SetField("REPLAYGAIN_ALBUM_GAIN", albumGain.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " dB");
                    originTagMap.SetField("REPLAYGAIN_ALBUM_PEAK", albumPeak.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture));

                    // Save changes to original file
                    originTagFile.Save();
                }
            }

            // Delete temporary directory and files within
            if (File.Exists(Path.GetDirectoryName(inputFLACs[0]) + "\\TempReplayGainData.txt"))
            {
                try
                {
                    File.Delete(Path.GetDirectoryName(inputFLACs[0]) + "\\TempReplayGainData.txt");
                }
                catch (IOException)
                {
                    MessageBox.Show("TempReplayGainData.txt is open and cannot be deleted. Delete manually.");
                }
            }

            return;
        }

        // Conversion work
        private void ConvertBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Arguments passed into the worker
            List<object> inputArguments = e.Argument as List<object>;
            string inputPath = (string)inputArguments[0];
            string tempPath = (string)inputArguments[1];
            string outputPath = (string)inputArguments[2];
            string syntaxInput = (string)inputArguments[3];
            bool RGEnabled = (bool)inputArguments[4];
            bool copyFileTypesEnabled = (bool)inputArguments[5];
            string copyFileTypes = (string)inputArguments[6];
            bool renameLogCueEnabled = (bool)inputArguments[7];
            bool stripImagesEnabled = (bool)inputArguments[8];
            bool addToExcel = (bool)inputArguments[9];
            string excelLogScore = (string)inputArguments[10];
            string excelNotes = (string)inputArguments[11];
            bool deleteTempEnabled = (bool)inputArguments[12];
            bool openFolderEnabled = (bool)inputArguments[13];
            string codecInput = (string)inputArguments[14];
            string presetInput = (string)inputArguments[15];

            // List of input files
            List<string> inputFLACs = new List<string>();
            inputFLACs.AddRange(GetRecursiveFilesSafe(tempPath, "*.flac"));

            if (inputFLACs.Count() == 0)
            {
                MessageBox.Show("No valid files to convert.");
                e.Cancel = true;
                return;
            }

            // Future list of output files
            List<string> outputFiles = new List<string>();

            // Future list of copied files (if enabled)
            List<string> copiedFiles = new List<string>();

            // If the codec is FLAC, calculate ReplayGain after we convert.
            // Resampling and reducing bit depth will affect audio data and thus ReplayGain, so it needs to be calculated afterwards
            if (codecInput == "FLAC")
            {
                // Send the necessary info to the conversion function and get back a list of converted files
                outputFiles = ConvertToFormat(inputFLACs, outputPath, syntaxInput, codecInput, presetInput);

                if (RGEnabled)
                {
                    CalculateReplayGain(outputFiles);
                }
            }
            // Else if a file is lossy, calculate ReplayGain before we convert.
            // MP3 and Opus both use their parent FLAC's ReplayGain data to calculate their own ReplayGain so it needs to be calculated for the parent before conversion
            else
            {
                if (RGEnabled)
                {
                    CalculateReplayGain(inputFLACs);
                }

                // Send the necessary info to the conversion function and get back a list of converted files
                outputFiles = ConvertToFormat(inputFLACs, outputPath, syntaxInput, codecInput, presetInput);
            }

            // Name of the folder that has been converted to, aka "parsedFolderSyntax"
            string lastFolder = new DirectoryInfo(Path.GetDirectoryName(outputFiles[0])).Name;

            // Full path of the output folder
            string outputFolder = NormalizePath(Path.GetDirectoryName(outputFiles[0]));

            // Used partially in guesswork, pulls data from first .flac file
            TagLib.File tempTagFile = TagLib.File.Create(inputFLACs[0]);
            TagLib.Ogg.XiphComment tempTagMap = (TagLib.Ogg.XiphComment)tempTagFile.GetTag(TagLib.TagTypes.Xiph);
            string artist = "";
            string album = "";

            // Gets albumartist (or artist if albumartist is missing) and album off of first file and store in a string for later use
            if (tempTagMap.GetFirstField("albumartist") != null)
            {
                artist = CleanString(tempTagMap.GetFirstField("albumartist"));
            }
            else if (tempTagMap.GetFirstField("album artist") != null)
            {
                artist = CleanString(tempTagMap.GetFirstField("album artist"));
            }
            else if (tempTagMap.GetFirstField("artist") != null)
            {
                artist = CleanString(tempTagMap.GetFirstField("artist"));
            }

            if (tempTagMap.GetFirstField("album") != null)
            {
                album = CleanString(tempTagMap.GetFirstField("album"));
            }

            // If copying files is enabled
            if (copyFileTypesEnabled == true)
            {
                // If the copy syntax isn't default or empty
                if (copyFileTypes != "e.g. *.jpg; *.log; *.cue; *.pdf" && copyFileTypes != "")
                {
                    // Begin recursive copy function
                    copiedFiles.AddRange(RecursiveFolderCopy(tempPath, outputFolder, copyFileTypes, true));
                }
                else
                {
                    MessageBox.Show("Copy files enabled but no filetypes specified.");
                }
            }

            if (renameLogCueEnabled == true)
            {
                RenameLogCue(copiedFiles, outputFolder, artist, album);
            }

            if (stripImagesEnabled == true)
            {
                // Pass the copiedFiles list into the StripImages function
                StripImages(copiedFiles);
            }

            if (deleteTempEnabled == true)
            {
                // Delete temp path recursively
                if (tempPath != outputFolder && Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }

            if (addToExcel == true)
            {
                AddToExcel(lastFolder, excelLogScore, excelNotes);
            }

            if (openFolderEnabled == true)
            {
                // Opens path in explorer
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = outputFolder,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }

            // List of arguments to pass into the completed event
            List<object> outputArguments = new List<object>();
            outputArguments.Add(inputPath);
            outputArguments.Add(tempPath);
            outputArguments.Add(DeleteTempFolderCheckbox.Checked);

            // Report finished and pass the arguments to the UI thread
            e.Result = outputArguments;
            ConvertBackgroundWorker.ReportProgress(100, e.Result);
        }

        private void InputPathBox_DragEnter(object sender, DragEventArgs e)
        {
            // Allow drag+dropping of folders into text box
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void InputPathBox_DragDrop(object sender, DragEventArgs e)
        {
            // Allow drag+dropping of folders into text box
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                InputPathBox.Lines = fileNames;
                InputPathBox.ForeColor = Color.Black;
            }
        }

        private void OutputPathTextBox_DragEnter(object sender, DragEventArgs e)
        {
            // Allow drag+dropping of folders into text box
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OutputPathTextBox_DragDrop(object sender, DragEventArgs e)
        {
            // Allow drag+dropping of folders into text box
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                OutputPathTextBox.Lines = fileNames;
                OutputPathTextBox.ForeColor = Color.Black;
            }
        }

        private void InputPathBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (InputPathBox.Text == "Input Folder")
            {
                InputPathBox.Text = "";
                InputPathBox.ForeColor = Color.Black;
            }
        }

        private void InputPathBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (InputPathBox.Text == "")
            {
                InputPathBox.Text = "Input Folder";
                InputPathBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void InputPathButton_Click(object sender, EventArgs e)
        {
            // Folder picker
            CommonOpenFileDialog fbd = new CommonOpenFileDialog();

            if (InputPathBox.Text != "Input Folder" && InputPathBox.Text != "" && Directory.Exists(InputPathBox.Text))
            {
                fbd.InitialDirectory = InputPathBox.Text;
            }
            else
            {
                fbd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            }

            fbd.IsFolderPicker = true;
            if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                InputPathBox.Text = fbd.FileName;
                InputPathBox.ForeColor = Color.Black;
            }
        }

        private void InputExplorerButton_Click(object sender, EventArgs e)
        {
            // Checks if path is valid before opening
            if (Directory.Exists(InputPathBox.Text))
            {
                // Opens path in explorer
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = InputPathBox.Text,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }

        private void TempPathBox_DragEnter(object sender, DragEventArgs e)
        {
            // Allow drag+dropping of folders into text box
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TempPathBox_DragDrop(object sender, DragEventArgs e)
        {
            // Allow drag+dropping of folders into text box
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                TempPathBox.Lines = fileNames;
                TempPathBox.ForeColor = Color.Black;
            }
        }

        private void TempPathBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (TempPathBox.Text == "Temp Folder")
            {
                TempPathBox.Text = "";
                TempPathBox.ForeColor = Color.Black;
            }
        }

        private void TempPathBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (TempPathBox.Text == "")
            {
                TempPathBox.Text = "Temp Folder";
                TempPathBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void TempPathButton_Click(object sender, EventArgs e)
        {
            // Folder picker
            CommonOpenFileDialog fbd = new CommonOpenFileDialog();

            if (TempPathBox.Text != "Temp Folder" && TempPathBox.Text != "" && Directory.Exists(TempPathBox.Text))
            {
                fbd.InitialDirectory = TempPathBox.Text;
            }
            else
            {
                fbd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            }

            fbd.IsFolderPicker = true;
            if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                TempPathBox.Text = fbd.FileName;
                TempPathBox.ForeColor = Color.Black;

                // Perform a metadata guess
                GuessButton.PerformClick();
            }

        }

        private void TempExplorerButton_Click(object sender, EventArgs e)
        {
            // Checks if path is valid before opening
            if (Directory.Exists(TempPathBox.Text))
            {
                // Opens path in explorer
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = TempPathBox.Text,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }

        private void ArtistTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (ArtistTextBox.Text == "Artist")
            {
                ArtistTextBox.Text = "";
                ArtistTextBox.ForeColor = Color.Black;
            }
        }

        private void ArtistTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (ArtistTextBox.Text == "")
            {
                ArtistTextBox.Text = "Artist";
                ArtistTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void AlbumTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (AlbumTextBox.Text == "Album")
            {
                AlbumTextBox.Text = "";
                AlbumTextBox.ForeColor = Color.Black;
            }
        }

        private void AlbumTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (AlbumTextBox.Text == "")
            {
                AlbumTextBox.Text = "Album";
                AlbumTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void DiscogsButton_Click(object sender, EventArgs e)
        {
            // If artist and album textboxes are not empty
            if (ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "" && AlbumTextBox.Text != "Album" && AlbumTextBox.Text != "")
            {
                System.Diagnostics.Process.Start("https://www.discogs.com/search/?type=release&title=" + AlbumTextBox.Text + "&artist=" + ArtistTextBox.Text);
            }
            // If only artist is not empty
            else if (ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "")
            {
                System.Diagnostics.Process.Start("https://www.discogs.com/search/?q=" + ArtistTextBox.Text + "&type=artist");
            }
            // If only album is not empty
            else if (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != "")
            {
                System.Diagnostics.Process.Start("https://www.discogs.com/search/?q=" + AlbumTextBox.Text + "&type=release");
            }
            else
            {
                MessageBox.Show("No artist and/or album specified.");
            }
        }

        private void MusicBrainzButton_Click(object sender, EventArgs e)
        {
            // If artist and album textboxes are not empty
            if ((ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "") && (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != ""))
            {
                System.Diagnostics.Process.Start("https://musicbrainz.org/taglookup?tag-lookup.artist=" + ArtistTextBox.Text + "&tag-lookup.release=" + AlbumTextBox.Text);
            }
            // If only artist is not empty
            else if (ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "")
            {
                System.Diagnostics.Process.Start("https://musicbrainz.org/search?query=" + ArtistTextBox.Text + "&type=artist");
            }
            // If only album is not empty
            else if (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != "")
            {
                System.Diagnostics.Process.Start("https://musicbrainz.org/search?query=" + AlbumTextBox.Text + "&type=release");
            }
            else
            {
                MessageBox.Show("No artist and/or album specified.");
            }
        }

        private void RedactedButton_Click(object sender, EventArgs e)
        {
            // If artist and album textboxes are not empty
            if ((ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "") && (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != ""))
            {
                System.Diagnostics.Process.Start("https://redacted.ch/torrents.php?artistname=" + ArtistTextBox.Text + "&groupname=" + AlbumTextBox.Text + "&order_by=seeders&order_way=desc&group_results=1&filter_cat[1]=1&action=basic&searchsubmit=1");
            }
            // If only artist is not empty
            else if (ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "")
            {
                System.Diagnostics.Process.Start("https://redacted.ch/artist.php?artistname=" + ArtistTextBox.Text);
            }
            // If only album is not empty
            else if (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != "")
            {
                System.Diagnostics.Process.Start("https://redacted.ch/artist.php?groupname=" + AlbumTextBox.Text + " &order_by=seeders&order_way=desc&group_results=1&filter_cat[1]=1&action=basic&searchsubmit=1");
            }
            else
            {
                MessageBox.Show("No artist and/or album specified.");
            }
        }

        private void AADButton_Click(object sender, EventArgs e)
        {
            // If the tempPathBox doesn't end with "\", add "\" to it
            string tempPath = NormalizePath(TempPathBox.Text);

            // If artist and album textboxes are not empty
            if ((ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "") && (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != ""))
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.FileName = Settings.Default.AADLocation;
                startInfo.Arguments = "-ar \"" + ArtistTextBox.Text + "\" -al \"" + AlbumTextBox.Text + "\" -p \"" + tempPath + "folder.%extension%\"";
                System.Diagnostics.Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show("Artist or album is empty.");
            }
        }

        private void Mp3tagButton_Click(object sender, EventArgs e)
        {
            if (TempPathBox.Text != "Temp Path" && TempPathBox.Text != "")
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.FileName = Settings.Default.Mp3tagLocation;
                startInfo.Arguments = "-fp:\"" + TempPathBox.Text + "\"";
                System.Diagnostics.Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show("No temp path specified.");
            }
        }

        private void ExcelLogScoreTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (ExcelLogScoreTextBox.Text == "Log score")
            {
                ExcelLogScoreTextBox.Text = "";
                ExcelLogScoreTextBox.ForeColor = Color.Black;
            }
        }

        private void ExcelLogScoreTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (ExcelLogScoreTextBox.Text == "")
            {
                ExcelLogScoreTextBox.Text = "Log score";
                ExcelLogScoreTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void ExcelNotesTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (ExcelNotesTextBox.Text == "Notes")
            {
                ExcelNotesTextBox.Text = "";
                ExcelNotesTextBox.ForeColor = Color.Black;
            }
        }

        private void ExcelNotesTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (ExcelNotesTextBox.Text == "")
            {
                ExcelNotesTextBox.Text = "Notes";
                ExcelNotesTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            // Input validation
            if (!Directory.Exists(TempPathBox.Text))
            {
                MessageBox.Show("Temp folder does not exist.");
                return;
            }
            else if (!Directory.Exists(OutputPathTextBox.Text))
            {
                MessageBox.Show("Output folder does not exist.");
                return;
            }
            else if (OutputNamingSyntaxTextBox.Text == "Output Folder+File Name (syntax in tooltip)" || OutputNamingSyntaxTextBox.Text == "")
            {
                MessageBox.Show("Naming syntax is blank.");
                return;
            }
            else if (ConvertToComboBox.Text == "" || PresetComboBox.Text == "")
            {
                MessageBox.Show("Conversion format is invalid.");
                return;
            }

            // Make sure the user actually wants to use the default temp folder, and didn't just misclick
            if (TempPathBox.Text == Settings.Default.DefaultTemp)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to convert using the default temp folder?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // Disable button to denote process is executing
            ConvertButton.Text = "Processing...";
            ConvertButton.Enabled = false;

            // Add arguments to send to worker process
            List<object> outputArguments = new List<object>();
            // If any of the paths don't end with "\", add "\" to them
            outputArguments.Add(NormalizePath(InputPathBox.Text));
            outputArguments.Add(NormalizePath(TempPathBox.Text));
            outputArguments.Add(NormalizePath(OutputPathTextBox.Text));

            outputArguments.Add(OutputNamingSyntaxTextBox.Text);
            outputArguments.Add(ReplayGainCheckbox.Checked);
            outputArguments.Add(CopyContentsCheckbox.Checked);
            outputArguments.Add(CopyFileTypesTextBox.Text);
            outputArguments.Add(RenameLogCueCheckbox.Checked);
            outputArguments.Add(StripImageMetadataCheckbox.Checked);
            outputArguments.Add(ExcelExportCheckbox.Checked);
            outputArguments.Add(ExcelLogScoreTextBox.Text);
            outputArguments.Add(ExcelNotesTextBox.Text);
            outputArguments.Add(DeleteTempFolderCheckbox.Checked);
            outputArguments.Add(ConvertOpenFolderCheckbox.Checked);
            outputArguments.Add(ConvertToComboBox.Text);
            outputArguments.Add(PresetComboBox.Text);

            if (!ConvertBackgroundWorker.IsBusy)
            {
                ConvertBackgroundWorker.RunWorkerAsync(outputArguments);
            }
            else
            {
                MessageBox.Show("Already running a conversion operation.");
            }
        }

        private void OutputNamingSyntaxTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (OutputNamingSyntaxTextBox.Text == "Output Folder+File Name (syntax in tooltip)")
            {
                OutputNamingSyntaxTextBox.Text = "";
                OutputNamingSyntaxTextBox.ForeColor = Color.Black;
            }
        }

        private void OutputNamingSyntaxTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (OutputNamingSyntaxTextBox.Text == "")
            {
                OutputNamingSyntaxTextBox.Text = "Output Folder+File Name (syntax in tooltip)";
                OutputNamingSyntaxTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void OutputPathTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (OutputPathTextBox.Text == "Output Folder (base path)")
            {
                OutputPathTextBox.Text = "";
                OutputPathTextBox.ForeColor = Color.Black;
            }
        }

        private void OutputPathTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (OutputPathTextBox.Text == "")
            {
                OutputPathTextBox.Text = "Output Folder (base path)";
                OutputPathTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void OutputExplorerButton_Click(object sender, EventArgs e)
        {
            // Checks if path is valid before opening
            if (Directory.Exists(OutputPathTextBox.Text))
            {
                // Opens path in explorer
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = OutputPathTextBox.Text,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }

        private void OutputPathButton_Click(object sender, EventArgs e)
        {
            // Folder picker
            CommonOpenFileDialog fbd = new CommonOpenFileDialog();

            if (OutputPathTextBox.Text != "Output Folder (base path)" && OutputPathTextBox.Text != "" && Directory.Exists(OutputPathTextBox.Text))
            {
                fbd.InitialDirectory = OutputPathTextBox.Text;
            }
            else
            {
                fbd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            }

            fbd.IsFolderPicker = true;
            if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OutputPathTextBox.Text = fbd.FileName;
                OutputPathTextBox.ForeColor = Color.Black;
            }
        }

        private void ConvertBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Set button back to default to denote process completion
            ConvertButton.Text = "Convert";
            ConvertButton.Enabled = true;

            // If the conversion process was cancelled
            if (e.Cancelled)
            {
                return;
            }

            // Arguments passed into the completed event
            List<object> inputArguments = e.Result as List<object>;
            string inputPath = (string)inputArguments[0];
            string tempPath = (string)inputArguments[1];
            bool sourceFolderDeleted = (bool)inputArguments[2];

            DirectoryInfo parentFolder;

            // Move temppath up one folder if the original folder was deleted, but doesn't move if the user has changed the folder since starting the conversion
            if (sourceFolderDeleted == true)
            {
                parentFolder = Directory.GetParent(Directory.GetParent(tempPath).FullName);
                if (parentFolder != null && Directory.Exists(parentFolder.FullName) && NormalizePath(TempPathBox.Text) == NormalizePath(tempPath))
                {
                    TempPathBox.Text = Directory.GetParent(Directory.GetParent(tempPath).FullName).FullName;
                }

                // Move input path up one folder, but doesn't move if the user has changed the folder since starting the conversion
                if (NormalizePath(InputPathBox.Text) == NormalizePath(inputPath))
                {
                    InputPathBox.Text = Settings.Default.DefaultInput;
                }

                // Empty the artist and album textboxes, assuming the user is done with the source album
                ArtistTextBox.Text = "Artist";
                ArtistTextBox.ForeColor = SystemColors.GrayText;
                AlbumTextBox.Text = "Album";
                AlbumTextBox.ForeColor = SystemColors.GrayText;
            }

            // Empty the log score and note textboxes
            ExcelLogScoreTextBox.Text = "Log score";
            ExcelLogScoreTextBox.ForeColor = SystemColors.GrayText;
            ExcelNotesTextBox.Text = "Notes";
            ExcelNotesTextBox.ForeColor = SystemColors.GrayText;
        }

        private void CopyFileTypesTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (CopyFileTypesTextBox.Text == "e.g. *.jpg; *.log; *.cue; *.pdf")
            {
                CopyFileTypesTextBox.Text = "";
                CopyFileTypesTextBox.ForeColor = Color.Black;
            }
        }

        private void CopyFileTypesTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (CopyFileTypesTextBox.Text == "")
            {
                CopyFileTypesTextBox.Text = "e.g. *.jpg; *.log; *.cue; *.pdf";
                CopyFileTypesTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void SpectrogramsButton_Click(object sender, EventArgs e)
        {
            // Input validation
            if (!Directory.Exists(TempPathBox.Text))
            {
                return;
            }

            // String array of input files
            List<string> inputFLACs = new List<string>();
            inputFLACs.AddRange(GetRecursiveFilesSafe(TempPathBox.Text, "*.flac"));

            // If no flacs are found, return
            if (inputFLACs.Count() == 0)
            {
                return;
            }

            // Initialize spek.exe to generate spectrograms
            System.Diagnostics.Process spekProcess = new System.Diagnostics.Process();
            spekProcess.StartInfo.FileName = Settings.Default.SpekLocation;
            spekProcess.StartInfo.UseShellExecute = false;

            // Opens each .flac file in spek, one at a time and in order
            foreach (string currentFLAC in inputFLACs)
            {
                spekProcess.StartInfo.Arguments = "\"" + currentFLAC + "\"";
                spekProcess.Start();
                spekProcess.WaitForExit();
            }

            // Parallelized version, opens all at once and in random order
            /*Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (currentFile) =>
            {
                // Initialize spek.exe to generate spectrograms
                System.Diagnostics.Process spekProcess = new System.Diagnostics.Process();
                spekProcess.StartInfo.FileName = Settings.Default.SpekLocation;
                spekProcess.StartInfo.Arguments = "\"" + currentFLAC + "\"";
                spekProcess.StartInfo.UseShellExecute = false;
                // Open window maximized; doesn't appear to work for spek
                spekProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                spekProcess.Start();
            }); */
        }

        // Guesses the artist and album from the first flac file
        private void GuessButton_Click(object sender, EventArgs e)
        {
            // Input validation
            if (!Directory.Exists(TempPathBox.Text))
            {
                return;
            }

            // List for future input flacs
            List<string> inputFLACs = new List<string>();

            // Logic for handling whether a directory should be skipped based on if it's a restricted system folder (root folders are allowed even though "system")
            inputFLACs.AddRange(GetRecursiveFilesSafe(TempPathBox.Text, "*.flac"));

            // If no flacs are found, return
            if (inputFLACs.Count() == 0)
            {
                return;
            }

            // Get the tags of the first flac in the list
            TagLib.File tagFile = TagLib.File.Create(inputFLACs[0]);
            TagLib.Ogg.XiphComment tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

            // Parse the tags for artist and album (preferred albumartist, then album artist, then artist)
            if (tagMap.GetFirstField("albumartist") != null)
            {
                ArtistTextBox.Text = tagMap.GetFirstField("albumartist");
                ArtistTextBox.ForeColor = Color.Black;
            }
            else if (tagMap.GetFirstField("album artist") != null)
            {
                ArtistTextBox.Text = tagMap.GetFirstField("album artist");
                ArtistTextBox.ForeColor = Color.Black;
            }
            else if (tagMap.GetFirstField("artist") != null)
            {
                ArtistTextBox.Text = tagMap.GetFirstField("artist");
                ArtistTextBox.ForeColor = Color.Black;
            }

            if (tagMap.GetFirstField("album") != null)
            {
                AlbumTextBox.Text = tagMap.GetFirstField("album");
                AlbumTextBox.ForeColor = Color.Black;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            // if the fromPath or toPath doesn't exist, return
            if (!Directory.Exists(InputPathBox.Text) || !Directory.Exists(TempPathBox.Text))
            {
                return;
            }

            // Make sure the user actually wants to use default input, and didn't just misclick
            if (InputPathBox.Text == Settings.Default.DefaultInput)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to copy the default input folder?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // Disable button to denote process is executing
            CopyButton.Text = "Copying...";
            CopyButton.Enabled = false;

            List<object> outputArguments = new List<object>();
            // If either of the paths don't end with "\", add "\" to them
            outputArguments.Add(NormalizePath(InputPathBox.Text));
            outputArguments.Add(NormalizePath(TempPathBox.Text));
            outputArguments.Add(AutoWavConvertCheckbox.Checked);

            if (!CopyBackgroundWorker.IsBusy)
            {
                // Run background worker
                CopyBackgroundWorker.RunWorkerAsync(outputArguments);
            }
            else
            {
                MessageBox.Show("Already running a copy operation.");
            }
        }

        private void CopyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Arguments passed into the worker
            List<object> inputArguments = e.Argument as List<object>;
            string inputPath = (string)inputArguments[0];
            string tempPath = (string)inputArguments[1];
            bool convertWAVs = (bool)inputArguments[2];

            // Get the name of the last folder in the inputPath
            string lastFolder = new DirectoryInfo(inputPath).Name;

            // Add lastFolder to the tempPath to give the last
            string outputPath = tempPath + lastFolder;

            // Begin recursive copy function
            RecursiveFolderCopy(inputPath, outputPath);

            // Convert .wavs to .flacs after copy
            if (convertWAVs == true)
            {
                // List of input wavs that can be converted
                List<string> inputWAVs = new List<string>();
                inputWAVs.AddRange(GetRecursiveFilesSafe(outputPath, "*.wav"));

                // Parallel convert files
                Parallel.ForEach(inputWAVs, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (currentWAV) => {
                    // Location of the output .flac file, by removing .wav and appending .flac
                    string outputFLAC = currentWAV.Substring(0, currentWAV.LastIndexOf(".wav")) + ".flac";

                    // Initialize flac.exe and convert to the outputFile, using V8 compression.
                    System.Diagnostics.Process flacProcess = new System.Diagnostics.Process();
                    flacProcess.StartInfo.FileName = Settings.Default.FLACLocation;
                    flacProcess.StartInfo.Arguments = "-f -V8 \"" + currentWAV + "\" -o \"" + outputFLAC + "\"";
                    flacProcess.StartInfo.UseShellExecute = false;
                    flacProcess.StartInfo.CreateNoWindow = true;
                    flacProcess.Start();
                    flacProcess.WaitForExit();

                    // Remove input .wav files
                    if (File.Exists(currentWAV))
                    {
                        File.Delete(currentWAV);
                    }
                });
            }

            // List of arguments to pass into the completed event
            List<object> outputArguments = new List<object>();
            outputArguments.Add(outputPath);

            // Report finished and pass the arguments to the UI thread
            e.Result = outputArguments;
            CopyBackgroundWorker.ReportProgress(100, e.Result);
        }

        private void CopyBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Arguments passed into the completed event
            List<object> inputArguments = e.Result as List<object>;
            string outputPath = (string)inputArguments[0];

            // Set button back to default to denote process completion
            CopyButton.Text = "↓ Copy input folder to temp folder ↓";
            CopyButton.Enabled = true;

            // Set TempPathBox to the folder we copied to
            TempPathBox.Text = outputPath;

            // Perform a metadata guess
            GuessButton.PerformClick();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm SettingsFormWindow = new SettingsForm();

            // Open settings form and apply settings if the user accepts it
            if (SettingsFormWindow.ShowDialog() == DialogResult.OK)
            {
                ApplyUserSettings();
            }
        }

        private void CopyContentsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // Enable or disable the file types textbox depending on check-state
            if (CopyContentsCheckbox.Checked == true)
            {
                CopyFileTypesTextBox.Enabled = true;
                RenameLogCueCheckbox.Enabled = true;
                RenameLogCueCheckbox.Checked = Settings.Default.DefaultRenameLogCue;
                StripImageMetadataCheckbox.Enabled = true;
                StripImageMetadataCheckbox.Checked = Settings.Default.DefaultStripImageMetadata;
            }
            else
            {
                CopyFileTypesTextBox.Enabled = false;
                RenameLogCueCheckbox.Enabled = false;
                RenameLogCueCheckbox.Checked = false;
                StripImageMetadataCheckbox.Enabled = false;
                StripImageMetadataCheckbox.Checked = false;
            }
        }

        private void ConvertToComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear presets
            PresetComboBox.Items.Clear();

            if (ConvertToComboBox.Text == "FLAC")
            {
                PresetComboBox.Items.Add("Standard");
                // SoX is required for proper downsampling and dithering
                if (Settings.Default.SoXLocation != "")
                {
                    PresetComboBox.Items.Add("Force 16-bit");
                    PresetComboBox.Items.Add("Force 44.1kHz/48kHz");
                    PresetComboBox.Items.Add("Force 16-bit and 44.1/48kHz");
                }

                // Set to "Standard" by default
                PresetComboBox.Text = "Standard";
            }
            else if (ConvertToComboBox.Text == "MP3")
            {
                PresetComboBox.Items.Add("245kbps VBR (V0)");
                PresetComboBox.Items.Add("225kbps VBR (V1)");
                PresetComboBox.Items.Add("190kbps VBR (V2)");
                PresetComboBox.Items.Add("175kbps VBR (V3)");
                PresetComboBox.Items.Add("165kbps VBR (V4)");
                PresetComboBox.Items.Add("130kbps VBR (V5)");
                PresetComboBox.Items.Add("115kbps VBR (V6)");
                PresetComboBox.Items.Add("100kbps VBR (V7)");
                PresetComboBox.Items.Add("85kbps VBR (V8)");
                PresetComboBox.Items.Add("65kbps VBR (V9)");
                PresetComboBox.Items.Add("320kbps CBR");
                PresetComboBox.Items.Add("256kbps CBR");
                PresetComboBox.Items.Add("192kbps CBR");
                PresetComboBox.Items.Add("128kbps CBR");
                PresetComboBox.Items.Add("64kbps CBR");

                // Set to "245kbps VBR (V0)" by default
                PresetComboBox.Text = "245kbps VBR (V0)";
            }
            else if (ConvertToComboBox.Text == "Opus")
            {
                PresetComboBox.Items.Add("192kbps VBR");
                PresetComboBox.Items.Add("160kbps VBR");
                PresetComboBox.Items.Add("128kbps VBR");
                PresetComboBox.Items.Add("96kbps VBR");
                PresetComboBox.Items.Add("64kbps VBR");
                PresetComboBox.Items.Add("32kbps VBR");

                // Set to "192kbps VBR" by default
                PresetComboBox.Text = "192kbps VBR";
            }
        }

        private void ResetPathsButton_Click(object sender, EventArgs e)
        {
            // Reset the input and temp paths back to default settings
            if (Settings.Default.DefaultInput != "")
            {
                InputPathBox.Text = Settings.Default.DefaultInput;
                InputPathBox.ForeColor = Color.Black;
            }
            else
            {
                InputPathBox.Text = "Input Folder";
                InputPathBox.ForeColor = SystemColors.GrayText;
            }
            if (Settings.Default.DefaultTemp != "")
            {
                TempPathBox.Text = Settings.Default.DefaultTemp;
                TempPathBox.ForeColor = Color.Black;
            }
            else
            {
                TempPathBox.Text = "Temp Folder";
                TempPathBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void InputPathUpOneButton_Click(object sender, EventArgs e)
        {
            if (InputPathBox.Text == "Input Folder" || InputPathBox.Text == "")
            {
                return;
            }

            // Move up one folder if valid
            DirectoryInfo parentFolder = Directory.GetParent(Directory.GetParent(NormalizePath(InputPathBox.Text)).FullName);
            if (parentFolder != null && Directory.Exists(parentFolder.FullName))
            {
                InputPathBox.Text = Directory.GetParent(Directory.GetParent(NormalizePath(InputPathBox.Text)).FullName).FullName;
            }
        }

        private void TempPathUpOneButton_Click(object sender, EventArgs e)
        {
            if (TempPathBox.Text == "Temp Folder" || TempPathBox.Text == "")
            {
                return;
            }

            // Move up one folder if valid
            DirectoryInfo parentFolder = Directory.GetParent(Directory.GetParent(NormalizePath(TempPathBox.Text)).FullName);
            if (parentFolder != null && Directory.Exists(parentFolder.FullName))
            {
                TempPathBox.Text = Directory.GetParent(Directory.GetParent(NormalizePath(TempPathBox.Text)).FullName).FullName;
            }
        }

        private void OutputPathUpOneButton_Click(object sender, EventArgs e)
        {
            if (OutputPathTextBox.Text == "Output Folder (base path)" || OutputPathTextBox.Text == "")
            {
                return;
            }

            // Move up one folder if valid
            DirectoryInfo parentFolder = Directory.GetParent(Directory.GetParent(NormalizePath(OutputPathTextBox.Text)).FullName);
            if (parentFolder != null && Directory.Exists(parentFolder.FullName))
            {
                OutputPathTextBox.Text = Directory.GetParent(Directory.GetParent(NormalizePath(OutputPathTextBox.Text)).FullName).FullName;
            }
        }

        private void TempPathBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If user presses Enter in the tempbox, perform a metadata guess
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                GuessButton.PerformClick();
            }
        }
    }
}
