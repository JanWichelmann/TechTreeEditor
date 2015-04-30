using IORAMHelper;
using System;
using System.Collections.Generic;

namespace GenieLibrary.DataElements.UnitTypes
{
	public class Type50 : IGenieDataElement, ICloneable
	{
		#region Variablen

		public short DefaultArmour;

		/// <summary>
		/// Format: Klasse =&gt; Wert.
		/// </summary>
		public Dictionary<short, short> Attacks;

		/// <summary>
		/// Format: Klasse =&gt; Wert.
		/// </summary>
		public Dictionary<short, short> Armours;

		public short TerrainRestrictionForDamageMultiplying;
		public float MaxRange;
		public float BlastRadius;
		public float ReloadTime;
		public short ProjectileUnitID;
		public short AccuracyPercent;
		public byte TowerMode;
		public short FrameDelay;

		/// <summary>
		/// Länge: 3.
		/// </summary>
		public List<float> GraphicDisplacement;

		public byte BlastLevel;
		public float MinRange;
		public float AccuracyErrorRadius;
		public short AttackGraphic;
		public short DisplayedMeleeArmour;
		public short DisplayedAttack;
		public float DisplayedRange;
		public float DisplayedReloadTime;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			DefaultArmour = buffer.ReadShort();

			ushort attackCount = buffer.ReadUShort();
			Attacks = new Dictionary<short, short>(attackCount);
			for(int i = 0; i < attackCount; ++i)
				Attacks[buffer.ReadShort()] = buffer.ReadShort();

			ushort armourCount = buffer.ReadUShort();
			Armours = new Dictionary<short, short>(armourCount);
			for(int i = 0; i < armourCount; ++i)
				Armours[buffer.ReadShort()] = buffer.ReadShort();

			TerrainRestrictionForDamageMultiplying = buffer.ReadShort();
			MaxRange = buffer.ReadFloat();
			BlastRadius = buffer.ReadFloat();
			ReloadTime = buffer.ReadFloat();
			ProjectileUnitID = buffer.ReadShort();
			AccuracyPercent = buffer.ReadShort();
			TowerMode = buffer.ReadByte();
			FrameDelay = buffer.ReadShort();

			GraphicDisplacement = new List<float>(3);
			for(int i = 0; i < 3; ++i)
				GraphicDisplacement.Add(buffer.ReadFloat());

			BlastLevel = buffer.ReadByte();
			MinRange = buffer.ReadFloat();
			AccuracyErrorRadius = buffer.ReadFloat();
			AttackGraphic = buffer.ReadShort();
			DisplayedMeleeArmour = buffer.ReadShort();
			DisplayedAttack = buffer.ReadShort();
			DisplayedRange = buffer.ReadFloat();
			DisplayedReloadTime = buffer.ReadFloat();
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteShort(DefaultArmour);

			buffer.WriteUShort((ushort)Attacks.Count);
			foreach(KeyValuePair<short, short> currA in Attacks)
			{
				buffer.WriteShort(currA.Key);
				buffer.WriteShort(currA.Value);
			}

			buffer.WriteUShort((ushort)Armours.Count);
			foreach(KeyValuePair<short, short> currA in Armours)
			{
				buffer.WriteShort(currA.Key);
				buffer.WriteShort(currA.Value);
			}

			buffer.WriteShort(TerrainRestrictionForDamageMultiplying);
			buffer.WriteFloat(MaxRange);
			buffer.WriteFloat(BlastRadius);
			buffer.WriteFloat(ReloadTime);
			buffer.WriteShort(ProjectileUnitID);
			buffer.WriteShort(AccuracyPercent);
			buffer.WriteByte(TowerMode);
			buffer.WriteShort(FrameDelay);

			AssertListLength(GraphicDisplacement, 3);
			GraphicDisplacement.ForEach(e => buffer.WriteFloat(e));

			buffer.WriteByte(BlastLevel);
			buffer.WriteFloat(MinRange);
			buffer.WriteFloat(AccuracyErrorRadius);
			buffer.WriteShort(AttackGraphic);
			buffer.WriteShort(DisplayedMeleeArmour);
			buffer.WriteShort(DisplayedAttack);
			buffer.WriteFloat(DisplayedRange);
			buffer.WriteFloat(DisplayedReloadTime);
		}

		/// <summary>
		/// Gibt eine tiefe Kopie dieses Objekts zurück.
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			// Erstmal alle Wert-Typen kopieren
			Type50 clone = (Type50)this.MemberwiseClone();

			// Referenztypen kopieren
			clone.Attacks = new Dictionary<short, short>(Attacks);
			clone.Armours = new Dictionary<short, short>(Armours);
			clone.GraphicDisplacement = new List<float>(GraphicDisplacement);

			// Fertig
			return clone;
		}

		#endregion Funktionen
	}
}