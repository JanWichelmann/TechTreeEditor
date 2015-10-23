using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace TechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert eine Einheiten-Fähigkeit.
	/// </summary>
	public class UnitAbility
	{
		#region Variablen

		/// <summary>
		/// Der Typ der Fähigkeit.
		/// </summary>
		public AbilityType Type { get; set; }

		/// <summary>
		/// Die betroffene Einheit.
		/// </summary>
		public TechTreeUnit Unit { get; set; }

		/// <summary>
		/// Die zugrundeliegenden anderweitigen Fähigkeitendaten (keine Baumreferenzen, daher als Werte/IDs gespeichert).
		/// </summary>
		public GenieLibrary.DataElements.UnitHeader.UnitCommand CommandData;

		/// <summary>
		/// Die gecachten Typen-Bezeichnungen.
		/// </summary>
		private static Dictionary<AbilityType, string> _abilityTypeNames = null;

		/// <summary>
		/// Die gecachten Einheiten-Klassen.
		/// </summary>
		private static string[] _classes = null;

		/// <summary>
		/// Die gecachten Ressourcen-Typen.
		/// </summary>
		private static string[] _resourceTypes = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Erstellt eine neue Fähigkeit.
		/// </summary>
		/// <param name="commandData">Referenz auf die zugrundeliegende Einheitenfähigkeiten-Struktur. Geänderte Werte werden direkt in dieser gespeichert.</param>
		public UnitAbility(GenieLibrary.DataElements.UnitHeader.UnitCommand commandData)
		{
			// Fähigkeiten-Struktur speichern
			CommandData = commandData;
		}

		/// <summary>
		/// Erstellt eine neue Fähigkeit aus dem angegebenen XElement.
		/// </summary>
		/// <param name="element">Das XElement, aus dem das Element erstellt werden soll.</param>
		/// <param name="elementIDs">Die IDs der Baumelemente.</param>
		/// <param name="commandData">Referenz auf die zugrundeliegende Einheitenfähigkeiten-Struktur. Geänderte Werte werden direkt in dieser gespeichert.</param>
		public UnitAbility(XElement element, Dictionary<int, TechTreeElement> elementIDs, GenieLibrary.DataElements.UnitHeader.UnitCommand commandData)
		{
			// Fähigkeiten-Struktur speichern
			CommandData = commandData;

			// Lese-Funktion aufrufen
			FromXml(element, elementIDs);
		}

		/// <summary>
		/// Konvertiert die enthaltenen Daten in ein XML-Format und schreibt sie in den übergebenen XmlWriter.
		/// Die ID der evtl. referenzierten Einheit muss bereits vorliegen!
		/// </summary>
		/// <param name="writer">Der XmlWriter, in den die Daten geschrieben werden sollen.</param>
		/// <param name="elementIDs">Die Liste mit den den TechTree-Elementen zugeordneten IDs.</param>
		public void ToXml(XmlWriter writer, Dictionary<TechTreeElement, int> elementIDs)
		{
			// Einheit-ID bestimmen
			int unitID = -1;
			if(Unit != null)
				unitID = elementIDs[Unit];

			// Element-Anfangstag schreiben
			writer.WriteStartElement("ability");
			{
				// Elementtyp schreiben
				writer.WriteElementNumber("type", (byte)Type);

				// Einheit-ID schreiben
				writer.WriteElementNumber("unit", unitID);
			}
			writer.WriteEndElement();
		}

		/// <summary>
		/// Liest das aktuelle Element aus dem angegebenen XElement.
		/// </summary>
		/// <param name="element">Das XElement, aus dem das Element erstellt werden soll.</param>
		/// <param name="elementIDs">Die IDs der Baumelemente.</param>
		public void FromXml(XElement element, Dictionary<int, TechTreeElement> elementIDs)
		{
			// Typ lesen
			Type = (AbilityType)(uint)element.Element("type"); // Ein byte-Cast geht hier scheinbar nicht

			// Einheit lesen
			int id = (int)element.Element("unit");
			if(id >= 0)
				Unit = (TechTreeUnit)elementIDs[id];
		}

		/// <summary>
		/// Gibt eine String-Repräsentation dieses Objekts zurück.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			// Ggf. Nachschlag-Listen erstellen
			if(_abilityTypeNames == null)
			{
				// Liste anlegen
				_abilityTypeNames = new Dictionary<AbilityType, string>();
				string[] abilityList = Strings.AbilityTypes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				foreach(string abilityEntry in abilityList)
				{
					// Eintrag splitten und in Liste einfügen
					string[] entrySplit = abilityEntry.Split('|');
					_abilityTypeNames.Add((AbilityType)uint.Parse(entrySplit[0]), entrySplit[1]);
				}
			}
			if(_classes == null)
				_classes = Strings.ClassNames.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			if(_resourceTypes == null)
				_resourceTypes = Strings.ResourceTypes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			// Ungültige Werte abfangen
			try
			{
				// Fähigkeitsstring erstellen
				string result = _abilityTypeNames[Type];
				if(Unit != null)
					result += ": " + Unit.Name;
				else if(CommandData.ClassID >= 0)
					result += ": "+string.Format(Strings.UnitAbility_ToString_Class, _classes[CommandData.ClassID]);
				else if(CommandData.Resource >= 0)
					result += ": " + string.Format(Strings.UnitAbility_ToString_Resource, _resourceTypes[CommandData.Resource]);
				return result;
			}
			catch(IndexOutOfRangeException)
			{
				return "?";
			}
			catch(KeyNotFoundException)
			{
				return "?";
			}
		}

		/// <summary>
		/// Gibt eine Kopie dieser Fähigkeit zurück.
		/// Die Einheitenfähigkeiten-Struktur aus der DAT-Datei muss anschließend extern in dieser gespeichert werden! Eine Kopie wird erstellt.
		/// </summary>
		/// <returns></returns>
		public UnitAbility Clone()
		{
			// Referenzen kopieren
			UnitAbility clone = (UnitAbility)this.MemberwiseClone();

			// Einheitenfähigkeiten-Struktur kopieren
			clone.CommandData = (GenieLibrary.DataElements.UnitHeader.UnitCommand)CommandData.Clone();

			// Fertig
			return clone;
		}

		#endregion Funktionen

		#region Enumerationen

		/// <summary>
		/// Definiert die verschiedenen möglichen Fähigkeiten-Typen.
		/// </summary>
		public enum AbilityType : byte
		{
			Undefined = 0,
			MoveTo = 1,
			Follow = 2,
			Garrison = 3,
			Explore = 4,
			GatherRebuild = 5,
			NaturalWondersCheat = 6,
			Attack = 7,
			Shoot = 8,
			Fly = 10,
			ScareHunt = 11,
			UnloadBoatLike = 12,
			Guard = 13,
			Escape = 20,
			Make = 21,
			Build = 101,
			MakeObject = 102,
			MakeTech = 103,
			Convert = 104,
			Heal = 105,
			Repair = 106,
			GetAutoConverted = 107,
			Discovery = 108,
			RetreatToShootingRange = 109,
			Hunt = 110,
			Trade = 111,
			GenerateWonderVictory = 120,
			DeselectWhenTasked = 121,
			Loot = 122,
			Housing = 123,
			Pack = 124,
			UnpackAttck = 125,
			OffMapTrade1 = 130,
			OffMapTrade2 = 131,
			PickUpUnit = 132,
			UnknownPickUpAbility1 = 133,
			UnknownPickUpAbility2 = 134,
			KidnapUnit = 135,
			DepositUnit = 136
		}

		#endregion Enumerationen
	}
}