using System;
using System.Collections.Generic;
using Cat.Structure;

namespace Cat.AbstractStructure
{
	public abstract class CatProperty : CatStructureObject, ICloneable
	{
		/// <summary>
		/// Name of the property
		/// </summary>
		public string _name;

		public CatProperty(string name, params Modifier[] modifiers) : base(modifiers)
		{
			_name = name;
		}

		public CatField ToField()
		{
			return this as CatField;
		}
		
		public CatMethod ToMethod()
		{
			return this as CatMethod;
		}

		public abstract object Clone();
	}
}