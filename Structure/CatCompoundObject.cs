using System;
using System.Collections.Generic;
using System.Linq;
using Cat.AbstractStructure;
using static Cat.CatCore;

namespace Cat.Structure
{
	/// <summary>
	/// Wrapper class for Cat objects
	/// </summary>
	public class CatCompoundObject : CatStructureObject
	{
		/// <summary>
		/// An Array of properties of this object
		/// </summary>
		public CatProperty[] Properties;
		
		/// <summary>
		/// Parent class for inheritance
		/// </summary>
		public CatClass ParentClass;

		public CatClass TypeClass;

		public CatCompoundObject(CatClass typeClass,params CatProperty[] properties) : base(typeClass.Name)
		{
			TypeClass = typeClass;
			Properties = properties;
		}

		public void SetParentClass(CatClass parent)
		{
			ParentClass = parent;
		}

		public void InjectProperty(CatProperty property)
		{
			Properties = Properties.Append(property).ToArray();
		}

		public CatProperty GetProperty(string name)
		{
			return Properties.FirstOrDefault(property => property.Name == name);
		}

		private static readonly CatCompoundObject NullObject = new CatCompoundObject(null);

		public static CatCompoundObject NewInstance()
		{
			return NullObject;
		}
	}
}