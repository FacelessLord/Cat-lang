using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using Cat.Utilities;
using static Cat.CatCore;

namespace Cat.Structure
{
	/// <summary>
	/// Wrapper class for Cat Methods
	/// </summary>
	public class CatMethod : CatProperty
	{

		/// <summary>
		/// Array of indexes of arguments in Heap
		/// </summary>
		public string[] Signature;

		/// <summary>
		/// Index of the return type in Heap
		/// </summary>
		public int ReturnType;

		/// <summary>
		/// Class name to work in
		/// </summary>
		public string Class;

		/// <summary>
		/// Line to start from
		/// </summary>
		public int Line;

		public CatMethod(string name, string[] signature, int returnType, string linkClass, int line,
			params Modifier[] modifiers) : base(name,"method", modifiers)
		{
			Signature = signature;
			ReturnType = returnType;
			Class = linkClass;
			Line = line;
			if (!ModifierHandler.IsMethod(Modifiers))
			{
				Modifiers += (int) Modifier.Method;
			}
		}

		public CatMethod(string rawSignature, string returnType, string linkClass, int line,
			params Modifier[] modifiers) : base("","method", modifiers)
		{
			var fullSign = ProcessSignature(rawSignature);
			Name = fullSign.Item1;
			Signature = fullSign.Item2;
			if(returnType != "")
				ReturnType = TypeHandler.GetTypeIndex(returnType);
			Class = linkClass;
			Line = line;
		}

		public override object Clone()
		{
			var newSignature = new string[Signature.Length];
			for (int i = 0; i < Signature.Length; i++)
			{
				newSignature[i] = Signature[i];
			}
			return new CatMethod(Name,newSignature,ReturnType,Class,Line){Modifiers = Modifiers};
		}

		public static CatMethod NewInstance()
		{
			return new CatMethod("","","",-1);
		}

		/// <summary>
		/// Method that gets all information from signature like: "abc(T1 v1, T2 v2, ... )"
		/// </summary>
		/// <param name="rawSign">Signature like: "abc(T1 v1, T2 v2, ... )"</param>
		/// <returns>Tuple of method name and argument types</returns>
		public static (string, string[]) ProcessSignature(string rawSign)
		{
			var openParenthese = rawSign.IndexOf('(');
			var closeParenthese = rawSign.IndexOf(')');
			var args = rawSign.Substring(openParenthese + 1, closeParenthese - openParenthese - 1);
			var singleArguments = args.Split(',');

			var types = new string[singleArguments.Length];

			for (var i = 0; i < singleArguments.Length; i++)
			{
				types[i] = singleArguments[i].Trim().Split()[0].Trim();
			}
			var name = rawSign.Substring(0, openParenthese).Trim();

			return (name, types);
		}
	}
}