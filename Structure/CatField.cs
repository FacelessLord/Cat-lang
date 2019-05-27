using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using Cat.Utilities;
using static Cat.CatCore;

namespace Cat.Structure
{
	/// <summary>
	/// Wrapper class for Cat fields
	/// </summary>
	public class CatField : CatProperty
	{
		/// <summary>
		/// Index of the type in heap
		/// </summary>
		public int _type;
		/// <summary>
		/// Either value of primitive type or link to the object
		/// </summary>
		public CatStructureObject Value;

		public CatField(string name, int type, CatStructureObject value, params Modifier[] modifiers) : base(name,"field",modifiers)
		{
			_type = type;
			Value = value;
			if (!ModifierHandler.IsField(Modifiers))
			{
				Modifiers += (int)Modifier.Field;
			}
		}		
		
		public CatField(string name, string type, CatStructureObject value, params Modifier[] modifiers) : base(name,"field",modifiers)
		{
			_type = TypeHandler.GetTypeIndex(type);
			Value = value;
			if (!ModifierHandler.IsField(Modifiers))
			{
				Modifiers += (int)Modifier.Field;
			}
		}

		private static readonly CatField NullField = new CatField("", L0, null);
		
		public static CatField NewInstance()
		{
			return NullField;
		}
		
		public override object Clone()
		{
			return new CatField(Name,_type, Value){Modifiers = Modifiers};
		}
	}
}