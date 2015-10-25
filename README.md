## Introduction

This application allows to edit the technology tree of *Age of Empires II: The Conquerors* in a graphical way. This includes editing attributes of units and researches, assigning them to building slots and defining dependencies. A new project can be created by importing a DAT file or using built-in templates; the export function writes the whole project data into a given base DAT.

The most of the planned features are implemented and were tested extensively, but there may still occur random crashes, so it is recommended to do backups regularly.

The software is available in english and german language; this should be determined automatically on initial startup.

#### Missing Features

##### Missing, but planned features are:
* Modification of the in-game tech tree view (the changes on units an researches are not applied to it yet)
* Undo/Redo commands (very complicated, so low priority)

##### Currently not planned features are:
* Modification of sounds (maybe later) and terrain data

## Legal Info & Credits

This software is published under the MIT/X11 license. Please read the LICENSE file for further information.

Lots of credit goes to the creators of the Advanced Genie Editor (http://aok.heavengames.com/blacksmith/showfile.php?fileid=11002) which source code I used to create a C# port of its genieutils library (see "GenieLibrary" repository).

Also I'd like to thank the developers of OpenTK (http://www.opentk.com/) for their wonderful C# OpenGL wrapper allowing me to write an efficient renderer for my technology tree data structure.