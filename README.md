![Main window](https://i.imgur.com/bhURi7t.png)

# MusicImportKit

Designed for power users who take lineage and data integrity seriously. Integrates many powerful tools into a natural workflow, and takes extra measures to make sure conversions are done the *right* way. Created due to my frustration with using many programs and conversion scripts in a slow and disjointed workflow.

## Includes

* Parallel conversion to FLAC (-V8 re-FLACing), MP3, and Opus.

* Proper downsampling (e.g. 96kHz -> 48kHz) and bit-depth reduction (e.g. 24-bit -> 16-bit) using SoX (with a VHQ triangular dither filter, guarding, and 44.1/48 sample-rate detection).

* Genuine LAME header info is preserved by exporting all tags from a .flac, decoding to .wav (destroying all tags in the process), encoding the .wav to .mp3 through LAME, and reapplying original tags to the .mp3 (including preserving unlimited custom tags through TXXX frame manipulation).

* MetaFLAC-powered ReplayGain on all formats, including proper ReplayGain calculation on albums with varying bit-depths and sample-rates, and those that have been resampled or had their bit-depth reduced.

* Custom Excel exports for database keeping.

* Quicklinks to Discogs and MusicBrainz using automatic artist+album metadata from the input files.

* Integration with AlbumArtDownloader (multi-source album art fetching), Mp3tag (powerful tagging software), and Spek (spectral analysis).

* Full custom parsing syntax, able to read any tag enclosed by "%" and several audio properties (codec, bitrate, sample-rate, bit-depth, etc). Includes several popular default syntaxes.

* Copy custom files from the input folder (and nested folders) into the output folder, with full regex+wildcards support.

* Optionally strip metadata from images as you copy and compress .pngs, reducing filesize and bloat.

* Impossible to make bad (Lossy->Lossless, Lossy->Lossy) transcodes, ensuring that data stays artifact-free.

* Robust codebase, currently tested on **164** albums of all shapes and sizes (including a few [witch.house](https://i.imgur.com/lBUJZfz.png) albums for good measure). All features have been double and triple-checked against proper traditional methods to make sure the output files match.

* All features operate as fast as possible while still maintaining proper output. This program will always trade speed for accuracy. Check "Necessary Limitations/Quirks" below for inconvenient aspects of that decision.

* Uses TagLib# to assist with tag reading.


## Basic Usage

1. Choose input folder: Pick a folder that contains .wavs or .flacs that you want to convert from (e.g. after unzipping an album from Bandcamp). Files in this folder will not be changed/touched.

2. Choose temp folder: Create a transient folder that exists as a working space while you prepare to convert (e.g. tagging and downloading art). Primarily created through the "Copy" button above it, but can also be pointed at any folder verbatim.

3. Guess metadata: Upon confirming a temp folder (through copy or otherwise), these boxes will be autofilled based on the first available .flac's metadata (but can be changed if the metadata is incorrect).

4. Use Discogs/MusicBrainz links: These buttons will search Discogs and MusicBrainz, using the Artist/Album textboxes above.

5. Use AlbumArtDownloader/Mp3Tag/Spek:
    * AlbumArtDownloader will use the Artist/Album textboxes above for its query, and save files to the temp folder.
    * Mp3Tag will open with the temp folder as its target, and you can freely edit tags.
    * The Spek button will open every .flac in the temp folder sequentially in Spek. Spek can be used to detect files which have been "upconverted" or "transcoded" (usually used in a negative context).
        * Converting from a lossy (MP3, Opus) file to a lossless (FLAC, WAV) file does not increase its quality, and you may find that Bandcamp artists that don't know better are just transcoding their MP3s to FLAC to upload to Bandcamp. This means you're not really getting lossless files; you're getting bloated MP3s.
        * True lossless files will extend to the very top of the spectral with no shelves visible
        * 320kBps CBR MP3s that have been transcoded to FLAC will have a "cut-off" at 20.5kHz and a barely visible "shelf" at 16kHz
        * 256kBps CBR MP3s that have been transcoded to FLAC will have a "cut-off" at 20kHz and a clearly visible "shelf" at 16kHz
        * 245kBps VBR (aka V0) MP3s that have been transcoded to FLAC will have a "cut-off" at 19.5kHz and a visible "shelf" at 16kHz
        * 192kBps CBR MP3s that have been transcoded to FLAC will have a "cut-off" at 19kHz and a clearly visible "shelf" at 16kHz
        * 190kBps VBR (aka V2) MP3s that have been transcoded to FLAC will have a "cut-off" at 18.5kHz and a visible "shelf" at 16kHz
        * 128kBps CBR MP3s that have been transcoded to FLAC will have a "cut-off" at 16kHz

6. Choose output folder: Pick a base folder that you want to send the converted files to. This folder path will be combined with your preferred syntax to create directories and files as desired.

7. Create preferred syntax: Create a syntax to specify what your folders and files are going to be named. You can send files directly to the output folder with something like "%tracknumber%. %title%" or send them to a folder with something like "%albumartist% - %album%\\%tracknumber%. %title%"

8. Choose options: Most options are straightforward.
    * Copy specific filetypes will copy all matching files in the temp folder to the output folder. Regex and wildcards are supported.
    * Append parsed data to Excel sheet will add the parsed syntax (minus the filename) to an Excel sheet. Optionally, you can include a log score (for how well the CD was ripped) and notes.

9. Choose conversion option:
    * FLAC:
        * FLAC encodes require flac.exe
        * All FLAC encodes use V8 (highest) compression. There is never a reason to use less than V8.
        * All FLAC conversions will re-encode your temp .flacs. Useful for forcing V8 compression, easy renaming and moving, ReplayGain, and other included features.
        * Forcing 16-bit will reduce 24-bit FLACs to 16-bit FLACs. This massively decreases the filesize, but drops genuine inaudible sound data. This feature requires sox.exe
        * Forcing 44.1kHz/48kHz will reduce a FLAC's sample rate to 44.1kHz or 48kHz, depending on its original sample rate. This will massively decrease the filesize, but drops genuine inaudible sound data. This feature requires sox.exe
        * Both 16-bit and 44.1/48 forcing will only occur if a file needs it.

    * MP3:
        * MP3 conversions require both lame.exe and flac.exe
        * CBR and VBR are supported. VBR options are superior and recommended, but CBR options are included for compatibility with certain hardware.
        * V0 is considered transparent, or indistinguishable from the original FLAC file. This is the recommended setting for high quality MP3 audio.
        * Other recommended encoder settings can be found [here](https://wiki.hydrogenaud.io/index.php?title=LAME#Recommended_encoder_settings).

    * Opus:
        * Opus conversions require opusenc.exe
        * 192kBps VBR is considered transparent, or indistinguishable from the original FLAC file. This is the recommended setting for high quality Opus audio.
        * Other recommended encoder settings can be found [here](https://wiki.hydrogenaud.io/index.php?title=Opus#Music_encoding_quality) and [here](https://wiki.xiph.org/Opus_Recommended_Settings#Recommended_Bitrates).


## Plugins

* [AlbumArt.exe](https://sourceforge.net/projects/album-art/)
    * Opens AlbumArtDownloader with the artist+album filled out (from the "guessed" textboxes above) and pointed at the temp folder

* [flac.exe](https://xiph.org/flac/)
    * Encode input .wavs to .flac
    * Re-encode input .flacs to .flac
    * Decode .flac to .wav, for feeding into LAME
    * Backup ReplayGain calculator in worst case scenario (see "Necessary Limitations/Quirks" below)

* [lame.exe](http://lame.sourceforge.net/) ([Unofficial binaries](http://rarewares.org/mp3-lame-bundle.php))
    * Convert .wav to .mp3 (automatically gets .wavs from flac.exe, which is also required for MP3 conversions)
    
* [metaflac.exe](https://xiph.org/flac/)
    * Primary ReplayGain calculator
    
* [Mp3Tag.exe](https://www.mp3tag.de/en/) (I highly recommend pairing with [Grammartron](https://community.mp3tag.de/t/case-conversion/11684))
    * Opens the temp folder in Mp3Tag for tag editing

* [opusenc.exe](https://opus-codec.org/downloads/)
    * Convert .flac into .opus (flac.exe not required)

* [sox.exe](http://sox.sourceforge.net/)
    * Resample .flacs
    * Reduce bit-depths of .flacs

* [spek.exe](http://spek.cc/)
    * Opens the temp folder in Spek for spectral analysis


## Necessary Limitations/Quirks

* Downsampling/Reducing Bit-Depth of FLACs:
    * SoX cannot handle crazy ASCII characters in a file's path or filename at all. Offending files are copied to %temp%/SoXTemp with a randomly generated name, fed into SoX, and moved back+renamed to their original name. This adds some disk usage that wouldn't otherwise be required. (Note: this should almost never happen in practice; only exceptionally/purposefully absurd filepaths cause issues).

* Metadata Stripping/PNG compression:
    * Stripping metadata from a PNG requires loading it into a lossless format, removing the metadata ourselves, and resaving it as a PNG. However, due to the nature of PNG, haphazardly resaving will probably create a bigger file than we started with. To alleviate this, we use OxiPNG to optimize PNGs to their smallest size. It's possible to compress PNGs even further with OxiPNG, but the process is extremely CPU-intensive and usually yields no gains.
    * OxiPNG also supports native metadata stripping, so we defer that to it.

* MP3 Conversions: 
    * Simpler methods of MP3 conversion (e.g. FFmpeg, which uses LAME as well) strip the LAME header info from the output MP3 and thus there is no (easy) way to tell if an unknown MP3 file that you find used LAME in its creation or an inferior tool (such as FhG). For being courteous to others (and our future selves), we take extra steps to preserve this data. Manual decoding to .wav and encoding to .mp3 is actually (~33%) faster than using an FFmpeg implementation, but destroys tags in the process so we handle that manually.

* ReplayGain:
    * MetaFLAC's ReplayGain implementation cannot be multithreaded and takes a *large* portion (~50%) of the conversion process time. Disable ReplayGain if you don't need it and speed is a priority.
    * MetaFLAC cannot handle files of varying bit-depths and sample rates, so we calculate those using flac.exe instead. This adds some computation time as FLAC will actually be re-encoding the file while calculating the ReplayGain, but it doesn't add as much time as it would take to upsample/increase BPS of all files to match each other with SoX and then running MetaFLAC on them (previous solution). In addition, upsampling (sample rate) causes audio data to change and ReplayGain calculation will be affected (you can upsample and downsample losslessly through SoX, but while it's in a state of non-original sample rate the audio data is different).
    * MetaFLAC and FLAC are both required for ReplayGain calculation. In most cases, FLAC will not be used at all.
