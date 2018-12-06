using System.Linq;

namespace Cat.Structure
{
	public enum Modifier
	{
		Field = 1,
		Method = 2,
		Static = 4,
		Final = 8
	}

	public static class ModifierHandler
	{
		public static int Compute(params Modifier[] modifiers)
		{
			return modifiers.Sum(m => (int) m);
		}
		
		public static bool IsField(int modifiers)
		{
			return (modifiers % 2) == 1;
		}
		
		public static bool IsMethod(int modifiers)
		{
			return (modifiers >> 1) % 2 == 1;
		}
		
		public static bool IsStatic(int modifiers)
		{
			return (modifiers >> 2) % 2 == 1;
		}
		
		public static bool IsFinal(int modifiers)
		{
			return (modifiers >> 3) % 2 == 1;
		}
	}
}