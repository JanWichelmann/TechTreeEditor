using IORAMHelper;
using System.Collections.Generic;

namespace GenieLibrary.DataElements
{
	public class UnitHeader : IGenieDataElement
	{
		#region Variablen

		public byte Exists;
		public List<UnitCommand> Commands;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			Exists = buffer.ReadByte();

			if(Exists != 0)
			{
				int commandCount = buffer.ReadUShort();
				Commands = new List<UnitCommand>(commandCount);
				for(int i = 0; i < commandCount; ++i)
					Commands.Add(new UnitCommand().ReadDataInline(buffer));
			}
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteByte(Exists);

			if(Exists != 0)
			{
				buffer.WriteUShort((ushort)Commands.Count);
				Commands.ForEach(e => e.WriteData(buffer));
			}
		}

		#endregion Funktionen

		#region Strukturen

		public class UnitCommand : IGenieDataElement
		{
			#region Variablen

			private short One;
			private short ID;
			private byte Unknown1;
			private short Type;
			private short ClassID;
			private short UnitID;
			private short Unknown2;
			private short ResourceIn;
			private short ResourceProductivityMultiplier;
			private short ResourceOut;
			private short Resource;
			private float WorkRateMultiplier;
			private float ExecutionRadius;
			private float ExtraRange;
			private byte Unknown4;
			private float Unknown5;
			private byte SelectionEnabler;
			private byte Unknown7;
			private short Unknown8;
			private short Unknown9;
			private byte SelectionMode;
			private byte Unknown11;
			private byte Unknown12;
			private List<short> Graphics;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				One = buffer.ReadShort();
				ID = buffer.ReadShort();
				Unknown1 = buffer.ReadByte();
				Type = buffer.ReadShort();
				ClassID = buffer.ReadShort();
				UnitID = buffer.ReadShort();
				Unknown2 = buffer.ReadShort();
				ResourceIn = buffer.ReadShort();
				ResourceProductivityMultiplier = buffer.ReadShort();
				ResourceOut = buffer.ReadShort();
				Resource = buffer.ReadShort();
				WorkRateMultiplier = buffer.ReadFloat();
				ExecutionRadius = buffer.ReadFloat();
				ExtraRange = buffer.ReadFloat();
				Unknown4 = buffer.ReadByte();
				Unknown5 = buffer.ReadFloat();
				SelectionEnabler = buffer.ReadByte();
				Unknown7 = buffer.ReadByte();
				Unknown8 = buffer.ReadShort();
				Unknown9 = buffer.ReadShort();
				SelectionMode = buffer.ReadByte();
				Unknown11 = buffer.ReadByte();
				Unknown12 = buffer.ReadByte();

				Graphics = new List<short>(6);
				for(int i = 0; i < 6; ++i)
					Graphics.Add(buffer.ReadShort());
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteShort(One);
				buffer.WriteShort(ID);
				buffer.WriteByte(Unknown1);
				buffer.WriteShort(Type);
				buffer.WriteShort(ClassID);
				buffer.WriteShort(UnitID);
				buffer.WriteShort(Unknown2);
				buffer.WriteShort(ResourceIn);
				buffer.WriteShort(ResourceProductivityMultiplier);
				buffer.WriteShort(ResourceOut);
				buffer.WriteShort(Resource);
				buffer.WriteFloat(WorkRateMultiplier);
				buffer.WriteFloat(ExecutionRadius);
				buffer.WriteFloat(ExtraRange);
				buffer.WriteByte(Unknown4);
				buffer.WriteFloat(Unknown5);
				buffer.WriteByte(SelectionEnabler);
				buffer.WriteByte(Unknown7);
				buffer.WriteShort(Unknown8);
				buffer.WriteShort(Unknown9);
				buffer.WriteByte(SelectionMode);
				buffer.WriteByte(Unknown11);
				buffer.WriteByte(Unknown12);

				AssertListLength(Graphics, 6);
				Graphics.ForEach(e => buffer.WriteShort(e));
			}

			#endregion Funktionen
		}

		#endregion Strukturen
	}
}