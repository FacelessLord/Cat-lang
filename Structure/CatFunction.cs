using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using static Cat.CatCore;

namespace Cat.Structure
{
	/// <summary>
	/// Wrapper class for Cat Methods
	/// </summary>
	public class CatFunction : CatMethod
	{
		public CatFunction() : base("internalFunc", new string[0], -1, "internalClass", -1, new Modifier[0])
		{
		}

		public Func<List<CatStructureObject>, int, CatStructureObject> Func; //lexems and address

		public int Priority = 0;
	}
}