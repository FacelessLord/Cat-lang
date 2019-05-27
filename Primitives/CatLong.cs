using System;
using Cat.AbstractStructure;
using Cat.Primitives.Precise;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatLong : CatNumber
    {
        public long Value;
        public CatLong(object value) : base("long")
        {
            switch (value)
            {
                case string s:
                    Value = long.Parse(s);
                    break;
                case CatLong b:
                    Value = b.Value;
                    break;
                default:
                    Value = (long) value;
                    break;
            }
        }
        public override CatStructureObject GetFieldValue(string field)
        {
            var baseRet = base.GetFieldValue(field);
            switch (field)
            {
                case "abs": return new CatInt(Math.Abs(Value));
                case "hashCode": return new CatInt(GetHashCode());
            }

            return baseRet;
        }

        public override bool HasField(string field)
        {
            switch (field)
            {
                case "abs":
                case "hashCode": return true;
            }

            return base.HasField(field);
        }
        
        public override bool IsBiggerThan(Type b)
        {
            return b == typeof(CatByte) || b == typeof(CatInt);
        }
        
        public override CatNumber CastTo(Type b)
        {
            switch (b.Name)
            {
                case "CatByte":
                    return ToByte(); 
                case "CatInt":
                    return ToInt(); 
                case "CatLong":
                    return this; 
                case "CatFloat":
                    return ToFloat(); 
                case "CatDouble":
                    return ToDouble(); 
                case "CatPrecise":
                    return ToPrecise(); 
            }

            throw new InvalidCastException();
        }
        public override CatByte ToByte()
        {
            return new CatByte(Value);
        }
        public override CatInt ToInt()
        {
            return new CatInt(Value);
        }
        public override CatLong ToLong()
        {
            return this;
        }
        public override CatFloat ToFloat()
        {
            return new CatFloat(Value);
        }
        public override CatDouble ToDouble()
        {
            return new CatDouble(Value);
        }
        public override CatPrecise ToPrecise()
        {
            return new CatPrecise(Value+"");
        }
        
        public static CatLong operator +(CatLong a, CatLong b)
        {
            return new CatLong(a.Value + b.Value);
        }

        public static CatLong operator -(CatLong a, CatLong b)
        {
            return new CatLong(a.Value -b.Value);
        }

        public static CatLong operator *(CatLong a, CatLong b)
        {
            return new CatLong(a.Value * b.Value);
        }

        public static CatLong operator /(CatLong a, CatLong b)
        {
            return new CatLong(a.Value / b.Value);
        }

        public static CatLong operator %(CatLong a, CatLong b)
        {
            return new CatLong(a.Value % b.Value);
        }

        public static CatLong operator ^(CatLong a, CatLong b)
        {
            return new CatLong(a.Value ^ b.Value);
        }

        public static CatLong operator |(CatLong a, CatLong b)
        {
            return new CatLong(a.Value | b.Value);
        }

        public static CatLong operator &(CatLong a, CatLong b)
        {
            return new CatLong(a.Value & b.Value);
        }

        public static bool operator >(CatLong a, CatLong b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(CatLong a, CatLong b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >=(CatLong a, CatLong b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(CatLong a, CatLong b)
        {
            return a.Value <= b.Value;
        }
    }
}