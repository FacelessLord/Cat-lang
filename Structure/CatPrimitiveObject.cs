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
		
		public CatPrimitiveObject(string type) : base(type)
		{
			_type = GetTypeIndex(type);
		}
		
		public override LinkedList<object> ToMemoryBlock()
		{
			var block = new LinkedList<object>();
			block.AddLast("|p" + _type);
			block.AddLast("o|");// end of the object

			return block;
		}

		private static readonly CatPrimitiveObject NullObject = new CatPrimitiveObject("");

		public static CatPrimitiveObject NewInstance()
		{
			return NullObject;
		}
		
		public override (CatStructureObject obj, int nextIndex) ReadFromHeapWithIndex(int startIndex)
		{
			try
			{
				var ptype = Heap[startIndex];
				var value = Heap[startIndex + 1];
				var type = ((string) ptype).Substring(2);
				var obj = new CatPrimitiveObject(type);
				return (obj, startIndex + 2);
			}
			catch (Exception e)
			{
				throw new HeapOrderingException();
			}
		}
	}
}