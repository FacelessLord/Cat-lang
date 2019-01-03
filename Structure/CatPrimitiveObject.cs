using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using Cat.Exceptions;
using static Cat.CatCore;

namespace Cat.Structure
{
	/// <inheritdoc />
	/// <summary>
	/// Wrapper class for Cat objects
	/// </summary>
	public class CatPrimitiveObject : CatObject
	{
		public CatPrimitiveObject(string type) : base(type)
		{
		}

		private static readonly CatPrimitiveObject NullObject = new CatPrimitiveObject("int");

		public static CatPrimitiveObject NewInstance()
		{
			return NullObject;
		}
	}
}