using IORAMHelper;
using System;

namespace GenieLibrary
{
	/// <summary>
	/// Definiert Eigenschaften eines DAT-Datei-Datenelements. Enthält u.a. Unterstützung für selbstständiges Lesen und Schreiben.
	/// </summary>
	public abstract class IGenieDataElement
	{
		#region Hilfsfunktionen

		/// <summary>
		/// Stellt sicher, dass die übergebene Liste die angegebene Länge hat, indem im Fehlerfall eine Exception ausgelöst wird.
		/// </summary>
		/// <param name="list">Die zu überprüfende Liste.</param>
		/// <param name="length">Die Soll-Länge der Liste.</param>
		protected void AssertListLength(System.Collections.ICollection list, int length)
		{
			// Länge abgleichen
			if(list.Count != length)
			{
				// Fehler auslösen
				throw new AssertionException("Die Listenlänge (" + list.Count + ") weicht von der Soll-Länge (" + length + ") ab.");
			}
		}

		/// <summary>
		/// Stellt sicher, dass der übergebene Wert true ist, sonst wird eine Exception ausgelöst.
		/// </summary>
		/// <param name="value">Der zu überprüfende Wahrheitswert.</param>
		protected void AssertTrue(bool value)
		{
			// Länge abgleichen
			if(!value)
			{
				// Fehler auslösen
				throw new AssertionException("Der Ausdruck wurde unerwartet zu false ausgewertet.");
			}
		}

		#endregion Hilfsfunktionen

		#region Abstrakte Funktionen

		/// <summary>
		/// Liest Daten ab der gegebenen Position im Puffer.
		/// </summary>
		/// <param name="buffer">Der Puffer, aus dem die Daten gelesen werden sollen.</param>
		public abstract void ReadData(RAMBuffer buffer);

		/// <summary>
		/// Schreibt die enthaltenen Daten an die gegebene Position im Puffer.
		/// </summary>
		/// <param name="buffer">Der Puffer, in den die Daten geschrieben werden sollen.</param>
		public abstract void WriteData(RAMBuffer buffer);

		#endregion Abstrakte Funktionen

		#region Hilfsfunktionen für übersichtlicheren Aufrufer-Code

		/// <summary>
		/// Liest Daten ab der gegebenen Position im Puffer und gibt dieses Objekt dann zurück.
		///
		/// TODO: Sehr hässlich...siehe http://stackoverflow.com/questions/7798256/can-a-base-class-method-return-this-even-in-a-derived-class
		/// </summary>
		/// <param name="buffer">Der Puffer, aus dem die Daten gelesen werden sollen.</param>
		/// <returns></returns>
		public dynamic ReadDataInline(RAMBuffer buffer)
		{
			// Daten lesen
			ReadData(buffer);

			// Objekt zurückgeben
			return this;
		}

		#endregion Hilfsfunktionen für übersichtlicheren Aufrufer-Code

		#region Strukturen

		/// <summary>
		/// Definiert das übliche Ressourcen-Tupel (Typ, Menge, Aktiviert) mit frei wählbaren Datentypen.
		/// </summary>
		/// <typeparam name="T">Der Datentyp des Ressourcen-Typs.</typeparam>
		/// <typeparam name="A">Der Datentyp der Ressourcen-Menge.</typeparam>
		/// <typeparam name="E">Der Datentyp des Ressourcen-Aktivierungs-Flags.</typeparam>
		public struct ResourceTuple<T, A, E>
		{
			public T Type;
			public A Amount;
			public E Enabled;
		}

		#endregion Strukturen

		#region Exceptions

		/// <summary>
		/// Definiert die bei einer fehlerhaften Überprüfung ausgelöste Exception.
		/// </summary>
		public class AssertionException : Exception
		{
			#region Funktionen

			/// <summary>
			/// Löst eine neue Assert-Exception aus.
			/// </summary>
			public AssertionException()
				: base("Assertion fehlgeschlagen.")
			{ }

			/// <summary>
			/// Löst eine neue Assert-Exception mit der angegebenen Fehlermeldung aus.
			/// </summary>
			/// <param name="message">Die zugehörige Fehlermeldung.</param>
			public AssertionException(string message)
				: base(message)
			{ }

			#endregion Funktionen
		}

		#endregion Exceptions
	}
}