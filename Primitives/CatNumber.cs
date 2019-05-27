using System;
using Cat.AbstractStructure;
using Cat.Primitives.Precise;
using Cat.Structure;

namespace Cat.Primitives
{
	public abstract class CatNumber : CatPrimitiveObject
	{
		public CatNumber(string type) : base(type)
		{
		}

		public abstract bool IsBiggerThan(Type b);

		public abstract CatNumber CastTo(Type b);

		public override CatInt ToInt()
		{
			return CastTo(typeof(CatInt)) as CatInt;
		}

		public override CatLong ToLong()
		{
			return CastTo(typeof(CatLong)) as CatLong;
		}

		public override CatFloat ToFloat()
		{
			return CastTo(typeof(CatFloat)) as CatFloat;
		}

		public override CatDouble ToDouble()
		{
			return CastTo(typeof(CatDouble)) as CatDouble;
		}

		public override CatPrecise ToPrecise()
		{
			return CastTo(typeof(CatPrecise)) as CatPrecise;
		}

		public static bool operator >(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab > ((CatByte) bo);
				case CatInt ab: return ab > ((CatInt) bo);
				case CatLong ab: return ab > ((CatLong) bo);
				case CatFloat ab: return ab > ((CatFloat) bo);
				case CatDouble ab: return ab > ((CatDouble) bo);
				case CatPrecise ab: return ab > ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}

		public static bool operator <(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab > ((CatByte) bo);
				case CatInt ab: return ab > ((CatInt) bo);
				case CatLong ab: return ab > ((CatLong) bo);
				case CatFloat ab: return ab > ((CatFloat) bo);
				case CatDouble ab: return ab > ((CatDouble) bo);
				case CatPrecise ab: return ab > ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}

		public static bool operator >=(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab >= ((CatByte) bo);
				case CatInt ab: return ab >= ((CatInt) bo);
				case CatLong ab: return ab >= ((CatLong) bo);
				case CatFloat ab: return ab >= ((CatFloat) bo);
				case CatDouble ab: return ab >= ((CatDouble) bo);
				case CatPrecise ab: return ab >= ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}

		public static bool operator <=(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab >= ((CatByte) bo);
				case CatInt ab: return ab >= ((CatInt) bo);
				case CatLong ab: return ab >= ((CatLong) bo);
				case CatFloat ab: return ab >= ((CatFloat) bo);
				case CatDouble ab: return ab >= ((CatDouble) bo);
				case CatPrecise ab: return ab >= ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}

		public static bool operator ==(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab == ((CatByte) bo);
				case CatInt ab: return ab == ((CatInt) bo);
				case CatLong ab: return ab == ((CatLong) bo);
				case CatFloat ab: return ab == ((CatFloat) bo);
				case CatDouble ab: return ab == ((CatDouble) bo);
				case CatPrecise ab: return ab == ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}

		public static bool operator !=(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab != ((CatByte) bo);
				case CatInt ab: return ab != ((CatInt) bo);
				case CatLong ab: return ab != ((CatLong) bo);
				case CatFloat ab: return ab != ((CatFloat) bo);
				case CatDouble ab: return ab != ((CatDouble) bo);
				case CatPrecise ab: return ab != ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}

		public static CatNumber operator %(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab % ((CatByte) bo);
				case CatInt ab: return ab % ((CatInt) bo);
				case CatLong ab: return ab % ((CatLong) bo);
				case CatFloat ab: return ab % ((CatFloat) bo);
				case CatDouble ab: return ab % ((CatDouble) bo);
				case CatPrecise ab: return ab % ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}

		public static CatNumber operator ^(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab ^ ((CatByte) bo);
				case CatInt ab: return ab ^ ((CatInt) bo);
				case CatLong ab: return ab ^ ((CatLong) bo);
				case CatFloat ab: return ab ^ ((CatFloat) bo);
				case CatDouble ab: return ab ^ ((CatDouble) bo);
				case CatPrecise ab: return ab ^ ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}

		public static CatNumber operator |(CatNumber ao, CatNumber bo)
		{

			switch (ao)
			{
				case CatByte ab: return ab | ((CatByte) bo);
				case CatInt ab: return ab | ((CatInt) bo);
				case CatLong ab: return ab | ((CatLong) bo);
				case CatFloat ab: return ab | ((CatFloat) bo);
				case CatDouble ab: return ab | ((CatDouble) bo);
				case CatPrecise ab: return ab | ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}
		
		public static CatStructureObject operator &(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab & ((CatByte) bo);
				case CatInt ab: return ab & ((CatInt) bo);
				case CatLong ab: return ab & ((CatLong) bo);
				case CatFloat ab: return ab & ((CatFloat) bo);
				case CatDouble ab: return ab & ((CatDouble) bo);
				case CatPrecise ab: return ab & ((CatPrecise) bo);
			}

			throw new InvalidCastException();
		}

		public static CatNumber operator +(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab + bo.ToByte();
				case CatInt ab: return ab + bo.ToInt();
				case CatLong ab: return ab + bo.ToLong();
				case CatFloat ab: return ab + bo.ToFloat();
				case CatDouble ab: return ab + bo.ToDouble();
				case CatPrecise ab: return ab + bo.ToPrecise();
			}

			throw new InvalidCastException();
		}

		public static CatNumber operator -(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab - bo.ToByte();
				case CatInt ab: return ab - bo.ToInt();
				case CatLong ab: return ab - bo.ToLong();
				case CatFloat ab: return ab - bo.ToFloat();
				case CatDouble ab: return ab - bo.ToDouble();
				case CatPrecise ab: return ab - bo.ToPrecise();
			}

			throw new InvalidCastException();
		}

		public static CatNumber operator *(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab * bo.ToByte();
				case CatInt ab: return ab * bo.ToInt();
				case CatLong ab: return ab * bo.ToLong();
				case CatFloat ab: return ab * bo.ToFloat();
				case CatDouble ab: return ab * bo.ToDouble();
				case CatPrecise ab: return ab * bo.ToPrecise();
			}

			throw new InvalidCastException();
		}

		public static CatNumber operator /(CatNumber ao, CatNumber bo)
		{
			switch (ao)
			{
				case CatByte ab: return ab / bo.ToByte();
				case CatInt ab: return ab / bo.ToInt();
				case CatLong ab: return ab / bo.ToLong();
				case CatFloat ab: return ab / bo.ToFloat();
				case CatDouble ab: return ab / bo.ToDouble();
				case CatPrecise ab: return ab / bo.ToPrecise();
			}

			throw new InvalidCastException();
		}
	}
}