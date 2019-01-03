using System.Collections.Generic;
using Cat.Structure;

namespace Cat.AbstractStructure
{
	public abstract class CatStructureObject
	{
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
		public bool IsConstructor()
		{
			return ModifierHandler.IsConstructor(_modifiers);
		}
	}
}