﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Office.Interop.Excel;
using MusicImportKit.Properties;

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
                RedactedButton.Visible = true;
            else
                RedactedButton.Visible = false;

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

            if (Settings.Default.ExifToolLocation != "")
            {
                StripImageMetadataCheckbox.Text = "Strip image metadata (bmp, gif, jpg, png)";
                if (CopyContentsCheckbox.Checked == true)
                {
                    StripImageMetadataCheckbox.Enabled = true;
                    StripImageMetadataCheckbox.Checked = Settings.Default.DefaultStripImageMetadata;
                }
            }
            else
            {
                StripImageMetadataCheckbox.Text = "Strip image metadata (bmp, gif, jpg, png) (requires exiftool.exe)";
                StripImageMetadataCheckbox.Enabled = false;
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

            // Add FLAC entries if flac.exe detected
            if (Settings.Default.FLACLocation != "")
            {
                AutoWavConvertCheckbox.Checked = Settings.Default.DefaultAutoWAVConvert;
                AutoWavConvertCheckbox.Enabled = true;
                AutoWavConvertCheckbox.Text = "Convert input .wav files to .flac";

                ConvertToComboBox.Items.Add("FLAC");

                // SoX is required for proper downsampling and dithering
                if (Settings.Default.SoXLocation != "")
                {
                    ConvertToComboBox.Items.Add("FLAC (resample to 16-bit (SoX))");
                }
            }
            else
            {
                AutoWavConvertCheckbox.Checked = false;
                AutoWavConvertCheckbox.Enabled = false;
                AutoWavConvertCheckbox.Text = "Convert input .wav files to .flac (requires flac.exe)";
            }

            // Add MP3 entries if lame.exe detected
            if (Settings.Default.LAMELocation != "")
            {
                ConvertToComboBox.Items.Add("MP3 (V2)");
                ConvertToComboBox.Items.Add("MP3 (V0)");
                ConvertToComboBox.Items.Add("MP3 (320 kBps)");
            }

            // Add Opus entries if opusenc.exe detected
            if (Settings.Default.OpusLocation != "")
            {
                ConvertToComboBox.Items.Add("Opus (192 kBps)");
            }

            // Set the user-preferred default
            if (ConvertToComboBox.Items.Contains(Settings.Default.DefaultConvertType) && Settings.Default.DefaultConvertType != "")
            {
                // Set the combobox to the user's preference
                ConvertToComboBox.Text = Settings.Default.DefaultConvertType;

                // Dynamic ReplayGain enable for FLAC, MP3, and Opus
                // FLAC uses metaflac to generate ReplayGain data
                // Opus converts the input FLAC's tags automatically (we metaflac the input FLACs before conversion if enabled)
                // MP3 RG tags can be copied manually (we metaflac the input FLACs before conversion if enabled)
                if (ConvertToComboBox.Text == "FLAC" || ConvertToComboBox.Text == "FLAC (resample to 16-bit (SoX))" ||
                    ConvertToComboBox.Text == "MP3 (V2)" || ConvertToComboBox.Text == "MP3 (V0)" || ConvertToComboBox.Text == "MP3 (320 kBps)" ||
                    ConvertToComboBox.Text == "Opus (192 kBps)")
                {
                    // If metaflac.exe is found, enable ReplayGain
                    if (Settings.Default.MetaFLACLocation != "")
                    {
                        ReplayGainCheckbox.Enabled = true;
                        ReplayGainCheckbox.Checked = Settings.Default.DefaultRG;
                        ReplayGainCheckbox.Text = "Apply ReplayGain";
                    }
                    // Otherwise, disable
                    else
                    {
                        ReplayGainCheckbox.Enabled = false;
                        ReplayGainCheckbox.Checked = false;
                        ReplayGainCheckbox.Text = "Apply ReplayGain (requires metaflac.exe)";
                    }
                }
            }
        }

        private string CleanString(string input)
        {
            if (input == null)
                return input;

            input = input.Replace('*', 'x');
            input = input.Replace('<', '[');
            input = input.Replace('>', ']');
            input = input.Replace('“', '-');
            input = input.Replace('”', '-');

            foreach (char c in Path.GetInvalidFileNameChars())
                input = input.Replace(c, '-');

            return input;
        }

        // Adds a "\" to the end of a path if it doesn't already have one
        private string NormalizePath(string path)
        {
            return path.EndsWith("\\") ? path : path + "\\";
        }

        // Recursively copy a folder's contents into another folder. Selectively copies certain files if 3rd parameter is specified
        private void RecursiveFolderCopy(string fromPath, string toPath, string specificFiletypeText = "", bool renameLogCue = false)
        {
            List<string> pendingFileTypes = new List<string>();
            string originalFiletypeText = specificFiletypeText;
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

            // Creates a directory of toPath + lastFolder, inherently checks if it exists before creation
            Directory.CreateDirectory(toPath);

            // Copy each file in the fromPath
            foreach (string currentFile in Directory.GetFiles(fromPath))
            {
                FileInfo curFileInfo = new FileInfo(currentFile);
                if (specificFiletypeText != "" && currentFile != toPath + curFileInfo.Name)
                {
                    foreach (string currentFileType in pendingFileTypes)
                    {
                        Match match = Regex.Match("^" + curFileInfo.Name + "$", currentFileType);
                        if (match.Success && File.Exists(curFileInfo.FullName))
                            File.Copy(curFileInfo.FullName, toPath + curFileInfo.Name, true);
                    }
                }
                else if (currentFile != toPath + curFileInfo.Name)
                {
                    if(File.Exists(curFileInfo.FullName))
                        File.Copy(curFileInfo.FullName, toPath + curFileInfo.Name, true);
                }
            }

            // Recurse each folder in the fromPath into this function again, so that they may create new folders and copy
            foreach (string currentFolder in Directory.GetDirectories(fromPath))
            {
                DirectoryInfo curFolderInfo = new DirectoryInfo(currentFolder);
                RecursiveFolderCopy(currentFolder, toPath + curFolderInfo.Name, originalFiletypeText);
            }

            return;
        }

        // Parses custom syntax (e.g. %tag% and &codec&) and returns a string based on the metadata/tags of a file
        private string ParseNamingSyntax(string syntax, string convertToInput, string filename, bool folderOnly, int futureBPS = -1, int futureSampleRate = -1)
        {
            string formattedString = "";
            string parsedString = "";
            int nextMarkerIndex = 0;
            string codec = "";
            string bitrate = "";

            // Get tags of (flac) file
            TagLib.File tagFile = TagLib.File.Create(filename);
            var tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

            // Prettier metadata to use for folder/filenames
            if (convertToInput == "FLAC" || convertToInput == "FLAC (resample to 16-bit (SoX))")
            {
                codec = "FLAC";
                bitrate = "Lossless";
            }
            else if (convertToInput == "MP3 (V2)")
            {
                codec = "MP3";
                bitrate = "V2";
            }
            else if (convertToInput == "MP3 (V0)")
            {
                codec = "MP3";
                bitrate = "V0";
            }
            else if (convertToInput == "MP3 (320 kBps)")
            {
                codec = "MP3";
                bitrate = "320";
            }
            else if (convertToInput == "Opus (192 kBps)")
            {
                codec = "Opus";
                bitrate = "192";
            }

            // Move through the input string, matching syntax, pushing its equivalent into a formatted string, then deleting the matched portion of the original and loop
            while (syntax.Length > 0)
            {
                if (syntax[0] == '%')
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
                else if (syntax[0] == '&')
                {
                    // Find the matching ampersand, e.g. &codec"&"
                    nextMarkerIndex = syntax.IndexOf('&', 1);

                    // Bit-depth
                    if (syntax.Substring(1, nextMarkerIndex - 1) == "bps")
                    {
                        // Manual BPS insertion
                        if (futureBPS != -1)
                            formattedString += futureBPS;
                        else
                            formattedString += tagFile.Properties.BitsPerSample.ToString();
                    }

                    // Lossless smartbit, uses bit-depth + "-" + a short sample rate
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "smartbit" && (convertToInput == "FLAC" || convertToInput == "FLAC (resample to 16-bit (SoX))"))
                    {
                        // Manual BPS and sample rate insertion
                        if (futureBPS != -1 && futureSampleRate != -1)
                            formattedString += futureBPS + "-" + futureSampleRate.ToString().Substring(0, 2);
                        else if (futureBPS != -1)
                            formattedString += futureBPS + "-" + tagFile.Properties.AudioSampleRate.ToString().Substring(0, 2);
                        else if (futureSampleRate != -1)
                            formattedString += tagFile.Properties.BitsPerSample.ToString() + "-" + futureSampleRate;
                        else
                            formattedString += tagFile.Properties.BitsPerSample.ToString() + "-" + tagFile.Properties.AudioSampleRate.ToString().Substring(0, 2);
                    }

                    // Lossy smartbit, uses bitrate
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "smartbit")
                        formattedString += bitrate;

                    // Sample rate e.g. 44100, 48000, 96000
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "samplerate")
                    {
                        // Manual sample rate insertion
                        if (futureSampleRate != -1)
                            formattedString += futureSampleRate;
                        else
                            formattedString += tagFile.Properties.AudioSampleRate.ToString();
                    }

                    // Shortened sample rate, e.g. 44, 48, 96
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "short-samplerate")
                    {
                        // Manual sample rate insertion
                        if (futureSampleRate != -1)
                            formattedString += futureSampleRate.ToString().Substring(0, 2);
                        else
                            formattedString += tagFile.Properties.AudioSampleRate.ToString().Substring(0, 2);
                    }

                    // Codec
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "codec")
                        formattedString += codec;

                    // Bitrate
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "bitrate")
                        formattedString += bitrate;

                    // Padded track number logic
                    else if (syntax.Substring(1, nextMarkerIndex - 1).ToLower() == "paddedtracknumber")
                    {
                        // Retrieve parsed tag from the file, and clean it of illegal characters on the way
                        parsedString = CleanString(tagMap.GetFirstField("tracknumber"));

                        // Pad the track number if it's 1-9
                        if (Int32.Parse(parsedString) >= 1 && Int32.Parse(parsedString) <= 9)
                            parsedString = parsedString.TrimStart('0').Insert(0, "0");

                        // If 0, set to 00
                        else if (Int32.Parse(parsedString) == 0)
                            parsedString = "00";

                        formattedString += parsedString;
                    }

                    // Remove the matched portion from the input string
                    syntax = syntax.Remove(0, nextMarkerIndex + 1);
                }
                else
                {
                    // Remove spaces and periods from the end of a folder name; not allowed
                    if (syntax[0] == '\\')
                        formattedString = formattedString.TrimEnd(' ', '.');

                    // Stop when reaching a folder delimiter. This will "remove" the filename from the output path. Only activates when "folderOnly" is passed in as true.
                    if (syntax.LastIndexOf('\\') == 0 && folderOnly)
                        return formattedString;

                    // Pass non-matching characters into the formattedString
                    formattedString += syntax[0];
                    syntax = syntax.Remove(0, 1);
                }
            }

            return formattedString;
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
            string convertToInput = (string)inputArguments[14];

            // Used for disambiguation
            string outputFolder = "";
            string lastFolder = "";
            // Uses the last folder written to in order to feed data to excel
            string parsedFolderStringForExcel = "";

            // List of input files
            List<string> inputFlacs = new List<string>();
            inputFlacs.AddRange(Directory.GetFiles(tempPath, "*.flac", SearchOption.AllDirectories));

            if (inputFlacs.Count() == 0)
            {
                MessageBox.Show("No valid files to convert.");
                return;
            }

            // Used partially in guesswork, pulls data from first .flac file
            TagLib.File tempTagFile = TagLib.File.Create(inputFlacs[0]);
            var tempTagMap = (TagLib.Ogg.XiphComment)tempTagFile.GetTag(TagLib.TagTypes.Xiph);
            string artist = "";
            string album = "";

            // Gets albumartist (or artist if albumartist is missing) and album off of first file and store in a string for later use
            if (tempTagMap.GetFirstField("albumartist") != null)
                artist = tempTagMap.GetFirstField("albumartist");
            else if (tempTagMap.GetFirstField("album artist") != null)
                artist = tempTagMap.GetFirstField("album artist");
            else if (tempTagMap.GetFirstField("artist") != null)
                artist = tempTagMap.GetFirstField("artist");
            if (tempTagMap.GetFirstField("album") != null)
                album = tempTagMap.GetFirstField("album");

            // Future lists of resultant output files. Note that this list will be randomly ordered due to parallelization.
            List<string> outputFiles = new List<string>();

            // Call metaflac.exe
            System.Diagnostics.Process replayGainProcess = new System.Diagnostics.Process();
            // Uses metaflac from user-settings location
            replayGainProcess.StartInfo.FileName = Settings.Default.MetaFLACLocation;
            replayGainProcess.StartInfo.UseShellExecute = false;
            replayGainProcess.StartInfo.CreateNoWindow = true;

            // Strip replaygain from the files
            // Add each input file onto the --remove-replay-gain command
            replayGainProcess.StartInfo.Arguments = "--remove-replay-gain";
            foreach (string currentFlac in inputFlacs)
                replayGainProcess.StartInfo.Arguments += " \"" + currentFlac + "\"";

            replayGainProcess.Start();
            replayGainProcess.WaitForExit();

            // Add ReplayGain if checkbox is checked
            if (RGEnabled == true)
            {
                // Add each input file onto the --add-replay-gain command
                replayGainProcess.StartInfo.Arguments = "--add-replay-gain";
                foreach (string currentFlac in inputFlacs)
                    replayGainProcess.StartInfo.Arguments += " \"" + currentFlac + "\"";

                replayGainProcess.Start();
                replayGainProcess.WaitForExit();

                /* Fully working snippet that applies ReplayGain to albums based on their tags, but other program functions do not work well with multiple album inputs, so pointless
                if (RGType == "Scan as albums (by tags)")
                {
                    // List to eventually track which albums we will be scanning for
                    List<string> pendingAlbumNames = new List<string>();

                    // Check every converted file's album tag
                    foreach (string currentFlac in inputFlacs)
                    {
                        TagLib.File tagFile = TagLib.File.Create(currentFlac);
                        var tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

                        // If we aren't tracking the album name yet, add it to the list
                        if (!pendingAlbumNames.Contains(tagMap.GetFirstField("album")))
                            pendingAlbumNames.Add(tagMap.GetFirstField("album"));
                    }

                    Parallel.ForEach(pendingAlbumNames, (currentAlbum) =>
                    {
                        System.Diagnostics.Process replayGainProcessParallel = new System.Diagnostics.Process();
                        // Uses metaflac from user-settings location
                        replayGainProcessParallel.StartInfo.FileName = Settings.Default.MetaFLACLocation;
                        replayGainProcessParallel.StartInfo.Arguments = "--add-replay-gain";
                        replayGainProcessParallel.StartInfo.UseShellExecute = false;
                        replayGainProcessParallel.StartInfo.CreateNoWindow = true;

                        foreach (string currentFile in outputFiles)
                        {
                            TagLib.File tagFile = TagLib.File.Create(currentFile);
                            var tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

                            // If currentFile's album matches the album that we're looking for
                            if (tagMap.GetFirstField("album") == currentAlbum)
                            {
                                // Add each output file onto the --add-replay-gain command
                                replayGainProcessParallel.StartInfo.Arguments += " \"" + currentFile + "\"";
                            }
                        }

                        // Start ReplayGain Process
                        replayGainProcessParallel.Start();
                        replayGainProcessParallel.WaitForExit();
                    });

                } */
            }

            // FLAC Conversion
            if (convertToInput == "FLAC" || convertToInput == "FLAC (resample to 16-bit (SoX))")
            {
                // We encode to temp so we can mimick encoding files "in place" without errors caused by input and output file being the same (only relevant for .flac to .flac)
                List<string> outputTempFiles = new List<string>();

                // Parallel convert files
                Parallel.ForEach(inputFlacs, (currentFlac) =>
                {
                    string parsedFolderSyntax = "";
                    string parsedFileSyntax = "";

                    // Get a tagmap of the current file
                    TagLib.File tagFile = TagLib.File.Create(currentFlac);
                    var tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

                    // If resampling is selected and the file actually needs to be resampled
                    if (convertToInput == "FLAC (resample to 16-bit (SoX))" && tagFile.Properties.BitsPerSample == 24)
                    {
                        // Call sox.exe and convert to the output+parsed syntax location, using V8 compression.
                        System.Diagnostics.Process soxProcess = new System.Diagnostics.Process();
                        // Uses sox from user-settings location
                        soxProcess.StartInfo.FileName = Settings.Default.SoXLocation;
                        soxProcess.StartInfo.UseShellExecute = false;
                        soxProcess.StartInfo.CreateNoWindow = true;

                        // If sample rate is a multiple of 44100 kHz, dither to 44100
                        if (tagFile.Properties.AudioSampleRate % 44100 == 0)
                        {
                            // String to hold the parsed syntax so we don't need to calculate it over and over
                            parsedFolderSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, true, 16, 44100);
                            parsedFileSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, false, 16, 44100);
                            // System Temp Path+Name of future file
                            string outputFlac = System.IO.Path.GetTempPath() + parsedFileSyntax + ".flac";

                            // Create directory (automatically checks if it exists or not)
                            Directory.CreateDirectory(System.IO.Path.GetTempPath() + parsedFolderSyntax);
                            Directory.CreateDirectory(outputPath + parsedFolderSyntax);
                            outputFolder = outputPath + parsedFolderSyntax;

                            // If the output folder doesn't end with "\", add "\" to it
                            outputFolder = NormalizePath(outputFolder);

                            // Add arguments to SoX
                            soxProcess.StartInfo.Arguments = "\"" + currentFlac + "\" -G -b 16 \"" + outputFlac + "\" rate -v -L 44100 dither";
                        }
                        // If sample rate is a multiple of 48000 kHz, dither to 48000
                        else if (tagFile.Properties.AudioSampleRate % 48000 == 0)
                        {
                            // String to hold the parsed syntax so we don't need to calculate it over and over
                            parsedFolderSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, true, 16, 48000);
                            parsedFileSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, false, 16, 48000);
                            // System Temp Path+Name of future file
                            string outputFlac = System.IO.Path.GetTempPath() + parsedFileSyntax + ".flac";

                            // Create directory (automatically checks if it exists or not)
                            Directory.CreateDirectory(System.IO.Path.GetTempPath() + parsedFolderSyntax);
                            Directory.CreateDirectory(outputPath + parsedFolderSyntax);
                            outputFolder = outputPath + parsedFolderSyntax;

                            // If the output folder doesn't end with "\", add "\" to it
                            outputFolder = NormalizePath(outputFolder);

                            // Add arguments to SoX
                            soxProcess.StartInfo.Arguments = currentFlac + " -b 16 \"" + outputFlac + "\" rate -v -L 48000 dither";
                        }

                        // Start SoX process
                        soxProcess.Start();
                        soxProcess.WaitForExit();

                        if (tagFile.Properties.AudioSampleRate % 44100 == 0)
                        {
                            // Add resultant file to output file list, mimicking the BPS and samplerate of the future file
                            outputTempFiles.Add(System.IO.Path.GetTempPath() + parsedFileSyntax + ".flac");
                            outputFiles.Add(outputPath + parsedFileSyntax + ".flac");
                        }
                        // If sample rate is a multiple of 48000 kHz, dither to 48000
                        else if (tagFile.Properties.AudioSampleRate % 48000 == 0)
                        {
                            // Add resultant file to output file list, mimicking the BPS and samplerate of the future file
                            outputTempFiles.Add(System.IO.Path.GetTempPath() + parsedFileSyntax + ".flac");
                            outputFiles.Add(outputPath + parsedFileSyntax + ".flac");
                        }
                    }
                    // If resampling isn't needed or not selected: plain FLAC re-encoding
                    else
                    {
                        // String to hold the parsed syntax so we don't need to calculate it over and over
                        parsedFolderSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, true);
                        parsedFileSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, false);
                        // System Temp Path+Name of future file
                        string outputFile = System.IO.Path.GetTempPath() + parsedFileSyntax + ".flac";

                        // Create directory (automatically checks if it exists or not)
                        Directory.CreateDirectory(System.IO.Path.GetTempPath() + parsedFolderSyntax);
                        Directory.CreateDirectory(outputPath + parsedFolderSyntax);
                        outputFolder = outputPath + parsedFolderSyntax;

                        // If the output folder doesn't end with "\", add "\" to it
                        outputFolder = NormalizePath(outputFolder);

                        // Call flac.exe and convert to the output+parsed syntax location, using V8 compression.
                        System.Diagnostics.Process flacProcess = new System.Diagnostics.Process();
                        // Uses flac from user-settings location
                        flacProcess.StartInfo.FileName = Settings.Default.FLACLocation;
                        flacProcess.StartInfo.Arguments = "-f -V8 \"" + currentFlac + "\" -o \"" + outputFile + "\"";
                        flacProcess.StartInfo.UseShellExecute = false;
                        flacProcess.StartInfo.CreateNoWindow = true;
                        flacProcess.Start();
                        flacProcess.WaitForExit();

                        // Add resultant file to output file list
                        outputTempFiles.Add(System.IO.Path.GetTempPath() + parsedFileSyntax + ".flac");
                        outputFiles.Add(outputPath + parsedFileSyntax + ".flac");
                    }

                    // Set Excel's folder data to this file's parsed syntax
                    parsedFolderStringForExcel = parsedFolderSyntax;
                });

                // Move encoded files out of system temp folder into output folder
                foreach (string currentFlac in outputTempFiles)
                {
                    string ParsedFileSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, false);
                    if (File.Exists(outputPath + ParsedFileSyntax + ".flac"))
                        File.Delete(outputPath + ParsedFileSyntax + ".flac");
                    if (File.Exists(currentFlac))
                        File.Move(currentFlac, outputPath + ParsedFileSyntax + ".flac");
                }
                // Delete any temp folders
                foreach (string currentFlac in outputFiles)
                {
                    string ParsedFolderSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, true);
                    if (Directory.Exists(System.IO.Path.GetTempPath() + ParsedFolderSyntax))
                        Directory.Delete(System.IO.Path.GetTempPath() + ParsedFolderSyntax);
                }
            }
            // MP3 Conversion
            else if (convertToInput == "MP3 (V2)" || convertToInput == "MP3 (V0)" || convertToInput == "MP3 (320 kBps)")
            {
                Parallel.ForEach(inputFlacs, (currentFlac) =>
                {
                    string parsedFolderSyntax = "";
                    string parsedFileSyntax = "";

                    // Get a tagmap of the current file
                    TagLib.File tagFile = TagLib.File.Create(currentFlac);
                    var tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

                    // Output Path+Name of future file
                    parsedFolderSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, true);
                    parsedFileSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, false);
                    string outputWAV = outputPath + parsedFileSyntax + ".wav";
                    string outputMp3 = outputPath + parsedFileSyntax + ".mp3";

                    // Create directory (automatically checks if it exists or not)
                    Directory.CreateDirectory(outputPath + parsedFolderSyntax);
                    outputFolder = outputPath + parsedFolderSyntax;

                    // If the output folder doesn't end with "\", add "\" to it
                    outputFolder = NormalizePath(outputFolder);

                    // Ending folder of the path
                    lastFolder = new DirectoryInfo(outputFolder).Name;

                    // Get all tags from input .flac and store them
                    List<string> pendingTagNames = new List<string>();
                    List<string> pendingTagData = new List<string>();

                    foreach (string currentField in tagMap)
                    {
                        pendingTagNames.Add(currentField);
                        pendingTagData.Add(tagMap.GetFirstField(currentField));
                    }

                    // Call flac.exe to decode to WAV
                    System.Diagnostics.Process deflacProcess = new System.Diagnostics.Process();
                    // Uses flac from user-settings location
                    deflacProcess.StartInfo.FileName = Settings.Default.FLACLocation;
                    deflacProcess.StartInfo.UseShellExecute = false;
                    deflacProcess.StartInfo.CreateNoWindow = true;
                    deflacProcess.StartInfo.Arguments = "-d \"" + currentFlac + "\" -o \"" + outputWAV + "\"";
                    deflacProcess.Start();
                    deflacProcess.WaitForExit();

                    // Call lame.exe
                    System.Diagnostics.Process lameProcess = new System.Diagnostics.Process();
                    // Uses lame from user-settings location
                    lameProcess.StartInfo.FileName = Settings.Default.LAMELocation;
                    lameProcess.StartInfo.UseShellExecute = false;
                    lameProcess.StartInfo.CreateNoWindow = true;

                    if (convertToInput == "MP3 (V2)")
                    {
                        lameProcess.StartInfo.Arguments = "-h -V 2 \"" + outputWAV + "\" \"" + outputMp3 + "\"";
                    }
                    else if (convertToInput == "MP3 (V0)")
                    {
                        lameProcess.StartInfo.Arguments = "-h -V 0 \"" + outputWAV + "\" \"" + outputMp3 + "\"";
                    }
                    else if (convertToInput == "MP3 (320 kBps)")
                    {
                        lameProcess.StartInfo.Arguments = "-h -b 320 \"" + outputWAV + "\" \"" + outputMp3 + "\"";
                    }

                    lameProcess.Start();
                    lameProcess.WaitForExit();

                    // Remove .wav files
                    if (File.Exists(outputWAV))
                        File.Delete(outputWAV);

                    // Add resultant file to output file list
                    outputFiles.Add(outputPath + parsedFileSyntax + ".mp3");

                    // Map out the resultant .mp3 file in preparation to migrate tags back
                    TagLib.File outputTagFile = TagLib.File.Create(outputMp3);
                    var outputTagMap = (TagLib.Id3v2.Tag)outputTagFile.GetTag(TagLib.TagTypes.Id3v2);

                    // Lists to hold multi-value tags
                    List<string> albumArtistList = new List<string>();
                    List<string> composerList = new List<string>();
                    List<string> genreList = new List<string>();
                    List<string> performerList = new List<string>();

                    // Migrate standard tags from the flac's taglist to the mp3's tag map
                    for (int i = 0; i < pendingTagNames.Count(); i++)
                    {
                        if (pendingTagNames[i].ToLower() == "album")
                            outputTagFile.Tag.Album = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "albumsort")
                            outputTagFile.Tag.AlbumSort = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "albumartist" || pendingTagNames[i].ToLower() == "album artist")
                            albumArtistList.Add(pendingTagData[i]);
                        else if (pendingTagNames[i].ToLower() == "beatsperminute" || pendingTagNames[i].ToLower() == "bpm")
                            outputTagFile.Tag.BeatsPerMinute = Convert.ToUInt32(pendingTagData[i]);
                        else if (pendingTagNames[i].ToLower() == "description" || pendingTagNames[i].ToLower() == "comment")
                            outputTagFile.Tag.Comment = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "composer")
                            composerList.Add(pendingTagData[i]);
                        else if (pendingTagNames[i].ToLower() == "copyright")
                            outputTagFile.Tag.Copyright = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "conductor")
                            outputTagFile.Tag.Conductor = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "disc" || pendingTagNames[i].ToLower() == "discnumber")
                            outputTagFile.Tag.Disc = Convert.ToUInt32(pendingTagData[i]);
                        else if (pendingTagNames[i].ToLower() == "disccount")
                            outputTagFile.Tag.DiscCount = Convert.ToUInt32(pendingTagData[i]);
                        else if (pendingTagNames[i].ToLower() == "genre")
                            genreList.Add(pendingTagData[i]);
                        else if (pendingTagNames[i].ToLower() == "grouping")
                            outputTagFile.Tag.Grouping = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "lyrics")
                            outputTagFile.Tag.Lyrics = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "musicbrainzartistid")
                            outputTagFile.Tag.MusicBrainzArtistId = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "musicbrainzdiscid")
                            outputTagFile.Tag.MusicBrainzDiscId = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "musicbrainzreleaseartistid")
                            outputTagFile.Tag.MusicBrainzReleaseArtistId = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "musicbrainzreleasecountry")
                            outputTagFile.Tag.MusicBrainzReleaseCountry = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "musicbrainzreleaseid")
                            outputTagFile.Tag.MusicBrainzReleaseId = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "musicbrainzreleasestatus")
                            outputTagFile.Tag.MusicBrainzReleaseStatus = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "musicbrainzreleasetype")
                            outputTagFile.Tag.MusicBrainzReleaseType = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "musicbrainztrackid")
                            outputTagFile.Tag.MusicBrainzTrackId = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "musicipid")
                            outputTagFile.Tag.MusicIpId = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "performer" || pendingTagNames[i].ToLower() == "artist")
                            performerList.Add(pendingTagData[i]);
                        else if (pendingTagNames[i].ToLower() == "title")
                            outputTagFile.Tag.Title = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "titlesort")
                            outputTagFile.Tag.TitleSort = pendingTagData[i];
                        else if (pendingTagNames[i].ToLower() == "track" || pendingTagNames[i].ToLower() == "tracknumber")
                            outputTagFile.Tag.Track = Convert.ToUInt32(pendingTagData[i]);
                        else if (pendingTagNames[i].ToLower() == "trackcount")
                            outputTagFile.Tag.TrackCount = Convert.ToUInt32(pendingTagData[i]);
                        else if (pendingTagNames[i].ToLower() == "year" || pendingTagNames[i].ToLower() == "date")
                            outputTagFile.Tag.Year = Convert.ToUInt32(pendingTagData[i]);
                        // if none of the above match, it's a custom field
                        else {
                            // Add the custom tag data to a special string array (required to be able to set it to tFrame.Text)
                            String[] customTagText = { pendingTagData[i] };

                            // Creates an empty frame for the custom tag
                            TagLib.Id3v2.UserTextInformationFrame tFrame = TagLib.Id3v2.UserTextInformationFrame.Get(outputTagMap, pendingTagNames[i], true);

                            // Set frame's data to our custom data
                            tFrame.Text = customTagText;

                            // Replaces empty frame with our own frame
                            outputTagMap.ReplaceFrame(TagLib.Id3v2.UserTextInformationFrame.Get(outputTagMap, pendingTagNames[i], false), tFrame);
                        }
                    }

                    // Migrate pictures from .flac to .mp3
                    if (tagFile.Tag.Pictures != null)
                        outputTagFile.Tag.Pictures = tagFile.Tag.Pictures;

                    // Migrate string arrays
                    outputTagFile.Tag.AlbumArtists = albumArtistList.ToArray();
                    outputTagFile.Tag.Composers = composerList.ToArray();
                    outputTagFile.Tag.Genres = genreList.ToArray();
                    outputTagFile.Tag.Performers = performerList.ToArray();

                    // Save tags to file
                    outputTagFile.Save();

                    // Set Excel's folder data to this file's parsed syntax
                    parsedFolderStringForExcel = parsedFolderSyntax;
                });
            }
            // Opus Conversion
            else if (convertToInput == "Opus (192 kBps)")
            {
                Parallel.ForEach(inputFlacs, (currentFlac) =>
                {
                    string parsedFolderSyntax = "";
                    string parsedFileSyntax = "";
                    
                    // Save common calls to a variable to save cycles
                    parsedFolderSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, true);
                    parsedFileSyntax = ParseNamingSyntax(syntaxInput, convertToInput, currentFlac, false);
                    // Output Path+Name of future file
                    string outputOpus = outputPath + parsedFileSyntax + ".opus";

                    // Create directory (automatically checks if it exists or not)
                    Directory.CreateDirectory(outputPath + parsedFolderSyntax);
                    outputFolder = outputPath + parsedFolderSyntax;

                    // If the output folder doesn't end with "\", add "\" to it
                    outputFolder = NormalizePath(outputFolder);

                    lastFolder = new DirectoryInfo(outputFolder).Name;

                    // Call opusenc.exe
                    System.Diagnostics.Process opusProcess = new System.Diagnostics.Process();
                    // Uses opusenc from user-settings location
                    opusProcess.StartInfo.FileName = Settings.Default.OpusLocation;
                    opusProcess.StartInfo.UseShellExecute = false;
                    opusProcess.StartInfo.CreateNoWindow = true;
                    opusProcess.StartInfo.Arguments = "--quiet --bitrate 192 --vbr --ignorelength \"" + currentFlac + "\" \"" + outputOpus + "\"";
                    opusProcess.Start();
                    opusProcess.WaitForExit();

                    // Add resultant file to output file list
                    outputFiles.Add(outputPath + parsedFileSyntax + ".opus");

                    // Set Excel's folder data to this file's parsed syntax
                    parsedFolderStringForExcel = parsedFolderSyntax;
                });
            }

            if (copyFileTypesEnabled == true)
            {
                // Begin recursive copy function
                RecursiveFolderCopy(tempPath, outputFolder, copyFileTypes);
            }

            if (renameLogCueEnabled == true)
            {
                // Create lists of the .logs and .cues in the outputFolder
                List<string> logList = new List<string>();
                logList.AddRange(Directory.GetFiles(outputFolder, "*.log", SearchOption.AllDirectories));
                List<string> cueList = new List<string>();
                cueList.AddRange(Directory.GetFiles(outputFolder, "*.cue", SearchOption.AllDirectories));

                // If there are 2 or more .cues in the output, alert the user to rename manually (not possible to detect which .cue is CD1/CD2/etc)
                if (cueList.Count() >= 2)
                {
                    MessageBox.Show("More than one .cue file detected in output folder. Rename manually.");
                    // Opens output path in explorer
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = outputFolder,
                        UseShellExecute = true,
                    });
                }
                // If there's only one .cue, rename it (only if it's not already named properly)
                else if (cueList.Count() == 1 && !File.Exists(outputFolder + album + ".cue"))
                {
                    if (File.Exists(cueList[0]))
                        File.Move(cueList[0], outputFolder + album + ".cue");
                }

                // If there are more than 2 .logs in the output, alert the user to rename manually (not possible to detect which .log is CD1/CD2/etc)
                if (logList.Count() >= 2)
                {
                    MessageBox.Show("More than one .log file detected in output folder. Rename manually.");
                    // Opens output path in explorer
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = outputFolder,
                        UseShellExecute = true,
                    });
                }
                // If there's only one .lie, rename it (only if it's not already named properly)
                else if (logList.Count() == 1 && !File.Exists(outputFolder + artist + " - " + album + ".log"))
                {
                    if (File.Exists(logList[0]))
                        File.Move(logList[0], outputFolder + artist + " - " + album + ".log");
                }
            }

            if (stripImagesEnabled == true)
            {
                // Add all image files in outputFolder to a list
                List<string> imageFiles = new List<string>();
                imageFiles.AddRange(Directory.GetFiles(outputFolder, "*.bmp", SearchOption.AllDirectories));
                imageFiles.AddRange(Directory.GetFiles(outputFolder, "*.gif", SearchOption.AllDirectories));
                imageFiles.AddRange(Directory.GetFiles(outputFolder, "*.jpg", SearchOption.AllDirectories));
                imageFiles.AddRange(Directory.GetFiles(outputFolder, "*.png", SearchOption.AllDirectories));

                // Parallel strip files (exiftool on windows is sluggish compared to linux; run in parallel and don't wait for completion)
                Parallel.ForEach(imageFiles, (currentImage) =>
                {
                    // Call exiftool.exe on each image
                    System.Diagnostics.Process exifProcess = new System.Diagnostics.Process();
                    // Uses exiftool from user-settings location
                    exifProcess.StartInfo.FileName = Settings.Default.ExifToolLocation;
                    exifProcess.StartInfo.Arguments = "-overwrite_original -all= \"" + currentImage + "\"";
                    exifProcess.StartInfo.UseShellExecute = false;
                    exifProcess.StartInfo.CreateNoWindow = true;
                    exifProcess.Start();
                });
            }

            if (deleteTempEnabled == true)
            {
                // Delete temp path recursively
                if (tempPath != outputFolder)
                    if (Directory.Exists(tempPath))
                        Directory.Delete(tempPath, true);
            }

            if (addToExcel == true)
            {
                // Initialization for Excel functionality
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook currentWorkbook = excel.Workbooks.Open(Settings.Default.ExcelSheetLocation);
                // Uses first worksheet by default
                Worksheet currentWorksheet = excel.Worksheets[1];
                Range usedRange = currentWorksheet.UsedRange;
                int numRows = usedRange.Rows.Count;

                // Insert output folder name, log score, and notes in columns 1, 2, and 3 respectively
                currentWorksheet.Cells[numRows + 1, 1] = parsedFolderStringForExcel;

                // Check if log score and notes are empty before writing
                if (excelLogScore != "Log score" && excelLogScore != "")
                    currentWorksheet.Cells[numRows + 1, 2] = excelLogScore;
                if (excelNotes != "Notes" && excelNotes != "")
                    currentWorksheet.Cells[numRows + 1, 3] = excelNotes;

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
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
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
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
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
                fbd.InitialDirectory = InputPathBox.Text;
            else
                fbd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

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
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
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
                fbd.InitialDirectory = TempPathBox.Text;
            else
                fbd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            fbd.IsFolderPicker = true;
            if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                TempPathBox.Text = fbd.FileName;
                TempPathBox.ForeColor = Color.Black;
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
                System.Diagnostics.Process.Start("https://www.discogs.com/search/?type=release&title=" + AlbumTextBox.Text + "&artist=" + ArtistTextBox.Text);
            // If only artist is not empty
            else if (ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "")
                System.Diagnostics.Process.Start("https://www.discogs.com/search/?q=" + ArtistTextBox.Text + "&type=artist");
            // If only album is not empty
            else if (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != "")
                System.Diagnostics.Process.Start("https://www.discogs.com/search/?q=" + AlbumTextBox.Text + "&type=release");
            else
                MessageBox.Show("No artist and/or album specified.");
        }

        private void MusicBrainzButton_Click(object sender, EventArgs e)
        {
            // If artist and album textboxes are not empty
            if ((ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "") && (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != ""))
                System.Diagnostics.Process.Start("https://musicbrainz.org/taglookup?tag-lookup.artist=" + ArtistTextBox.Text + "&tag-lookup.release=" + AlbumTextBox.Text);
            // If only artist is not empty
            else if (ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "")
                System.Diagnostics.Process.Start("https://musicbrainz.org/search?query=" + ArtistTextBox.Text + "&type=artist");
            // If only album is not empty
            else if (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != "")
                System.Diagnostics.Process.Start("https://musicbrainz.org/search?query=" + AlbumTextBox.Text + "&type=release");
            else
                MessageBox.Show("No artist and/or album specified.");
        }

        private void RedactedButton_Click(object sender, EventArgs e)
        {
            // If artist and album textboxes are not empty
            if ((ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "") && (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != ""))
                System.Diagnostics.Process.Start("https://redacted.ch/torrents.php?artistname=" + ArtistTextBox.Text + "&groupname=" + AlbumTextBox.Text + "&order_by=seeders&order_way=desc&group_results=1&filter_cat[1]=1&action=basic&searchsubmit=1");
            // If only artist is not empty
            else if (ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "")
                System.Diagnostics.Process.Start("https://redacted.ch/artist.php?artistname=" + ArtistTextBox.Text);
            // If only album is not empty
            else if (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != "")
                System.Diagnostics.Process.Start("https://redacted.ch/artist.php?groupname=" + AlbumTextBox.Text + " &order_by=seeders&order_way=desc&group_results=1&filter_cat[1]=1&action=basic&searchsubmit=1");
            else
                MessageBox.Show("No artist and/or album specified.");
        }

        private void AADButton_Click(object sender, EventArgs e)
        {
            // If the tempPathBox doesn't end with "\", add "\" to it
            string tempPath = NormalizePath(TempPathBox.Text);

            // If artist and album textboxes are not empty
            if ((ArtistTextBox.Text != "Artist" && ArtistTextBox.Text != "") && (AlbumTextBox.Text != "Album" && AlbumTextBox.Text != ""))
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                // Uses AlbumArtDownloader from user-settings location
                startInfo.FileName = Settings.Default.AADLocation;
                startInfo.Arguments = "-ar \"" + ArtistTextBox.Text + "\" -al \"" + AlbumTextBox.Text + "\" -p \"" + tempPath + "folder.%extension%\"";
                System.Diagnostics.Process.Start(startInfo);
            }
            else
                MessageBox.Show("Artist or album is empty.");
        }

        private void Mp3tagButton_Click(object sender, EventArgs e)
        {
            if (TempPathBox.Text != "Temp Path" && TempPathBox.Text != "")
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                // Uses Mp3tag from user-settings location
                startInfo.FileName = Settings.Default.Mp3tagLocation;
                startInfo.Arguments = "-fp:\"" + TempPathBox.Text + "\"";
                System.Diagnostics.Process.Start(startInfo);
            }
            else
                MessageBox.Show("No temp path specified.");
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
            if (!Directory.Exists(TempPathBox.Text) || !Directory.Exists(OutputPathTextBox.Text) || ConvertToComboBox.Text == "")
                return;

            // Make sure the user actually wants to use the default temp folder, and didn't just misclick
            if (TempPathBox.Text == Settings.Default.DefaultTemp)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to convert using the default temp folder?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
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

            if (!ConvertBackgroundWorker.IsBusy)
                ConvertBackgroundWorker.RunWorkerAsync(outputArguments);
            else
                MessageBox.Show("Already running a conversion operation.");
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
                fbd.InitialDirectory = OutputPathTextBox.Text;
            else
                fbd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            fbd.IsFolderPicker = true;
            if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OutputPathTextBox.Text = fbd.FileName;
                OutputPathTextBox.ForeColor = Color.Black;
            }
        }

        private void ConvertBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Arguments passed into the completed event
            List<object> inputArguments = e.Result as List<object>;
            string inputPath = (string)inputArguments[0];
            string tempPath = (string)inputArguments[1];
            bool sourceFolderDeleted = (bool)inputArguments[2];

            // Set button back to default to denote process completion
            ConvertButton.Text = "Convert";
            ConvertButton.Enabled = true;


            DirectoryInfo parentFolder;

            // Move temppath up one folder if the original folder was deleted, but doesn't move if the user has changed the folder since starting the conversion
            if (sourceFolderDeleted == true)
            {
                parentFolder = Directory.GetParent(Directory.GetParent(tempPath).FullName);
                if (parentFolder != null && Directory.Exists(parentFolder.FullName) && NormalizePath(TempPathBox.Text) == NormalizePath(tempPath))
                    TempPathBox.Text = Directory.GetParent(Directory.GetParent(tempPath).FullName).FullName;

                // Move input path up one folder, but doesn't move if the user has changed the folder since starting the conversion
                if (NormalizePath(InputPathBox.Text) == NormalizePath(inputPath))
                    InputPathBox.Text = Settings.Default.DefaultInput;

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
                return;

            // String array of input files
            List<string> inputFlacs = new List<string>();
            inputFlacs.AddRange(Directory.GetFiles(TempPathBox.Text, "*.flac", SearchOption.AllDirectories));

            // If no flacs are found, return
            if (inputFlacs.Count() == 0)
                return;

            // Call spek.exe to generate spectrograms
            System.Diagnostics.Process spekProcess = new System.Diagnostics.Process();
            // Uses Spek from user-settings location
            spekProcess.StartInfo.FileName = Settings.Default.SpekLocation;
            spekProcess.StartInfo.UseShellExecute = false;

            // Opens each .flac file in spek, one at a time and in order
            foreach (string currentFlac in inputFlacs)
            {
                spekProcess.StartInfo.Arguments = "\"" + currentFlac + "\"";
                spekProcess.Start();
                spekProcess.WaitForExit();
            }

            // Parallelized version, opens all at once and in random order
            /*Parallel.ForEach(files, (currentFile) =>
            {
                // Call spek.exe to generate spectrograms
                System.Diagnostics.Process spekProcess = new System.Diagnostics.Process();
                // Uses Spek from user-settings location
                spekProcess.StartInfo.FileName = Settings.Default.SpekLocation;
                spekProcess.StartInfo.Arguments = "\"" + currentFlac + "\"";
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
                return;

            // Add input flacs into a list
            List<string> inputFlacs = new List<string>();
            inputFlacs.AddRange(Directory.GetFiles(TempPathBox.Text, "*.flac", SearchOption.AllDirectories));

            // If no flacs are found, return
            if (inputFlacs.Count() == 0)
                return;

            // Get the tags of the first flac in the list
            TagLib.File tagFile = TagLib.File.Create(inputFlacs[0]);
            var tagMap = (TagLib.Ogg.XiphComment)tagFile.GetTag(TagLib.TagTypes.Xiph);

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
                return;

            // Make sure the user actually wants to use default input, and didn't just misclick
            if (InputPathBox.Text == Settings.Default.DefaultInput)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to copy the default input folder?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
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
            bool convertWavs = (bool)inputArguments[2];

            // Get the name of the last folder in the inputPath
            string lastFolder = new DirectoryInfo(inputPath).Name;

            // Add lastFolder to the tempPath to give the last
            string outputPath = tempPath + lastFolder;

            // Begin recursive copy function
            RecursiveFolderCopy(inputPath, outputPath);

            // Convert .wavs to .flacs after copy
            if (convertWavs == true)
            {
                // List of input wavs that can be converted
                List<string> inputWavs = new List<string>();
                inputWavs.AddRange(Directory.GetFiles(outputPath, "*.wav", SearchOption.AllDirectories));

                // Parallel convert files
                Parallel.ForEach(inputWavs, (currentWav) =>
                {
                    // Location of the output .flac file, by removing .wav and appending .flac
                    string outputFlacFile = currentWav.Substring(0, currentWav.LastIndexOf(".wav")) + ".flac";

                    // Call flac.exe and convert to the outputFile, using V8 compression.
                    System.Diagnostics.Process flacProcess = new System.Diagnostics.Process();
                    // Uses flac from user-settings location
                    flacProcess.StartInfo.FileName = Settings.Default.FLACLocation;
                    flacProcess.StartInfo.Arguments = "-f -V8 \"" + currentWav + "\" -o \"" + outputFlacFile + "\"";
                    flacProcess.StartInfo.UseShellExecute = false;
                    flacProcess.StartInfo.CreateNoWindow = true;
                    flacProcess.Start();
                    flacProcess.WaitForExit();

                    // Remove input .wav files
                    if (File.Exists(currentWav))
                        File.Delete(currentWav);
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

            GuessButton.PerformClick();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm SettingsFormWindow = new SettingsForm();

            // Open settings form and apply settings if the user accepts it
            if (SettingsFormWindow.ShowDialog() == DialogResult.OK)
                ApplyUserSettings();
        }

        private void CopyContentsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // Enable or disable the file types textbox depending on check-state
            if (CopyContentsCheckbox.Checked == true)
            {
                CopyFileTypesTextBox.Enabled = true;
                RenameLogCueCheckbox.Enabled = true;
                RenameLogCueCheckbox.Checked = Settings.Default.DefaultRenameLogCue;
                if (Settings.Default.ExifToolLocation != "")
                {
                    StripImageMetadataCheckbox.Enabled = true;
                    StripImageMetadataCheckbox.Checked = Settings.Default.DefaultStripImageMetadata;
                }
            }
            else
            {
                CopyFileTypesTextBox.Enabled = false;
                RenameLogCueCheckbox.Enabled = false;
                RenameLogCueCheckbox.Checked = false;
                if (Settings.Default.ExifToolLocation != "")
                {
                    StripImageMetadataCheckbox.Enabled = false;
                    StripImageMetadataCheckbox.Checked = false;
                }
            }
        }

        private void ConvertToComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Dynamic ReplayGain enable for FLAC, MP3, and Opus
            // FLAC uses metaflac to generate ReplayGain data
            // Opus converts the input FLAC's tags automatically (we metaflac the input FLACs before conversion if enabled)
            // MP3 RG tags can be copied manually (we metaflac the input FLACs before conversion if enabled)
            if (ConvertToComboBox.Text == "FLAC" || ConvertToComboBox.Text == "FLAC (resample to 16-bit (SoX))" ||
                ConvertToComboBox.Text == "MP3 (V2)" || ConvertToComboBox.Text == "MP3 (V0)" || ConvertToComboBox.Text == "MP3 (320 kBps)" ||
                ConvertToComboBox.Text == "Opus (192 kBps)")
            {
                // If metaflac.exe is found, enable ReplayGain
                if (Settings.Default.MetaFLACLocation != "")
                {
                    ReplayGainCheckbox.Enabled = true;
                    ReplayGainCheckbox.Text = "Apply ReplayGain";
                }
                // Otherwise, disable
                else
                {
                    ReplayGainCheckbox.Enabled = false;
                    ReplayGainCheckbox.Checked = false;
                    ReplayGainCheckbox.Text = "Apply ReplayGain (requires metaflac.exe)";
                }
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
                return;

            // Move up one folder if valid
            DirectoryInfo parentFolder = Directory.GetParent(Directory.GetParent(NormalizePath(InputPathBox.Text)).FullName);
            if (parentFolder != null && Directory.Exists(parentFolder.FullName))
                InputPathBox.Text = Directory.GetParent(Directory.GetParent(NormalizePath(InputPathBox.Text)).FullName).FullName;
        }

        private void TempPathUpOneButton_Click(object sender, EventArgs e)
        {
            if (TempPathBox.Text == "Temp Folder" || TempPathBox.Text == "")
                return;

            // Move up one folder if valid
            DirectoryInfo parentFolder = Directory.GetParent(Directory.GetParent(NormalizePath(TempPathBox.Text)).FullName);
            if (parentFolder != null && Directory.Exists(parentFolder.FullName))
                TempPathBox.Text = Directory.GetParent(Directory.GetParent(NormalizePath(TempPathBox.Text)).FullName).FullName;
        }

        private void OutputPathUpOneButton_Click(object sender, EventArgs e)
        {
            if (OutputPathTextBox.Text == "Output Folder (base path)" || OutputPathTextBox.Text == "")
                return;

            // Move up one folder if valid
            DirectoryInfo parentFolder = Directory.GetParent(Directory.GetParent(NormalizePath(OutputPathTextBox.Text)).FullName);
            if (parentFolder != null && Directory.Exists(parentFolder.FullName))
                OutputPathTextBox.Text = Directory.GetParent(Directory.GetParent(NormalizePath(OutputPathTextBox.Text)).FullName).FullName;
        }
    }
}
