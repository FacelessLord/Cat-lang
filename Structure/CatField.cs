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
			_type = GetTypeIndex(type);
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

		public override LinkedList<object> ToMemoryBlock()
		{
			var block = new LinkedList<object>();
			block.AddLast("|f" + _name);
			block.AddLast(_modifiers);
			block.AddLast(_type);
			block.AddLast(_value);
			return block;
		}
		
		public override object Clone()
		{
			return new CatField(_name,_type, _value){_modifiers = _modifiers};
		}

		public override (CatStructureObject obj, int nextIndex) ReadFromHeapWithIndex(int startIndex)
		{
			try
			{
				var fname = Heap[startIndex];
				var modifiers = Heap[startIndex + 1];
				var type = Heap[startIndex + 2];
				var value = Heap[startIndex + 3];
				var name = ((string) fname).Substring(2);
				var obj = new CatField(name, (string) type, (int) value) {_modifiers = (int) modifiers};
				return (obj, startIndex + 4);
			}
			catch (Exception e)
			{
				throw new HeapOrderingException();
			}
		}
	}
}