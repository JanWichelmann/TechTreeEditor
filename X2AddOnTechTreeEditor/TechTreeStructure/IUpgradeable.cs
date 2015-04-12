using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X2AddOnTechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert ein Interface für Klassen, die ein Weiterentwicklungs-Element enthalten.
	/// </summary>
	public interface IUpgradeable
	{
		/// <summary>
		/// Die direkte Weiterentwicklung dieses Elements.
		/// </summary>
		TechTreeUnit Successor { get; set; }

		/// <summary>
		/// Die Technologie, die dieses Element weiterentwickelt.
		/// </summary>
		TechTreeResearch SuccessorResearch { get; set; }
	}
}
