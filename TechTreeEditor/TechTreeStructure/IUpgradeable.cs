namespace TechTreeEditor.TechTreeStructure
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