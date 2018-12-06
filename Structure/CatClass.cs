using System.Collections.Generic;
using System.Linq;
using Cat.AbstractStructure;
using static Cat.CatCore;

namespace Cat.Structure
{
	public class CatClass : CatStructureObject
	{
		/// <summary>
		/// Properties of this Class
		/// </summary>
		private CatProperty[] _properties;

		/// <summary>
		/// Name of this Class to find in "Classes"
		/// </summary>
		public string _name;

		/// <summary>
		/// Parent class for inheritance
		/// </summary>
		private CatClass _parent;

		public CatClass(string name, params CatProperty[] properties)
		{
			_name = name;
			_properties = properties;
		}

		public CatClass SetParent(CatClass parent)
		{
			_parent = parent;
			return this;
		}

		public override LinkedList<object> ToMemoryBlock()
		{
			var block = new LinkedList<object>();
			block.AddLast("|C" + _name);
			if(_parent != null)
			{
				block.AddLast("|P" + _parent._name);
			}
			else
			{
				block.AddLast("|P|");
			}
			var fieldCount = 0;
			var methodCount = 0;
			for (var i = 0; i < _properties.Length; i++)
			{
				if (Heap[i] is string sf && sf.StartsWith("|f"))
					fieldCount++;
				if (Heap[i] is string sm && sm.StartsWith("|m"))
					methodCount++;
			}

			block.AddLast(fieldCount);
			block.AddLast(methodCount);

			foreach (var p in _properties)
			{
				var pBlock = p.ToMemoryBlock();
				foreach (var v in pBlock)
				{
					block.AddLast(v);
				}
			}
			block.AddLast("C|");

			return block;
		}

		public CatCompoundObject CreateObjectFromClass()
		{
			var cco = new CatCompoundObject(_name,(CatProperty[])_properties.MemberwiseClone());
			cco.SetParent(_parent);
			return cco;                                    
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