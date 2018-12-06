using Cat.Structure;

namespace Cat.AbstractStructure
{
	public abstract class CatProperty : CatStructureObject
	{
		/// <summary>
		/// Name of the property
		/// </summary>
		public string _name;

		public CatProperty(string name, params Modifier[] modifiers) : base(modifiers)
		{
			_name = name;
		}
	}
}