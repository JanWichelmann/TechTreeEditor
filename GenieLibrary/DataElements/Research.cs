using IORAMHelper;
using System.Collections.Generic;
using System;

namespace GenieLibrary.DataElements
{
	public class Research : IGenieDataElement,ICloneable
	{
		#region Variablen

		/// <summary>
		/// Länge: 6.
		/// </summary>
		public List<short> RequiredTechs;

		/// <summary>
		/// Länge: 3.
		/// </summary>
		public List<ResourceTuple<short, short, byte>> ResourceCosts;

		public short RequiredTechCount;
		public short Civ;
		public short FullTechMode;
		public short ResearchLocation;
		public ushort LanguageDLLName1;
		public ushort LanguageDLLDescription;
		public short ResearchTime;
		public short TechageID;
		public short Type;
		public short IconID;
		public byte ButtonID;
		public int LanguageDLLHelp;
		public int LanguageDLLName2;
		public int Unknown1;
		public string Name;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			RequiredTechs = new List<short>(6);
			for(int i = 0; i < 6; ++i)
				RequiredTechs.Add(buffer.ReadShort());

			ResourceCosts = new List<ResourceTuple<short, short, byte>>(3);
			for(int i = 0; i < 3; ++i)
				ResourceCosts.Add(new ResourceTuple<short, short, byte>() { Type = buffer.ReadShort(), Amount = buffer.ReadShort(), Enabled = buffer.ReadByte() });

			RequiredTechCount = buffer.ReadShort();
			Civ = buffer.ReadShort();
			FullTechMode = buffer.ReadShort();
			ResearchLocation = buffer.ReadShort();
			LanguageDLLName1 = buffer.ReadUShort();
			LanguageDLLDescription = buffer.ReadUShort();
			ResearchTime = buffer.ReadShort();
			TechageID = buffer.ReadShort();
			Type = buffer.ReadShort();
			IconID = buffer.ReadShort();
			ButtonID = buffer.ReadByte();
			LanguageDLLHelp = buffer.ReadInteger();
			LanguageDLLName2 = buffer.ReadInteger();
			Unknown1 = buffer.ReadInteger();

			int nameLength = buffer.ReadUShort();
			Name = buffer.ReadString(nameLength);
		}

		public override void WriteData(RAMBuffer buffer)
		{
			AssertListLength(RequiredTechs, 6);
			RequiredTechs.ForEach(e => buffer.WriteShort(e));

			AssertListLength(ResourceCosts, 3);
			ResourceCosts.ForEach(e =>
			{
				buffer.WriteShort(e.Type);
				buffer.WriteShort(e.Amount);
				buffer.WriteByte(e.Enabled);
			});

			buffer.WriteShort(RequiredTechCount);
			buffer.WriteShort(Civ);
			buffer.WriteShort(FullTechMode);
			buffer.WriteShort(ResearchLocation);
			buffer.WriteUShort(LanguageDLLName1);
			buffer.WriteUShort(LanguageDLLDescription);
			buffer.WriteShort(ResearchTime);
			buffer.WriteShort(TechageID);
			buffer.WriteShort(Type);
			buffer.WriteShort(IconID);
			buffer.WriteByte(ButtonID);
			buffer.WriteInteger(LanguageDLLHelp);
			buffer.WriteInteger(LanguageDLLName2);
			buffer.WriteInteger(Unknown1);

			buffer.WriteUShort((ushort)Name.Length);
			buffer.WriteString(Name);
		}

		/// <summary>
		/// Gibt eine tiefe Kopie dieses Objekts zurück.
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			// Erstmal alle Wert-Typen kopieren
			Research clone = (Research)this.MemberwiseClone();

			// Referenztypen kopieren
			clone.RequiredTechs = new List<short>(RequiredTechs);
			clone.ResourceCosts = new List<ResourceTuple<short, short, byte>>(ResourceCosts); // Ressourcen-Kosten sind Strukturen und damit Werttypen

			// Fertig
			return clone;
		}

		#endregion Funktionen
	}
}