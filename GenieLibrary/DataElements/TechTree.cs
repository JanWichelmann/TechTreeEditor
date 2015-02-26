using IORAMHelper;
using System.Collections.Generic;

namespace GenieLibrary.DataElements
{
	public class TechTree : IGenieDataElement
	{
		#region Variablen

		public int Unknown2;
		public List<TechTreeAge> TechTreeAges;
		public List<BuildingConnection> BuildingConnections;
		public List<UnitConnection> UnitConnections;
		public List<ResearchConnection> ResearchConnections;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			byte ageCount = buffer.ReadByte();
			byte buildingCount = buffer.ReadByte();
			byte unitCount = buffer.ReadByte();
			byte researchCount = buffer.ReadByte();
			Unknown2 = buffer.ReadInteger();

			TechTreeAges = new List<TechTreeAge>(ageCount);
			for(int i = 0; i < ageCount; ++i)
				TechTreeAges.Add(new TechTreeAge().ReadDataInline(buffer));

			BuildingConnections = new List<BuildingConnection>(buildingCount);
			for(int i = 0; i < buildingCount; ++i)
				BuildingConnections.Add(new BuildingConnection().ReadDataInline(buffer));

			UnitConnections = new List<UnitConnection>(unitCount);
			for(int i = 0; i < unitCount; ++i)
				UnitConnections.Add(new UnitConnection().ReadDataInline(buffer));

			ResearchConnections = new List<ResearchConnection>(researchCount);
			for(int i = 0; i < researchCount; ++i)
				ResearchConnections.Add(new ResearchConnection().ReadDataInline(buffer));
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteByte((byte)TechTreeAges.Count);
			buffer.WriteByte((byte)BuildingConnections.Count);
			buffer.WriteByte((byte)UnitConnections.Count);
			buffer.WriteByte((byte)ResearchConnections.Count);
			buffer.WriteInteger(Unknown2);

			TechTreeAges.ForEach(e => e.WriteData(buffer));
			BuildingConnections.ForEach(e => e.WriteData(buffer));
			UnitConnections.ForEach(e => e.WriteData(buffer));
			ResearchConnections.ForEach(e => e.WriteData(buffer));
		}

		#endregion Funktionen

		#region Strukturen

		public class Common : IGenieDataElement
		{
			#region Variablen

			public int SlotsUsed;

			/// <summary>
			/// Länge: 10.
			/// </summary>
			public List<int> UnitResearch;

			/// <summary>
			/// Länge: 10.
			/// </summary>
			public List<int> Mode;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				SlotsUsed = buffer.ReadInteger();

				UnitResearch = new List<int>(10);
				for(int i = 0; i < 10; ++i)
					UnitResearch.Add(buffer.ReadInteger());

				Mode = new List<int>(10);
				for(int i = 0; i < 10; ++i)
					Mode.Add(buffer.ReadInteger());
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteInteger(SlotsUsed);

				AssertListLength(UnitResearch, 10);
				UnitResearch.ForEach(e => buffer.WriteInteger(e));

				AssertListLength(Mode, 10);
				Mode.ForEach(e => buffer.WriteInteger(e));
			}

			#endregion Funktionen
		}

		public class TechTreeAge : IGenieDataElement
		{
			#region Variablen

			public int ID;
			public byte Unknown2;
			public List<int> Buildings;
			public List<int> Units;
			public List<int> Researches;
			public Common Common;
			public byte SlotsUsed;

			/// <summary>
			/// Länge: 10.
			/// </summary>
			public List<byte> Unknown4;

			/// <summary>
			/// Länge: 10.
			/// </summary>
			public List<byte> Unknown5;

			public byte Unknown6;
			public int LineMode;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				ID = buffer.ReadInteger();
				Unknown2 = buffer.ReadByte();

				byte buildingCount = buffer.ReadByte();
				Buildings = new List<int>(buildingCount);
				for(int i = 0; i < buildingCount; ++i)
					Buildings.Add(buffer.ReadInteger());

				byte unitCount = buffer.ReadByte();
				Units = new List<int>(unitCount);
				for(int i = 0; i < unitCount; ++i)
					Units.Add(buffer.ReadInteger());

				byte researchCount = buffer.ReadByte();
				Researches = new List<int>(researchCount);
				for(int i = 0; i < researchCount; ++i)
					Researches.Add(buffer.ReadInteger());

				Common = new Common().ReadDataInline(buffer);
				SlotsUsed = buffer.ReadByte();

				Unknown4 = new List<byte>(10);
				for(int i = 0; i < 10; ++i)
					Unknown4.Add(buffer.ReadByte());

				Unknown5 = new List<byte>(10);
				for(int i = 0; i < 10; ++i)
					Unknown5.Add(buffer.ReadByte());

				Unknown6 = buffer.ReadByte();
				LineMode = buffer.ReadInteger();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteInteger(ID);
				buffer.WriteByte(Unknown2);

				buffer.WriteByte((byte)Buildings.Count);
				Buildings.ForEach(e => buffer.WriteInteger(e));

				buffer.WriteByte((byte)Units.Count);
				Units.ForEach(e => buffer.WriteInteger(e));

				buffer.WriteByte((byte)Researches.Count);
				Researches.ForEach(e => buffer.WriteInteger(e));

				Common.WriteData(buffer);
				buffer.WriteByte(SlotsUsed);

				AssertListLength(Unknown4, 10);
				Unknown4.ForEach(e => buffer.WriteByte(e));

				AssertListLength(Unknown5, 10);
				Unknown5.ForEach(e => buffer.WriteByte(e));

				buffer.WriteByte(Unknown6);
				buffer.WriteInteger(LineMode);
			}

			#endregion Funktionen
		}

		public class BuildingConnection : IGenieDataElement
		{
			#region Variablen

			public int ID;
			public byte Unknown1;
			public List<int> Buildings;
			public List<int> Units;
			public List<int> Researches;
			public Common Common;
			public byte LocationInAge;

			/// <summary>
			/// Länge: 5.
			/// </summary>
			public List<byte> UnitsTechsTotal;

			/// <summary>
			/// Länge: 5.
			/// </summary>
			public List<byte> UnitsTechsFirst;

			public int LineMode;
			public int EnablingResearch;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				ID = buffer.ReadInteger();
				Unknown1 = buffer.ReadByte();

				byte buildingCount = buffer.ReadByte();
				Buildings = new List<int>(buildingCount);
				for(int i = 0; i < buildingCount; ++i)
					Buildings.Add(buffer.ReadInteger());

				byte unitCount = buffer.ReadByte();
				Units = new List<int>(unitCount);
				for(int i = 0; i < unitCount; ++i)
					Units.Add(buffer.ReadInteger());

				byte researchCount = buffer.ReadByte();
				Researches = new List<int>(researchCount);
				for(int i = 0; i < researchCount; ++i)
					Researches.Add(buffer.ReadInteger());

				Common = new Common().ReadDataInline(buffer);
				LocationInAge = buffer.ReadByte();

				UnitsTechsTotal = new List<byte>(5);
				for(int i = 0; i < 5; ++i)
					UnitsTechsTotal.Add(buffer.ReadByte());

				UnitsTechsFirst = new List<byte>(5);
				for(int i = 0; i < 5; ++i)
					UnitsTechsFirst.Add(buffer.ReadByte());

				LineMode = buffer.ReadInteger();
				EnablingResearch = buffer.ReadInteger();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteInteger(ID);
				buffer.WriteByte(Unknown1);

				buffer.WriteByte((byte)Buildings.Count);
				Buildings.ForEach(e => buffer.WriteInteger(e));

				buffer.WriteByte((byte)Units.Count);
				Units.ForEach(e => buffer.WriteInteger(e));

				buffer.WriteByte((byte)Researches.Count);
				Researches.ForEach(e => buffer.WriteInteger(e));

				Common.WriteData(buffer);
				buffer.WriteByte(LocationInAge);

				AssertListLength(UnitsTechsTotal, 5);
				UnitsTechsTotal.ForEach(e => buffer.WriteByte(e));

				AssertListLength(UnitsTechsFirst, 5);
				UnitsTechsFirst.ForEach(e => buffer.WriteByte(e));

				buffer.WriteInteger(LineMode);
				buffer.WriteInteger(EnablingResearch);
			}

			#endregion Funktionen
		}

		public class UnitConnection : IGenieDataElement
		{
			#region Variablen

			public int ID;
			public byte Unknown1;
			public int UpperBuilding;
			public Common Common;
			public int VerticalLine;
			public List<int> Units;
			public int LocationInAge;
			public int RequiredResearch;
			public int LineMode;
			public int EnablingResearch;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				ID = buffer.ReadInteger();
				Unknown1 = buffer.ReadByte();
				UpperBuilding = buffer.ReadInteger();

				Common = new Common().ReadDataInline(buffer);

				VerticalLine = buffer.ReadInteger();

				byte unitCount = buffer.ReadByte();
				Units = new List<int>(unitCount);
				for(int i = 0; i < unitCount; ++i)
					Units.Add(buffer.ReadInteger());

				LocationInAge = buffer.ReadInteger();
				RequiredResearch = buffer.ReadInteger();
				LineMode = buffer.ReadInteger();
				EnablingResearch = buffer.ReadInteger();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteInteger(ID);
				buffer.WriteByte(Unknown1);
				buffer.WriteInteger(UpperBuilding);

				Common.WriteData(buffer);

				buffer.WriteInteger(VerticalLine);

				buffer.WriteByte((byte)Units.Count);
				Units.ForEach(e => buffer.WriteInteger(e));

				buffer.WriteInteger(LocationInAge);
				buffer.WriteInteger(RequiredResearch);
				buffer.WriteInteger(LineMode);
				buffer.WriteInteger(EnablingResearch);
			}

			#endregion Funktionen
		}

		public class ResearchConnection : IGenieDataElement
		{
			#region Variablen

			public int ID;
			public byte Unknown1;
			public int UpperBuilding;
			public List<int> Buildings;
			public List<int> Units;
			public List<int> Researches;
			public Common Common;
			public int VerticalLine;
			public int LocationInAge;
			public int LineMode;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				ID = buffer.ReadInteger();
				Unknown1 = buffer.ReadByte();
				UpperBuilding = buffer.ReadInteger();

				byte buildingCount = buffer.ReadByte();
				Buildings = new List<int>(buildingCount);
				for(int i = 0; i < buildingCount; ++i)
					Buildings.Add(buffer.ReadInteger());

				byte unitCount = buffer.ReadByte();
				Units = new List<int>(unitCount);
				for(int i = 0; i < unitCount; ++i)
					Units.Add(buffer.ReadInteger());

				byte researchCount = buffer.ReadByte();
				Researches = new List<int>(researchCount);
				for(int i = 0; i < researchCount; ++i)
					Researches.Add(buffer.ReadInteger());

				Common = new Common().ReadDataInline(buffer);

				VerticalLine = buffer.ReadInteger();
				LocationInAge = buffer.ReadInteger();
				LineMode = buffer.ReadInteger();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteInteger(ID);
				buffer.WriteByte(Unknown1);
				buffer.WriteInteger(UpperBuilding);

				buffer.WriteByte((byte)Buildings.Count);
				Buildings.ForEach(e => buffer.WriteInteger(e));

				buffer.WriteByte((byte)Units.Count);
				Units.ForEach(e => buffer.WriteInteger(e));

				buffer.WriteByte((byte)Researches.Count);
				Researches.ForEach(e => buffer.WriteInteger(e));

				Common.WriteData(buffer);

				buffer.WriteInteger(VerticalLine);
				buffer.WriteInteger(LocationInAge);
				buffer.WriteInteger(LineMode);
			}

			#endregion Funktionen
		}

		#endregion Strukturen
	}
}