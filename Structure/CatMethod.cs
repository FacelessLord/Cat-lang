using System;
using System.Collections.Generic;
using CalculatorConsole;
using Cat.AbstractStructure;
using static Cat.CatCore;

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
		private string _name;
		/// <summary>
		/// Array of indexes of arguments in Heap
		/// </summary>
		private int[] _signature;
		/// <summary>
		/// Index of the return type in Heap
		/// </summary>
		private int _returnType;
		/// <summary>
		/// Class name to work in
		/// </summary>
		private string _class;
		/// <summary>
		/// Line to start from
		/// </summary>
		private int _line;

		public CatMethod(string name, int[] signature, int returnType, string linkClass, int line,
			params Modifier[] modifiers) : base(name, modifiers)
		{
			_signature = signature;
			_returnType = returnType;
			_class = linkClass;
			_line = line;
			if (!ModifierHandler.IsMethod(Modifiers))
			{
				Modifiers += (int)Modifier.Method;
			}
		}
		
		public CatMethod(string rawSignature, string returnType, string linkClass, int line,
			params Modifier[] modifiers) : base("",modifiers)
		{
			var fullSign = ProcessSignature(rawSignature);
			_name = fullSign.Item1;
			_signature = fullSign.Item2;
			_returnType = GetTypeIndex(returnType);
			_class = linkClass;
			_line = line;
		}

		public override LinkedList<object> ToMemoryBlock()
		{
			var block = new LinkedList<object>();
			block.AddLast("|m" + _name);
			block.AddLast(Modifiers);
			block.AddLast(_signature.Length);
			foreach (var t in _signature)
			{
				block.AddLast(t);
			}

			block.AddLast(_returnType);
			block.AddLast(_class);
			block.AddLast(_line);
			return block;
		}

		/// <summary>
		/// Method that gets all information from signature like: "abc(T1 v1, T2 v2, ... )"
		/// </summary>
		/// <param name="rawSign">Signature like: "abc(T1 v1, T2 v2, ... )"</param>
		/// <returns>Tuple of method name and argument types</returns>
		public static (string, int[]) ProcessSignature(string rawSign)
		{
			var openParenthese = rawSign.IndexOf('(');
			var closeParenthese = rawSign.IndexOf(')');
			var args = rawSign.Substring(openParenthese + 1, closeParenthese - openParenthese - 1);
			var singleArguments = args.Split(',');

			var types = new int[singleArguments.Length];

			for (var i = 0; i < singleArguments.Length; i++)
			{
				types[i] = GetTypeIndex(singleArguments[i].Trim().Split()[0]);
			}

			var name = rawSign.Trim().Substring(0, openParenthese).Trim();

			return (name, types);
		}
	}
}