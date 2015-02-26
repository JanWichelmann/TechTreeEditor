using IORAMHelper;

namespace GenieLibrary.DataElements
{
	public class PlayerColor : IGenieDataElement
	{
		#region Variablen

		public int ID;
		public int Palette;
		public int Color;
		public int Unknown1;
		public int Unknown2;
		public int MinimapColor;
		public int Unknown3;
		public int Unknown4;
		public int StatisticsText;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			ID = buffer.ReadInteger();
			Palette = buffer.ReadInteger();
			Color = buffer.ReadInteger();
			Unknown1 = buffer.ReadInteger();
			Unknown2 = buffer.ReadInteger();
			MinimapColor = buffer.ReadInteger();
			Unknown3 = buffer.ReadInteger();
			Unknown4 = buffer.ReadInteger();
			StatisticsText = buffer.ReadInteger();
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteInteger(ID);
			buffer.WriteInteger(Palette);
			buffer.WriteInteger(Color);
			buffer.WriteInteger(Unknown1);
			buffer.WriteInteger(Unknown2);
			buffer.WriteInteger(MinimapColor);
			buffer.WriteInteger(Unknown3);
			buffer.WriteInteger(Unknown4);
			buffer.WriteInteger(StatisticsText);
		}

		#endregion Funktionen
	}
}