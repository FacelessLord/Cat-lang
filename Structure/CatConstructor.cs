using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using static Cat.CatCore;
using Cat.Exceptions;

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
			var newSignature = new string[_signature.Length];
			for (int i = 0; i < _signature.Length; i++)
			{
				newSignature[i] = _signature[i];
			}
			return new CatConstructor(_name,newSignature,_returnType,_class,_line){_modifiers = _modifiers};
		}
	}
}