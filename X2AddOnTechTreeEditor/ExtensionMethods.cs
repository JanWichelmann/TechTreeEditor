using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;

namespace X2AddOnTechTreeEditor
{
	/// <summary>
	/// Definiert Hilfsmethoden, die an vorhandene Typen per Extension angebunden werden.
	/// </summary>
	internal static class ExtensionMethods
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
	}
}
