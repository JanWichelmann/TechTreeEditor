using IORAMHelper;
using System;

namespace GenieLibrary.DataElements.UnitTypes
{
	public class Bird : IGenieDataElement, ICloneable
	{
		#region Variablen

		public short SheepConversion;
		public float SearchRadius;
		public float WorkRate;
		public short DropSite1;
		public short DropSite2;
		public byte VillagerMode;
		public short AttackSound;
		public short MoveSound;
		public byte AnimalMode;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			SheepConversion = buffer.ReadShort();
			SearchRadius = buffer.ReadFloat();
			WorkRate = buffer.ReadFloat();
			DropSite1 = buffer.ReadShort();
			DropSite2 = buffer.ReadShort();
			VillagerMode = buffer.ReadByte();
			AttackSound = buffer.ReadShort();
			MoveSound = buffer.ReadShort();
			AnimalMode = buffer.ReadByte();
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteShort(SheepConversion);
			buffer.WriteFloat(SearchRadius);
			buffer.WriteFloat(WorkRate);
			buffer.WriteShort(DropSite1);
			buffer.WriteShort(DropSite2);
			buffer.WriteByte(VillagerMode);
			buffer.WriteShort(AttackSound);
			buffer.WriteShort(MoveSound);
			buffer.WriteByte(AnimalMode);
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
}