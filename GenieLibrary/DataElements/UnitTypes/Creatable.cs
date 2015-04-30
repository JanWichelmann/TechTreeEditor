using IORAMHelper;
using System;
using System.Collections.Generic;

namespace GenieLibrary.DataElements.UnitTypes
{
	public class Creatable : IGenieDataElement, ICloneable
	{
		#region Variablen

		/// <summary>
		/// Länge: 3;
		/// </summary>
		public List<ResourceTuple<short, short, short>> ResourceCosts;

		public short TrainTime;
		public short TrainLocationID;
		public byte ButtonID;
		public int Unknown26;
		public int Unknown27;
		public byte Unknown28;
		public byte HeroMode;
		public int GarrisonGraphic;
		public float DuplicatedMissilesMin;
		public byte DuplicatedMissilesMax;

		/// <summary>
		/// Länge: 3.
		/// </summary>
		public List<float> MissileSpawningArea;

		public int AlternativeProjectileUnit;
		public int ChargingGraphic;
		public byte ChargingMode;
		public short DisplayedPierceArmour;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			ResourceCosts = new List<ResourceTuple<short, short, short>>(3);
			for(int i = 0; i < 3; ++i)
				ResourceCosts.Add(new ResourceTuple<short, short, short>() { Type = buffer.ReadShort(), Amount = buffer.ReadShort(), Enabled = buffer.ReadShort() });

			TrainTime = buffer.ReadShort();
			TrainLocationID = buffer.ReadShort();
			ButtonID = buffer.ReadByte();
			Unknown26 = buffer.ReadInteger();
			Unknown27 = buffer.ReadInteger();
			Unknown28 = buffer.ReadByte();
			HeroMode = buffer.ReadByte();
			GarrisonGraphic = buffer.ReadInteger();
			DuplicatedMissilesMin = buffer.ReadFloat();
			DuplicatedMissilesMax = buffer.ReadByte();

			MissileSpawningArea = new List<float>(3);
			for(int i = 0; i < 3; ++i)
				MissileSpawningArea.Add(buffer.ReadFloat());

			AlternativeProjectileUnit = buffer.ReadInteger();
			ChargingGraphic = buffer.ReadInteger();
			ChargingMode = buffer.ReadByte();
			DisplayedPierceArmour = buffer.ReadShort();
		}

		public override void WriteData(RAMBuffer buffer)
		{
			AssertListLength(ResourceCosts, 3);
			ResourceCosts.ForEach(e =>
			{
				buffer.WriteShort(e.Type);
				buffer.WriteShort(e.Amount);
				buffer.WriteShort(e.Enabled);
			});

			buffer.WriteShort(TrainTime);
			buffer.WriteShort(TrainLocationID);
			buffer.WriteByte(ButtonID);
			buffer.WriteInteger(Unknown26);
			buffer.WriteInteger(Unknown27);
			buffer.WriteByte(Unknown28);
			buffer.WriteByte(HeroMode);
			buffer.WriteInteger(GarrisonGraphic);
			buffer.WriteFloat(DuplicatedMissilesMin);
			buffer.WriteByte(DuplicatedMissilesMax);

			AssertListLength(MissileSpawningArea, 3);
			MissileSpawningArea.ForEach(e => buffer.WriteFloat(e));

			buffer.WriteInteger(AlternativeProjectileUnit);
			buffer.WriteInteger(ChargingGraphic);
			buffer.WriteByte(ChargingMode);
			buffer.WriteShort(DisplayedPierceArmour);
		}

		/// <summary>
		/// Gibt eine tiefe Kopie dieses Objekts zurück.
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			// Erstmal alle Wert-Typen kopieren
			Creatable clone = (Creatable)this.MemberwiseClone();

			// Referenztypen kopieren
			clone.ResourceCosts = new List<ResourceTuple<short, short, short>>(ResourceCosts);
			clone.MissileSpawningArea = new List<float>(MissileSpawningArea);

			// Fertig
			return clone;
		}

		#endregion Funktionen
	}
}