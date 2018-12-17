namespace MusicImportKit
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.Mp3tagLocationTextBox = new System.Windows.Forms.TextBox();
            this.AADLocationTextBox = new System.Windows.Forms.TextBox();
            this.SpekLocationTextBox = new System.Windows.Forms.TextBox();
            this.FlacLocationTextBox = new System.Windows.Forms.TextBox();
            this.SoxLocationTextBox = new System.Windows.Forms.TextBox();
            this.DefaultTempFolderLocationTextBox = new System.Windows.Forms.TextBox();
            this.DefaultInputFolderLocationTextBox = new System.Windows.Forms.TextBox();
            this.DefaultOutputFolderLocationTextBox = new System.Windows.Forms.TextBox();
            this.DefaultRGState = new System.Windows.Forms.CheckBox();
            this.DefaultCopyFiletypesState = new System.Windows.Forms.CheckBox();
            this.DefaultOpenFolderState = new System.Windows.Forms.CheckBox();
            this.DefaultConvertToComboBox = new System.Windows.Forms.ComboBox();
            this.DefaultConvertToLabel = new System.Windows.Forms.Label();
            this.AcceptSettingsButton = new System.Windows.Forms.Button();
            this.CancelSettingsButton = new System.Windows.Forms.Button();
            this.DefaultSpecificFiletypeTextTextBox = new System.Windows.Forms.TextBox();
            this.DefaultTempFolderDeleteState = new System.Windows.Forms.CheckBox();
            this.DefaultRedactedState = new System.Windows.Forms.CheckBox();
            this.DefaultRenameLogCueState = new System.Windows.Forms.CheckBox();
            this.DefaultStripImageMetadataState = new System.Windows.Forms.CheckBox();
            this.DefaultInputPathButton = new System.Windows.Forms.Button();
            this.DefaultTempPathButton = new System.Windows.Forms.Button();
            this.DefaultOutputPathButton = new System.Windows.Forms.Button();
            this.LameLocationTextBox = new System.Windows.Forms.TextBox();
            this.OpusencLocationTextBox = new System.Windows.Forms.TextBox();
            this.DefaultAutoWavConvertState = new System.Windows.Forms.CheckBox();
            this.DefaultExcelAppendState = new System.Windows.Forms.CheckBox();
            this.DefaultExcelLocationTextBox = new System.Windows.Forms.TextBox();
            this.DefaultAADPathButton = new System.Windows.Forms.Button();
            this.DefaultFlacPathButton = new System.Windows.Forms.Button();
            this.DefaultLamePathButton = new System.Windows.Forms.Button();
            this.DefaultMp3tagPathButton = new System.Windows.Forms.Button();
            this.DefaultOpusPathButton = new System.Windows.Forms.Button();
            this.DefaultSoxPathButton = new System.Windows.Forms.Button();
            this.DefaultSpekPathButton = new System.Windows.Forms.Button();
            this.DefaultExcelSheetPathButton = new System.Windows.Forms.Button();
            this.ParseSyntaxTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.DefaultParseStyle = new System.Windows.Forms.ComboBox();
            this.DefaultPresetComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Mp3tagLocationTextBox
            // 
            this.Mp3tagLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Mp3tagLocationTextBox.Location = new System.Drawing.Point(480, 91);
            this.Mp3tagLocationTextBox.Name = "Mp3tagLocationTextBox";
            this.Mp3tagLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.Mp3tagLocationTextBox.TabIndex = 30;
            this.Mp3tagLocationTextBox.Text = "Mp3tag.exe location";
            this.Mp3tagLocationTextBox.Enter += new System.EventHandler(this.Mp3tagLocationTextBox_Enter);
            this.Mp3tagLocationTextBox.Leave += new System.EventHandler(this.Mp3tagLocationTextBox_Leave);
            // 
            // AADLocationTextBox
            // 
            this.AADLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.AADLocationTextBox.Location = new System.Drawing.Point(480, 12);
            this.AADLocationTextBox.Name = "AADLocationTextBox";
            this.AADLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.AADLocationTextBox.TabIndex = 22;
            this.AADLocationTextBox.Text = "AlbumArt.exe location";
            this.AADLocationTextBox.Enter += new System.EventHandler(this.AADLocationTextBox_Enter);
            this.AADLocationTextBox.Leave += new System.EventHandler(this.AADLocationTextBox_Leave);
            // 
            // SpekLocationTextBox
            // 
            this.SpekLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.SpekLocationTextBox.Location = new System.Drawing.Point(480, 169);
            this.SpekLocationTextBox.Name = "SpekLocationTextBox";
            this.SpekLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.SpekLocationTextBox.TabIndex = 36;
            this.SpekLocationTextBox.Text = "spek.exe location";
            this.SpekLocationTextBox.Enter += new System.EventHandler(this.SpekLocationTextBox_Enter);
            this.SpekLocationTextBox.Leave += new System.EventHandler(this.SpekLocationTextBox_Leave);
            // 
            // FlacLocationTextBox
            // 
            this.FlacLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.FlacLocationTextBox.Location = new System.Drawing.Point(480, 38);
            this.FlacLocationTextBox.Name = "FlacLocationTextBox";
            this.FlacLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.FlacLocationTextBox.TabIndex = 24;
            this.FlacLocationTextBox.Text = "flac.exe location";
            this.FlacLocationTextBox.Enter += new System.EventHandler(this.FlacLocationTextBox_Enter);
            this.FlacLocationTextBox.Leave += new System.EventHandler(this.FlacLocationTextBox_Leave);
            // 
            // SoxLocationTextBox
            // 
            this.SoxLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.SoxLocationTextBox.Location = new System.Drawing.Point(480, 143);
            this.SoxLocationTextBox.Name = "SoxLocationTextBox";
            this.SoxLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.SoxLocationTextBox.TabIndex = 34;
            this.SoxLocationTextBox.Text = "sox.exe location";
            this.SoxLocationTextBox.Enter += new System.EventHandler(this.SoxLocationTextBox_Enter);
            this.SoxLocationTextBox.Leave += new System.EventHandler(this.SoxLocationTextBox_Leave);
            // 
            // DefaultTempFolderLocationTextBox
            // 
            this.DefaultTempFolderLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DefaultTempFolderLocationTextBox.Location = new System.Drawing.Point(12, 38);
            this.DefaultTempFolderLocationTextBox.Name = "DefaultTempFolderLocationTextBox";
            this.DefaultTempFolderLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.DefaultTempFolderLocationTextBox.TabIndex = 2;
            this.DefaultTempFolderLocationTextBox.Text = "Default Temp Folder";
            this.DefaultTempFolderLocationTextBox.Enter += new System.EventHandler(this.DefaultTempFolderLocationTextBox_Enter);
            this.DefaultTempFolderLocationTextBox.Leave += new System.EventHandler(this.DefaultTempFolderLocationTextBox_Leave);
            // 
            // DefaultInputFolderLocationTextBox
            // 
            this.DefaultInputFolderLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DefaultInputFolderLocationTextBox.Location = new System.Drawing.Point(12, 12);
            this.DefaultInputFolderLocationTextBox.Name = "DefaultInputFolderLocationTextBox";
            this.DefaultInputFolderLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.DefaultInputFolderLocationTextBox.TabIndex = 0;
            this.DefaultInputFolderLocationTextBox.Text = "Default Input Folder";
            this.DefaultInputFolderLocationTextBox.Enter += new System.EventHandler(this.DefaultInputFolderLocationTextBox_Enter);
            this.DefaultInputFolderLocationTextBox.Leave += new System.EventHandler(this.DefaultInputFolderLocationTextBox_Leave);
            // 
            // DefaultOutputFolderLocationTextBox
            // 
            this.DefaultOutputFolderLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DefaultOutputFolderLocationTextBox.Location = new System.Drawing.Point(12, 64);
            this.DefaultOutputFolderLocationTextBox.Name = "DefaultOutputFolderLocationTextBox";
            this.DefaultOutputFolderLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.DefaultOutputFolderLocationTextBox.TabIndex = 4;
            this.DefaultOutputFolderLocationTextBox.Text = "Default Output Folder (base path)";
            this.DefaultOutputFolderLocationTextBox.Enter += new System.EventHandler(this.DefaultOutputFolderLocationTextBox_Enter);
            this.DefaultOutputFolderLocationTextBox.Leave += new System.EventHandler(this.DefaultOutputFolderLocationTextBox_Leave);
            // 
            // DefaultRGState
            // 
            this.DefaultRGState.AutoSize = true;
            this.DefaultRGState.Location = new System.Drawing.Point(12, 162);
            this.DefaultRGState.Name = "DefaultRGState";
            this.DefaultRGState.Size = new System.Drawing.Size(171, 17);
            this.DefaultRGState.TabIndex = 9;
            this.DefaultRGState.Text = "ReplayGain enabled by default";
            this.DefaultRGState.UseVisualStyleBackColor = true;
            // 
            // DefaultCopyFiletypesState
            // 
            this.DefaultCopyFiletypesState.AutoSize = true;
            this.DefaultCopyFiletypesState.Location = new System.Drawing.Point(12, 185);
            this.DefaultCopyFiletypesState.Name = "DefaultCopyFiletypesState";
            this.DefaultCopyFiletypesState.Size = new System.Drawing.Size(179, 17);
            this.DefaultCopyFiletypesState.TabIndex = 10;
            this.DefaultCopyFiletypesState.Text = "Copy specific filetypes by default";
            this.DefaultCopyFiletypesState.UseVisualStyleBackColor = true;
            // 
            // DefaultOpenFolderState
            // 
            this.DefaultOpenFolderState.AutoSize = true;
            this.DefaultOpenFolderState.Location = new System.Drawing.Point(12, 326);
            this.DefaultOpenFolderState.Name = "DefaultOpenFolderState";
            this.DefaultOpenFolderState.Size = new System.Drawing.Size(186, 17);
            this.DefaultOpenFolderState.TabIndex = 18;
            this.DefaultOpenFolderState.Text = "Open folder when done by default";
            this.DefaultOpenFolderState.UseVisualStyleBackColor = true;
            // 
            // DefaultConvertToComboBox
            // 
            this.DefaultConvertToComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DefaultConvertToComboBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DefaultConvertToComboBox.FormattingEnabled = true;
            this.DefaultConvertToComboBox.Items.AddRange(new object[] {
            "FLAC",
            "MP3",
            "Opus"});
            this.DefaultConvertToComboBox.Location = new System.Drawing.Point(12, 376);
            this.DefaultConvertToComboBox.Name = "DefaultConvertToComboBox";
            this.DefaultConvertToComboBox.Size = new System.Drawing.Size(50, 21);
            this.DefaultConvertToComboBox.TabIndex = 19;
            this.DefaultConvertToComboBox.SelectedIndexChanged += new System.EventHandler(this.DefaultConvertToComboBox_SelectedIndexChanged);
            // 
            // DefaultConvertToLabel
            // 
            this.DefaultConvertToLabel.AutoSize = true;
            this.DefaultConvertToLabel.Location = new System.Drawing.Point(12, 360);
            this.DefaultConvertToLabel.Name = "DefaultConvertToLabel";
            this.DefaultConvertToLabel.Size = new System.Drawing.Size(132, 13);
            this.DefaultConvertToLabel.TabIndex = 21;
            this.DefaultConvertToLabel.Text = "Default Conversion Format";
            // 
            // AcceptSettingsButton
            // 
            this.AcceptSettingsButton.Location = new System.Drawing.Point(399, 375);
            this.AcceptSettingsButton.Name = "AcceptSettingsButton";
            this.AcceptSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptSettingsButton.TabIndex = 38;
            this.AcceptSettingsButton.Text = "Accept";
            this.AcceptSettingsButton.UseVisualStyleBackColor = true;
            this.AcceptSettingsButton.Click += new System.EventHandler(this.AcceptSettingsButton_Click);
            // 
            // CancelSettingsButton
            // 
            this.CancelSettingsButton.Location = new System.Drawing.Point(480, 375);
            this.CancelSettingsButton.Name = "CancelSettingsButton";
            this.CancelSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.CancelSettingsButton.TabIndex = 39;
            this.CancelSettingsButton.Text = "Cancel";
            this.CancelSettingsButton.UseVisualStyleBackColor = true;
            this.CancelSettingsButton.Click += new System.EventHandler(this.CancelSettingsButton_Click);
            // 
            // DefaultSpecificFiletypeTextTextBox
            // 
            this.DefaultSpecificFiletypeTextTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DefaultSpecificFiletypeTextTextBox.Location = new System.Drawing.Point(197, 183);
            this.DefaultSpecificFiletypeTextTextBox.Name = "DefaultSpecificFiletypeTextTextBox";
            this.DefaultSpecificFiletypeTextTextBox.Size = new System.Drawing.Size(188, 20);
            this.DefaultSpecificFiletypeTextTextBox.TabIndex = 11;
            this.DefaultSpecificFiletypeTextTextBox.Text = "Filetypes to copy";
            this.DefaultSpecificFiletypeTextTextBox.Enter += new System.EventHandler(this.DefaultSpecificFiletypeTextTextBox_Enter);
            this.DefaultSpecificFiletypeTextTextBox.Leave += new System.EventHandler(this.DefaultSpecificFiletypeTextTextBox_Leave);
            // 
            // DefaultTempFolderDeleteState
            // 
            this.DefaultTempFolderDeleteState.AutoSize = true;
            this.DefaultTempFolderDeleteState.Location = new System.Drawing.Point(12, 303);
            this.DefaultTempFolderDeleteState.Name = "DefaultTempFolderDeleteState";
            this.DefaultTempFolderDeleteState.Size = new System.Drawing.Size(196, 17);
            this.DefaultTempFolderDeleteState.TabIndex = 17;
            this.DefaultTempFolderDeleteState.Text = "Delete source temp folder by default";
            this.DefaultTempFolderDeleteState.UseVisualStyleBackColor = true;
            // 
            // DefaultRedactedState
            // 
            this.DefaultRedactedState.AutoSize = true;
            this.DefaultRedactedState.Location = new System.Drawing.Point(12, 139);
            this.DefaultRedactedState.Name = "DefaultRedactedState";
            this.DefaultRedactedState.Size = new System.Drawing.Size(164, 17);
            this.DefaultRedactedState.TabIndex = 8;
            this.DefaultRedactedState.Text = "Redacted.ch search enabled";
            this.DefaultRedactedState.UseVisualStyleBackColor = true;
            // 
            // DefaultRenameLogCueState
            // 
            this.DefaultRenameLogCueState.AutoSize = true;
            this.DefaultRenameLogCueState.Location = new System.Drawing.Point(28, 208);
            this.DefaultRenameLogCueState.Name = "DefaultRenameLogCueState";
            this.DefaultRenameLogCueState.Size = new System.Drawing.Size(180, 17);
            this.DefaultRenameLogCueState.TabIndex = 12;
            this.DefaultRenameLogCueState.Text = "Rename .log and .cue by default";
            this.DefaultRenameLogCueState.UseVisualStyleBackColor = true;
            // 
            // DefaultStripImageMetadataState
            // 
            this.DefaultStripImageMetadataState.AutoSize = true;
            this.DefaultStripImageMetadataState.Location = new System.Drawing.Point(28, 231);
            this.DefaultStripImageMetadataState.Name = "DefaultStripImageMetadataState";
            this.DefaultStripImageMetadataState.Size = new System.Drawing.Size(174, 17);
            this.DefaultStripImageMetadataState.TabIndex = 13;
            this.DefaultStripImageMetadataState.Text = "Strip image metadata by default";
            this.DefaultStripImageMetadataState.UseVisualStyleBackColor = true;
            // 
            // DefaultInputPathButton
            // 
            this.DefaultInputPathButton.Location = new System.Drawing.Point(391, 11);
            this.DefaultInputPathButton.Name = "DefaultInputPathButton";
            this.DefaultInputPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultInputPathButton.TabIndex = 1;
            this.DefaultInputPathButton.Text = "Choose Folder";
            this.DefaultInputPathButton.UseVisualStyleBackColor = true;
            this.DefaultInputPathButton.Click += new System.EventHandler(this.DefaultInputPathButton_Click);
            // 
            // DefaultTempPathButton
            // 
            this.DefaultTempPathButton.Location = new System.Drawing.Point(391, 37);
            this.DefaultTempPathButton.Name = "DefaultTempPathButton";
            this.DefaultTempPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultTempPathButton.TabIndex = 3;
            this.DefaultTempPathButton.Text = "Choose Folder";
            this.DefaultTempPathButton.UseVisualStyleBackColor = true;
            this.DefaultTempPathButton.Click += new System.EventHandler(this.DefaultTempPathButton_Click);
            // 
            // DefaultOutputPathButton
            // 
            this.DefaultOutputPathButton.Location = new System.Drawing.Point(391, 63);
            this.DefaultOutputPathButton.Name = "DefaultOutputPathButton";
            this.DefaultOutputPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultOutputPathButton.TabIndex = 5;
            this.DefaultOutputPathButton.Text = "Choose Folder";
            this.DefaultOutputPathButton.UseVisualStyleBackColor = true;
            this.DefaultOutputPathButton.Click += new System.EventHandler(this.DefaultOutputPathButton_Click);
            // 
            // LameLocationTextBox
            // 
            this.LameLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.LameLocationTextBox.Location = new System.Drawing.Point(480, 64);
            this.LameLocationTextBox.Name = "LameLocationTextBox";
            this.LameLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.LameLocationTextBox.TabIndex = 26;
            this.LameLocationTextBox.Text = "lame.exe location";
            this.LameLocationTextBox.Enter += new System.EventHandler(this.LameLocationTextBox_Enter);
            this.LameLocationTextBox.Leave += new System.EventHandler(this.LameLocationTextBox_Leave);
            // 
            // OpusencLocationTextBox
            // 
            this.OpusencLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.OpusencLocationTextBox.Location = new System.Drawing.Point(480, 117);
            this.OpusencLocationTextBox.Name = "OpusencLocationTextBox";
            this.OpusencLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.OpusencLocationTextBox.TabIndex = 32;
            this.OpusencLocationTextBox.Text = "opusenc.exe location";
            this.OpusencLocationTextBox.Enter += new System.EventHandler(this.OpusencLocationTextBox_Enter);
            this.OpusencLocationTextBox.Leave += new System.EventHandler(this.OpusencLocationTextBox_Leave);
            // 
            // DefaultAutoWavConvertState
            // 
            this.DefaultAutoWavConvertState.AutoSize = true;
            this.DefaultAutoWavConvertState.Location = new System.Drawing.Point(12, 116);
            this.DefaultAutoWavConvertState.Name = "DefaultAutoWavConvertState";
            this.DefaultAutoWavConvertState.Size = new System.Drawing.Size(220, 17);
            this.DefaultAutoWavConvertState.TabIndex = 7;
            this.DefaultAutoWavConvertState.Text = "Convert input .wav files to .flac by default";
            this.DefaultAutoWavConvertState.UseVisualStyleBackColor = true;
            // 
            // DefaultExcelAppendState
            // 
            this.DefaultExcelAppendState.AutoSize = true;
            this.DefaultExcelAppendState.Location = new System.Drawing.Point(12, 254);
            this.DefaultExcelAppendState.Name = "DefaultExcelAppendState";
            this.DefaultExcelAppendState.Size = new System.Drawing.Size(241, 17);
            this.DefaultExcelAppendState.TabIndex = 14;
            this.DefaultExcelAppendState.Text = "Append parsed data to Excel sheet by default";
            this.DefaultExcelAppendState.UseVisualStyleBackColor = true;
            // 
            // DefaultExcelLocationTextBox
            // 
            this.DefaultExcelLocationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DefaultExcelLocationTextBox.Location = new System.Drawing.Point(12, 277);
            this.DefaultExcelLocationTextBox.Name = "DefaultExcelLocationTextBox";
            this.DefaultExcelLocationTextBox.Size = new System.Drawing.Size(373, 20);
            this.DefaultExcelLocationTextBox.TabIndex = 15;
            this.DefaultExcelLocationTextBox.Text = "Default Excel sheet location";
            this.DefaultExcelLocationTextBox.Enter += new System.EventHandler(this.DefaultExcelLocationTextBox_Enter);
            this.DefaultExcelLocationTextBox.Leave += new System.EventHandler(this.DefaultExcelLocationTextBox_Leave);
            // 
            // DefaultAADPathButton
            // 
            this.DefaultAADPathButton.Location = new System.Drawing.Point(859, 11);
            this.DefaultAADPathButton.Name = "DefaultAADPathButton";
            this.DefaultAADPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultAADPathButton.TabIndex = 23;
            this.DefaultAADPathButton.Text = "Choose File";
            this.DefaultAADPathButton.UseVisualStyleBackColor = true;
            this.DefaultAADPathButton.Click += new System.EventHandler(this.DefaultAADPathButton_Click);
            // 
            // DefaultFlacPathButton
            // 
            this.DefaultFlacPathButton.Location = new System.Drawing.Point(859, 37);
            this.DefaultFlacPathButton.Name = "DefaultFlacPathButton";
            this.DefaultFlacPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultFlacPathButton.TabIndex = 25;
            this.DefaultFlacPathButton.Text = "Choose File";
            this.DefaultFlacPathButton.UseVisualStyleBackColor = true;
            this.DefaultFlacPathButton.Click += new System.EventHandler(this.DefaultFlacPathButton_Click);
            // 
            // DefaultLamePathButton
            // 
            this.DefaultLamePathButton.Location = new System.Drawing.Point(859, 63);
            this.DefaultLamePathButton.Name = "DefaultLamePathButton";
            this.DefaultLamePathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultLamePathButton.TabIndex = 27;
            this.DefaultLamePathButton.Text = "Choose File";
            this.DefaultLamePathButton.UseVisualStyleBackColor = true;
            this.DefaultLamePathButton.Click += new System.EventHandler(this.DefaultLamePathButton_Click);
            // 
            // DefaultMp3tagPathButton
            // 
            this.DefaultMp3tagPathButton.Location = new System.Drawing.Point(859, 90);
            this.DefaultMp3tagPathButton.Name = "DefaultMp3tagPathButton";
            this.DefaultMp3tagPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultMp3tagPathButton.TabIndex = 31;
            this.DefaultMp3tagPathButton.Text = "Choose File";
            this.DefaultMp3tagPathButton.UseVisualStyleBackColor = true;
            this.DefaultMp3tagPathButton.Click += new System.EventHandler(this.DefaultMp3tagPathButton_Click);
            // 
            // DefaultOpusPathButton
            // 
            this.DefaultOpusPathButton.Location = new System.Drawing.Point(859, 116);
            this.DefaultOpusPathButton.Name = "DefaultOpusPathButton";
            this.DefaultOpusPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultOpusPathButton.TabIndex = 33;
            this.DefaultOpusPathButton.Text = "Choose File";
            this.DefaultOpusPathButton.UseVisualStyleBackColor = true;
            this.DefaultOpusPathButton.Click += new System.EventHandler(this.DefaultOpusPathButton_Click);
            // 
            // DefaultSoxPathButton
            // 
            this.DefaultSoxPathButton.Location = new System.Drawing.Point(859, 142);
            this.DefaultSoxPathButton.Name = "DefaultSoxPathButton";
            this.DefaultSoxPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultSoxPathButton.TabIndex = 35;
            this.DefaultSoxPathButton.Text = "Choose File";
            this.DefaultSoxPathButton.UseVisualStyleBackColor = true;
            this.DefaultSoxPathButton.Click += new System.EventHandler(this.DefaultSoxPathButton_Click);
            // 
            // DefaultSpekPathButton
            // 
            this.DefaultSpekPathButton.Location = new System.Drawing.Point(859, 168);
            this.DefaultSpekPathButton.Name = "DefaultSpekPathButton";
            this.DefaultSpekPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultSpekPathButton.TabIndex = 37;
            this.DefaultSpekPathButton.Text = "Choose File";
            this.DefaultSpekPathButton.UseVisualStyleBackColor = true;
            this.DefaultSpekPathButton.Click += new System.EventHandler(this.DefaultSpekPathButton_Click);
            // 
            // DefaultExcelSheetPathButton
            // 
            this.DefaultExcelSheetPathButton.Location = new System.Drawing.Point(391, 276);
            this.DefaultExcelSheetPathButton.Name = "DefaultExcelSheetPathButton";
            this.DefaultExcelSheetPathButton.Size = new System.Drawing.Size(83, 22);
            this.DefaultExcelSheetPathButton.TabIndex = 16;
            this.DefaultExcelSheetPathButton.Text = "Choose File";
            this.DefaultExcelSheetPathButton.UseVisualStyleBackColor = true;
            this.DefaultExcelSheetPathButton.Click += new System.EventHandler(this.DefaultExcelSheetPathButton_Click);
            // 
            // DefaultParseStyle
            // 
            this.DefaultParseStyle.DropDownWidth = 630;
            this.DefaultParseStyle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DefaultParseStyle.FormattingEnabled = true;
            this.DefaultParseStyle.Items.AddRange(new object[] {
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
            this.DefaultParseStyle.Location = new System.Drawing.Point(12, 90);
            this.DefaultParseStyle.Name = "DefaultParseStyle";
            this.DefaultParseStyle.Size = new System.Drawing.Size(373, 21);
            this.DefaultParseStyle.TabIndex = 6;
            this.DefaultParseStyle.Text = "Default Output Folder+File Name (syntax in tooltip)";
            this.ParseSyntaxTooltip.SetToolTip(this.DefaultParseStyle, resources.GetString("DefaultParseStyle.ToolTip"));
            this.DefaultParseStyle.Enter += new System.EventHandler(this.DefaultParseStyle_Enter);
            this.DefaultParseStyle.Leave += new System.EventHandler(this.DefaultParseStyle_Leave);
            // 
            // DefaultPresetComboBox
            // 
            this.DefaultPresetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DefaultPresetComboBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DefaultPresetComboBox.FormattingEnabled = true;
            this.DefaultPresetComboBox.Location = new System.Drawing.Point(68, 376);
            this.DefaultPresetComboBox.Name = "DefaultPresetComboBox";
            this.DefaultPresetComboBox.Size = new System.Drawing.Size(161, 21);
            this.DefaultPresetComboBox.TabIndex = 20;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 409);
            this.Controls.Add(this.DefaultParseStyle);
            this.Controls.Add(this.DefaultPresetComboBox);
            this.Controls.Add(this.DefaultExcelSheetPathButton);
            this.Controls.Add(this.DefaultSpekPathButton);
            this.Controls.Add(this.DefaultSoxPathButton);
            this.Controls.Add(this.DefaultOpusPathButton);
            this.Controls.Add(this.DefaultMp3tagPathButton);
            this.Controls.Add(this.DefaultLamePathButton);
            this.Controls.Add(this.DefaultFlacPathButton);
            this.Controls.Add(this.DefaultAADPathButton);
            this.Controls.Add(this.DefaultExcelLocationTextBox);
            this.Controls.Add(this.DefaultExcelAppendState);
            this.Controls.Add(this.DefaultAutoWavConvertState);
            this.Controls.Add(this.OpusencLocationTextBox);
            this.Controls.Add(this.LameLocationTextBox);
            this.Controls.Add(this.DefaultOutputPathButton);
            this.Controls.Add(this.DefaultTempPathButton);
            this.Controls.Add(this.DefaultInputPathButton);
            this.Controls.Add(this.DefaultStripImageMetadataState);
            this.Controls.Add(this.DefaultRenameLogCueState);
            this.Controls.Add(this.DefaultRedactedState);
            this.Controls.Add(this.DefaultTempFolderDeleteState);
            this.Controls.Add(this.DefaultSpecificFiletypeTextTextBox);
            this.Controls.Add(this.CancelSettingsButton);
            this.Controls.Add(this.AcceptSettingsButton);
            this.Controls.Add(this.DefaultConvertToLabel);
            this.Controls.Add(this.DefaultConvertToComboBox);
            this.Controls.Add(this.DefaultOpenFolderState);
            this.Controls.Add(this.DefaultCopyFiletypesState);
            this.Controls.Add(this.DefaultRGState);
            this.Controls.Add(this.DefaultOutputFolderLocationTextBox);
            this.Controls.Add(this.DefaultInputFolderLocationTextBox);
            this.Controls.Add(this.DefaultTempFolderLocationTextBox);
            this.Controls.Add(this.SoxLocationTextBox);
            this.Controls.Add(this.FlacLocationTextBox);
            this.Controls.Add(this.SpekLocationTextBox);
            this.Controls.Add(this.AADLocationTextBox);
            this.Controls.Add(this.Mp3tagLocationTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Mp3tagLocationTextBox;
        private System.Windows.Forms.TextBox AADLocationTextBox;
        private System.Windows.Forms.TextBox SpekLocationTextBox;
        private System.Windows.Forms.TextBox FlacLocationTextBox;
        private System.Windows.Forms.TextBox SoxLocationTextBox;
        private System.Windows.Forms.TextBox DefaultTempFolderLocationTextBox;
        private System.Windows.Forms.TextBox DefaultInputFolderLocationTextBox;
        private System.Windows.Forms.TextBox DefaultOutputFolderLocationTextBox;
        private System.Windows.Forms.CheckBox DefaultRGState;
        private System.Windows.Forms.CheckBox DefaultCopyFiletypesState;
        private System.Windows.Forms.CheckBox DefaultOpenFolderState;
        private System.Windows.Forms.ComboBox DefaultConvertToComboBox;
        private System.Windows.Forms.Label DefaultConvertToLabel;
        private System.Windows.Forms.Button AcceptSettingsButton;
        private System.Windows.Forms.Button CancelSettingsButton;
        private System.Windows.Forms.TextBox DefaultSpecificFiletypeTextTextBox;
        private System.Windows.Forms.CheckBox DefaultTempFolderDeleteState;
        private System.Windows.Forms.CheckBox DefaultRedactedState;
        private System.Windows.Forms.CheckBox DefaultRenameLogCueState;
        private System.Windows.Forms.CheckBox DefaultStripImageMetadataState;
        private System.Windows.Forms.Button DefaultInputPathButton;
        private System.Windows.Forms.Button DefaultTempPathButton;
        private System.Windows.Forms.Button DefaultOutputPathButton;
        private System.Windows.Forms.TextBox LameLocationTextBox;
        private System.Windows.Forms.TextBox OpusencLocationTextBox;
        private System.Windows.Forms.CheckBox DefaultAutoWavConvertState;
        private System.Windows.Forms.CheckBox DefaultExcelAppendState;
        private System.Windows.Forms.TextBox DefaultExcelLocationTextBox;
        private System.Windows.Forms.Button DefaultAADPathButton;
        private System.Windows.Forms.Button DefaultFlacPathButton;
        private System.Windows.Forms.Button DefaultLamePathButton;
        private System.Windows.Forms.Button DefaultMp3tagPathButton;
        private System.Windows.Forms.Button DefaultOpusPathButton;
        private System.Windows.Forms.Button DefaultSoxPathButton;
        private System.Windows.Forms.Button DefaultSpekPathButton;
        private System.Windows.Forms.Button DefaultExcelSheetPathButton;
        private System.Windows.Forms.ToolTip ParseSyntaxTooltip;
        private System.Windows.Forms.ComboBox DefaultPresetComboBox;
        private System.Windows.Forms.ComboBox DefaultParseStyle;
    }
}