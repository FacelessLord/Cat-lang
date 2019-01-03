using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using Cat.Exceptions;
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
		public object _value;

		public CatField(string name, int type, object value, params Modifier[] modifiers) : base(name,modifiers)
		{
			_type = type;
			_value = value;
			if (!ModifierHandler.IsField(_modifiers))
			{
				_modifiers += (int)Modifier.Field;
			}
		}		
		
		public CatField(string name, string type, object value, params Modifier[] modifiers) : base(name,modifiers)
		{
			_type = TypeHandler.GetTypeIndex(type);
			_value = value;
			if (!ModifierHandler.IsField(_modifiers))
			{
				_modifiers += (int)Modifier.Field;
			}
		}

		private static readonly CatField NullField = new CatField("", L0, V0);
		
		public static CatField NewInstance()
		{
			return NullField;
		}
		
		public override object Clone()
		{
			return new CatField(_name,_type, _value){_modifiers = _modifiers};
		}
	}
}