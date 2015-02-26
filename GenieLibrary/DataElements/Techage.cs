using IORAMHelper;
using System.Collections.Generic;

namespace GenieLibrary.DataElements
{
	public class Techage : IGenieDataElement
	{
		#region Variablen

		/// <summary>
		/// Länge: 31.
		/// </summary>
		public string Name;

		public List<TechageEffect> Effects;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			Name = buffer.ReadString(31);

			int effectCount = buffer.ReadUShort();
			Effects = new List<TechageEffect>(effectCount);
			for(int i = 0; i < effectCount; ++i)
				Effects.Add(new TechageEffect().ReadDataInline(buffer));
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteString(Name, 31);
			buffer.WriteUShort((ushort)Effects.Count);
			Effects.ForEach(e => e.WriteData(buffer));
		}

		#endregion Funktionen

		#region Strukturen

		public class TechageEffect : IGenieDataElement
		{
			#region Variablen

			public byte Type;
			public short A;
			public short B;
			public short C;
			public float D;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				Type = buffer.ReadByte();
				A = buffer.ReadShort();
				B = buffer.ReadShort();
				C = buffer.ReadShort();
				D = buffer.ReadFloat();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteByte(Type);
				buffer.WriteShort(A);
				buffer.WriteShort(B);
				buffer.WriteShort(C);
				buffer.WriteFloat(D);
			}

			#endregion Funktionen
		}

		#endregion Strukturen
	}
}