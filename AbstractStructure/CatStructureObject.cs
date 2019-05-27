using System;
using System.Collections.Generic;
using Cat.Handlers;
using Cat.Primitives;
using Cat.Primitives.Precise;
using Cat.Structure;

namespace Cat.AbstractStructure
{
	public class CatStructureObject
	{
		public int Modifiers;
		public string Type;

		public CatStructureObject(string type, params Modifier[] modifiers)
		{
			Modifiers = ModifierHandler.Compute(modifiers);
			Type = type;
			//HeapHandler.LoadObjectToHeap(this);
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

		public bool IsConstructor()
		{
			return ModifierHandler.IsConstructor(Modifiers);
		}

		public static CatStructureObject operator +(CatStructureObject ao, CatStructureObject bo)// to Add method in partial/extension clas
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return bn + av;
				}
				else
				{
					var bv = bn.CastTo(at);
					return bv + an;
				}
			}

			return new CatString(ao + "+" + bo);
		}

		public static CatStructureObject operator -(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av - bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an - bv;
				}
			}

			return new CatString(ao + "-" + bo);
		}

		public static CatStructureObject operator *(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av * bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an * bv;
				}
			}

			return new CatString(ao + "*" + bo);
		}

		public static CatStructureObject operator /(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av / bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an / bv;
				}
			}

			throw new ArgumentException();
		}

		public static CatStructureObject operator !(CatStructureObject ao)
		{
			throw new ArgumentException();
		}

		public static CatStructureObject operator ~(CatStructureObject ao)
		{
			if (ao is CatNumber an )
			{
				return !an;
			}
			throw new ArgumentException();
		}

		public static CatStructureObject operator %(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av % bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an % bv;
				}
			}

			throw new ArgumentException();
		}

		public static CatStructureObject operator ^(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av ^ bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an ^ bv;
				}
			}

			throw new ArgumentException();
		}

		public static CatStructureObject operator |(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av | bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an | bv;
				}
			}

			throw new ArgumentException();
		}

		public static CatStructureObject operator &(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av & bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an & bv;
				}
			}
			throw new ArgumentException();
		}

		public static bool operator >(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av > bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an > bv;
				}
			}

			throw new ArgumentException();
		}

		public static bool operator <(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av < bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an < bv;
				}
			}

			throw new ArgumentException();
		}

		public static bool operator >=(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av >= bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an >= bv;
				}
			}

			throw new ArgumentException();
		}

		public static bool operator <=(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av <= bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an <= bv;
				}
			}

			throw new ArgumentException();
		}

		public static bool operator ==(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av == bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an == bv;
				}
			}

			throw new ArgumentException();
		}

		public static bool operator !=(CatStructureObject ao, CatStructureObject bo)
		{
			if (ao is CatNumber an && bo is CatNumber bn)
			{
				var at = an.GetType();
				if (bn.IsBiggerThan(at))
				{
					var av = an.CastTo(bn.GetType());
					return av != bn;
				}
				else
				{
					var bv = bn.CastTo(at);
					return an != bv;
				}
			}

			throw new ArgumentException();
		}

		public static CatString ToCatString(CatStructureObject a)
		{
			if (a is CatString s)
				return s;
			return new CatString(a + "");
		}

		public virtual CatStructureObject GetFieldValue(string field)
		{
			switch (field)
			{
				case "modifiers": return new CatInt(Modifiers);
				case "type": return new CatString(Type);
				default: return null;
			}
		}

		public virtual bool HasField(string field)
		{
			switch (field)
			{
				case "modifiers":
				case "type": return true;
				default: return false;
			}
		}

		public virtual CatByte ToByte()
		{
			return new CatByte(GetHashCode());
		}

		public virtual CatInt ToInt()
		{
			return new CatInt(GetHashCode());
		}

		public virtual CatLong ToLong()
		{
			return new CatLong(GetHashCode());
		}

		public virtual CatDouble ToDouble()
		{
			return new CatDouble(GetHashCode());
		}

		public virtual CatFloat ToFloat()
		{
			return new CatFloat(GetHashCode());
		}

		public virtual CatPrecise ToPrecise()
		{
			return new CatPrecise(GetHashCode());
		}
	}
}