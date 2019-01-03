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
		public CatClass _parent;

		public CatClass(string name, params CatProperty[] properties)
		{
			_name = name;
			_properties = properties;
		}

		public CatClass SetParentClass(CatClass parent)
		{
			_parent = parent;
			return this;
		}

		private static readonly CatClass NullClass = new CatClass("");

		public static CatClass NewInstance()
		{
			return NullClass;
		}

		/// <summary>
		/// Method
		/// </summary>
		/// <returns></returns>
		public CatCompoundObject CreateObjectFromClass()
		{
			var newProperties = new CatProperty[_properties.Length];
			for (int i = 0; i < _properties.Length; i++)
			{
				newProperties[i] =(CatProperty) _properties[i].Clone();
			}
			var cco = new CatCompoundObject(this,newProperties);
			cco.SetParentClass(_parent);
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

		~CatClass()
		{
			_parent = null;
			_properties = null;
		}
	}
}