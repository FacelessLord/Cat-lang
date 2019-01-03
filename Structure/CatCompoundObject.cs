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
		public CatClass _parentClass;

		public CatClass _typeClass;

		public CatCompoundObject(CatClass typeClass,params CatProperty[] properties) : base(typeClass._name)
		{
			_typeClass = typeClass;
			_properties = properties;
		}

		public void SetParentClass(CatClass parent)
		{
			_parentClass = parent;
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
	}
}