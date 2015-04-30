using IORAMHelper;
using System;
using System.Collections.Generic;

namespace GenieLibrary.DataElements
{
	public class Graphic : IGenieDataElement, ICloneable
	{
		#region Variablen

		/// <summary>
		/// Länge: 21.
		/// </summary>
		public string Name1;

		/// <summary>
		/// Länge: 13.
		/// </summary>
		public string Name2;

		public int SLP;
		public byte Unknown1;
		public byte Unknown2;
		public byte Layer;
		public byte PlayerColor;
		public byte Rainbow;
		public byte Replay;

		/// <summary>
		/// Länge: 4.
		/// </summary>
		public List<short> Coordinates;

		public short SoundID;
		public byte AttackSoundUsed;
		public ushort FrameCount;
		public ushort AngleCount;
		public float NewSpeed;
		public float FrameRate;
		public float ReplayDelay;
		public byte SequenceType;
		public short ID;
		public byte MirroringMode;
		public byte Unknown3;
		public List<GraphicDelta> Deltas;

		/// <summary>
		/// Länge: AngleCount.
		/// </summary>
		public List<GraphicAttackSound> AttackSounds;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			Name1 = buffer.ReadString(21);
			Name2 = buffer.ReadString(13);

			SLP = buffer.ReadInteger();
			Unknown1 = buffer.ReadByte();
			Unknown2 = buffer.ReadByte();
			Layer = buffer.ReadByte();
			PlayerColor = buffer.ReadByte();
			Rainbow = buffer.ReadByte();
			Replay = buffer.ReadByte();

			Coordinates = new List<short>(4);
			for(int i = 0; i < 4; ++i)
				Coordinates.Add(buffer.ReadShort());

			int deltaCount = buffer.ReadUShort();

			SoundID = buffer.ReadShort();
			AttackSoundUsed = buffer.ReadByte();
			FrameCount = buffer.ReadUShort();
			AngleCount = buffer.ReadUShort();
			NewSpeed = buffer.ReadFloat();
			FrameRate = buffer.ReadFloat();
			ReplayDelay = buffer.ReadFloat();
			SequenceType = buffer.ReadByte();
			ID = buffer.ReadShort();
			MirroringMode = buffer.ReadByte();
			Unknown3 = buffer.ReadByte();

			Deltas = new List<GraphicDelta>(deltaCount);
			for(int i = 0; i < deltaCount; ++i)
				Deltas.Add(new GraphicDelta().ReadDataInline(buffer));

			if(AttackSoundUsed != 0)
			{
				AttackSounds = new List<GraphicAttackSound>(AngleCount);
				for(int i = 0; i < AngleCount; ++i)
					AttackSounds.Add(new GraphicAttackSound().ReadDataInline(buffer));
			}
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteString(Name1, 21);
			buffer.WriteString(Name2, 13);

			buffer.WriteInteger(SLP);
			buffer.WriteByte(Unknown1);
			buffer.WriteByte(Unknown2);
			buffer.WriteByte(Layer);
			buffer.WriteByte(PlayerColor);
			buffer.WriteByte(Rainbow);
			buffer.WriteByte(Replay);

			AssertListLength(Coordinates, 4);
			Coordinates.ForEach(e => buffer.WriteShort(e));

			buffer.WriteUShort((ushort)Deltas.Count);
			buffer.WriteShort(SoundID);
			buffer.WriteByte(AttackSoundUsed);
			buffer.WriteUShort(FrameCount);
			buffer.WriteUShort(AngleCount);
			buffer.WriteFloat(NewSpeed);
			buffer.WriteFloat(FrameRate);
			buffer.WriteFloat(ReplayDelay);
			buffer.WriteByte(SequenceType);
			buffer.WriteShort(ID);
			buffer.WriteByte(MirroringMode);
			buffer.WriteByte(Unknown3);

			Deltas.ForEach(e => e.WriteData(buffer));
			if(AttackSoundUsed != 0)
			{
				AssertListLength(AttackSounds, AngleCount);
				AttackSounds.ForEach(e => e.WriteData(buffer));
			}
		}

		/// <summary>
		/// Gibt eine eindeutige Repräsentation dieser Grafik bestehend aus ID und Name zurück.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if(ID >= 0)
				return ID.ToString() + ": " + Name1.TrimEnd('\0');
			else
				return "None";
		}

		/// <summary>
		/// Gibt eine tiefe Kopie dieses Objekts zurück.
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			// Erstmal alle Wert-Typen kopieren
			Graphic clone = (Graphic)this.MemberwiseClone();

			// Referenztypen kopieren
			clone.Name1 = (string)Name1.Clone();
			clone.Name2 = (string)Name2.Clone();
			clone.Coordinates = new List<short>(Coordinates);
			if(AttackSounds != null)
			{
				clone.AttackSounds = new List<GraphicAttackSound>();
				AttackSounds.ForEach(a => clone.AttackSounds.Add((GraphicAttackSound)a.Clone()));
			}
			clone.Deltas = new List<GraphicDelta>(Deltas.Count);
			Deltas.ForEach(d => clone.Deltas.Add((GraphicDelta)d.Clone()));

			// Fertig
			return clone;
		}

		#endregion Funktionen

		#region Strukturen

		public class GraphicAttackSound : IGenieDataElement, ICloneable
		{
			#region Variablen

			public short SoundDelay1;
			public short SoundID1;
			public short SoundDelay2;
			public short SoundID2;
			public short SoundDelay3;
			public short SoundID3;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				SoundDelay1 = buffer.ReadShort();
				SoundID1 = buffer.ReadShort();
				SoundDelay2 = buffer.ReadShort();
				SoundID2 = buffer.ReadShort();
				SoundDelay3 = buffer.ReadShort();
				SoundID3 = buffer.ReadShort();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteShort(SoundDelay1);
				buffer.WriteShort(SoundID1);
				buffer.WriteShort(SoundDelay2);
				buffer.WriteShort(SoundID2);
				buffer.WriteShort(SoundDelay3);
				buffer.WriteShort(SoundID3);
			}

			/// <summary>
			/// Gibt eine tiefe Kopie dieses Objekts zurück.
			/// </summary>
			/// <returns></returns>
			public object Clone()
			{
				// Nur Wert-Typen vorhanden
				return this.MemberwiseClone();
			}

			#endregion Funktionen
		}

		public class GraphicDelta : IGenieDataElement, ICloneable
		{
			#region Variablen

			public short GraphicID;
			public short Unknown1;
			public short Unknown2;
			public short Unknown3;
			public short DirectionX;
			public short DirectionY;
			public short Unknown4;
			public short Unknown5;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				GraphicID = buffer.ReadShort();
				Unknown1 = buffer.ReadShort();
				Unknown2 = buffer.ReadShort();
				Unknown3 = buffer.ReadShort();
				DirectionX = buffer.ReadShort();
				DirectionY = buffer.ReadShort();
				Unknown4 = buffer.ReadShort();
				Unknown5 = buffer.ReadShort();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteShort(GraphicID);
				buffer.WriteShort(Unknown1);
				buffer.WriteShort(Unknown2);
				buffer.WriteShort(Unknown3);
				buffer.WriteShort(DirectionX);
				buffer.WriteShort(DirectionY);
				buffer.WriteShort(Unknown4);
				buffer.WriteShort(Unknown5);
			}

			/// <summary>
			/// Gibt eine tiefe Kopie dieses Objekts zurück.
			/// </summary>
			/// <returns></returns>
			public object Clone()
			{
				// Nur Wert-Typen vorhanden
				return this.MemberwiseClone();
			}

			#endregion Funktionen
		}

		#endregion Strukturen
	}
}