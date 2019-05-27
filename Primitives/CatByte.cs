using System;
using Cat.AbstractStructure;
using Cat.Primitives.Precise;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatByte : CatNumber
    {
        public byte Value;
        public CatByte(object value) : base("byte")
        {
            switch (value)
            {
                case string s:
                    Value = byte.Parse(s);
                    break;
                case CatByte b:
                    Value = b.Value;
                    break;
                default:
                    Value = (byte) value;
                    break;
            }
        }

        public override CatStructureObject GetFieldValue(string field)
        {
            switch (field)
            {
                case "hashCode": return new CatInt(GetHashCode());
            }

            return base.GetFieldValue(field);
        }

        public override bool HasField(string field)
        {
            switch (field)
            {
                case "hashCode": return true;
            }

            return base.HasField(field);
        }

        public override bool IsBiggerThan(Type b)
        {
            return b == typeof(CatByte);
        }

        public override string ToString()
        {
            return Value+"";
        }
        public override CatNumber CastTo(Type b)
        {
            switch (b.Name)
            {
                case "CatByte":
                    return this; 
                case "CatInt":
                    return ToInt(); 
                case "CatLong":
                    return ToLong(); 
                case "CatFloat":
                    return ToFloat(); 
                case "CatDouble":
                    return ToDouble(); 
                case "CatPrecise":
                    return ToPrecise(); 
            }

            throw new InvalidCastException();
        }

        public override CatInt ToInt()
        {
            return new CatInt(Value);
        }
        public override CatLong ToLong()
        {
            return new CatLong(Value);
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
        
        public static CatByte operator +(CatByte a, CatByte b)
        {
            return new CatByte(a.Value + b.Value);
        }

        public static CatByte operator -(CatByte a, CatByte b)
        {
            return new CatByte(a.Value -b.Value);
        }

        public static CatByte operator *(CatByte a, CatByte b)
        {
            return new CatByte(a.Value * b.Value);
        }

        public static CatByte operator /(CatByte a, CatByte b)
        {
            return new CatByte(a.Value / b.Value);
        }

        public static CatByte operator %(CatByte a, CatByte b)
        {
            return new CatByte(a.Value % b.Value);
        }

        public static CatByte operator ^(CatByte a, CatByte b)
        {
            return new CatByte(a.Value ^ b.Value);
        }

        public static CatByte operator |(CatByte a, CatByte b)
        {
            return new CatByte(a.Value | b.Value);
        }

        public static CatByte operator &(CatByte a, CatByte b)
        {
            return new CatByte(a.Value & b.Value);
        }

        public static bool operator >(CatByte a, CatByte b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(CatByte a, CatByte b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >=(CatByte a, CatByte b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(CatByte a, CatByte b)
        {
            return a.Value <= b.Value;
        }
    }
}