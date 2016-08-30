using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLPLoader;
using IORAMHelper;
using System.Collections.ObjectModel;

namespace X2AddOnShipTool
{
	/// <summary>
	/// Repräsentiert ein Segel mit allen assoziierten Informationen.
	/// </summary>
	class Sail
	{
		#region Variablen

		/// <summary>
		/// Die SLPs für die einzelnen Völker.
		/// </summary>
		public Dictionary<ShipFile.Civ, SLPFile> SailSlps { get; private set; }

		/// <summary>
		/// Gibt an, ob das Segel in Benutzung ist.
		/// </summary>
		public bool Used { get; set; } = false;

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public Sail()
		{
			// Objekte erstellen
			SailSlps = new Dictionary<ShipFile.Civ, SLPFile>();
		}

		/// <summary>
		/// Konstruktor. Liest die Daten aus dem gegebenen Puffer.
		/// </summary>
		/// <param name="buffer">Der Puffer, aus dem die Segeldaten gelesen werden sollen.</param>
		public Sail(RAMBuffer buffer)
			: this()
		{
			// Benutzung lesen
			Used = (buffer.ReadByte() == 1);

			// SLPs lesen
			int count = buffer.ReadByte();
			for(int i = 0; i < count; ++i)
			{
				// Lesen
				ShipFile.Civ currCiv = (ShipFile.Civ)buffer.ReadByte();
				SLPFile currSlp = new SLPFile(buffer);
				SailSlps.Add(currCiv, currSlp);
			}
		}

		#endregion

		#region Enumerationen

		/// <summary>
		/// Die verschiedenen Segeltypen und -positionen.
		/// </summary>
		public enum SailType : byte
		{
			MainGo = 1,
			Small1 = 2,
			Mid1 = 3,
			Large1 = 4,
			MainStop = 5,
			Large2 = 6,
			Mid2 = 7,
			Small2 = 8
		}

		/// <summary>
		/// Die Namen der einzelnen Segeltypen.
		/// </summary>

		public static readonly ReadOnlyDictionary<SailType, string> SailTypeNames = new ReadOnlyDictionary<SailType, string>(new Dictionary<SailType, string>()
		{
			[SailType.Small1] = "Segel hinten 1",
			[SailType.Small2] = "Segel hinten 2",
			[SailType.Mid1] = "Segel vorn 1",
			[SailType.Mid2] = "Segel vorn 2",
			[SailType.Large1] = "Segel mitte 1",
			[SailType.Large2] = "Segel mitte 2",
			[SailType.MainGo] = "Segel haupt Wind",
			[SailType.MainStop] = "Segel haupt"
		});

		#endregion

		#region Segelliste

		/// <summary>
		/// Die Segel-SLP-Ressourcen-IDs. Direkt abgelesen von den SHIP***-Grafiken aus der DAT.
		/// </summary>
		private Tuple<SailType, ShipFile.Civ, ushort>[] SAIL_RESOURCE_IDs =
		{
			/*new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainGo, ShipFile.Civ.ME, 4301),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainGo, ShipFile.Civ.AS, 4302),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainGo, ShipFile.Civ.OR, 4303),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainGo, ShipFile.Civ.WE, 4304),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainGo, ShipFile.Civ.IN, 5093),*/

			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small1, ShipFile.Civ.ME, 4305),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small1, ShipFile.Civ.AS, 4306),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small1, ShipFile.Civ.OR, 4307),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small1, ShipFile.Civ.WE, 4308),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small1, ShipFile.Civ.IN, 5094),

			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid1, ShipFile.Civ.ME, 4317),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid1, ShipFile.Civ.AS, 4318),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid1, ShipFile.Civ.OR, 4319),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid1, ShipFile.Civ.WE, 4320),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid1, ShipFile.Civ.IN, 5097),

			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large1, ShipFile.Civ.ME, 4309),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large1, ShipFile.Civ.AS, 4310),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large1, ShipFile.Civ.OR, 4311),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large1, ShipFile.Civ.WE, 4312),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large1, ShipFile.Civ.IN, 5095),

			/*new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainStop, ShipFile.Civ.ME, 4598),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainStop, ShipFile.Civ.AS, 4599),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainStop, ShipFile.Civ.OR, 4600),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainStop, ShipFile.Civ.WE, 4601),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.MainStop, ShipFile.Civ.IN, 5092),*/

			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large2, ShipFile.Civ.ME, 4321),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large2, ShipFile.Civ.AS, 4322),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large2, ShipFile.Civ.OR, 4323),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large2, ShipFile.Civ.WE, 4324),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Large2, ShipFile.Civ.IN, 5098),

			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid2, ShipFile.Civ.ME, 4313),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid2, ShipFile.Civ.AS, 4314),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid2, ShipFile.Civ.OR, 4315),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid2, ShipFile.Civ.WE, 4316),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Mid2, ShipFile.Civ.IN, 5096),

			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small2, ShipFile.Civ.ME, 4325),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small2, ShipFile.Civ.AS, 4326),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small2, ShipFile.Civ.OR, 4327),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small2, ShipFile.Civ.WE, 4328),
			new Tuple<SailType, ShipFile.Civ, ushort>(SailType.Small2, ShipFile.Civ.IN, 5099),
		};

		#endregion
	}
}
