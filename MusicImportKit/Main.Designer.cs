namespace MusicImportKit
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.InputPathBox = new System.Windows.Forms.TextBox();
            this.InputPathButton = new System.Windows.Forms.Button();
            this.InputExplorerButton = new System.Windows.Forms.Button();
            this.TempExplorerButton = new System.Windows.Forms.Button();
            this.TempPathButton = new System.Windows.Forms.Button();
            this.TempPathBox = new System.Windows.Forms.TextBox();
            this.ArtistTextBox = new System.Windows.Forms.TextBox();
            this.AlbumTextBox = new System.Windows.Forms.TextBox();
            this.DiscogsButton = new System.Windows.Forms.Button();
            this.MusicBrainzButton = new System.Windows.Forms.Button();
            this.AADButton = new System.Windows.Forms.Button();
            this.Mp3tagButton = new System.Windows.Forms.Button();
            this.ConvertToComboBox = new System.Windows.Forms.ComboBox();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.OutputNamingSyntaxTextBox = new System.Windows.Forms.ComboBox();
            this.OutputPathTextBox = new System.Windows.Forms.TextBox();
            this.OutputNamingSyntaxTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.CopyFileTypesTextBox = new System.Windows.Forms.TextBox();
            this.ConvertBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ReplayGainCheckbox = new System.Windows.Forms.CheckBox();
            this.CopyContentsCheckbox = new System.Windows.Forms.CheckBox();
            this.ConvertOpenFolderCheckbox = new System.Windows.Forms.CheckBox();
            this.SpectrogramsButton = new System.Windows.Forms.Button();
            this.GuessButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.CopyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.CopyFileTypesTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.DeleteTempFolderCheckbox = new System.Windows.Forms.CheckBox();
            this.RedactedButton = new System.Windows.Forms.Button();
            this.RenameLogCueCheckbox = new System.Windows.Forms.CheckBox();
            this.StripImageMetadataCheckbox = new System.Windows.Forms.CheckBox();
            this.OutputExplorerButton = new System.Windows.Forms.Button();
            this.OutputPathButton = new System.Windows.Forms.Button();
            this.AutoWavConvertCheckbox = new System.Windows.Forms.CheckBox();
            this.ResetPathsButton = new System.Windows.Forms.Button();
            this.ExcelExportCheckbox = new System.Windows.Forms.CheckBox();
            this.ExcelLogScoreTextBox = new System.Windows.Forms.TextBox();
            this.ExcelNotesTextBox = new System.Windows.Forms.TextBox();
            this.InputPathUpOneButton = new System.Windows.Forms.Button();
            this.TempPathUpOneButton = new System.Windows.Forms.Button();
            this.OutputPathUpOneButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.PresetComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // InputPathBox
            // 
            this.InputPathBox.AllowDrop = true;
            this.InputPathBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.InputPathBox.Location = new System.Drawing.Point(12, 12);
            this.InputPathBox.Name = "InputPathBox";
            this.InputPathBox.Size = new System.Drawing.Size(398, 20);
            this.InputPathBox.TabIndex = 0;
            this.InputPathBox.Text = "Input Folder";
            this.InputPathBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.InputPathBox_DragDrop);
            this.InputPathBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.InputPathBox_DragEnter);
            this.InputPathBox.Enter += new System.EventHandler(this.InputPathBox_Enter);
            this.InputPathBox.Leave += new System.EventHandler(this.InputPathBox_Leave);
            // 
            // InputPathButton
            // 
            this.InputPathButton.Location = new System.Drawing.Point(438, 11);
            this.InputPathButton.Name = "InputPathButton";
            this.InputPathButton.Size = new System.Drawing.Size(93, 22);
            this.InputPathButton.TabIndex = 2;
            this.InputPathButton.Text = "Ch&oose Folder";
            this.InputPathButton.UseVisualStyleBackColor = true;
            this.InputPathButton.Click += new System.EventHandler(this.InputPathButton_Click);
            // 
            // InputExplorerButton
            // 
            this.InputExplorerButton.Location = new System.Drawing.Point(536, 11);
            this.InputExplorerButton.Name = "InputExplorerButton";
            this.InputExplorerButton.Size = new System.Drawing.Size(93, 22);
            this.InputExplorerButton.TabIndex = 3;
            this.InputExplorerButton.Text = "Open in Explorer";
            this.InputExplorerButton.UseVisualStyleBackColor = true;
            this.InputExplorerButton.Click += new System.EventHandler(this.InputExplorerButton_Click);
            // 
            // TempExplorerButton
            // 
            this.TempExplorerButton.Location = new System.Drawing.Point(536, 66);
            this.TempExplorerButton.Name = "TempExplorerButton";
            this.TempExplorerButton.Size = new System.Drawing.Size(93, 22);
            this.TempExplorerButton.TabIndex = 10;
            this.TempExplorerButton.Text = "Open in Explorer";
            this.TempExplorerButton.UseVisualStyleBackColor = true;
            this.TempExplorerButton.Click += new System.EventHandler(this.TempExplorerButton_Click);
            // 
            // TempPathButton
            // 
            this.TempPathButton.Location = new System.Drawing.Point(438, 66);
            this.TempPathButton.Name = "TempPathButton";
            this.TempPathButton.Size = new System.Drawing.Size(93, 22);
            this.TempPathButton.TabIndex = 9;
            this.TempPathButton.Text = "Choose Folder";
            this.TempPathButton.UseVisualStyleBackColor = true;
            this.TempPathButton.Click += new System.EventHandler(this.TempPathButton_Click);
            // 
            // TempPathBox
            // 
            this.TempPathBox.AllowDrop = true;
            this.TempPathBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TempPathBox.Location = new System.Drawing.Point(12, 67);
            this.TempPathBox.Name = "TempPathBox";
            this.TempPathBox.Size = new System.Drawing.Size(398, 20);
            this.TempPathBox.TabIndex = 7;
            this.TempPathBox.Text = "Temp Folder";
            this.TempPathBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.TempPathBox_DragDrop);
            this.TempPathBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.TempPathBox_DragEnter);
            this.TempPathBox.Enter += new System.EventHandler(this.TempPathBox_Enter);
            this.TempPathBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TempPathBox_KeyDown);
            this.TempPathBox.Leave += new System.EventHandler(this.TempPathBox_Leave);
            // 
            // ArtistTextBox
            // 
            this.ArtistTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ArtistTextBox.Location = new System.Drawing.Point(12, 93);
            this.ArtistTextBox.Name = "ArtistTextBox";
            this.ArtistTextBox.Size = new System.Drawing.Size(274, 20);
            this.ArtistTextBox.TabIndex = 11;
            this.ArtistTextBox.Text = "Artist";
            this.ArtistTextBox.Enter += new System.EventHandler(this.ArtistTextBox_Enter);
            this.ArtistTextBox.Leave += new System.EventHandler(this.ArtistTextBox_Leave);
            // 
            // AlbumTextBox
            // 
            this.AlbumTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.AlbumTextBox.Location = new System.Drawing.Point(12, 119);
            this.AlbumTextBox.Name = "AlbumTextBox";
            this.AlbumTextBox.Size = new System.Drawing.Size(274, 20);
            this.AlbumTextBox.TabIndex = 12;
            this.AlbumTextBox.Text = "Album";
            this.AlbumTextBox.Enter += new System.EventHandler(this.AlbumTextBox_Enter);
            this.AlbumTextBox.Leave += new System.EventHandler(this.AlbumTextBox_Leave);
            // 
            // DiscogsButton
            // 
            this.DiscogsButton.Location = new System.Drawing.Point(12, 145);
            this.DiscogsButton.Name = "DiscogsButton";
            this.DiscogsButton.Size = new System.Drawing.Size(74, 22);
            this.DiscogsButton.TabIndex = 14;
            this.DiscogsButton.Text = "Discogs";
            this.DiscogsButton.UseVisualStyleBackColor = true;
            this.DiscogsButton.Click += new System.EventHandler(this.DiscogsButton_Click);
            // 
            // MusicBrainzButton
            // 
            this.MusicBrainzButton.Location = new System.Drawing.Point(92, 145);
            this.MusicBrainzButton.Name = "MusicBrainzButton";
            this.MusicBrainzButton.Size = new System.Drawing.Size(74, 22);
            this.MusicBrainzButton.TabIndex = 15;
            this.MusicBrainzButton.Text = "MusicBrainz";
            this.MusicBrainzButton.UseVisualStyleBackColor = true;
            this.MusicBrainzButton.Click += new System.EventHandler(this.MusicBrainzButton_Click);
            // 
            // AADButton
            // 
            this.AADButton.Enabled = false;
            this.AADButton.Location = new System.Drawing.Point(12, 174);
            this.AADButton.Name = "AADButton";
            this.AADButton.Size = new System.Drawing.Size(114, 22);
            this.AADButton.TabIndex = 17;
            this.AADButton.Text = "AlbumArtDownloader";
            this.AADButton.UseVisualStyleBackColor = true;
            this.AADButton.Click += new System.EventHandler(this.AADButton_Click);
            // 
            // Mp3tagButton
            // 
            this.Mp3tagButton.Enabled = false;
            this.Mp3tagButton.Location = new System.Drawing.Point(132, 174);
            this.Mp3tagButton.Name = "Mp3tagButton";
            this.Mp3tagButton.Size = new System.Drawing.Size(114, 22);
            this.Mp3tagButton.TabIndex = 18;
            this.Mp3tagButton.Text = "Mp3Tag";
            this.Mp3tagButton.UseVisualStyleBackColor = true;
            this.Mp3tagButton.Click += new System.EventHandler(this.Mp3tagButton_Click);
            // 
            // ConvertToComboBox
            // 
            this.ConvertToComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConvertToComboBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ConvertToComboBox.FormattingEnabled = true;
            this.ConvertToComboBox.Location = new System.Drawing.Point(12, 420);
            this.ConvertToComboBox.Name = "ConvertToComboBox";
            this.ConvertToComboBox.Size = new System.Drawing.Size(50, 21);
            this.ConvertToComboBox.TabIndex = 35;
            this.ConvertToComboBox.SelectedIndexChanged += new System.EventHandler(this.ConvertToComboBox_SelectedIndexChanged);
            // 
            // ConvertButton
            // 
            this.ConvertButton.Location = new System.Drawing.Point(235, 418);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(76, 23);
            this.ConvertButton.TabIndex = 37;
            this.ConvertButton.Text = "Convert";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // OutputNamingSyntaxTextBox
            // 
            this.OutputNamingSyntaxTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.OutputNamingSyntaxTextBox.FormattingEnabled = true;
            this.OutputNamingSyntaxTextBox.Items.AddRange(new object[] {
            "%AlbumArtist% - %Album% (%Date%) {%Edition%} [%Mediatype% &codec& - &smartbit&]\\&" +
                "PaddedTrackNumber& - %Title%",
            "%AlbumArtist% - %Album% (%Date%) [%Mediatype% &codec& - &smartbit&] {%Edition% %C" +
                "atalog%}\\&PaddedTrackNumber& - %Title%",
            "%AlbumArtist% - %Album% (%Date%) [%Mediatype% &codec& - &smartbit&] {%Edition%}\\&" +
                "PaddedTrackNumber& - %Title%",
            "%AlbumArtist% - %Album% (%Date%) [%Mediatype% &codec& - &smartbit&] {%Catalog%}\\&" +
                "PaddedTrackNumber& - %Title%",
            "%AlbumArtist% - %Date% - %Album% (%Edition%) [%Mediatype% &codec& - &smartbit&]\\&" +
                "PaddedTrackNumber& - %Title%"});
            this.OutputNamingSyntaxTextBox.Location = new System.Drawing.Point(12, 228);
            this.OutputNamingSyntaxTextBox.Name = "OutputNamingSyntaxTextBox";
            this.OutputNamingSyntaxTextBox.Size = new System.Drawing.Size(781, 21);
            this.OutputNamingSyntaxTextBox.TabIndex = 24;
            this.OutputNamingSyntaxTextBox.Text = "Output Folder+File Name (syntax in tooltip)";
            this.OutputNamingSyntaxTooltip.SetToolTip(this.OutputNamingSyntaxTextBox, resources.GetString("OutputNamingSyntaxTextBox.ToolTip"));
            this.OutputNamingSyntaxTextBox.Enter += new System.EventHandler(this.OutputNamingSyntaxTextBox_Enter);
            this.OutputNamingSyntaxTextBox.Leave += new System.EventHandler(this.OutputNamingSyntaxTextBox_Leave);
            // 
            // OutputPathTextBox
            // 
            this.OutputPathTextBox.AllowDrop = true;
            this.OutputPathTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.OutputPathTextBox.Location = new System.Drawing.Point(12, 202);
            this.OutputPathTextBox.Name = "OutputPathTextBox";
            this.OutputPathTextBox.Size = new System.Drawing.Size(398, 20);
            this.OutputPathTextBox.TabIndex = 20;
            this.OutputPathTextBox.Text = "Output Folder (base path)";
            this.OutputPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.OutputPathTextBox_DragDrop);
            this.OutputPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.OutputPathTextBox_DragEnter);
            this.OutputPathTextBox.Enter += new System.EventHandler(this.OutputPathTextBox_Enter);
            this.OutputPathTextBox.Leave += new System.EventHandler(this.OutputPathTextBox_Leave);
            // 
            // CopyFileTypesTextBox
            // 
            this.CopyFileTypesTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.CopyFileTypesTextBox.Location = new System.Drawing.Point(148, 280);
            this.CopyFileTypesTextBox.Name = "CopyFileTypesTextBox";
            this.CopyFileTypesTextBox.Size = new System.Drawing.Size(459, 20);
            this.CopyFileTypesTextBox.TabIndex = 27;
            this.CopyFileTypesTextBox.Text = "e.g. *.jpg; *.log; *.cue; *.pdf";
            this.CopyFileTypesTooltip.SetToolTip(this.CopyFileTypesTextBox, "Separate with \";\"\r\nRegex and wildcards (*, ?) accepted");
            this.CopyFileTypesTextBox.Enter += new System.EventHandler(this.CopyFileTypesTextBox_Enter);
            this.CopyFileTypesTextBox.Leave += new System.EventHandler(this.CopyFileTypesTextBox_Leave);
            // 
            // ConvertBackgroundWorker
            // 
            this.ConvertBackgroundWorker.WorkerReportsProgress = true;
            this.ConvertBackgroundWorker.WorkerSupportsCancellation = true;
            this.ConvertBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConvertBackgroundWorker_DoWork);
            this.ConvertBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ConvertBackgroundWorker_RunWorkerCompleted);
            // 
            // ReplayGainCheckbox
            // 
            this.ReplayGainCheckbox.AutoSize = true;
            this.ReplayGainCheckbox.Enabled = false;
            this.ReplayGainCheckbox.Location = new System.Drawing.Point(12, 259);
            this.ReplayGainCheckbox.Name = "ReplayGainCheckbox";
            this.ReplayGainCheckbox.Size = new System.Drawing.Size(110, 17);
            this.ReplayGainCheckbox.TabIndex = 25;
            this.ReplayGainCheckbox.Text = "Apply ReplayGain";
            this.ReplayGainCheckbox.UseVisualStyleBackColor = true;
            // 
            // CopyContentsCheckbox
            // 
            this.CopyContentsCheckbox.AutoSize = true;
            this.CopyContentsCheckbox.Location = new System.Drawing.Point(12, 282);
            this.CopyContentsCheckbox.Name = "CopyContentsCheckbox";
            this.CopyContentsCheckbox.Size = new System.Drawing.Size(130, 17);
            this.CopyContentsCheckbox.TabIndex = 26;
            this.CopyContentsCheckbox.Text = "Copy specific filetypes";
            this.CopyContentsCheckbox.UseVisualStyleBackColor = true;
            this.CopyContentsCheckbox.CheckedChanged += new System.EventHandler(this.CopyContentsCheckbox_CheckedChanged);
            // 
            // ConvertOpenFolderCheckbox
            // 
            this.ConvertOpenFolderCheckbox.AutoSize = true;
            this.ConvertOpenFolderCheckbox.Location = new System.Drawing.Point(12, 397);
            this.ConvertOpenFolderCheckbox.Name = "ConvertOpenFolderCheckbox";
            this.ConvertOpenFolderCheckbox.Size = new System.Drawing.Size(137, 17);
            this.ConvertOpenFolderCheckbox.TabIndex = 34;
            this.ConvertOpenFolderCheckbox.Text = "Open folder when done";
            this.ConvertOpenFolderCheckbox.UseVisualStyleBackColor = true;
            // 
            // SpectrogramsButton
            // 
            this.SpectrogramsButton.Enabled = false;
            this.SpectrogramsButton.Location = new System.Drawing.Point(252, 174);
            this.SpectrogramsButton.Name = "SpectrogramsButton";
            this.SpectrogramsButton.Size = new System.Drawing.Size(114, 22);
            this.SpectrogramsButton.TabIndex = 19;
            this.SpectrogramsButton.Text = "Spek";
            this.SpectrogramsButton.UseVisualStyleBackColor = true;
            this.SpectrogramsButton.Click += new System.EventHandler(this.SpectrogramsButton_Click);
            // 
            // GuessButton
            // 
            this.GuessButton.Location = new System.Drawing.Point(292, 105);
            this.GuessButton.Name = "GuessButton";
            this.GuessButton.Size = new System.Drawing.Size(105, 22);
            this.GuessButton.TabIndex = 13;
            this.GuessButton.Text = "Guess Artist/Album";
            this.GuessButton.UseVisualStyleBackColor = true;
            this.GuessButton.Click += new System.EventHandler(this.GuessButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(12, 38);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(179, 23);
            this.CopyButton.TabIndex = 4;
            this.CopyButton.Text = "↓ Copy input folder to temp folder ↓";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(740, 11);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(53, 22);
            this.SettingsButton.TabIndex = 38;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // CopyBackgroundWorker
            // 
            this.CopyBackgroundWorker.WorkerReportsProgress = true;
            this.CopyBackgroundWorker.WorkerSupportsCancellation = true;
            this.CopyBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CopyBackgroundWorker_DoWork);
            this.CopyBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CopyBackgroundWorker_RunWorkerCompleted);
            // 
            // DeleteTempFolderCheckbox
            // 
            this.DeleteTempFolderCheckbox.AutoSize = true;
            this.DeleteTempFolderCheckbox.Location = new System.Drawing.Point(12, 374);
            this.DeleteTempFolderCheckbox.Name = "DeleteTempFolderCheckbox";
            this.DeleteTempFolderCheckbox.Size = new System.Drawing.Size(147, 17);
            this.DeleteTempFolderCheckbox.TabIndex = 33;
            this.DeleteTempFolderCheckbox.Text = "Delete source temp folder";
            this.DeleteTempFolderCheckbox.UseVisualStyleBackColor = true;
            // 
            // RedactedButton
            // 
            this.RedactedButton.Location = new System.Drawing.Point(172, 145);
            this.RedactedButton.Name = "RedactedButton";
            this.RedactedButton.Size = new System.Drawing.Size(74, 22);
            this.RedactedButton.TabIndex = 16;
            this.RedactedButton.Text = "Redacted";
            this.RedactedButton.UseVisualStyleBackColor = true;
            this.RedactedButton.Visible = false;
            this.RedactedButton.Click += new System.EventHandler(this.RedactedButton_Click);
            // 
            // RenameLogCueCheckbox
            // 
            this.RenameLogCueCheckbox.AutoSize = true;
            this.RenameLogCueCheckbox.Location = new System.Drawing.Point(28, 305);
            this.RenameLogCueCheckbox.Name = "RenameLogCueCheckbox";
            this.RenameLogCueCheckbox.Size = new System.Drawing.Size(371, 17);
            this.RenameLogCueCheckbox.TabIndex = 28;
            this.RenameLogCueCheckbox.Text = "Rename .log and .cue to %albumartist% - %album%.log and %album%.cue";
            this.RenameLogCueCheckbox.UseVisualStyleBackColor = true;
            // 
            // StripImageMetadataCheckbox
            // 
            this.StripImageMetadataCheckbox.AutoSize = true;
            this.StripImageMetadataCheckbox.Location = new System.Drawing.Point(28, 328);
            this.StripImageMetadataCheckbox.Name = "StripImageMetadataCheckbox";
            this.StripImageMetadataCheckbox.Size = new System.Drawing.Size(215, 17);
            this.StripImageMetadataCheckbox.TabIndex = 29;
            this.StripImageMetadataCheckbox.Text = "Strip image metadata (bmp, gif, jpg, png)";
            this.StripImageMetadataCheckbox.UseVisualStyleBackColor = true;
            // 
            // OutputExplorerButton
            // 
            this.OutputExplorerButton.Location = new System.Drawing.Point(537, 201);
            this.OutputExplorerButton.Name = "OutputExplorerButton";
            this.OutputExplorerButton.Size = new System.Drawing.Size(93, 22);
            this.OutputExplorerButton.TabIndex = 23;
            this.OutputExplorerButton.Text = "Open in Explorer";
            this.OutputExplorerButton.UseVisualStyleBackColor = true;
            this.OutputExplorerButton.Click += new System.EventHandler(this.OutputExplorerButton_Click);
            // 
            // OutputPathButton
            // 
            this.OutputPathButton.Location = new System.Drawing.Point(438, 201);
            this.OutputPathButton.Name = "OutputPathButton";
            this.OutputPathButton.Size = new System.Drawing.Size(93, 22);
            this.OutputPathButton.TabIndex = 22;
            this.OutputPathButton.Text = "Choose Folder";
            this.OutputPathButton.UseVisualStyleBackColor = true;
            this.OutputPathButton.Click += new System.EventHandler(this.OutputPathButton_Click);
            // 
            // AutoWavConvertCheckbox
            // 
            this.AutoWavConvertCheckbox.AutoSize = true;
            this.AutoWavConvertCheckbox.Location = new System.Drawing.Point(197, 42);
            this.AutoWavConvertCheckbox.Name = "AutoWavConvertCheckbox";
            this.AutoWavConvertCheckbox.Size = new System.Drawing.Size(171, 17);
            this.AutoWavConvertCheckbox.TabIndex = 5;
            this.AutoWavConvertCheckbox.Text = "Convert input .wav files to .flac";
            this.AutoWavConvertCheckbox.UseVisualStyleBackColor = true;
            // 
            // ResetPathsButton
            // 
            this.ResetPathsButton.Location = new System.Drawing.Point(497, 38);
            this.ResetPathsButton.Name = "ResetPathsButton";
            this.ResetPathsButton.Size = new System.Drawing.Size(73, 23);
            this.ResetPathsButton.TabIndex = 6;
            this.ResetPathsButton.Text = "Reset Paths";
            this.ResetPathsButton.UseVisualStyleBackColor = true;
            this.ResetPathsButton.Click += new System.EventHandler(this.ResetPathsButton_Click);
            // 
            // ExcelExportCheckbox
            // 
            this.ExcelExportCheckbox.AutoSize = true;
            this.ExcelExportCheckbox.Location = new System.Drawing.Point(12, 351);
            this.ExcelExportCheckbox.Name = "ExcelExportCheckbox";
            this.ExcelExportCheckbox.Size = new System.Drawing.Size(192, 17);
            this.ExcelExportCheckbox.TabIndex = 30;
            this.ExcelExportCheckbox.Text = "Append parsed data to Excel sheet";
            this.ExcelExportCheckbox.UseVisualStyleBackColor = true;
            // 
            // ExcelLogScoreTextBox
            // 
            this.ExcelLogScoreTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ExcelLogScoreTextBox.Location = new System.Drawing.Point(210, 349);
            this.ExcelLogScoreTextBox.Name = "ExcelLogScoreTextBox";
            this.ExcelLogScoreTextBox.Size = new System.Drawing.Size(76, 20);
            this.ExcelLogScoreTextBox.TabIndex = 31;
            this.ExcelLogScoreTextBox.Text = "Log score";
            this.ExcelLogScoreTextBox.Enter += new System.EventHandler(this.ExcelLogScoreTextBox_Enter);
            this.ExcelLogScoreTextBox.Leave += new System.EventHandler(this.ExcelLogScoreTextBox_Leave);
            // 
            // ExcelNotesTextBox
            // 
            this.ExcelNotesTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ExcelNotesTextBox.Location = new System.Drawing.Point(292, 349);
            this.ExcelNotesTextBox.Name = "ExcelNotesTextBox";
            this.ExcelNotesTextBox.Size = new System.Drawing.Size(316, 20);
            this.ExcelNotesTextBox.TabIndex = 32;
            this.ExcelNotesTextBox.Text = "Notes";
            this.ExcelNotesTextBox.Enter += new System.EventHandler(this.ExcelNotesTextBox_Enter);
            this.ExcelNotesTextBox.Leave += new System.EventHandler(this.ExcelNotesTextBox_Leave);
            // 
            // InputPathUpOneButton
            // 
            this.InputPathUpOneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputPathUpOneButton.Location = new System.Drawing.Point(410, 11);
            this.InputPathUpOneButton.Name = "InputPathUpOneButton";
            this.InputPathUpOneButton.Size = new System.Drawing.Size(22, 22);
            this.InputPathUpOneButton.TabIndex = 1;
            this.InputPathUpOneButton.Text = "↑";
            this.InputPathUpOneButton.UseVisualStyleBackColor = true;
            this.InputPathUpOneButton.Click += new System.EventHandler(this.InputPathUpOneButton_Click);
            // 
            // TempPathUpOneButton
            // 
            this.TempPathUpOneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TempPathUpOneButton.Location = new System.Drawing.Point(410, 66);
            this.TempPathUpOneButton.Name = "TempPathUpOneButton";
            this.TempPathUpOneButton.Size = new System.Drawing.Size(22, 22);
            this.TempPathUpOneButton.TabIndex = 8;
            this.TempPathUpOneButton.Text = "↑";
            this.TempPathUpOneButton.UseVisualStyleBackColor = true;
            this.TempPathUpOneButton.Click += new System.EventHandler(this.TempPathUpOneButton_Click);
            // 
            // OutputPathUpOneButton
            // 
            this.OutputPathUpOneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputPathUpOneButton.Location = new System.Drawing.Point(410, 201);
            this.OutputPathUpOneButton.Name = "OutputPathUpOneButton";
            this.OutputPathUpOneButton.Size = new System.Drawing.Size(22, 22);
            this.OutputPathUpOneButton.TabIndex = 21;
            this.OutputPathUpOneButton.Text = "↑";
            this.OutputPathUpOneButton.UseVisualStyleBackColor = true;
            this.OutputPathUpOneButton.Click += new System.EventHandler(this.OutputPathUpOneButton_Click);
            // 
            // PresetComboBox
            // 
            this.PresetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PresetComboBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PresetComboBox.FormattingEnabled = true;
            this.PresetComboBox.Location = new System.Drawing.Point(68, 420);
            this.PresetComboBox.Name = "PresetComboBox";
            this.PresetComboBox.Size = new System.Drawing.Size(161, 21);
            this.PresetComboBox.TabIndex = 39;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 453);
            this.Controls.Add(this.PresetComboBox);
            this.Controls.Add(this.OutputPathUpOneButton);
            this.Controls.Add(this.TempPathUpOneButton);
            this.Controls.Add(this.InputPathUpOneButton);
            this.Controls.Add(this.ExcelNotesTextBox);
            this.Controls.Add(this.ExcelLogScoreTextBox);
            this.Controls.Add(this.ExcelExportCheckbox);
            this.Controls.Add(this.ResetPathsButton);
            this.Controls.Add(this.AutoWavConvertCheckbox);
            this.Controls.Add(this.OutputPathButton);
            this.Controls.Add(this.OutputExplorerButton);
            this.Controls.Add(this.StripImageMetadataCheckbox);
            this.Controls.Add(this.RenameLogCueCheckbox);
            this.Controls.Add(this.RedactedButton);
            this.Controls.Add(this.DeleteTempFolderCheckbox);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.GuessButton);
            this.Controls.Add(this.SpectrogramsButton);
            this.Controls.Add(this.CopyFileTypesTextBox);
            this.Controls.Add(this.ConvertOpenFolderCheckbox);
            this.Controls.Add(this.CopyContentsCheckbox);
            this.Controls.Add(this.ReplayGainCheckbox);
            this.Controls.Add(this.OutputPathTextBox);
            this.Controls.Add(this.OutputNamingSyntaxTextBox);
            this.Controls.Add(this.ConvertButton);
            this.Controls.Add(this.ConvertToComboBox);
            this.Controls.Add(this.Mp3tagButton);
            this.Controls.Add(this.AADButton);
            this.Controls.Add(this.MusicBrainzButton);
            this.Controls.Add(this.DiscogsButton);
            this.Controls.Add(this.AlbumTextBox);
            this.Controls.Add(this.ArtistTextBox);
            this.Controls.Add(this.TempExplorerButton);
            this.Controls.Add(this.TempPathButton);
            this.Controls.Add(this.TempPathBox);
            this.Controls.Add(this.InputExplorerButton);
            this.Controls.Add(this.InputPathButton);
            this.Controls.Add(this.InputPathBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "MusicImportKit v2.0.5";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputPathBox;
        private System.Windows.Forms.Button InputPathButton;
        private System.Windows.Forms.Button InputExplorerButton;
        private System.Windows.Forms.Button TempExplorerButton;
        private System.Windows.Forms.Button TempPathButton;
        private System.Windows.Forms.TextBox TempPathBox;
        private System.Windows.Forms.TextBox ArtistTextBox;
        private System.Windows.Forms.TextBox AlbumTextBox;
        private System.Windows.Forms.Button DiscogsButton;
        private System.Windows.Forms.Button MusicBrainzButton;
        private System.Windows.Forms.Button AADButton;
        private System.Windows.Forms.Button Mp3tagButton;
        private System.Windows.Forms.ComboBox ConvertToComboBox;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.ComboBox OutputNamingSyntaxTextBox;
        private System.Windows.Forms.TextBox OutputPathTextBox;
        private System.Windows.Forms.ToolTip OutputNamingSyntaxTooltip;
        private System.ComponentModel.BackgroundWorker ConvertBackgroundWorker;
        private System.Windows.Forms.CheckBox ReplayGainCheckbox;
        private System.Windows.Forms.CheckBox CopyContentsCheckbox;
        private System.Windows.Forms.CheckBox ConvertOpenFolderCheckbox;
        private System.Windows.Forms.TextBox CopyFileTypesTextBox;
        private System.Windows.Forms.Button SpectrogramsButton;
        private System.Windows.Forms.Button GuessButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button SettingsButton;
        private System.ComponentModel.BackgroundWorker CopyBackgroundWorker;
        private System.Windows.Forms.ToolTip CopyFileTypesTooltip;
        private System.Windows.Forms.CheckBox DeleteTempFolderCheckbox;
        private System.Windows.Forms.Button RedactedButton;
        private System.Windows.Forms.CheckBox RenameLogCueCheckbox;
        private System.Windows.Forms.CheckBox StripImageMetadataCheckbox;
        private System.Windows.Forms.Button OutputExplorerButton;
        private System.Windows.Forms.Button OutputPathButton;
        private System.Windows.Forms.CheckBox AutoWavConvertCheckbox;
        private System.Windows.Forms.Button ResetPathsButton;
        private System.Windows.Forms.CheckBox ExcelExportCheckbox;
        private System.Windows.Forms.TextBox ExcelLogScoreTextBox;
        private System.Windows.Forms.TextBox ExcelNotesTextBox;
        private System.Windows.Forms.Button InputPathUpOneButton;
        private System.Windows.Forms.Button TempPathUpOneButton;
        private System.Windows.Forms.Button OutputPathUpOneButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox PresetComboBox;
    }
}

