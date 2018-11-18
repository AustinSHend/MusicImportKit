using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using MusicImportKit.Properties;

namespace MusicImportKit
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            if (Settings.Default.DefaultInput != "")
            {
                DefaultInputFolderLocationTextBox.Text = Settings.Default.DefaultInput;
                DefaultInputFolderLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.DefaultTemp != "")
            {
                DefaultTempFolderLocationTextBox.Text = Settings.Default.DefaultTemp;
                DefaultTempFolderLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.DefaultOutput != "")
            {
                DefaultOutputFolderLocationTextBox.Text = Settings.Default.DefaultOutput;
                DefaultOutputFolderLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.AADLocation != "")
            {
                AADLocationTextBox.Text = Settings.Default.AADLocation;
                AADLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.FLACLocation != "")
            {
                FlacLocationTextBox.Text = Settings.Default.FLACLocation;
                FlacLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.LAMELocation != "")
            {
                LameLocationTextBox.Text = Settings.Default.LAMELocation;
                LameLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.MetaFLACLocation != "")
            {
                MetaFlacLocationTextBox.Text = Settings.Default.MetaFLACLocation;
                MetaFlacLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.Mp3tagLocation != "")
            {
                Mp3tagLocationTextBox.Text = Settings.Default.Mp3tagLocation;
                Mp3tagLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.OpusLocation != "")
            {
                OpusencLocationTextBox.Text = Settings.Default.OpusLocation;
                OpusencLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.SoXLocation != "")
            {
                SoxLocationTextBox.Text = Settings.Default.SoXLocation;
                SoxLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.SpekLocation != "")
            {
                SpekLocationTextBox.Text = Settings.Default.SpekLocation;
                SpekLocationTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.DefaultParse != "")
            {
                DefaultParseStyle.Text = Settings.Default.DefaultParse;
                DefaultParseStyle.ForeColor = Color.Black;
            }

            if (Settings.Default.DefaultSpecificFiletypeText != "")
            {
                DefaultSpecificFiletypeTextTextBox.Text = Settings.Default.DefaultSpecificFiletypeText;
                DefaultSpecificFiletypeTextTextBox.ForeColor = Color.Black;
            }

            if (Settings.Default.DefaultConvertFormat != "")
            {
                DefaultConvertToComboBox.Text = Settings.Default.DefaultConvertFormat;
            }

            if (Settings.Default.DefaultConvertPreset != "")
            {
                DefaultPresetComboBox.Text = Settings.Default.DefaultConvertPreset;
            }

            if (Settings.Default.ExcelSheetLocation != "")
            {
                DefaultExcelLocationTextBox.Text = Settings.Default.ExcelSheetLocation;
                DefaultExcelLocationTextBox.ForeColor = Color.Black;
            }

            DefaultAutoWavConvertState.Checked = Settings.Default.DefaultAutoWAVConvert;
            DefaultRedactedState.Checked = Settings.Default.DefaultRedacted;
            DefaultRGState.Checked = Settings.Default.DefaultRG;
            DefaultCopyFiletypesState.Checked = Settings.Default.DefaultSpecificFiletypes;
            DefaultRenameLogCueState.Checked = Settings.Default.DefaultRenameLogCue;
            DefaultStripImageMetadataState.Checked = Settings.Default.DefaultStripImageMetadata;
            DefaultExcelAppendState.Checked = Settings.Default.DefaultExcelExport;
            DefaultTempFolderDeleteState.Checked = Settings.Default.DefaultDeleteTemp;
            DefaultOpenFolderState.Checked = Settings.Default.DefaultFolderOpen;
        }

        // Adds a "\" to the end of a path if it doesn't already have one
        private string NormalizePath(string path)
        {
            return path.EndsWith("\\") ? path : path + "\\";
        }

        private void AADLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (AADLocationTextBox.Text == "AlbumArt.exe location")
            {
                AADLocationTextBox.Text = "";
                AADLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void AADLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (AADLocationTextBox.Text == "")
            {
                AADLocationTextBox.Text = "AlbumArt.exe location";
                AADLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void FlacLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (FlacLocationTextBox.Text == "flac.exe location")
            {
                FlacLocationTextBox.Text = "";
                FlacLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void FlacLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (FlacLocationTextBox.Text == "")
            {
                FlacLocationTextBox.Text = "flac.exe location";
                FlacLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void LameLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (LameLocationTextBox.Text == "lame.exe location")
            {
                LameLocationTextBox.Text = "";
                LameLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void LameLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (LameLocationTextBox.Text == "")
            {
                LameLocationTextBox.Text = "lame.exe location";
                LameLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void MetaFlacLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (MetaFlacLocationTextBox.Text == "metaflac.exe location")
            {
                MetaFlacLocationTextBox.Text = "";
                MetaFlacLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void MetaFlacLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (MetaFlacLocationTextBox.Text == "")
            {
                MetaFlacLocationTextBox.Text = "metaflac.exe location";
                MetaFlacLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void Mp3tagLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (Mp3tagLocationTextBox.Text == "Mp3tag.exe location")
            {
                Mp3tagLocationTextBox.Text = "";
                Mp3tagLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void Mp3tagLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (Mp3tagLocationTextBox.Text == "")
            {
                Mp3tagLocationTextBox.Text = "Mp3tag.exe location";
                Mp3tagLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void OpusencLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (OpusencLocationTextBox.Text == "opusenc.exe location")
            {
                OpusencLocationTextBox.Text = "";
                OpusencLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void OpusencLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (OpusencLocationTextBox.Text == "")
            {
                OpusencLocationTextBox.Text = "opusenc.exe location";
                OpusencLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void SoxLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (SoxLocationTextBox.Text == "sox.exe location")
            {
                SoxLocationTextBox.Text = "";
                SoxLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void SoxLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (SoxLocationTextBox.Text == "")
            {
                SoxLocationTextBox.Text = "sox.exe location";
                SoxLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void SpekLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (SpekLocationTextBox.Text == "spek.exe location")
            {
                SpekLocationTextBox.Text = "";
                SpekLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void SpekLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (SpekLocationTextBox.Text == "")
            {
                SpekLocationTextBox.Text = "spek.exe location";
                SpekLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void DefaultInputFolderLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultInputFolderLocationTextBox.Text == "Default Input Folder")
            {
                DefaultInputFolderLocationTextBox.Text = "";
                DefaultInputFolderLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultInputFolderLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultInputFolderLocationTextBox.Text == "")
            {
                DefaultInputFolderLocationTextBox.Text = "Default Input Folder";
                DefaultInputFolderLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void DefaultTempFolderLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultTempFolderLocationTextBox.Text == "Default Temp Folder")
            {
                DefaultTempFolderLocationTextBox.Text = "";
                DefaultTempFolderLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultTempFolderLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultTempFolderLocationTextBox.Text == "")
            {
                DefaultTempFolderLocationTextBox.Text = "Default Temp Folder";
                DefaultTempFolderLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void DefaultOutputFolderLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultOutputFolderLocationTextBox.Text == "Default Output Folder (base path)")
            {
                DefaultOutputFolderLocationTextBox.Text = "";
                DefaultOutputFolderLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultOutputFolderLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultOutputFolderLocationTextBox.Text == "")
            {
                DefaultOutputFolderLocationTextBox.Text = "Default Output Folder (base path)";
                DefaultOutputFolderLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void DefaultParseStyle_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultParseStyle.Text == "Default Output Folder+File Name (syntax in tooltip)")
            {
                DefaultParseStyle.Text = "";
                DefaultParseStyle.ForeColor = Color.Black;
            }
        }

        private void DefaultParseStyle_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultParseStyle.Text == "")
            {
                DefaultParseStyle.Text = "Default Output Folder+File Name (syntax in tooltip)";
                DefaultParseStyle.ForeColor = SystemColors.GrayText;
            }
        }

        private void DefaultSpecificFiletypeTextTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultSpecificFiletypeTextTextBox.Text == "Filetypes to copy")
            {
                DefaultSpecificFiletypeTextTextBox.Text = "";
                DefaultSpecificFiletypeTextTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultSpecificFiletypeTextTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultSpecificFiletypeTextTextBox.Text == "")
            {
                DefaultSpecificFiletypeTextTextBox.Text = "Filetypes to copy";
                DefaultSpecificFiletypeTextTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void DefaultExcelLocationTextBox_Enter(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultExcelLocationTextBox.Text == "Default Excel sheet location")
            {
                DefaultExcelLocationTextBox.Text = "";
                DefaultExcelLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultExcelLocationTextBox_Leave(object sender, EventArgs e)
        {
            // Text watermarking
            if (DefaultExcelLocationTextBox.Text == "")
            {
                DefaultExcelLocationTextBox.Text = "Default Excel sheet location";
                DefaultExcelLocationTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void AcceptSettingsButton_Click(object sender, EventArgs e)
        {
            // Checks if paths are valid before saving
            if (Directory.Exists(DefaultInputFolderLocationTextBox.Text))
            {
                Settings.Default.DefaultInput = NormalizePath(DefaultInputFolderLocationTextBox.Text);
            }
            else
            {
                Settings.Default.DefaultInput = "";
            }

            if (Directory.Exists(DefaultTempFolderLocationTextBox.Text))
            {
                Settings.Default.DefaultTemp = NormalizePath(DefaultTempFolderLocationTextBox.Text);
            }
            else
            {
                Settings.Default.DefaultTemp = "";
            }

            if (Directory.Exists(DefaultOutputFolderLocationTextBox.Text))
            {
                Settings.Default.DefaultOutput = NormalizePath(DefaultOutputFolderLocationTextBox.Text);
            }
            else
            {
                Settings.Default.DefaultOutput = "";
            }

            if (File.Exists(AADLocationTextBox.Text))
            {
                Settings.Default.AADLocation = AADLocationTextBox.Text;
            }
            else
            {
                Settings.Default.AADLocation = "";
            }

            if (File.Exists(FlacLocationTextBox.Text))
            {
                Settings.Default.FLACLocation = FlacLocationTextBox.Text;
            }
            else
            {
                Settings.Default.FLACLocation = "";
            }

            if (File.Exists(LameLocationTextBox.Text))
            {
                Settings.Default.LAMELocation = LameLocationTextBox.Text;
            }
            else
            {
                Settings.Default.LAMELocation = "";
            }

            if (File.Exists(MetaFlacLocationTextBox.Text))
            {
                Settings.Default.MetaFLACLocation = MetaFlacLocationTextBox.Text;
            }
            else
            {
                Settings.Default.MetaFLACLocation = "";
            }

            if (File.Exists(Mp3tagLocationTextBox.Text))
            {
                Settings.Default.Mp3tagLocation = Mp3tagLocationTextBox.Text;
            }
            else
            {
                Settings.Default.Mp3tagLocation = "";
            }

            if (File.Exists(OpusencLocationTextBox.Text))
            {
                Settings.Default.OpusLocation = OpusencLocationTextBox.Text;
            }
            else
            {
                Settings.Default.OpusLocation = "";
            }

            if (File.Exists(SoxLocationTextBox.Text))
            {
                Settings.Default.SoXLocation = SoxLocationTextBox.Text;
            }
            else
            {
                Settings.Default.SoXLocation = "";
            }

            if (File.Exists(SpekLocationTextBox.Text))
            {
                Settings.Default.SpekLocation = SpekLocationTextBox.Text;
            }
            else
            {
                Settings.Default.SpekLocation = "";
            }

            if (DefaultParseStyle.Text != "Default Output Folder+File Name (syntax in tooltip)" && DefaultParseStyle.Text != "")
            {
                Settings.Default.DefaultParse = DefaultParseStyle.Text;
            }
            else
            {
                Settings.Default.DefaultParse = "";
            }

            if (DefaultConvertToComboBox.Text != "")
            {
                Settings.Default.DefaultConvertFormat = DefaultConvertToComboBox.Text;
            }
            else
            {
                Settings.Default.DefaultConvertFormat = "FLAC";
            }

            if (DefaultPresetComboBox.Text != "")
            {
                Settings.Default.DefaultConvertPreset = DefaultPresetComboBox.Text;
            }
            else
            {
                Settings.Default.DefaultConvertPreset = "Standard";
            }

            if (DefaultSpecificFiletypeTextTextBox.Text != "Filetypes to copy" && DefaultSpecificFiletypeTextTextBox.Text != "")
            {
                Settings.Default.DefaultSpecificFiletypeText = DefaultSpecificFiletypeTextTextBox.Text;
            }
            else
            {
                Settings.Default.DefaultSpecificFiletypeText = "";
            }

            if (DefaultExcelLocationTextBox.Text != "" && File.Exists(DefaultExcelLocationTextBox.Text))
            {
                Settings.Default.ExcelSheetLocation = DefaultExcelLocationTextBox.Text;
            }
            else
            {
                Settings.Default.ExcelSheetLocation = "";
            }

            Settings.Default.DefaultAutoWAVConvert = DefaultAutoWavConvertState.Checked;
            Settings.Default.DefaultRedacted = DefaultRedactedState.Checked;
            Settings.Default.DefaultRG = DefaultRGState.Checked;
            Settings.Default.DefaultSpecificFiletypes = DefaultCopyFiletypesState.Checked;
            Settings.Default.DefaultRenameLogCue = DefaultRenameLogCueState.Checked;
            Settings.Default.DefaultStripImageMetadata = DefaultStripImageMetadataState.Checked;
            Settings.Default.DefaultDeleteTemp = DefaultTempFolderDeleteState.Checked;
            Settings.Default.DefaultFolderOpen = DefaultOpenFolderState.Checked;
            Settings.Default.DefaultExcelExport = DefaultExcelAppendState.Checked;

            Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelSettingsButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DefaultInputPathButton_Click(object sender, EventArgs e)
        {
            // Folder picker
            CommonOpenFileDialog fbd = new CommonOpenFileDialog();

            if (DefaultInputFolderLocationTextBox.Text != "Default Input Folder" && DefaultInputFolderLocationTextBox.Text != "" && Directory.Exists(DefaultInputFolderLocationTextBox.Text))
                fbd.InitialDirectory = DefaultInputFolderLocationTextBox.Text;
            else
                fbd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            fbd.IsFolderPicker = true;
            if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DefaultInputFolderLocationTextBox.Text = fbd.FileName;
                DefaultInputFolderLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultTempPathButton_Click(object sender, EventArgs e)
        {
            // Folder picker
            CommonOpenFileDialog fbd = new CommonOpenFileDialog();

            if (DefaultTempFolderLocationTextBox.Text != "Default Temp Folder" && DefaultTempFolderLocationTextBox.Text != "" && Directory.Exists(DefaultTempFolderLocationTextBox.Text))
                fbd.InitialDirectory = DefaultTempFolderLocationTextBox.Text;
            else
                fbd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            fbd.IsFolderPicker = true;
            if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DefaultTempFolderLocationTextBox.Text = fbd.FileName;
                DefaultTempFolderLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultOutputPathButton_Click(object sender, EventArgs e)
        {
            // Folder picker
            CommonOpenFileDialog fbd = new CommonOpenFileDialog();

            if (DefaultOutputFolderLocationTextBox.Text != "Default Output Folder (base path)" && DefaultOutputFolderLocationTextBox.Text != "" && Directory.Exists(DefaultOutputFolderLocationTextBox.Text))
                fbd.InitialDirectory = DefaultOutputFolderLocationTextBox.Text;
            else
                fbd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            fbd.IsFolderPicker = true;
            if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DefaultOutputFolderLocationTextBox.Text = fbd.FileName;
                DefaultOutputFolderLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultAADPathButton_Click(object sender, EventArgs e)
        {
            // File picker
            OpenFileDialog ofd = new OpenFileDialog();
            if (AADLocationTextBox.Text != "AlbumArt.exe location" && AADLocationTextBox.Text != "" && Directory.Exists(Directory.GetParent(AADLocationTextBox.Text).FullName))
                ofd.InitialDirectory = Directory.GetParent(AADLocationTextBox.Text).FullName;
            else
                ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.DefaultExt = ".exe";
            ofd.Filter = "AlbumArt.exe|albumArt.exe";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                AADLocationTextBox.Text = ofd.FileName;
                AADLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultFlacPathButton_Click(object sender, EventArgs e)
        {
            // File picker
            OpenFileDialog ofd = new OpenFileDialog();
            if (FlacLocationTextBox.Text != "flac.exe location" && FlacLocationTextBox.Text != "" && Directory.Exists(Directory.GetParent(FlacLocationTextBox.Text).FullName))
                ofd.InitialDirectory = Directory.GetParent(FlacLocationTextBox.Text).FullName;
            else
                ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.DefaultExt = ".exe";
            ofd.Filter = "flac.exe|flac.exe";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FlacLocationTextBox.Text = ofd.FileName;
                FlacLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultLamePathButton_Click(object sender, EventArgs e)
        {
            // File picker
            OpenFileDialog ofd = new OpenFileDialog();
            if (LameLocationTextBox.Text != "lame.exe location" && LameLocationTextBox.Text != "" && Directory.Exists(Directory.GetParent(LameLocationTextBox.Text).FullName))
                ofd.InitialDirectory = Directory.GetParent(LameLocationTextBox.Text).FullName;
            else
                ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.DefaultExt = ".exe";
            ofd.Filter = "lame.exe|lame.exe";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LameLocationTextBox.Text = ofd.FileName;
                LameLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultMetaFlacPathButton_Click(object sender, EventArgs e)
        {
            // File picker
            OpenFileDialog ofd = new OpenFileDialog();
            if (MetaFlacLocationTextBox.Text != "metaflac.exe location" && MetaFlacLocationTextBox.Text != "" && Directory.Exists(Directory.GetParent(MetaFlacLocationTextBox.Text).FullName))
                ofd.InitialDirectory = Directory.GetParent(MetaFlacLocationTextBox.Text).FullName;
            else
                ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.DefaultExt = ".exe";
            ofd.Filter = "metaflac.exe|metaflac.exe";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MetaFlacLocationTextBox.Text = ofd.FileName;
                MetaFlacLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultMp3tagPathButton_Click(object sender, EventArgs e)
        {
            // File picker
            OpenFileDialog ofd = new OpenFileDialog();
            if (Mp3tagLocationTextBox.Text != "Mp3tag.exe location" && Mp3tagLocationTextBox.Text != "" && Directory.Exists(Directory.GetParent(Mp3tagLocationTextBox.Text).FullName))
                ofd.InitialDirectory = Directory.GetParent(Mp3tagLocationTextBox.Text).FullName;
            else
                ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.DefaultExt = ".exe";
            ofd.Filter = "Mp3tag.exe|Mp3tag.exe";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Mp3tagLocationTextBox.Text = ofd.FileName;
                Mp3tagLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultOpusPathButton_Click(object sender, EventArgs e)
        {
            // File picker
            OpenFileDialog ofd = new OpenFileDialog();
            if (OpusencLocationTextBox.Text != "opusenc.exe location" && OpusencLocationTextBox.Text != "" && Directory.Exists(Directory.GetParent(OpusencLocationTextBox.Text).FullName))
                ofd.InitialDirectory = Directory.GetParent(OpusencLocationTextBox.Text).FullName;
            else
                ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.DefaultExt = ".exe";
            ofd.Filter = "opusenc.exe|opusenc.exe";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OpusencLocationTextBox.Text = ofd.FileName;
                OpusencLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultSoxPathButton_Click(object sender, EventArgs e)
        {
            // File picker
            OpenFileDialog ofd = new OpenFileDialog();
            if (SoxLocationTextBox.Text != "sox.exe location" && SoxLocationTextBox.Text != "" && Directory.Exists(Directory.GetParent(SoxLocationTextBox.Text).FullName))
                ofd.InitialDirectory = Directory.GetParent(SoxLocationTextBox.Text).FullName;
            else
                ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.DefaultExt = ".exe";
            ofd.Filter = "sox.exe|sox.exe";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SoxLocationTextBox.Text = ofd.FileName;
                SoxLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultSpekPathButton_Click(object sender, EventArgs e)
        {
            // File picker
            OpenFileDialog ofd = new OpenFileDialog();
            if (SpekLocationTextBox.Text != "spek.exe location" && SpekLocationTextBox.Text != "" && Directory.Exists(Directory.GetParent(SpekLocationTextBox.Text).FullName))
                ofd.InitialDirectory = Directory.GetParent(SpekLocationTextBox.Text).FullName;
            else
                ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.DefaultExt = ".exe";
            ofd.Filter = "spek.exe|spek.exe";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SpekLocationTextBox.Text = ofd.FileName;
                SpekLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultExcelSheetPathButton_Click(object sender, EventArgs e)
        {
            // File picker
            OpenFileDialog ofd = new OpenFileDialog();
            if (DefaultExcelLocationTextBox.Text != "Default Excel sheet location" && DefaultExcelLocationTextBox.Text != "" && Directory.Exists(Directory.GetParent(DefaultExcelLocationTextBox.Text).FullName))
                ofd.InitialDirectory = Directory.GetParent(DefaultExcelLocationTextBox.Text).FullName;
            else
                ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.DefaultExt = ".xls";
            ofd.Filter = "Excel workbook (*.xls*)|*.xls*";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DefaultExcelLocationTextBox.Text = ofd.FileName;
                DefaultExcelLocationTextBox.ForeColor = Color.Black;
            }
        }

        private void DefaultConvertToComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear presets
            DefaultPresetComboBox.Items.Clear();

            if (DefaultConvertToComboBox.Text == "FLAC")
            {
                DefaultPresetComboBox.Items.Add("Standard");
                // SoX is required for proper downsampling and dithering
                DefaultPresetComboBox.Items.Add("Force 16-bit");
                DefaultPresetComboBox.Items.Add("Force 44.1kHz/48kHz");
                DefaultPresetComboBox.Items.Add("Force 16-bit and 44.1/48kHz");
            }
            else if (DefaultConvertToComboBox.Text == "MP3")
            {
                DefaultPresetComboBox.Items.Add("245kbps VBR (V0)");
                DefaultPresetComboBox.Items.Add("225kbps VBR (V1)");
                DefaultPresetComboBox.Items.Add("190kbps VBR (V2)");
                DefaultPresetComboBox.Items.Add("175kbps VBR (V3)");
                DefaultPresetComboBox.Items.Add("165kbps VBR (V4)");
                DefaultPresetComboBox.Items.Add("130kbps VBR (V5)");
                DefaultPresetComboBox.Items.Add("115kbps VBR (V6)");
                DefaultPresetComboBox.Items.Add("100kbps VBR (V7)");
                DefaultPresetComboBox.Items.Add("85kbps VBR (V8)");
                DefaultPresetComboBox.Items.Add("65kbps VBR (V9)");
                DefaultPresetComboBox.Items.Add("320kbps CBR");
                DefaultPresetComboBox.Items.Add("256kbps CBR");
                DefaultPresetComboBox.Items.Add("192kbps CBR");
                DefaultPresetComboBox.Items.Add("128kbps CBR");
                DefaultPresetComboBox.Items.Add("64kbps CBR");
            }
            else if (DefaultConvertToComboBox.Text == "Opus")
            {
                DefaultPresetComboBox.Items.Add("192kbps VBR");
                DefaultPresetComboBox.Items.Add("160kbps VBR");
                DefaultPresetComboBox.Items.Add("128kbps VBR");
                DefaultPresetComboBox.Items.Add("96kbps VBR");
                DefaultPresetComboBox.Items.Add("64kbps VBR");
                DefaultPresetComboBox.Items.Add("32kbps VBR");
            }
        }
    }
}
