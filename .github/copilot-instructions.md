Beware of reading the contents of anything in the TRANS4D/BlockData/ directory. The files are huge and could cause you to spin forever.

Specifically for RegionManager.cs: Do not read more than 200 lines of this file at once as it contains thousands of lines of data initialization that can overwhelm the context window.

This repository is a
