using IORAMHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenieLibrary.DataElements
{
	public class Civ : IGenieDataElement
	{
		#region Variablen

		public byte One;

		/// <summary>
		/// Länge: 20.
		/// </summary>
		public string Name;

		public short TechTreeID;
		public short TeamBonusID;
		public List<float> Resources;
		public byte IconSet;
		public List<int> UnitPointers;
		public SortedList<int, Unit> Units;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			One = buffer.ReadByte();
			Name = buffer.ReadString(20);
			int resourceCount = buffer.ReadUShort();
			TechTreeID = buffer.ReadShort();
			TeamBonusID = buffer.ReadShort();

			Resources = new List<float>(resourceCount);
			for(int i = 0; i < resourceCount; ++i)
				Resources.Add(buffer.ReadFloat());

			IconSet = buffer.ReadByte();

			int unitCount = buffer.ReadUShort();
			UnitPointers = new List<int>(unitCount);
			for(int i = 0; i < unitCount; ++i)
				UnitPointers.Add(buffer.ReadInteger());

			Units = new SortedList<int, Unit>(unitCount);
			for(int p = 0; p < UnitPointers.Count; ++p)
				if(UnitPointers[p] != 0)
					Units.Add(p, new Unit().ReadDataInline(buffer));
		}

		public override void WriteData(RAMBuffer buffer)
		{
			buffer.WriteByte(One);
			buffer.WriteString(Name, 20);
			buffer.WriteUShort((ushort)Resources.Count);
			buffer.WriteShort(TechTreeID);
			buffer.WriteShort(TeamBonusID);
			Resources.ForEach(e => buffer.WriteFloat(e));
			buffer.WriteByte(IconSet);
			buffer.WriteUShort((ushort)UnitPointers.Count);
			UnitPointers.ForEach(e => buffer.WriteInteger(e));

			// Sicherstellen, dass genau jede definierte Einheit einen entsprechenden Pointer hat; hier nur über die Listenlänge, sollte aber die meisten auftretenden Fehler abdecken
			AssertListLength(Units, UnitPointers.Count(p => p != 0));
			foreach(var u in Units)
				u.Value.WriteData(buffer);
		}

		#endregion Funktionen

		#region Strukturen

		public class Unit : IGenieDataElement, ICloneable
		{
			#region Variablen

			public UnitType Type;
			public ushort NameLength1;
			public short ID1;
			public ushort LanguageDLLName;
			public ushort LanguageDLLCreation;
			public short Class;
			public short StandingGraphic1;
			public short StandingGraphic2;
			public short DyingGraphic1;
			public short DyingGraphic2;
			public byte DeathMode;
			public short HitPoints;
			public float LineOfSight;
			public byte GarrisonCapacity;
			public float SizeRadius1;
			public float SizeRadius2;
			public float HPBarHeight1;
			public short TrainSound1;
			public short TrainSound2;
			public short DeadUnitID;
			public byte PlacementMode;
			public byte AirMode;
			public short IconID;
			public byte HideInEditor;
			public short Unknown1;
			public byte Enabled;
			public byte Disabled;
			public short PlacementBypassTerrain1;
			public short PlacementBypassTerrain2;
			public short PlacementTerrain1;
			public short PlacementTerrain2;
			public float EditorRadius1;
			public float EditorRadius2;
			public byte HillMode;
			public byte VisibleInFog;
			public short TerrainRestriction;
			public byte FlyMode;
			public short ResourceCapacity;
			public float ResourceDecay;
			public byte BlastType;
			public byte Unknown2;
			public byte InteractionMode;
			public byte MinimapMode;
			public byte CommandAttribute;
			public float Unknown3A;
			public byte MinimapColor;
			public int LanguageDLLHelp;
			public int LanguageDLLHotKeyText;
			public int HotKey;
			public byte Unselectable;
			public byte Unknown6;
			public byte UnknownSelectionMode;
			public byte Unknown8;
			public byte SelectionMask;
			public byte SelectionShapeType;
			public byte SelectionShape;
			public byte Attribute;
			public byte Civilization;
			public short Nothing;
			public byte SelectionEffect;
			public byte EditorSelectionColor;
			public float SelectionRadius1;
			public float SelectionRadius2;
			public float HPBarHeight2;

			/// <summary>
			/// Länge: 3.
			/// </summary>
			public List<ResourceTuple<short, float, byte>> ResourceStorages;

			public List<DamageGraphic> DamageGraphics;
			public short SelectionSound;
			public short DyingSound;
			public byte AttackMode;
			public byte EdibleMeat;
			public string Name1;
			public short ID2;
			public short ID3;
			public float Speed;

			public UnitTypes.DeadFish DeadFish;
			public UnitTypes.Bird Bird;
			public UnitTypes.Type50 Type50;
			public UnitTypes.Projectile Projectile;
			public UnitTypes.Creatable Creatable;
			public UnitTypes.Building Building;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				Type = (UnitType)buffer.ReadByte();
				NameLength1 = buffer.ReadUShort();
				ID1 = buffer.ReadShort();
				LanguageDLLName = buffer.ReadUShort();
				LanguageDLLCreation = buffer.ReadUShort();
				Class = buffer.ReadShort();
				StandingGraphic1 = buffer.ReadShort();
				StandingGraphic2 = buffer.ReadShort();
				DyingGraphic1 = buffer.ReadShort();
				DyingGraphic2 = buffer.ReadShort();
				DeathMode = buffer.ReadByte();
				HitPoints = buffer.ReadShort();
				LineOfSight = buffer.ReadFloat();
				GarrisonCapacity = buffer.ReadByte();
				SizeRadius1 = buffer.ReadFloat();
				SizeRadius2 = buffer.ReadFloat();
				HPBarHeight1 = buffer.ReadFloat();
				TrainSound1 = buffer.ReadShort();
				TrainSound2 = buffer.ReadShort();
				DeadUnitID = buffer.ReadShort();
				PlacementMode = buffer.ReadByte();
				AirMode = buffer.ReadByte();
				IconID = buffer.ReadShort();
				HideInEditor = buffer.ReadByte();
				Unknown1 = buffer.ReadShort();
				Enabled = buffer.ReadByte();
				Disabled = buffer.ReadByte();
				PlacementBypassTerrain1 = buffer.ReadShort();
				PlacementBypassTerrain2 = buffer.ReadShort();
				PlacementTerrain1 = buffer.ReadShort();
				PlacementTerrain2 = buffer.ReadShort();
				EditorRadius1 = buffer.ReadFloat();
				EditorRadius2 = buffer.ReadFloat();
				HillMode = buffer.ReadByte();
				VisibleInFog = buffer.ReadByte();
				TerrainRestriction = buffer.ReadShort();
				FlyMode = buffer.ReadByte();
				ResourceCapacity = buffer.ReadShort();
				ResourceDecay = buffer.ReadFloat();
				BlastType = buffer.ReadByte();
				Unknown2 = buffer.ReadByte();
				InteractionMode = buffer.ReadByte();
				MinimapMode = buffer.ReadByte();
				CommandAttribute = buffer.ReadByte();
				Unknown3A = buffer.ReadFloat();
				MinimapColor = buffer.ReadByte();
				LanguageDLLHelp = buffer.ReadInteger();
				LanguageDLLHotKeyText = buffer.ReadInteger();
				HotKey = buffer.ReadInteger();
				Unselectable = buffer.ReadByte();
				Unknown6 = buffer.ReadByte();
				UnknownSelectionMode = buffer.ReadByte();
				Unknown8 = buffer.ReadByte();
				SelectionMask = buffer.ReadByte();
				SelectionShapeType = buffer.ReadByte();
				SelectionShape = buffer.ReadByte();
				Attribute = buffer.ReadByte();
				Civilization = buffer.ReadByte();
				Nothing = buffer.ReadShort();
				SelectionEffect = buffer.ReadByte();
				EditorSelectionColor = buffer.ReadByte();
				SelectionRadius1 = buffer.ReadFloat();
				SelectionRadius2 = buffer.ReadFloat();
				HPBarHeight2 = buffer.ReadFloat();

				ResourceStorages = new List<ResourceTuple<short, float, byte>>(3);
				for(int i = 0; i < 3; ++i)
					ResourceStorages.Add(new ResourceTuple<short, float, byte>() { Type = buffer.ReadShort(), Amount = buffer.ReadFloat(), Enabled = buffer.ReadByte() });

				int damageGraphicCount = buffer.ReadByte();
				DamageGraphics = new List<DamageGraphic>();
				for(int i = 0; i < damageGraphicCount; ++i)
					DamageGraphics.Add(new DamageGraphic().ReadDataInline(buffer));

				SelectionSound = buffer.ReadShort();
				DyingSound = buffer.ReadShort();
				AttackMode = buffer.ReadByte();
				EdibleMeat = buffer.ReadByte();
				Name1 = buffer.ReadString(NameLength1);
				ID2 = buffer.ReadShort();
				ID3 = buffer.ReadShort();

				if(Type >= UnitType.Flag)
					Speed = buffer.ReadFloat();
				if(Type >= UnitType.DeadFish)
					DeadFish = new UnitTypes.DeadFish().ReadDataInline(buffer);
				if(Type >= UnitType.Bird)
					Bird = new UnitTypes.Bird().ReadDataInline(buffer);
				if(Type >= UnitType.Type50)
					Type50 = new UnitTypes.Type50().ReadDataInline(buffer);
				if(Type == UnitType.Projectile)
					Projectile = new UnitTypes.Projectile().ReadDataInline(buffer);
				if(Type >= UnitType.Creatable)
					Creatable = new UnitTypes.Creatable().ReadDataInline(buffer);
				if(Type == UnitType.Building)
					Building = new UnitTypes.Building().ReadDataInline(buffer);
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteByte((byte)Type);
				buffer.WriteUShort((ushort)Name1.Length);
				buffer.WriteShort(ID1);
				buffer.WriteUShort(LanguageDLLName);
				buffer.WriteUShort(LanguageDLLCreation);
				buffer.WriteShort(Class);
				buffer.WriteShort(StandingGraphic1);
				buffer.WriteShort(StandingGraphic2);
				buffer.WriteShort(DyingGraphic1);
				buffer.WriteShort(DyingGraphic2);
				buffer.WriteByte(DeathMode);
				buffer.WriteShort(HitPoints);
				buffer.WriteFloat(LineOfSight);
				buffer.WriteByte(GarrisonCapacity);
				buffer.WriteFloat(SizeRadius1);
				buffer.WriteFloat(SizeRadius2);
				buffer.WriteFloat(HPBarHeight1);
				buffer.WriteShort(TrainSound1);
				buffer.WriteShort(TrainSound2);
				buffer.WriteShort(DeadUnitID);
				buffer.WriteByte(PlacementMode);
				buffer.WriteByte(AirMode);
				buffer.WriteShort(IconID);
				buffer.WriteByte(HideInEditor);
				buffer.WriteShort(Unknown1);
				buffer.WriteByte(Enabled);
				buffer.WriteByte(Disabled);
				buffer.WriteShort(PlacementBypassTerrain1);
				buffer.WriteShort(PlacementBypassTerrain2);
				buffer.WriteShort(PlacementTerrain1);
				buffer.WriteShort(PlacementTerrain2);
				buffer.WriteFloat(EditorRadius1);
				buffer.WriteFloat(EditorRadius2);
				buffer.WriteByte(HillMode);
				buffer.WriteByte(VisibleInFog);
				buffer.WriteShort(TerrainRestriction);
				buffer.WriteByte(FlyMode);
				buffer.WriteShort(ResourceCapacity);
				buffer.WriteFloat(ResourceDecay);
				buffer.WriteByte(BlastType);
				buffer.WriteByte(Unknown2);
				buffer.WriteByte(InteractionMode);
				buffer.WriteByte(MinimapMode);
				buffer.WriteByte(CommandAttribute);
				buffer.WriteFloat(Unknown3A);
				buffer.WriteByte(MinimapColor);
				buffer.WriteInteger(LanguageDLLHelp);
				buffer.WriteInteger(LanguageDLLHotKeyText);
				buffer.WriteInteger(HotKey);
				buffer.WriteByte(Unselectable);
				buffer.WriteByte(Unknown6);
				buffer.WriteByte(UnknownSelectionMode);
				buffer.WriteByte(Unknown8);
				buffer.WriteByte(SelectionMask);
				buffer.WriteByte(SelectionShapeType);
				buffer.WriteByte(SelectionShape);
				buffer.WriteByte(Attribute);
				buffer.WriteByte(Civilization);
				buffer.WriteShort(Nothing);
				buffer.WriteByte(SelectionEffect);
				buffer.WriteByte(EditorSelectionColor);
				buffer.WriteFloat(SelectionRadius1);
				buffer.WriteFloat(SelectionRadius2);
				buffer.WriteFloat(HPBarHeight2);

				AssertListLength(ResourceStorages, 3);
				ResourceStorages.ForEach(e =>
				{
					buffer.WriteShort(e.Type);
					buffer.WriteFloat(e.Amount);
					buffer.WriteByte(e.Enabled);
				});

				buffer.WriteByte((byte)DamageGraphics.Count);
				DamageGraphics.ForEach(e => e.WriteData(buffer));

				buffer.WriteShort(SelectionSound);
				buffer.WriteShort(DyingSound);
				buffer.WriteByte(AttackMode);
				buffer.WriteByte(EdibleMeat);
				buffer.WriteString(Name1);
				buffer.WriteShort(ID2);
				buffer.WriteShort(ID3);

				if(Type >= UnitType.Flag)
					buffer.WriteFloat(Speed);
				if(Type >= UnitType.DeadFish)
					DeadFish.WriteData(buffer);
				if(Type >= UnitType.Bird)
					Bird.WriteData(buffer);
				if(Type >= UnitType.Type50)
					Type50.WriteData(buffer);
				if(Type == UnitType.Projectile)
					Projectile.WriteData(buffer);
				if(Type >= UnitType.Creatable)
					Creatable.WriteData(buffer);
				if(Type == UnitType.Building)
					Building.WriteData(buffer);
			}

			/// <summary>
			/// Gibt eine tiefe Kopie dieses Objekts zurück.
			/// </summary>
			/// <returns></returns>
			public object Clone()
			{
				// Erstmal alle Wert-Typen kopieren
				Unit clone = (Unit)this.MemberwiseClone();

				// Referenztypen kopieren
				clone.ResourceStorages = new List<ResourceTuple<short, float, byte>>(ResourceStorages);
				clone.DamageGraphics = new List<DamageGraphic>(DamageGraphics.Count);
				DamageGraphics.ForEach(d => clone.DamageGraphics.Add((DamageGraphic)d.Clone()));
				if(DeadFish != null)
					clone.DeadFish = (UnitTypes.DeadFish)DeadFish.Clone();
				if(Bird != null)
					clone.Bird = (UnitTypes.Bird)Bird.Clone();
				if(Type50 != null)
					clone.Type50 = (UnitTypes.Type50)Type50.Clone();
				if(Projectile != null)
					clone.Projectile = (UnitTypes.Projectile)Projectile.Clone();
				if(Creatable != null)
					clone.Creatable = (UnitTypes.Creatable)Creatable.Clone();
				if(Building != null)
					clone.Building = (UnitTypes.Building)Building.Clone();

				// Fertig
				return clone;
			}

			#endregion Funktionen

			#region Strukturen

			public class DamageGraphic : IGenieDataElement, ICloneable
			{
				#region Variablen

				public short GraphicID;
				public byte DamagePercent;
				public byte ApplyMode;
				public byte Unknown2;

				#endregion Variablen

				#region Funktionen

				public override void ReadData(RAMBuffer buffer)
				{
					GraphicID = buffer.ReadShort();
					DamagePercent = buffer.ReadByte();
					ApplyMode = buffer.ReadByte();
					Unknown2 = buffer.ReadByte();
				}

				public override void WriteData(RAMBuffer buffer)
				{
					buffer.WriteShort(GraphicID);
					buffer.WriteByte(DamagePercent);
					buffer.WriteByte(ApplyMode);
					buffer.WriteByte(Unknown2);
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

			#endregion Strukturen

			#region Enumerationen

			public enum UnitType : byte
			{
				EyeCandy = 10,
				Tree = 15,
				Flag = 20,
				Type25 = 25,
				DeadFish = 30,
				Bird = 40,
				Type50 = 50,
				Projectile = 60,
				Creatable = 70,
				Building = 80
			}

			#endregion Enumerationen
		}

		#endregion Strukturen
	}
}