using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using IORAMHelper;
using System.Xml;
using System.Xml.Linq;
using System.IO.Compression;

namespace X2AddOnTechTreeEditor
{
	/// <summary>
	/// Definiert eine TechTree-Datei, die alle Daten einer Instanz erhält.
	/// </summary>
	class TechTreeFile
	{
		#region Variablen

		/// <summary>
		/// Die originale Basis-DAT-Datei. Diese darf nach der ersten Erstellung aus Kompatibilitätsgründen nicht mehr verändert werden.
		/// </summary>
		private GenieLibrary.GenieFile _basicGenieFile = null;

		/// <summary>
		/// Die Elternelemente des enthaltenen Technologiebaums.
		/// </summary>
		private List<TechTreeStructure.TechTreeElement> _techTreeParentElements = null;

		/// <summary>
		/// Die Language-Dateien (Zuordnung: Name => Inhalt).
		/// </summary>
		private Dictionary<string, RAMBuffer> _languageFiles = null;

		/// <summary>
		/// Das Language-DLL-Wrapperobjekt.
		/// </summary>
		private GenieLibrary.LanguageFileWrapper _languageFileWrapper = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt eine neue leere TechTree-Datei.
		/// </summary>
		/// <param name="basicGenieFile">Die DAT-Datei, auf der die enthaltenen Definitionen basieren sollen.</param>
		public TechTreeFile(GenieLibrary.GenieFile basicGenieFile)
		{
			// Parameter speichern
			_basicGenieFile = basicGenieFile;

			// Elternelement-Liste erstellen
			_techTreeParentElements = new List<TechTreeStructure.TechTreeElement>();
		}

		/// <summary>
		/// Konstruktor.
		/// Lädt die angegebene TechTree-Datei.
		/// </summary>
		/// <param name="filename">Die zu ladende TechTree-Datei.</param>
		public TechTreeFile(string filename)
		{
			// Datei laden
			ReadData(new FileStream(filename, FileMode.Open));
		}

		/// <summary>
		/// Liest die Daten aus dem übergebenen Stream.
		/// </summary>
		/// <param name="stream">Der Stream mit den zu lesenden Daten.</param>
		private void ReadData(FileStream stream)
		{
			// ZIP-Archiv aus Stream erstellen
			using(ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
			{
				// Projektdaten lesen
				using(Stream currFile = archive.GetEntry("project.xml").Open())
				{
					// XML-Format benutzen
					XDocument doc = XDocument.Load(currFile);
					XElement mainElement = doc.Element("project");

					// Language-Dateien lesen
					_languageFiles = new Dictionary<string, RAMBuffer>();
					foreach(XElement fileElement in mainElement.Element("languagefiles").Descendants("file"))
					{
						// Datei lesen
						using(Stream langFile = archive.GetEntry(fileElement.Value).Open())
						{
							_languageFiles.Add(fileElement.Value, new RAMBuffer(langFile));
						}
					}

					// Language-Dateien laden
					CreateLanguageFileHandles();
				}

				// Technologiebaum lesen
				_techTreeParentElements = new List<TechTreeStructure.TechTreeElement>();
				using(Stream currFile = archive.GetEntry("techtree.xml").Open())
				{
					// XML-Format benutzen
					XDocument doc = XDocument.Load(currFile);
					XElement mainElement = doc.Element("techtree").Element("elements");

					// Elemente lesen
					// Diese sind in Dokumentreihenfolge, d.h. alle Abhängigkeiten müssen vor dem jeweiligen Element stehen.
					Dictionary<int, TechTreeStructure.TechTreeElement> readElements = new Dictionary<int, TechTreeStructure.TechTreeElement>();
					foreach(XElement elem in mainElement.Descendants("element"))
					{
						// Element lesen und zur Liste hinzufügen
						TechTreeStructure.TechTreeElement tte = TechTreeStructure.TechTreeElement.CreateFromType((string)elem.Attribute("type"));
						tte.FromXml(elem, readElements);
						readElements.Add((int)elem.Attribute("id"), tte);
					}
				}

				// DAT-Datei laden
				using(Stream currFile = archive.GetEntry("dat.unz").Open())
				{
					_basicGenieFile = new GenieLibrary.GenieFile(new RAMBuffer(currFile));
				}
			}
		}

		/// <summary>
		/// Speichert die internen Daten in der angegebenen Datei.
		/// </summary>
		/// <param name="filename">Die Datei, in der die Daten gespeichert werden sollen.</param>
		public void WriteData(string filename)
		{
			// Dateistream erstellen
			using(FileStream stream = new FileStream(filename, FileMode.Create))
			{
				// ZIP-Archiv auf Stream erstellen
				using(ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create))
				{
					// Projektdaten schreiben
					using(Stream currFile = archive.CreateEntry("project.xml").Open())
					{
						// XML-Format benutzen
						XmlWriterSettings settings = new XmlWriterSettings();
						settings.Indent = true;
						using(XmlWriter writer = XmlWriter.Create(currFile, settings))
						{
							// Dokument-Element und Hauptelement schreiben
							writer.WriteStartDocument();
							writer.WriteStartElement("project");

							// Language-DLL-Namen schreiben
							writer.WriteStartElement("languagefiles");
							{
								// Dateien einzeln schreiben
								foreach(string currDLLName in _languageFiles.Keys)
								{
									// Dateinamen schreiben
									writer.WriteElementString("file", currDLLName);
								}
							}
							writer.WriteEndElement();

							// Haupt- und Dokumentelement schließen
							writer.WriteEndElement();
							writer.WriteEndDocument();
						}
					}

					// Language-DLLs schreiben
					foreach(var file in _languageFiles)
					{
						// Pro Datei Eintrag erstellen
						using(Stream currFile = archive.CreateEntry(file.Key).Open())
						{
							// Dateiinhalt schreiben
							file.Value.ToMemoryStream().CopyTo(currFile);
						}
					}

					// Technologiebaumdaten schreiben
					using(Stream currFile = archive.CreateEntry("techtree.xml").Open())
					{
						// XML-Format benutzen
						XmlWriterSettings settings = new XmlWriterSettings();
						settings.Indent = true;
						using(XmlWriter writer = XmlWriter.Create(currFile, settings))
						{
							// Dokument-Element und Hauptelement schreiben
							writer.WriteStartDocument();
							writer.WriteStartElement("techtree");

							// Elternelemente schreiben
							writer.WriteStartElement("elements");
							{
								// Pro Element Unterbaum schreiben (aber alles auf einer Ebene)
								int lastID = 0;
								_techTreeParentElements.ForEach(p => lastID = p.ToXml(writer, new Dictionary<TechTreeStructure.TechTreeElement, int>(), lastID));
							}
							writer.WriteEndElement();

							// Haupt- und Dokumentelement schließen
							writer.WriteEndElement();
							writer.WriteEndDocument();
						}
					}

					// DAT-Datei schreiben
					using(Stream currFile = archive.CreateEntry("dat.unz").Open())
					{
						_basicGenieFile.WriteData(currFile);
					}
				}
			}
		}

		/// <summary>
		/// Lädt die enthaltenen Language-DLLs.
		/// </summary>
		private void CreateLanguageFileHandles()
		{
			// Dateien in temporärem Verzeichnis mappen
			List<string> tempFileNames = new List<string>();
			foreach(KeyValuePair<string, RAMBuffer> currFile in _languageFiles)
			{
				// Datei mappen
				string filename = Path.GetTempFileName();
				currFile.Value.Save(filename);
				tempFileNames.Add(filename);
			}

			// DLL-Handles erstellen
			_languageFileWrapper = new GenieLibrary.LanguageFileWrapper(tempFileNames.ToArray());
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft die originale Basis-DAT-Datei ab.
		/// </summary>
		public GenieLibrary.GenieFile BasicGenieFile
		{
			get { return _basicGenieFile; }
		}

		/// <summary>
		/// Ruft die Elternelemente des enthaltenen Technologiebaums ab oder setzt diese.
		/// </summary>
		public List<TechTreeStructure.TechTreeElement> TechTreeParentElements
		{
			get { return _techTreeParentElements; }
			set { _techTreeParentElements = value; }
		}

		/// <summary>
		/// Ruft das Language-DLL-Wrapperobjekt ab.
		/// </summary>
		public GenieLibrary.LanguageFileWrapper LanguageFileWrapper
		{
			get { return _languageFileWrapper; }
		}

		/// <summary>
		/// Die Language-Dateien. Neuzuweisen dieser Eigenschaft überschreibt alle bereits enthaltenen Language-Dateien!
		/// </summary>
		public List<string> LanguageFileNames
		{
			get { return _languageFiles.Keys.ToList(); }
			set
			{
				// Dateien laden
				_languageFiles = new Dictionary<string, RAMBuffer>();
				foreach(string file in value)
					_languageFiles.Add(Path.GetFileName(file).ToLower(), new RAMBuffer(file));

				// Handles erstellen
				CreateLanguageFileHandles();
			}
		}

		#endregion
	}
}
