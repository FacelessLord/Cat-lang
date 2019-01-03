using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using static Cat.CatCore;
using Cat.Exceptions;

namespace Cat.Structure
{
	/// <summary>
	/// Wrapper class for Cat Methods
	/// </summary>
	public class CatMethod : CatProperty
	{
		/// <summary>
		/// Name of the field
		/// </summary>
		public string _name;

		/// <summary>
		/// Array of indexes of arguments in Heap
		/// </summary>
		public string[] _signature;

		/// <summary>
		/// Index of the return type in Heap
		/// </summary>
		public int _returnType;

		/// <summary>
		/// Class name to work in
		/// </summary>
		public string _class;

		/// <summary>
		/// Line to start from
		/// </summary>
		public int _line;

		public CatMethod(string name, string[] signature, int returnType, string linkClass, int line,
			params Modifier[] modifiers) : base(name, modifiers)
		{
			_signature = signature;
			_returnType = returnType;
			_class = linkClass;
			_line = line;
			if (!ModifierHandler.IsMethod(_modifiers))
			{
				_modifiers += (int) Modifier.Method;
			}
		}

		public CatMethod(string rawSignature, string returnType, string linkClass, int line,
			params Modifier[] modifiers) : base("", modifiers)
		{
			var fullSign = ProcessSignature(rawSignature);
			_name = fullSign.Item1;
			_signature = fullSign.Item2;
			if(returnType != "")
				_returnType = TypeHandler.GetTypeIndex(returnType);
			_class = linkClass;
			_line = line;
		}

		public override object Clone()
		{
			var newSignature = new string[_signature.Length];
			for (int i = 0; i < _signature.Length; i++)
			{
				newSignature[i] = _signature[i];
			}
			return new CatMethod(_name,newSignature,_returnType,_class,_line){_modifiers = _modifiers};
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