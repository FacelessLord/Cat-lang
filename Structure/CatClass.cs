using System;
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
		public CatProperty[] Properties;

		/// <summary>
		/// Name of this Class to find in "Classes"
		/// </summary>
		public string Name;

		/// <summary>
		/// Parent class name for inheritance
		/// </summary>
		public CatClass Parent;

		public CatClass(string name, params CatProperty[] properties) : base("class")
		{
			Name = name;
			Properties = properties;
		}

		public CatClass SetParentClass(CatClass parent)
		{
			Parent = parent;
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
			var newProperties = new CatProperty[Properties.Length];
			for (int i = 0; i < Properties.Length; i++)
			{
				newProperties[i] =(CatProperty) Properties[i].Clone();
			}
			var cco = new CatCompoundObject(this,newProperties);
			cco.SetParentClass(Parent);
			return cco;                                    
		}

		public void InjectProperty(CatProperty property)
		{
			Properties = Properties.Append(property).ToArray();
		}

		public CatProperty GetProperty(string name)
		{
			return Properties.FirstOrDefault(property => property.Name == name);
		}

		~CatClass()
		{
			Parent = null;
			Properties = null;
		}
	}
}