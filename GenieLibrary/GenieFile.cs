using IORAMHelper;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace GenieLibrary
{
	/// <summary>
	/// Diese Klasse ist für das Laden, Verwalten und Schreiben einer DAT-Datei zuständig. Diese Datei muss bereits dekomprimiert sein, um diese lesen zu können.
	/// </summary>
	/// <remarks></remarks>
	public class GenieFile : IGenieDataElement
	{
		#region Variablen

		/// <summary>
		/// Der Dateiname der DAT-Datei mitsamt Pfad.
		/// </summary>
		/// <remarks></remarks>
		public string _filename = "";

		/// <summary>
		/// Enthält eine Instanz der RAMBuffer-Klasse und damit alle Daten.
		/// </summary>
		/// <remarks></remarks>
		private RAMBuffer _buffer = null;

		#region Datenelemente

		/// <summary>
		/// Die Terrain-Anzahl.
		/// </summary>
		public int TerrainCount;

		/// <summary>
		/// Pointer zu Terrain-Beschränkungen.
		/// </summary>
		public List<int> TerrainRestrictionPointers1;

		/// <summary>
		/// Pointer zu Terrain-Beschränkungen.
		/// </summary>
		public List<int> TerrainRestrictionPointers2;

		/// <summary>
		/// Die Terrain-Beschränkungen.
		/// </summary>
		public List<DataElements.TerrainRestriction> TerrainRestrictions;

		/// <summary>
		/// Die Spielerfarben.
		/// </summary>
		public List<DataElements.PlayerColor> PlayerColors;

		/// <summary>
		/// Die Sounds.
		/// </summary>
		public List<DataElements.Sound> Sounds;

		/// <summary>
		/// Grafik-Pointer.
		/// </summary>
		public List<int> GraphicPointers;

		/// <summary>
		/// Die Grafiken.
		/// </summary>
		public Dictionary<int, DataElements.Graphic> Graphics;

		/// <summary>
		/// Ungenutzte Daten.
		/// </summary>
		public List<byte> Unused1;

		/// <summary>
		/// Die Technologie-Effekte.
		/// </summary>
		public List<DataElements.Techage> Techages;

		/// <summary>
		/// Die Einheiten-Header.
		/// </summary>
		public List<DataElements.UnitHeader> UnitHeaders;

		/// <summary>
		/// Die Kulturen.
		/// </summary>
		public List<DataElements.Civ> Civs;

		/// <summary>
		/// Die Technologien.
		/// </summary>
		public List<DataElements.Research> Researches;

		/// <summary>
		/// Der Technologie-Baum.
		/// </summary>
		public DataElements.TechTree TechTree;

		/// <summary>
		/// Unbekannte Daten bezüglich des Technologie-Baums.
		/// </summary>
		public List<int> TechTreeUnknown;

		#endregion Datenelemente

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Erstellt eine neue Instanz von GenieFile und lädt alle Daten.
		/// </summary>
		/// <param name="filename">Der Pfad und Dateiname der zu ladenden (unkomprimierten) DAT-Datei.</param>
		/// <remarks></remarks>
		public GenieFile(string filename)
		{
			// Speichern des Dateinamens
			_filename = filename;

			// Sicherstellen, dass die Datei existiert
			if(!File.Exists(filename))
			{
				MessageBox.Show("Fehler: Die DAT-Datei wurde nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Erstellen des Puffer-Objekts und Laden der Daten
			_buffer = new RAMBuffer(_filename);

			// Laden der Daten
			ReadData();
		}

		/// <summary>
		/// Erstellt eine neue Instanz von GenieFile und lädt alle Daten aus dem übergebenen Puffer.
		/// </summary>
		/// <param name="buffer">Der Puffer mit den zu ladenden Daten.</param>
		/// <remarks></remarks>
		public GenieFile(RAMBuffer buffer)
		{
			// Puffer speichern
			_buffer = buffer;

			// Laden der Daten
			ReadData();
		}

		/// <summary>
		/// Lädt alle Daten aus dem internen Puffer in die jeweiligen Variablen.
		/// </summary>
		/// <remarks></remarks>
		private void ReadData()
		{
			// Dateiversion lesen
			string version = _buffer.ReadString(8);
			if(version != "VER 5.7\0")
				throw new InvalidDataException("Falsches Dateiformat oder falsche Dateiversion.");

			// Anzahlen lesen
			ushort terrainRestrictionCount = _buffer.ReadUShort();
			ushort terrainCount = _buffer.ReadUShort();

			// Terrain-Pointer lesen
			TerrainRestrictionPointers1 = new List<int>(terrainRestrictionCount);
			for(int i = 0; i < terrainRestrictionCount; ++i)
				TerrainRestrictionPointers1.Add(_buffer.ReadInteger());
			TerrainRestrictionPointers2 = new List<int>(terrainRestrictionCount);
			for(int i = 0; i < terrainRestrictionCount; ++i)
				TerrainRestrictionPointers2.Add(_buffer.ReadInteger());

			// Terrain-Beschränkungen lesen
			TerrainRestrictions = new List<DataElements.TerrainRestriction>(terrainRestrictionCount);
			DataElements.TerrainRestriction.TerrainCount = terrainCount;
			for(int i = 0; i < terrainRestrictionCount; ++i)
				TerrainRestrictions.Add(new DataElements.TerrainRestriction().ReadDataInline(_buffer));

			// Anzahl lesen
			ushort playerColorCount = _buffer.ReadUShort();

			// Spielerfarben lesen
			PlayerColors = new List<DataElements.PlayerColor>(playerColorCount);
			for(int i = 0; i < playerColorCount; ++i)
				PlayerColors.Add(new DataElements.PlayerColor().ReadDataInline(_buffer));

			// Anzahl lesen
			ushort soundCount = _buffer.ReadUShort();

			// Sounds lesen
			Sounds = new List<DataElements.Sound>(soundCount);
			for(int i = 0; i < soundCount; ++i)
				Sounds.Add(new DataElements.Sound().ReadDataInline(_buffer));

			// Anzahl lesen
			int graphicCount = _buffer.ReadUShort();

			// Grafik-Pointer lesen
			GraphicPointers = new List<int>(graphicCount);
			for(int i = 0; i < graphicCount; ++i)
				GraphicPointers.Add(_buffer.ReadInteger());

			// Grafiken lesen
			Graphics = new Dictionary<int, DataElements.Graphic>(graphicCount);
			for(int p = 0; p < GraphicPointers.Count; ++p)
				if(GraphicPointers[p] != 0)
					Graphics.Add(p, new DataElements.Graphic().ReadDataInline(_buffer));

			// Ungenutzte Daten lesen
			int unusedCount = 36368 + terrainCount * 436;
			Unused1 = new List<byte>(unusedCount);
			for(int i = 0; i < unusedCount; ++i)
				Unused1.Add(_buffer.ReadByte());

			// Anzahl lesen
			int techageCount = _buffer.ReadInteger();

			// Technologie-Effekte lesen
			Techages = new List<DataElements.Techage>(techageCount);
			for(int i = 0; i < techageCount; ++i)
				Techages.Add(new DataElements.Techage().ReadDataInline(_buffer));

			// Anzahl lesen
			int unitCount = _buffer.ReadInteger();

			// Einheiten-Header lesen
			UnitHeaders = new List<DataElements.UnitHeader>(unitCount);
			for(int i = 0; i < unitCount; ++i)
				UnitHeaders.Add(new DataElements.UnitHeader().ReadDataInline(_buffer));

			// Anzahl lesen
			int civCount = _buffer.ReadUShort();

			// Kulturen lesen
			Civs = new List<DataElements.Civ>(civCount);
			for(int i = 0; i < civCount; ++i)
				Civs.Add(new DataElements.Civ().ReadDataInline(_buffer));

			// Anzahl lesen
			int researchCount = _buffer.ReadUShort();

			// Technologien lesen
			Researches = new List<DataElements.Research>(researchCount);
			for(int i = 0; i < researchCount; ++i)
				Researches.Add(new DataElements.Research().ReadDataInline(_buffer));

			// Unbekannte Technologiebaum-Daten lesen
			TechTreeUnknown = new List<int>(7);
			for(int i = 0; i < 7; ++i)
				TechTreeUnknown.Add(_buffer.ReadInteger());

			// Technologiebaum lesen
			TechTree = new DataElements.TechTree().ReadDataInline(_buffer);

			// Puffer leeren, um Speicher zu sparen
			_buffer.Clear();
		}

		/// <summary>
		/// Geerbt. Liest Daten ab der gegebenen Position im Puffer.
		/// </summary>
		/// <param name="buffer">Der Puffer, aus dem die Daten gelesen werden sollen.</param>
		public override void ReadData(RAMBuffer buffer)
		{
			// Puffer als internen Puffer speichern und Daten laden
			_buffer = buffer;
			ReadData();
		}

		/// <summary>
		/// Schreibt die enthaltenen Daten in das interne RAMBuffer-Objekt.
		/// </summary>
		/// <remarks></remarks>
		public void WriteData()
		{
			// Puffer initialisieren
			if(_buffer == null)
				_buffer = new RAMBuffer();
			else if(_buffer.Length != 0)
				_buffer.Clear();

			// Dateiversion schreiben
			_buffer.WriteString("VER 5.7", 8);

			// Anzahlen schreiben
			AssertTrue(TerrainRestrictionPointers1.Count == TerrainRestrictionPointers2.Count);
			_buffer.WriteUShort((ushort)TerrainRestrictionPointers1.Count);
			_buffer.WriteUShort((ushort)DataElements.TerrainRestriction.TerrainCount);

			// Terrain-Pointer schreiben
			TerrainRestrictionPointers1.ForEach(e => _buffer.WriteInteger(e));
			TerrainRestrictionPointers2.ForEach(e => _buffer.WriteInteger(e));

			// Terrain-Beschränkungen schreiben
			TerrainRestrictions.ForEach(e => e.WriteData(_buffer));

			// Anzahl schreiben
			_buffer.WriteUShort((ushort)PlayerColors.Count);

			// Spielerfarben schreiben
			PlayerColors.ForEach(e => e.WriteData(_buffer));

			// Anzahl schreiben
			_buffer.WriteUShort((ushort)Sounds.Count);

			// Sounds schreiben
			Sounds.ForEach(e => e.WriteData(_buffer));

			// Anzahl schreiben
			_buffer.WriteUShort((ushort)GraphicPointers.Count);

			// Grafik-Pointer schreiben
			GraphicPointers.ForEach(e => _buffer.WriteInteger(e));

			// Grafiken schreiben Sicherstellen, dass genau jede definierte Grafik einen entsprechenden Pointer hat; hier nur über die Listenlänge, sollte aber die meisten auftretenden Fehler abdecken
			AssertListLength(Graphics, GraphicPointers.Count(p => p != 0));
			foreach(var e in Graphics)
				e.Value.WriteData(_buffer);

			// Ungenutzte Daten schreiben
			AssertListLength(Unused1, 36368 + DataElements.TerrainRestriction.TerrainCount * 436);
			Unused1.ForEach(e => _buffer.WriteByte(e));

			// Anzahl schreiben
			_buffer.WriteInteger(Techages.Count);

			// Technologie-Effekte schreiben
			Techages.ForEach(e => e.WriteData(_buffer));

			// Anzahl schreiben
			_buffer.WriteInteger(UnitHeaders.Count);

			// Einheiten-Header schreiben
			UnitHeaders.ForEach(e => e.WriteData(_buffer));

			// Anzahl schreiben
			_buffer.WriteUShort((ushort)Civs.Count);

			// Kulturen schreiben
			Civs.ForEach(e => e.WriteData(_buffer));

			// Anzahl schreiben
			_buffer.WriteUShort((ushort)Researches.Count);

			// Technologien schreiben
			Researches.ForEach(e => e.WriteData(_buffer));

			// Unbekannte Technologiebaum-Daten schreiben
			AssertListLength(TechTreeUnknown, 7);
			TechTreeUnknown.ForEach(e => _buffer.WriteInteger(e));

			// Technologiebaum schreiben
			TechTree.WriteData(_buffer);

			// Fertig
		}

		/// <summary>
		/// Schreibt die enthaltenen Daten in eine neue DAT-Datei.
		/// </summary>
		/// <param name="destFile">Die Datei, in die die Daten geschrieben werden sollen.</param>
		/// <remarks></remarks>
		public void WriteData(string destFile)
		{
			// Daten in Puffer schreiben
			WriteData();

			// Puffer in Datei schreiben
			_buffer.Save(destFile);
		}

		/// <summary>
		/// Schreibt die enthaltenen Daten in den übergebenen Stream.
		/// </summary>
		/// <param name="destFile">Der Stream, in den die Daten geschrieben werden sollen.</param>
		/// <remarks></remarks>
		public void WriteData(Stream stream)
		{
			// Daten in Puffer schreiben
			WriteData();

			// Puffer in Stream schreiben
			_buffer.ToMemoryStream().CopyTo(stream);
		}

		/// <summary>
		/// Geerbt. Schreibt die enthaltenen Daten an die gegebene Position im Puffer.
		/// </summary>
		/// <param name="buffer">Der Puffer, in den die Daten geschrieben werden sollen.</param>
		public override void WriteData(RAMBuffer buffer)
		{
			// Puffer als internen Puffer speichern und Daten schreiben
			_buffer = buffer;
			WriteData();
		}

		#endregion Funktionen

		#region Statische Funktionen

		/// <summary>
		/// Komprimiert die gegebenen DAT-Daten (zlib-Kompression).
		/// </summary>
		/// <param name="dat">Die zu komprimierenden Daten.</param>
		/// <returns></returns>
		public static RAMBuffer CompressData(RAMBuffer dat)
		{
			// Ausgabe-Stream erstellen
			MemoryStream output = new MemoryStream();

			// Daten in Memory-Stream schreiben
			using(MemoryStream input = dat.ToMemoryStream())
			{
				// Kompressions-Stream erstellen
				using(DeflateStream compressor = new DeflateStream(output, CompressionMode.Compress))
				{
					// (De-)Komprimieren
					input.CopyTo(compressor);
					input.Close();
				}
			}

			// Ergebnis in Puffer schreiben
			return new RAMBuffer(output.ToArray());
		}

		/// <summary>
		/// Dekomprimiert die gegebenen DAT-Daten (zlib-Kompression).
		/// </summary>
		/// <param name="dat">Die zu dekomprimierenden Daten.</param>
		/// <returns></returns>
		public static RAMBuffer DecompressData(RAMBuffer dat)
		{
			// Ausgabe-Stream erstellen
			MemoryStream output = new MemoryStream();

			// Daten in Memory-Stream schreiben
			using(MemoryStream input = dat.ToMemoryStream())
			{
				// Kompressions-Stream erstellen
				using(DeflateStream decompressor = new DeflateStream(input, CompressionMode.Decompress))
				{
					// (De-)Komprimieren
					decompressor.CopyTo(output);
					decompressor.Close();
				}
			}

			// Ergebnis in Puffer schreiben
			return new RAMBuffer(output.ToArray());
		}

		#endregion Statische Funktionen
	}
}