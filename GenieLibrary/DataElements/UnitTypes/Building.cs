using IORAMHelper;
using System;
using System.Collections.Generic;

namespace GenieLibrary.DataElements.UnitTypes
{
	public class Building : IGenieDataElement, ICloneable
	{
		#region Variablen

		public short ConstructionGraphicID;
		public short SnowGraphicID;
		public byte AdjacentMode;
		public short GraphicsAngle;
		public byte DisappearsWhenBuilt;
		public short StackUnitID;
		public short FoundationTerrainID;
		public short OldTerrainLikeID;
		public short ResearchID;
		public byte Unknown33;

		/// <summary>
		/// Länge: 4.
		/// </summary>
		public List<BuildingAnnex> Annexes;

		public short HeadUnit;
		public short TransformUnit;
		public short UnknownSound;
		public short ConstructionSound;
		public byte GarrisonType;
		public float GarrisonHealRate;
		public float Unknown35;
		public short PileUnit;

		/// <summary>
		/// Länge: 6.
		/// </summary>
		public List<byte> LootingTable;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			ConstructionGraphicID = buffer.ReadShort();
			SnowGraphicID = buffer.ReadShort();
			AdjacentMode = buffer.ReadByte();
			GraphicsAngle = buffer.ReadShort();
			DisappearsWhenBuilt = buffer.ReadByte();
			StackUnitID = buffer.ReadShort();
			FoundationTerrainID = buffer.ReadShort();
			OldTerrainLikeID = buffer.ReadShort();
			ResearchID = buffer.ReadShort();
			Unknown33 = buffer.ReadByte();

			Annexes = new List<BuildingAnnex>(4);
			for(int i = 0; i < 4; ++i)
				Annexes.Add(new BuildingAnnex().ReadDataInline(buffer));

			HeadUnit = buffer.ReadShort();
			TransformUnit = buffer.ReadShort();
			UnknownSound = buffer.ReadShort();
			ConstructionSound = buffer.ReadShort();
			GarrisonType = buffer.ReadByte();
			GarrisonHealRate = buffer.ReadFloat();
			Unknown35 = buffer.ReadFloat();
			PileUnit = buffer.ReadShort();

			LootingTable = new List<byte>(6);
			for(int i = 0; i < 6; ++i)
				LootingTable.Add(buffer.ReadByte());
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteShort(ConstructionGraphicID);
			buffer.WriteShort(SnowGraphicID);
			buffer.WriteByte(AdjacentMode);
			buffer.WriteShort(GraphicsAngle);
			buffer.WriteByte(DisappearsWhenBuilt);
			buffer.WriteShort(StackUnitID);
			buffer.WriteShort(FoundationTerrainID);
			buffer.WriteShort(OldTerrainLikeID);
			buffer.WriteShort(ResearchID);
			buffer.WriteByte(Unknown33);

			AssertListLength(Annexes, 4);
			Annexes.ForEach(e => e.WriteData(buffer));

			buffer.WriteShort(HeadUnit);
			buffer.WriteShort(TransformUnit);
			buffer.WriteShort(UnknownSound);
			buffer.WriteShort(ConstructionSound);
			buffer.WriteByte(GarrisonType);
			buffer.WriteFloat(GarrisonHealRate);
			buffer.WriteFloat(Unknown35);
			buffer.WriteShort(PileUnit);

			AssertListLength(LootingTable, 6);
			LootingTable.ForEach(e => buffer.WriteByte(e));
		}

		/// <summary>
		/// Gibt eine tiefe Kopie dieses Objekts zurück.
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			// Erstmal alle Wert-Typen kopieren
			Building clone = (Building)this.MemberwiseClone();

			// Referenztypen kopieren
			clone.Annexes = new List<BuildingAnnex>(Annexes.Count);
			Annexes.ForEach(a => clone.Annexes.Add((BuildingAnnex)a.Clone()));
			clone.LootingTable = new List<byte>(LootingTable);

			// Fertig
			return clone;
		}

		#endregion Funktionen

		#region Strukturen

		public class BuildingAnnex : IGenieDataElement, ICloneable
		{
			#region Variablen

			public short UnitID;
			public float MisplacementX;
			public float MisplacementY;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				UnitID = buffer.ReadShort();
				MisplacementX = buffer.ReadFloat();
				MisplacementY = buffer.ReadFloat();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteShort(UnitID);
				buffer.WriteFloat(MisplacementX);
				buffer.WriteFloat(MisplacementY);
			}

			/// <summary>
			/// Gibt eine tiefe Kopie dieses Objekts zurück.
			/// </summary>
			/// <returns></returns>
			public object Clone()
			{
				// Keine Referenztypen vorhanden, flache Kopie reicht
				return this.MemberwiseClone();
			}

			#endregion Funktionen
		}

		#endregion Strukturen
	}
}