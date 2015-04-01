﻿using GenieLibrary;
using GenieLibrary.DataElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X2AddOnTechTreeEditor
{
	/// <summary>
	/// Verwaltet die DAT-Einheiten aller Kulturen.
	/// </summary>
	public class GenieUnitManager
	{
		#region Variablen

		/// <summary>
		/// Die Genie-Datenstruktur mit den Einheitenlisten.
		/// </summary>
		GenieFile _dat;

		/// <summary>
		/// Die Kopier-Informationen der einzelnen Kulturen.
		/// </summary>
		bool[] _copyCivs;

		/// <summary>
		/// Der Index der aktuell ausgewählten Kultur.
		/// </summary>
		int _currCivIndex = 0;

		/// <summary>
		/// Die aktuell ausgewählte Kultur.
		/// </summary>
		Civ _currCiv = null;

		/// <summary>
		/// Der Index der aktuell ausgewählten Einheit.
		/// </summary>
		int _currUnitIndex = -1;

		/// <summary>
		/// Die aktuell ausgewählte Einheit.
		/// </summary>
		Civ.Unit _currUnit = null;

		/// <summary>
		/// Gibt an, ob die Einheiten für Änderungsvorgänge aktuell gesperrt sind.
		/// </summary>
		bool _locked = false;

		#endregion

		#region Funktionen

		/// <summary>
		/// Erstellt einen neuen Manager für die DAT-Einheiten.
		/// </summary>
		/// <param name="dat">Die zu verwaltende Genie-Datenstruktur.</param>
		public GenieUnitManager(GenieFile dat)
		{
			// Datenstruktur speichern
			_dat = dat;

			// Kultur-Kopier-Array erstellen
			_copyCivs = new bool[_dat.Civs.Count];
		}

		/// <summary>
		/// Führt die angegebene Aktualisierungsfunktion für alle markierten Kulturen aus.
		/// </summary>
		/// <param name="updater">Die auszuführende Aktion.</param>
		public void UpdateUnitAttribute(Action<Civ.Unit> updater)
		{
			// Gesperrt?
			if(_locked)
				return;

			// Aktion für alle ausgewählten Kulturen durchführen
			for(int i = 0; i < _copyCivs.Length; ++i)
			{
				// Kultur ausgewählt?
				if(_copyCivs[i])
				{
					// Einheit aktualisieren
					updater(_dat.Civs[i].Units[_currUnitIndex]);
				}
			}

			// Falls die ausgewählte Kultur nicht enthalten ist, auch für diese die Änderung durchführen
			if(!_copyCivs[_currCivIndex])
				updater(_dat.Civs[_currCivIndex].Units[_currUnitIndex]);
		}

		/// <summary>
		/// Sperrt die Einheiten für Bearbeitungsvorgänge.
		/// </summary>
		public void Lock()
		{
			// Sperren
			_locked = true;
		}

		/// <summary>
		/// Gibt die Einheiten für Bearbeitungsvorgänge wieder frei.
		/// </summary>
		public void Unlock()
		{
			// Entsperren
			_locked = false;
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft den Kopier-Wert der Kultur mit der angegebenen ID ab oder legt diesen fest.
		/// </summary>
		/// <param name="civID">Die ID der Kultur, auf deren Kopierwert zugegriffen werden soll.</param>
		/// <returns></returns>
		public bool this[int civID]
		{
			get { return _copyCivs[civID]; }
			set { _copyCivs[civID] = value; }
		}

		/// <summary>
		/// Ruft den Index der aktuell ausgewählte Kultur ab oder legt diesen fest.
		/// </summary>
		public int SelectedCivIndex
		{
			get
			{
				// Index zurückgeben
				return _currCivIndex;
			}
			set
			{
				// Neue Auswahl setzen
				if(_copyCivs.Length > value)
				{
					// Neue Kultur auswählen
					_currCivIndex = value;
					_currCiv = _dat.Civs[value];

					// Einheit erneut aus der richtigen Zivilisation auswählen
					if(_currUnitIndex > -1)
						_currUnit = _currCiv.Units[_currUnitIndex];
				}
			}
		}

		/// <summary>
		/// Ruft den Index der aktuell ausgewählten Einheit ab oder legt diesen fest.
		/// </summary>
		public int SelectedUnitIndex
		{
			get
			{
				// Index zurückgeben
				return _currUnitIndex;
			}
			set
			{
				// Neue Auswahl setzen
				if(_dat.UnitHeaders.Count > value)
				{
					_currUnitIndex = value;
					if(_currCiv != null)
						_currUnit = _currCiv.Units[value];
					else
						_currUnit = null;
				}
				else
				{
					_currUnitIndex = -1;
					_currUnit = null;
				}
			}
		}

		/// <summary>
		/// Ruft die aktuell ausgewählte Einheit ab.
		/// </summary>
		public Civ.Unit SelectedUnit
		{
			get { return _currUnit; }
		}

		#endregion
	}
}
