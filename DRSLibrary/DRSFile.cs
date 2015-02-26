using IORAMHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DRSLibrary
{
	/// <summary>
	/// Diese Klasse ist für das Laden, Verwalten und Schreiben einer DRS-Datei zuständig.
	/// </summary>
	/// <remarks></remarks>
	public class DRSFile
	{
		#region Variablen

		/// <summary>
		/// Der Dateiname der DRS-Datei mitsamt Pfad.
		/// </summary>
		/// <remarks></remarks>
		public string _filename = "";

		/// <summary>
		/// Enthält eine Instanz der RAMBuffer-Klasse und damit alle Data.
		/// </summary>
		/// <remarks></remarks>
		private RAMBuffer _buffer = null;

		/// <summary>
		/// Der Header der geladenen Datei.
		/// </summary>
		private Header _header = new Header();

		/// <summary>
		/// Die Tabelleninformationen der geladenen Datei.
		/// </summary>
		private List<TableInfo> _tableInfos = new List<TableInfo>();

		/// <summary>
		/// Die Tabellen der geladenen Datei.
		/// </summary>
		private List<Table> _tables = new List<Table>();

		/// <summary>
		/// Die gespeicherten Dateien in der geladenen Datei.
		/// Format: Ressourcen-ID =&gt; Daten.
		/// </summary>
		private Dictionary<uint, byte[]> _files = new Dictionary<uint, byte[]>();

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Erstellt eine neue Instanz von DRSFile und lädt alle Daten.
		/// </summary>
		/// <param name="filename">Der Pfad und Dateiname der zu ladenden DRS-Datei.</param>
		/// <remarks></remarks>
		public DRSFile(string filename)
		{
			// Speichern des Dateinamens
			_filename = filename;

			// Sicherstellen, dass die Datei existiert
			if(!File.Exists(filename))
			{
				MessageBox.Show("Fehler: Die DRS-Datei wurde nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Erstellen des Puffer-Objekts und Laden der Daten
			_buffer = new RAMBuffer(_filename);

			// Laden der Daten
			LoadData();
		}

		/// <summary>
		/// Lädt alle Daten aus dem internen Puffer in die jeweiligen Variablen.
		/// </summary>
		/// <remarks></remarks>
		private void LoadData()
		{
			// --- HEADER --- Copyright
			_header.Copyright = _buffer.ReadString(40);

			// Version
			_header.Version = _buffer.ReadString(4);

			// Datei-Typ
			_header.FileType = _buffer.ReadString(12);

			// Tabellenanzahl
			_header.TableCount = _buffer.ReadUInteger();

			// Offset der ersten Datei
			_header.FirstFileOffset = _buffer.ReadUInteger();

			// --- TABELLENINFORMATIONEN ---
			for(int i = 0; i < _header.TableCount; ++i)
			{
				TableInfo currTableInfo = new TableInfo();

				// Unbekannt1
				currTableInfo.Unknown1 = _buffer.ReadByte();

				// Ressourcentyp
				currTableInfo.ResourceType = _buffer.ReadString(3);

				// Tabellenoffset
				currTableInfo.TableOffset = _buffer.ReadUInteger();

				// Dateien-Anzahl
				currTableInfo.FileCount = _buffer.ReadUInteger();

				_tableInfos.Add(currTableInfo);
			}

			// --- TABELLEN ---
			for(int i = 0; i < _header.TableCount; ++i)
			{
				// Tabelleninformationen holen
				TableInfo currTableInfo = _tableInfos[i];

				// Neue Tabelle anlegen
				Table aktTabelle = new Table();
				aktTabelle.Entries = new List<TableEntry>();

				// Einträge lesen
				TableEntry currTableEntry;
				for(uint j = 0; j < currTableInfo.FileCount; ++j)
				{
					// Neuer Eintrag
					currTableEntry = new TableEntry();

					// Werte lesen
					currTableEntry.FileID = _buffer.ReadUInteger();
					currTableEntry.FileOffset = _buffer.ReadUInteger();
					currTableEntry.FileSize = _buffer.ReadUInteger();

					// Eintrag speichern
					aktTabelle.Entries.Add(currTableEntry);
				}

				// Tabelle speichern
				_tables.Add(aktTabelle);
			}

			// --- DATEIEN ---
			for(int i = 0; i < _tables.Count; ++i)
			{
				// Tabelle abrufen
				Table currTable = _tables[i];

				// Alle Einträge durchlaufen
				TableEntry currTableEntry;
				for(int j = 0; j < currTable.Entries.Count; ++j)
				{
					// Eintrag abrufen
					currTableEntry = currTable.Entries[j];

					// Fehlervorbeugung
					if(!_files.ContainsKey(currTableEntry.FileID))
					{
						// Datei-ID gibt es noch nicht, einfach einlesen
						_files.Add(currTableEntry.FileID, _buffer.ReadByteArray((int)currTableEntry.FileSize));
					}
					else
					{
						// Datei-ID existiert schon, an Platzhalter schreiben (TODO: Unschön...)
						_files.Add(65535, _buffer.ReadByteArray((int)currTableEntry.FileSize));
						currTableEntry.FileID = 65535;
						_tables[i].Entries[j] = currTableEntry;
					}
				}
			}

			// Puffer leeren, um Speicher zu sparen
			_buffer.Clear();
		}

		/// <summary>
		/// Schreibt die enthaltenen Daten in das interne RAMBuffer-Objekt.
		/// </summary>
		/// <remarks></remarks>
		public void WriteData()
		{
			// Puffer initialisieren
			_buffer = new RAMBuffer();

			// --- HEADER ---
			WriteString(_buffer, _header.Copyright, 40);
			WriteString(_buffer, _header.Version, 4);
			WriteString(_buffer, _header.FileType, 12);
			_buffer.WriteUInteger(_header.TableCount);

			// ErsteDateiOffset wird vor den Tabellen berechnet, daher erstmal 0
			_buffer.WriteUInteger(0);

			// Erstes Tabellen-Offset ausrechnen Hilft bei der Geschwindigkeitserhöhung beim TABELLEN-Prozess
			uint aktTableOffset = (uint)(_buffer.Position + _tableInfos.Count * 12);

			// --- TABELLENINFORMATIONEN ---
			TableInfo currTableInfo;
			for(int i = 0; i < _tableInfos.Count; ++i)
			{
				// Tabelleninformationen holen
				currTableInfo = _tableInfos[i];

				// Werte schreiben
				_buffer.WriteByte(currTableInfo.Unknown1);
				WriteString(_buffer, currTableInfo.ResourceType, 3);
				_buffer.WriteUInteger(aktTableOffset);
				_buffer.WriteUInteger(currTableInfo.FileCount);

				// Offset erhöhen
				aktTableOffset += currTableInfo.FileCount * 12;
			}

			// Erstes Datei-Offset ausrechnen Hilft bei der Geschwindigkeitserhöhung beim DATEIEN-Prozess
			uint aktFileOffset = (uint)_buffer.Position;
			for(int i = 0; i < _tables.Count; ++i)
				aktFileOffset += (uint)(_tables[i].Entries.Count * 12);

			// Header->ErsteDateiOffset
			uint aktPos = (uint)_buffer.Position;
			_buffer.Position = 60;
			_buffer.WriteUInteger(aktFileOffset);
			_buffer.Position = (int)aktPos;

			// --- TABELLEN ---
			Table currTable;
			TableEntry currEntry;
			for(int i = 0; i < _tables.Count; ++i)
			{
				// Tabelle abrufen
				currTable = _tables[i];

				// Einträge schreiben
				for(int j = 0; j < currTable.Entries.Count; ++j)
				{
					// Eintrag abrufen
					currEntry = currTable.Entries[j];

					// Werte schreiben
					_buffer.WriteUInteger(currEntry.FileID);
					_buffer.WriteUInteger(aktFileOffset);
					_buffer.WriteUInteger(currEntry.FileSize);

					// Offset erhöhen
					aktFileOffset += currEntry.FileSize;
				}
			}

			// --- DATEIEN ---
			foreach(Table aktT in _tables)
			{
				// Alle Einträge durchlaufen
				foreach(TableEntry currE in aktT.Entries)
				{
					// Zum Eintrag gehörige Datei suchen
					foreach(KeyValuePair<uint, byte[]> currFile in _files)
					{
						// Datei gefunden?
						if(currE.FileID == currFile.Key)
						{
							_buffer.Write(currFile.Value);
							break;
						}
					}
				}
			}
		}

		/// <summary>
		/// Schreibt die enthaltenen Data in eine neue DRS-Datei.
		/// </summary>
		/// <param name="destFile">Die Datei, in die die Data geschrieben werden sollen.</param>
		/// <remarks></remarks>
		public void WriteData(string destFile)
		{
			// Data in Puffer schreiben
			WriteData();

			// Puffer in Datei schreiben
			_buffer.Save(destFile);

			// Puffer leeren
			_buffer.Clear();
		}

		/// <summary>
		/// Fügt eine Ressource hinzu oder ersetzt diese.
		/// </summary>
		/// <param name="data">Die hinzuzufügende / zu ersetzende Ressource.</param>
		/// <param name="resourceID">Die ID der hinzuzufügenden / zu ersetzenden Ressource.</param>
		/// <param name="resourceType">Der Typ der Ressource, rückwärts geschrieben und drei Zeichen lang.</param>
		/// <remarks></remarks>
		public void AddReplaceRessource(RAMBuffer data, ushort resourceID, string resourceType)
		{
			// Neue Ressource?
			if(!_files.ContainsKey(resourceID))
			{
				// Die zu ermittelnden Tabelleninfos
				TableInfo myTI = new TableInfo();
				Table myT = new Table();

				// Suchen der Tabelle mit dem passenden Ressourcen-Typen
				int i;
				bool found = false;
				for(i = 0; i < _tableInfos.Count; ++i)
				{
					// Passender Ressourcen-Typ?
					if(_tableInfos[i].ResourceType == resourceType)
					{
						// Tabelleninfos merken
						myTI = _tableInfos[i];
						myT = _tables[i];
						found = true;
						break;
					}
				}

				// Neue Tabelle erforderlich?
				if(!found)
				{
					// Tabelleninfo anlegen
					myTI.Unknown1 = (byte)'a';
					myTI.TableOffset = 0;
					myTI.ResourceType = resourceType;
					myTI.FileCount = 0;
					_tableInfos.Add(myTI);

					// Neue Tabelle ist der einzige Eintrag
					myT.Entries = new List<TableEntry>();
					_tables.Add(myT);
				}

				// Eine Datei mehr
				myTI.FileCount += 1;

				// Tabelleneintrag für die Datei erstellen
				TableEntry eintr = new TableEntry();
				eintr.FileSize = (uint)data.Length;
				eintr.FileID = resourceID;
				myT.Entries.Add(eintr);

				// Datei speichern
				data.Position = 0;
				_files.Add(resourceID, data.ReadByteArray(data.Length));

				// Tabelleninfos speichern
				_tableInfos[i] = myTI;
				_tables[i] = myT;
			}
			else
			{
				// Ressource ersetzen
				data.Position = 0;
				_files[resourceID] = data.ReadByteArray(data.Length);

				// Tabellen durchlaufen und passende Datei-ID suchen
				Table currTable;
				TableEntry currEntry;
				for(int i = 0; i < _tables.Count; ++i)
				{
					// Aktuelle Tabelle abrufen
					currTable = _tables[i];

					// Eintrag mit passender Datei-ID suchen
					for(int j = 0; j < currTable.Entries.Count; ++j)
					{
						// Eintrg abrufen
						currEntry = currTable.Entries[j];

						// Passende ID?
						if(currEntry.FileID == resourceID)
						{
							// Dateigröße ersetzen
							currEntry.FileSize = (uint)data.Length;
							_tables[i].Entries[j] = currEntry;

							// Fertig
							return;
						}
					}
				}
			}
		}

		/// <summary>
		/// Gibt eine vollständige Liste aller enthaltenen Dateien zurück; dazu die Dateitypen und IDs.
		/// </summary>
		/// <remarks></remarks>
		public Dictionary<ushort, ExternalFile> GetFullContentList()
		{
			// Der Rückgabewert
			Dictionary<ushort, ExternalFile> ret = new Dictionary<ushort, ExternalFile>();

			// Der Name der DRS-Datei
			string shortName = Path.GetFileNameWithoutExtension(_filename);

			// Tabellenblöcke einzeln durchlaufen
			int i = 0;
			foreach(TableInfo aktTI in _tableInfos)
			{
				// Ressourcen-Typ
				string aktTyp = ReverseString(aktTI.ResourceType);

				// Tabelle durchlaufen
				foreach(TableEntry aktE in _tables[i].Entries)
				{
					// Dateieigenschaften sammeln
					ExternalFile aktFile = new ExternalFile();
					aktFile.DRSFile = shortName;
					aktFile.ResourceType = aktTyp;
					aktFile.FileID = aktE.FileID;
					aktFile.Data = _files[aktE.FileID];

					// Datei hinzufügen
					ret.Add((ushort)aktE.FileID, aktFile);
				}

				// Nächster
				++i;
			}

			// Fertig
			return ret;
		}

		/// <summary>
		/// Gibt die Daten der gegebenen Ressourcen-ID zurück.
		/// </summary>
		/// <param name="resourceID">Die ID der Ressource, deren Daten abgerufen werden sollen.</param>
		/// <returns></returns>
		public byte[] GetResourceData(ushort resourceID)
		{
			// Ressourcen-Daten zurückgeben
			return _files[resourceID];
		}

		#endregion Funktionen

		#region Hilfsfunktionen

		/// <summary>
		/// Dreht die angegebene Zeichenfolge um (wichtig für verkehrt herum geschriebene Ressourcen-Typen).
		/// </summary>
		/// <param name="wert">Der umzudrehende Wert.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string ReverseString(string wert)
		{
			// String in Array zerlegen
			char[] arr = wert.ToCharArray();

			// Array umdrehen
			Array.Reverse(arr);

			// Fertig
			return new string(arr);
		}

		/// <summary>
		/// Schreibt einen String der gegebenen Länge. Falls der String kürzer ist, wird der Rest mit 0-Bytes aufgefüllt.
		/// </summary>
		/// <param name="buffer">Der Puffer, in den der String geschrieben werden soll.</param>
		/// <param name="value">Der zu schreibende String.</param>
		/// <param name="length">Die Soll-Länge des zu schreibenden Strings.</param>
		private void WriteString(RAMBuffer buffer, string value, int length)
		{
			// Byte-Array anlegen
			byte[] val = new byte[length];

			// String in Byte-Array kopieren, dabei maximal length Zeichen berücksichtigen
			Encoding.Default.GetBytes(value.Substring(0, Math.Min(length, value.Length))).CopyTo(val, 0);

			// Wert in den Puffer schreiben
			buffer.Write(val);
		}

		#endregion Hilfsfunktionen

		#region Strukturen

		/// <summary>
		/// Definiert den DRS-Header.
		/// </summary>
		private struct Header
		{
			/// <summary>
			/// Der Copyright-Hinweis. Länge: 40
			/// </summary>
			/// <remarks></remarks>
			public string Copyright;

			/// <summary>
			/// Die Version. Länge: 4
			/// </summary>
			/// <remarks></remarks>
			public string Version;

			/// <summary>
			/// Der Dateityp. Länge: 12
			/// </summary>
			/// <remarks></remarks>
			public string FileType;

			/// <summary>
			/// Die Anzahl der Tabellen.
			/// </summary>
			/// <remarks></remarks>
			public uint TableCount;

			/// <summary>
			/// Der Index der ersten Datei (ab Dateibeginn).
			/// </summary>
			/// <remarks></remarks>
			public uint FirstFileOffset;
		}

		/// <summary>
		/// Definiert eine DRS-Tabelleninformation.
		/// </summary>
		private struct TableInfo
		{
			/// <summary>
			/// Unbekannt.
			/// </summary>
			/// <remarks></remarks>
			public byte Unknown1;

			/// <summary>
			/// Der Ressourcen-Typ. Länge: 3
			/// </summary>
			/// <remarks></remarks>
			public string ResourceType;

			/// <summary>
			/// Der Index der Table (ab Dateibeginn).
			/// </summary>
			/// <remarks></remarks>
			public uint TableOffset;

			/// <summary>
			/// Die Anzahl der Dateien in der Table.
			/// </summary>
			/// <remarks></remarks>
			public uint FileCount;
		}

		/// <summary>
		/// Definiert eine DRS-Tabelle.
		/// </summary>
		private struct Table
		{
			/// <summary>
			/// Die enthaltenen Einträge.
			/// </summary>
			public List<TableEntry> Entries;
		}

		/// <summary>
		/// Definiert einen DRS-Tabelleneintrag.
		/// </summary>
		private struct TableEntry
		{
			/// <summary>
			/// Die Ressourcen-ID der Datei.
			/// </summary>
			/// <remarks></remarks>
			public uint FileID;

			/// <summary>
			/// Der Index der Datei (ab Dateibeginn).
			/// </summary>
			/// <remarks></remarks>
			public uint FileOffset;

			/// <summary>
			/// Die Größe der Datei.
			/// </summary>
			/// <remarks></remarks>
			public uint FileSize;
		}

		/// <summary>
		/// Definiert eine öffentlich benutzbare Repräsentation einer in der DRS-Datei gespeicherte Datei.
		/// </summary>
		/// <remarks></remarks>
		public struct ExternalFile
		{
			/// <summary>
			/// Der kurze Dateiname der DRS-Datei (ohne Erweiterung u.ä.).
			/// </summary>
			/// <remarks></remarks>
			public string DRSFile;

			/// <summary>
			/// Die Ressourcen-ID der Datei.
			/// </summary>
			/// <remarks></remarks>
			public uint FileID;

			/// <summary>
			/// Der Ressourcen-Typ. Länge: 3
			/// </summary>
			/// <remarks></remarks>
			public string ResourceType;

			/// <summary>
			/// Die Dateidaten.
			/// </summary>
			/// <remarks></remarks>
			public byte[] Data;

			/// <summary>
			/// Erstellt aus der angegebenen Binär-Datei eine neue External-File-Struktur.
			/// </summary>
			/// <param name="data">Die auszulesende Binär-Datei.</param>
			/// <returns></returns>
			/// <remarks></remarks>
			public static ExternalFile FromBinary(RAMBuffer data)
			{
				// An den Anfang springen
				data.Position = 0;

				// Der Rückgabewert
				ExternalFile ret = new ExternalFile();

				// Ausnahmen abfangen für ungültige Dateien
				try
				{
					// DRS-Dateiname
					uint drsNameLen = data.ReadUInteger();
					ret.DRSFile = System.Text.Encoding.ASCII.GetString(data.ReadByteArray((int)drsNameLen));

					// Datei-ID
					ret.FileID = data.ReadUInteger();

					// Ressourcen-Typ
					uint resTypeLen = data.ReadUInteger();
					ret.ResourceType = System.Text.Encoding.ASCII.GetString(data.ReadByteArray((int)resTypeLen));

					// Data
					uint dataLen = data.ReadUInteger();
					ret.Data = data.ReadByteArray((int)dataLen);
				}
				catch
				{
					// Fehler
					MessageBox.Show("Fehler: Das Datenobjekt konnte nicht fehlerfrei in ein ExternalFile-Objekt umgewandelt werden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				// Fertig
				return ret;
			}
		}

		#endregion Strukturen
	}
}