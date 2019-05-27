using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using static Cat.CatCore;

namespace Cat.Structure
{
	/// <summary>
	/// Wrapper class for Cat Constructor
	/// </summary>
	public class CatConstructor : CatMethod
	{
		public CatConstructor(string name, string[] signature, int returnType, string linkClass, int line, params Modifier[] modifiers) :
			base(name, signature, returnType, linkClass, line, modifiers)
		{
		}

		public CatConstructor(string rawSignature, string returnType, string linkClass, int line, params Modifier[] modifiers) :
			base(rawSignature, returnType, linkClass, line, modifiers)
		{
		}

		public override object Clone()
		{
			var newSignature = new string[Signature.Length];
			for (int i = 0; i < Signature.Length; i++)
			{
				newSignature[i] = Signature[i];
			}
			return new CatConstructor(Name,newSignature,ReturnType,Class,Line){Modifiers = Modifiers};
		}
	}
}