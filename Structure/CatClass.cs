using System;
using System.Collections.Generic;
using System.Linq;
using Cat.AbstractStructure;
using Cat.Exceptions;
using static Cat.CatCore;

namespace Cat.Structure
{
	public class CatClass : CatStructureObject
	{
		/// <summary>
		/// Properties of this Class
		/// </summary>
		public CatProperty[] _properties;

		/// <summary>
		/// Name of this Class to find in "Classes"
		/// </summary>
		public string _name;

		/// <summary>
		/// Parent class name for inheritance
		/// </summary>
		public string _parent;

		public CatClass(string name, params CatProperty[] properties)
		{
			_name = name;
			_properties = properties;
		}

		public CatClass SetParent(string parent)
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
				block.AddLast("|P" + _parent);
			}
			else
			{
				block.AddLast("|P|");
			}
			var fieldCount = 0;
			var methodCount = 0;
			foreach (var t in _properties)
			{
				if (t.IsField())
					fieldCount++;
				if (t.IsMethod())
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
			var newProperties = new CatProperty[_properties.Length];
			for (int i = 0; i < _properties.Length; i++)
			{
				newProperties[i] =(CatProperty) _properties[i].Clone();
			}
			var cco = new CatCompoundObject(_name,newProperties);
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
		
		public new static (CatClass obj, int nextIndex) ReadFromHeapWithIndex(int startIndex)
		{
			try
			{
				var cname =(string) Heap[startIndex];
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

				var name = cname.Substring(2);
				var parentName = pparent.Substring(2);
				var parent = "void";
				if (parentName != "|")
				{
					parent = parentName;
				}
				var obj = new CatClass(name, properties.ToArray()){_parent = parent};
			
				return (obj,startIndex+3+j);
			}
			catch (Exception e)
			{
				throw new HeapOrderingException();
			}
		}
	}
}