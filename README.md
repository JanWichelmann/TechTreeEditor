## Introduction

This application allows to edit the game data of *Age of Empires II: The Conquerors* in a graphical way, using the data's logical structure (tech tree). This includes editing attributes of units and researches, assigning them to building slots and defining dependencies. A new project can be created by importing a DAT file or using built-in templates; the export function writes the whole project data into a given base DAT.

The most of the planned features are implemented and were tested extensively, and since this software is the primary tool used to develop the (german) [Agearena-AddOn](https://agearena.de/addon), it should be pretty stable. Anyway, there might still occur rare crashes, so it is recommended to do backups regularly.

The software is available in english and german language; this should be determined automatically on initial startup.


## Legal Info & Credits

This software is published under the MIT/X11 license. Please read the LICENSE file for further information.

Lots of credit goes to the creators of the Advanced Genie Editor (http://aok.heavengames.com/blacksmith/showfile.php?fileid=11002) which source code I used to create a C# port of its genieutils library (see "GenieLibrary" repository).

Also I'd like to thank the developers of OpenTK (https://opentk.github.io/) for their wonderful C# OpenGL wrapper allowing me to write an efficient renderer for my technology tree data structure.