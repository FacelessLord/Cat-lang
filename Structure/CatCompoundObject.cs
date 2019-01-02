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
		/// An Array of properties of this object
		/// </summary>
		public CatProperty[] _properties;
		
		/// <summary>
		/// Parent class for inheritance
		/// </summary>
		public CatClass _parent;

		public CatClass _typeObject;

		public CatCompoundObject(CatClass typeClass,params CatProperty[] properties) : base(typeClass._name)
		{
			_type = GetTypeIndex(typeClass._name);
			_typeObject = typeClass;
			_properties = properties;
		}

		public void SetParentClass(CatClass parent)
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

		private static readonly CatCompoundObject NullObject = new CatCompoundObject(null);

		public static CatCompoundObject NewInstance()
		{
			return NullObject;
		}
		
		public override (CatStructureObject obj, int nextIndex) ReadFromHeapWithIndex(int startIndex)
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
						var propIndex = CatField.NewInstance().ReadFromHeapWithIndex(startIndex + 2 + j);
						j += propIndex.nextIndex;
						properties.Add((CatField)propIndex.obj);
					}
					else
					{
						var propIndex =  CatMethod.NewInstance().ReadFromHeapWithIndex(startIndex + 2 + j);
						j += propIndex.nextIndex;
						properties.Add((CatMethod)propIndex.obj);
					}
				}

				var type = ctype.Substring(2);
				var parentName = pparent.Substring(2);
				var parent = "void";
				if (parentName != "|")
				{
					parent = parentName;
				}
				
				var obj = new CatCompoundObject(CatCore.GetClassForName(type), properties.ToArray()){_parent = CatCore.GetClassForName(parent)};
			
				return (obj,startIndex+3+j);
			}
			catch (Exception e)
			{
				throw new HeapOrderingException();
			}
		}
	}
}