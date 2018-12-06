using System.Collections.Generic;
using Cat.Structure;

namespace Cat.AbstractStructure
{
	public abstract class CatStructureObject
	{
		/// <summary>
		/// Method that will aggregate values of the Structure object
		/// </summary>
		/// <returns> LinkedList that contains all values of Structure object</returns>
		public abstract LinkedList<object> ToMemoryBlock();
		public int _modifiers;

		public CatStructureObject(params Modifier[] modifiers)
		{
			_modifiers = ModifierHandler.Compute(modifiers);
		}

		public bool IsField()
		{
			return ModifierHandler.IsField(_modifiers);
		}

		public bool IsMethod()
		{
			return ModifierHandler.IsMethod(_modifiers);
		}
		public bool IsStatic()
		{
			return ModifierHandler.IsStatic(_modifiers);
		}
		public bool IsFinal()
		{
			return ModifierHandler.IsFinal(_modifiers);
		}

		public static CatStructureObject ReadFromHeap(int startIndex)
		{
			return ReadFromHeapWithIndex(startIndex).obj;
		}

		public static (CatStructureObject obj, int nextIndex) ReadFromHeapWithIndex(int startIndex)
		{
			throw new System.NotImplementedException();
		}
	}
}