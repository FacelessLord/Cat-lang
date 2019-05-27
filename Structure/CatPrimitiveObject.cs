using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using Cat.Primitives;
using static Cat.CatCore;

namespace Cat.Structure
{
	/// <inheritdoc />
	/// <summary>
	/// Wrapper class for Cat objects
	/// </summary>
	public class CatPrimitiveObject : CatStructureObject
	{
		public CatPrimitiveObject(string type) : base(type)
		{
		}
		
		public override string ToString()
		{
			return Type;
		}

	}
}