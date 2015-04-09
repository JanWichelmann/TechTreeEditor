using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X2AddOnTechTreeEditor.TechTreeStructure
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
