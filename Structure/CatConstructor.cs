using System;
using System.Collections.Generic;
using CalculatorConsole;
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
		/// <summary>
		/// Name of the field
		/// </summary>
		public string _name;

		/// <summary>
		/// Array of indexes of arguments in Heap
		/// </summary>
		public int[] _signature = new int[0];

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

		public CatConstructor(string name, int[] signature, int returnType, string linkClass, int line, params Modifier[] modifiers) : base(name, signature, returnType, linkClass, line, modifiers)
		{
		}

		public CatConstructor(string rawSignature, string returnType, string linkClass, int line, params Modifier[] modifiers) : base(rawSignature, returnType, linkClass, line, modifiers)
		{
		}
		
		public override LinkedList<object> ToMemoryBlock()
		{
			var block = new LinkedList<object>();
			block.AddLast("|m" + _name);
			block.AddLast(_modifiers);
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

		public override object Clone()
		{
			var newSignature = new int[_signature.Length];
			for (int i = 0; i < _signature.Length; i++)
			{
				newSignature[i] = _signature[i];
			}
			return new CatConstructor(_name,newSignature,_returnType,_class,_line){_modifiers = _modifiers};
		}

		public new static (CatConstructor obj, int nextIndex) ReadFromHeapWithIndex(int startIndex)
		{
			try
			{
				var mname = Heap[startIndex];
				var modifiers = Heap[startIndex + 1];
				var sigLength = (int) Heap[startIndex + 2];
				var signature = new int[sigLength];
				for (int i = 0; i < sigLength; i++)
				{
					signature[i] = (int) Heap[startIndex + 3 + i];
				}

				var returnType = Heap[startIndex + 3 + sigLength];
				var clazz = Heap[startIndex + 4 + sigLength];
				var line = Heap[startIndex + 5 + sigLength];

				var name = ((string) mname).Substring(2);
				var obj = new CatConstructor(name, (string) returnType, (string) clazz,(int) line) {_modifiers = (int) modifiers};
				return (obj, startIndex + 6 + sigLength);
			}
			catch (Exception e)
			{
				throw new HeapOrderingException();
			}
		}
	}
}