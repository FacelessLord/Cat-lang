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
		public string Name;

		public CatProperty(string name,string type, params Modifier[] modifiers) : base(type,modifiers)
		{
			Name = name;
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