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
		protected int Modifiers;

		public CatStructureObject(params Modifier[] modifiers)
		{
			Modifiers = ModifierHandler.Compute(modifiers);
		}

		public bool IsField()
		{
			return ModifierHandler.IsField(Modifiers);
		}

		public bool IsMethod()
		{
			return ModifierHandler.IsMethod(Modifiers);
		}
		public bool IsStatic()
		{
			return ModifierHandler.IsStatic(Modifiers);
		}
		public bool IsFinal()
		{
			return ModifierHandler.IsFinal(Modifiers);
		}

		public abstract CatStructureObject ReadFromHeap(int startIndex);
	}
}