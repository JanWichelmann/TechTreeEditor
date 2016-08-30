using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IORAMHelper;
using SLPLoader;
using System.IO;
using System.Collections.ObjectModel;

namespace X2AddOnShipTool
{
	/// <summary>
	/// Enthält Informationen über ein zu verankerndes Schiff. Entspricht im Prinzip einer Projektdatei.
	/// </summary>
	class ShipFile
	{
		#region Variablen

		/// <summary>
		/// Der Name des Schiffes.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Die Rumpf-SLP.
		/// </summary>
		public SLPFile BaseSlp { get; set; }

		/// <summary>
		/// Die Schatten-SLP.
		/// </summary>
		public SLPFile ShadowSlp { get; set; }

		/// <summary>
		/// Die Segel.
		/// </summary>
		public Dictionary<Sail.SailType, Sail> Sails { get; private set; }

		/// <summary>
		/// Die 1-Typen der invertierten Segel.
		/// </summary>
		public List<Sail.SailType> InvertedSails { get; private set; }

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public ShipFile()
		{
			// Variablen erstellen
			Sails = new Dictionary<Sail.SailType, Sail>();
			InvertedSails = new List<Sail.SailType>();
		}

		/// <summary>
		/// Konstruktor. Lädt die angegebene Datei.
		/// </summary>
		/// <param name="filename">Die zu ladende Datei.</param>
		public ShipFile(string filename)
			: this()
		{
			// Datei laden
			RAMBuffer buffer = new RAMBuffer(filename);

			// Namen lesen
			Name = buffer.ReadString(buffer.ReadInteger());

			// Rumpf-SLP lesen
			if(buffer.ReadByte() == 1)
				BaseSlp = new SLPFile(buffer);

			// Schatten-SLP lesen
			if(buffer.ReadByte() == 1)
				ShadowSlp = new SLPFile(buffer);

			// Segel lesen
			int sailCount = buffer.ReadByte();
			for(int i = 0; i < sailCount; ++i)
			{
				// Lesen
				Sail.SailType currType = (Sail.SailType)buffer.ReadByte();
				Sail currSail = new Sail(buffer);
				Sails.Add(currType, currSail);
			}

			// Invertierte Segeltypen lesen
			int invSailCount = buffer.ReadByte();
			for(int i = 0; i < invSailCount; ++i)
				InvertedSails.Add((Sail.SailType)buffer.ReadByte());
		}

		#endregion

		#region Enumeration

		/// <summary>
		/// Die verfügbaren Kulturen.
		/// </summary>
		public enum Civ : byte
		{
			ME = 0,
			AS = 1,
			OR = 2,
			WE = 3,
			IN = 4
		}

		/// <summary>
		/// Die Namen der einzelnen Kulturen.
		/// </summary>

		public static readonly ReadOnlyDictionary<Civ, string> CivNames = new ReadOnlyDictionary<Civ, string>(new Dictionary<Civ, string>()
		{
			[Civ.ME] = "ME",
			[Civ.AS] = "AS",
			[Civ.OR] = "OR",
			[Civ.WE] = "WE",
			[Civ.IN] = "IN"
		});

		#endregion
	}
}
