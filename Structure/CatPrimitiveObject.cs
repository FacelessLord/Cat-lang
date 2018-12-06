using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using Cat.Exceptions;
using static Cat.CatCore;

namespace Cat.Structure
{
	/// <inheritdoc />
	/// <summary>
	/// Wrapper class for Cat objects
	/// </summary>
	public class CatPrimitiveObject : CatObject
	{
		/// <summary>
		/// Index of the type in heap
		/// </summary>
		public int _type;
		/// <summary>
		/// Either value of primitive type or link to the object
		/// </summary>
		public object _value;
		
		public CatPrimitiveObject(string type,object value) : base(type)
		{
			_type = GetTypeIndex(type);
			_value = value;
		}
		
		public override LinkedList<object> ToMemoryBlock()
		{
			var block = new LinkedList<object>();
			block.AddLast("|p" + _type);
			block.AddLast(_value);
			block.AddLast("o|");// end of the object

			return block;
		}
		
		public new static (CatPrimitiveObject obj, int nexIndex) ReadFromHeapWithIndex(int startIndex)
		{
			try
			{
				var ptype = Heap[startIndex];
				var value = Heap[startIndex + 1];
				var type = ((string) ptype).Substring(2);
				var obj = new CatPrimitiveObject(type,value);
				return (obj, startIndex + 2);
			}
			catch (Exception e)
			{
				throw new HeapOrderingException();
			}
		}
	}
}