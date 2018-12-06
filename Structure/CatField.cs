using System.Collections.Generic;
using Cat.AbstractStructure;
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
		private int _type;
		/// <summary>
		/// Either value of primitive type or link to the object
		/// </summary>
		private object _value;

		public CatField(string name, string type, int value, params Modifier[] modifiers) : base(name,modifiers)
		{
			_type = GetTypeIndex(type);
			_value = value;
			if (!ModifierHandler.IsField(Modifiers))
			{
				Modifiers += (int)Modifier.Field;
			}
		}

		public override LinkedList<object> ToMemoryBlock()
		{
			var block = new LinkedList<object>();
			block.AddLast("|f" + _name);
			block.AddLast(Modifiers);
			block.AddLast(_type);
			block.AddLast(_value);
			return block;
		}
	}
}