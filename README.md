![Main window](https://i.imgur.com/cEWbmA5.png)

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

* Optionally strip metadata from images as you copy, reducing filesize and bloat.

* Impossible to make bad (Lossy->Lossless, Lossy->Lossy) transcodes, ensuring that data stays artifact-free.

* Robust codebase, currently tested on **113** albums of all shapes and sizes (including a few [witch.house](https://i.imgur.com/lBUJZfz.png) albums for good measure). All features have been double and triple-checked against proper traditional methods to make sure the output files match.

* Uses TagLib# to assist with tag reading.
