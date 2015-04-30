using IORAMHelper;
using System;
using System.Collections.Generic;

namespace GenieLibrary.DataElements.UnitTypes
{
	public class DeadFish : IGenieDataElement, ICloneable
	{
		#region Variablen

		public short WalkingGraphic1;
		public short WalkingGraphic2;
		public float RotationSpeed;
		public byte Unknown11;
		public short TrackingUnit;
		public byte TrackingUnitUsed;
		public float TrackingUnitDensity;
		public byte Unknown16;

		/// <summary>
		/// Länge: 5.
		/// </summary>
		public List<int> Unknown16B;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			WalkingGraphic1 = buffer.ReadShort();
			WalkingGraphic2 = buffer.ReadShort();
			RotationSpeed = buffer.ReadFloat();
			Unknown11 = buffer.ReadByte();
			TrackingUnit = buffer.ReadShort();
			TrackingUnitUsed = buffer.ReadByte();
			TrackingUnitDensity = buffer.ReadFloat();
			Unknown16 = buffer.ReadByte();

			Unknown16B = new List<int>(5);
			for(int i = 0; i < 5; ++i)
				Unknown16B.Add(buffer.ReadInteger());
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteShort(WalkingGraphic1);
			buffer.WriteShort(WalkingGraphic2);
			buffer.WriteFloat(RotationSpeed);
			buffer.WriteByte(Unknown11);
			buffer.WriteShort(TrackingUnit);
			buffer.WriteByte(TrackingUnitUsed);
			buffer.WriteFloat(TrackingUnitDensity);
			buffer.WriteByte(Unknown16);

			AssertListLength(Unknown16B, 5);
			Unknown16B.ForEach(e => buffer.WriteInteger(e));
		}

		/// <summary>
		/// Gibt eine tiefe Kopie dieses Objekts zurück.
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			// Erstmal alle Wert-Typen kopieren
			DeadFish clone = (DeadFish)this.MemberwiseClone();

			// Referenztypen kopieren
			clone.Unknown16B = new List<int>(Unknown16B);

			// Fertig
			return clone;
		}

		#endregion Funktionen
	}
}