using IORAMHelper;
using System.Collections.Generic;

namespace GenieLibrary.DataElements
{
	public class Sound : IGenieDataElement
	{
		#region Variablen

		public short ID;
		public short Unknown1;
		public int Unknown2;
		public List<SoundItem> Items;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			ID = buffer.ReadShort();
			Unknown1 = buffer.ReadShort();
			short count = buffer.ReadShort();
			Unknown2 = buffer.ReadInteger();

			Items = new List<SoundItem>(count);
			for(int i = 0; i < count; ++i)
				Items.Add(new SoundItem().ReadDataInline(buffer));
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteShort(ID);
			buffer.WriteShort(Unknown1);
			buffer.WriteShort((short)Items.Count);
			buffer.WriteInteger(Unknown2);

			Items.ForEach(e => e.WriteData(buffer));
		}

		#endregion Funktionen

		#region Strukturen

		public class SoundItem : IGenieDataElement
		{
			#region Variablen

			/// <summary>
			/// Länge: 13.
			/// </summary>
			public string FileName;

			public int ResourceID;
			public short Probability;
			public short Civ;
			public short Unknown1;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				FileName = buffer.ReadString(13);
				ResourceID = buffer.ReadInteger();
				Probability = buffer.ReadShort();
				Civ = buffer.ReadShort();
				Unknown1 = buffer.ReadShort();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteString(FileName, 13);
				buffer.WriteInteger(ResourceID);
				buffer.WriteShort(Probability);
				buffer.WriteShort(Civ);
				buffer.WriteShort(Unknown1);
			}

			#endregion Funktionen
		}

		#endregion Strukturen
	}
}