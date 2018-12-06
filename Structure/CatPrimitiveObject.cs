using System.Collections.Generic;
using Cat.AbstractStructure;
using static Cat.CatCore;

namespace Cat.Structure
{
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
		private object _value;
		
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
	}
}