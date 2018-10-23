![Main window](https://i.imgur.com/IW4Mjd3.png)

# MusicImportKit

Designed for power users who take lineage and data integrity seriously. Integrates many powerful tools into a natural workflow, and takes extra measures to make sure conversions are done the right way. Created due to my frustration with using many programs and conversion scripts in a slow and disjointed workflow.

## Includes

* Conversion to FLAC (-V8 re-FLACing), MP3, and Opus.

* Proper dithering and downsampling of 24-bit to 16-bit FLAC using SoX (with a VHQ triangular dither filter, guarding, and 44/48 sample-rate detection).

* Genuine LAME header info is preserved by exporting all tags from a .flac, converting to .wav (destroying all tags in the process), putting the .wav through LAME, and reapplying original tags to the .mp3 (including preserving unlimited custom tags through TXXX frame manipulation).

* MetaFLAC-powered ReplayGain on all formats.

* Custom Excel exports for database keeping.

* Quicklinks to Discogs and MusicBrainz using automatic artist+album metadata from the input files.

* Integration with AlbumArtDownloader (multi-source album art fetching), Mp3tag (powerful tagging software), and Spek (spectral analysis).

* Full custom parsing syntax, able to read any tag enclosed by "%" and several audio properties (codec, bitrate, sample-rate, bit-depth, etc).

* Copy files from the input folder (and nested folders) into the output folder, with full regex+wildcards support.

* Strip metadata from images as you copy, reducing filesize and bloat.
