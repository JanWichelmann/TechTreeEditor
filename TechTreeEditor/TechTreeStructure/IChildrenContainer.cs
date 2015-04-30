using System;
using System.Collections.Generic;

namespace TechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert ein Interface für Klassen, die Kindelemente mit zugeordneten Button-IDs enthalten.
	/// </summary>
	public interface IChildrenContainer
	{
		/// <summary>
		/// Die in diesem Element enthaltenen Kindelemente.
		/// </summary>
		List<Tuple<byte, TechTreeElement>> Children { get; }
	}
}