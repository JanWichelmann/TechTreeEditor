using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace TechTreeEditor
{
	/// <summary>
	/// Definiert Hilfsmethoden, die an vorhandene Typen per Extension angebunden werden.
	/// </summary>
	public static class ExtensionMethods
	{
		/// <summary>
		/// Schreibt ein Element mit dem angegebenen lokalen Namen und Zahlenwert. Der Wert wird in das XML-übliche Format konvertiert.
		/// </summary>
		/// <param name="writer">Der XmlWriter.</param>
		/// <param name="localName">Der lokale Name des Elements.</param>
		/// <param name="value">Der Wert des Elements.</param>
		public static void WriteElementNumber(this XmlWriter writer, string localName, float value)
		{
			// Element schreiben
			writer.WriteElementString(localName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
		}

		/// <summary>
		/// Schreibt ein Element mit dem angegebenen lokalen Namen und Zahlenwert. Der Wert wird in das XML-übliche Format konvertiert.
		/// </summary>
		/// <param name="writer">Der XmlWriter.</param>
		/// <param name="localName">Der lokale Name des Elements.</param>
		/// <param name="value">Der Wert des Elements.</param>
		public static void WriteElementNumber(this XmlWriter writer, string localName, int value)
		{
			// Element schreiben
			writer.WriteElementString(localName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
		}

		/// <summary>
		/// Schreibt ein Element mit dem angegebenen lokalen Namen und Zahlenwert. Der Wert wird in das XML-übliche Format konvertiert.
		/// </summary>
		/// <param name="writer">Der XmlWriter.</param>
		/// <param name="localName">Der lokale Name des Elements.</param>
		/// <param name="value">Der Wert des Elements.</param>
		public static void WriteElementNumber(this XmlWriter writer, string localName, bool value)
		{
			// Element schreiben
			writer.WriteElementString(localName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
		}

		/// <summary>
		/// Schreibt das Attribut mit dem angegebenen lokalen Namen und Zahlenwert. Der Wert wird in das XML-übliche Format konvertiert.
		/// </summary>
		/// <param name="writer">Der XmlWriter.</param>
		/// <param name="localName">Der lokale Name des Attributs.</param>
		/// <param name="value">Der Wert des Attributs.</param>
		public static void WriteAttributeNumber(this XmlWriter writer, string localName, float value)
		{
			// Element schreiben
			writer.WriteAttributeString(localName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
		}

		/// <summary>
		/// Schreibt das Attribut mit dem angegebenen lokalen Namen und Zahlenwert. Der Wert wird in das XML-übliche Format konvertiert.
		/// </summary>
		/// <param name="writer">Der XmlWriter.</param>
		/// <param name="localName">Der lokale Name des Attributs.</param>
		/// <param name="value">Der Wert des Attributs.</param>
		public static void WriteAttributeNumber(this XmlWriter writer, string localName, int value)
		{
			// Element schreiben
			writer.WriteAttributeString(localName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
		}

		/// <summary>
		/// Schreibt das Attribut mit dem angegebenen lokalen Namen und Zahlenwert. Der Wert wird in das XML-übliche Format konvertiert.
		/// </summary>
		/// <param name="writer">Der XmlWriter.</param>
		/// <param name="localName">Der lokale Name des Attributs.</param>
		/// <param name="value">Der Wert des Attributs.</param>
		public static void WriteAttributeNumber(this XmlWriter writer, string localName, bool value)
		{
			// Element schreiben
			writer.WriteAttributeString(localName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
		}

		/// <summary>
		/// Schreibt den angegebenen Zahlenwert. Der Wert wird in das XML-übliche Format konvertiert.
		/// </summary>
		/// <param name="writer">Der XmlWriter.</param>
		/// <param name="value">Der zu schreibende Wert.</param>
		public static void WriteNumber(this XmlWriter writer, float value)
		{
			// Element schreiben
			writer.WriteString(value.ToString(CultureInfo.InvariantCulture.NumberFormat));
		}

		/// <summary>
		/// Schreibt den angegebenen Zahlenwert. Der Wert wird in das XML-übliche Format konvertiert.
		/// </summary>
		/// <param name="writer">Der XmlWriter.</param>
		/// <param name="value">Der schreibende Wert.</param>
		public static void WriteNumber(this XmlWriter writer, int value)
		{
			// Element schreiben
			writer.WriteString(value.ToString(CultureInfo.InvariantCulture.NumberFormat));
		}

		/// <summary>
		/// Prüft, ob die angegebene Zeichenfolge in diesem String vorkommt.
		/// </summary>
		/// <param name="str">Der String.</param>
		/// <param name="value">Die zu suchende Zeichenfolge.</param>
		/// <param name="comparison">Der String-Vergleichs-Modus.</param>
		/// <returns></returns>
		public static bool Contains(this string str, string value, StringComparison comparison)
		{
			// Vorkommen prüfen
			return str.IndexOf(value, comparison) >= 0;
		}

		/// <summary>
		/// Versucht, den übergebenen Wert sicher in den Zielwert zu konvertieren und gibt diesen dann zurück.
		/// </summary>
		/// <typeparam name="T">Der Zieltyp.</typeparam>
		/// <param name="val">Der zu konvertierende Wert.</param>
		/// <returns></returns>
		public static T SafeConvert<T>(this decimal val) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
		{
			// Casten und zurückgeben
			try
			{
				return (T)Convert.ChangeType(val, typeof(T), CultureInfo.InvariantCulture);
			}
			catch(OverflowException) { }
			catch(InvalidCastException) { }

			// Leeren Wert zurückgeben
			return default(T);
		}

		/// <summary>
		/// Verschiebt das Element am angegebenen Index in der Liste an deren Ende.
		/// </summary>
		/// <typeparam name="T">Der Typ der Listenelemente.</typeparam>
		/// <param name="list">Die Liste.</param>
		/// <param name="index">Der Index des nach hinten zu verschiebenden Elements.</param>
		public static void MoveToEnd<T>(this List<T> list, int index)
		{
			// Element entfernen und hinten wieder einfügen
			T elem = list[index];
			list.RemoveAt(index);
			list.Add(elem);
		}

		/// <summary>
		/// Verschiebt das Element am angegebenen Index in der Liste um das gegebene Offset. Das verdrängte (nächste) Element wird zurückgegeben.
		/// </summary>
		/// <typeparam name="T">Der Typ der Listenelemente.</typeparam>
		/// <param name="list">Die Liste.</param>
		/// <param name="index">Der Index des zu verschiebenden Elements.</param>
		/// <param name="offset">Die Anzahl der Positionen, um die das Element verschoben werden soll. Zu große Werte werden entsprechend korrigiert.</param>
		public static T Move<T>(this List<T> list, int index, int offset)
		{
			// Überhaupt was zu tun?
			if(offset == 0 || list.Count <= 1)
				return default(T);

			// Wertebereich einstellen
			int newIndex = index + offset;
			if(newIndex < 0)
				newIndex = 0;
			if(newIndex >= list.Count)
				newIndex = list.Count - 1;

			// Altes Element holen
			T prevElement = default(T);
			prevElement = list[newIndex];

			// Element verschieben
			T elem = list[index];
			list.RemoveAt(index);
			list.Insert(newIndex, elem);

			// Vorher dort stehendes Element zurückgeben
			return prevElement;
		}
	}
}