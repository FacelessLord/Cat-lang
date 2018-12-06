using System.Collections.Generic;
using System.Linq;
using Cat.AbstractStructure;
using static Cat.CatCore;

namespace Cat.Structure
{
	/// <summary>
	/// Wrapper class for Cat objects
	/// </summary>
	public class CatCompoundObject : CatObject
	{
		/// <summary>
		/// Index of the type in heap
		/// </summary>
		public int _type;
		/// <summary>
		/// An Array of properties of this object
		/// </summary>
		private CatProperty[] _properties;
		
		/// <summary>
		/// Parent class for inheritance
		/// </summary>
		private CatClass _parent;
		
		public CatCompoundObject(string type,params CatProperty[] properties) : base(type)
		{
			_type = GetTypeIndex(type);
			_properties = properties;
		}

		public void SetParent(CatClass parent)
		{
			_parent = parent;
		}
		
		public override LinkedList<object> ToMemoryBlock()
		{
			var block = new LinkedList<object>();
			block.AddLast("|c" + _type);
			if (_parent != null)
			{
				block.AddLast("|P" + _parent._name);
			}
			else
			{
				block.AddLast("|P|");
			}
			
			foreach (var p in _properties)
			{
				var pBlock = p.ToMemoryBlock();
				foreach (var val in pBlock)//saving all properties of an object
				{
					block.AddLast(val);
				}
			}

			block.AddLast("o|");// end of the object

			return block;
		}

		public void InjectProperty(CatProperty property)
		{
			_properties = _properties.Append(property).ToArray();
		}

		public CatProperty GetProperty(string name)
		{
			return _properties.FirstOrDefault(property => property._name == name);
		}
	}
}