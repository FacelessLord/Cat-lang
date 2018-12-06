using System;
using System.Collections.Generic;
using System.Linq;
using Cat.AbstractStructure;
using Cat.Exceptions;
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
		public CatProperty[] _properties;
		
		/// <summary>
		/// Parent class for inheritance
		/// </summary>
		public string _parent;
		
		public CatCompoundObject(string type,params CatProperty[] properties) : base(type)
		{
			_type = GetTypeIndex(type);
			_properties = properties;
		}

		public void SetParent(string parent)
		{
			_parent = parent;
		}
		
		public override LinkedList<object> ToMemoryBlock()
		{
			var block = new LinkedList<object>();
			block.AddLast("|c" + _type);
			if (_parent != null)
			{
				block.AddLast("|P" + _parent);
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
		
		public new static (CatCompoundObject obj, int nextIndex) ReadFromHeapWithIndex(int startIndex)
		{
			try
			{
				var ctype =(string) Heap[startIndex];
				var pparent = (string) Heap[startIndex + 1];
				var properties = new List<CatProperty>();
				int j = 0;
				while (!(Heap[startIndex + 2 + j] is string entry) || entry.StartsWith("|f")|| entry.StartsWith("|m"))
				{
					var propertyInitializer = (string)Heap[startIndex + 2 + j];
					if (propertyInitializer.StartsWith("|f"))
					{
						var propIndex = CatField.ReadFromHeapWithIndex(startIndex + 2 + j);
						j += propIndex.nextIndex;
						properties.Add(propIndex.obj);
					}
					else
					{
						var propIndex = CatMethod.ReadFromHeapWithIndex(startIndex + 2 + j);
						j += propIndex.nextIndex;
						properties.Add(propIndex.obj);
					}
				}

				var type = ctype.Substring(2);
				var parentName = pparent.Substring(2);
				var parent = "void";
				if (parentName != "|")
				{
					parent = parentName;
				}
				var obj = new CatCompoundObject(type, properties.ToArray()){_parent = parent};
			
				return (obj,startIndex+3+j);
			}
			catch (Exception e)
			{
				throw new HeapOrderingException();
			}
		}
	}
}