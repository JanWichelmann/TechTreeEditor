using IORAMHelper;
using System;

namespace GenieLibrary.DataElements.UnitTypes
{
	public class Projectile : IGenieDataElement, ICloneable
	{
		#region Variablen

		public byte StretchMode;
		public byte CompensationMode;
		public byte DropAnimationMode;
		public byte PenetrationMode;
		public byte Unknown24;
		public float ProjectileArc;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			StretchMode = buffer.ReadByte();
			CompensationMode = buffer.ReadByte();
			DropAnimationMode = buffer.ReadByte();
			PenetrationMode = buffer.ReadByte();
			Unknown24 = buffer.ReadByte();
			ProjectileArc = buffer.ReadFloat();
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteByte(StretchMode);
			buffer.WriteByte(CompensationMode);
			buffer.WriteByte(DropAnimationMode);
			buffer.WriteByte(PenetrationMode);
			buffer.WriteByte(Unknown24);
			buffer.WriteFloat(ProjectileArc);
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