using System;
using Cat.AbstractStructure;
using Cat.Primitives.Precise;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatInt : CatNumber
    {
        public int Value;
        public CatInt(object value) : base("int")
        {
            Value = 0;
            switch (value)
            {
                case string s:
                    Value = int.Parse(s);
                    break;
                case CatInt b:
                    Value = b.Value;
                    break;
                default:
                    Value = (int) value;
                    break;
            }
        }

        public override string ToString()
        {
            return Value+"";
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
            return b == typeof(CatByte);
        }
        
        public override CatNumber CastTo(Type b)
        {
            switch (b.Name)
            {
                case "CatByte":
                    return ToByte(); 
                case "CatInt":
                    return this; 
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
        public override CatByte ToByte()
        {
            return new CatByte(Value);
        }
        public override CatInt ToInt()
        {
            return this;
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
        
        public static CatInt operator +(CatInt a, CatInt b)
        {
            return new CatInt(a.Value + b.Value);
        }

        public static CatInt operator -(CatInt a, CatInt b)
        {
            return new CatInt(a.Value -b.Value);
        }

        public static CatInt operator *(CatInt a, CatInt b)
        {
            return new CatInt(a.Value * b.Value);
        }

        public static CatInt operator /(CatInt a, CatInt b)
        {
            return new CatInt(a.Value / b.Value);
        }

        public static CatInt operator %(CatInt a, CatInt b)
        {
            return new CatInt(a.Value % b.Value);
        }

        public static CatInt operator ^(CatInt a, CatInt b)
        {
            return new CatInt(a.Value ^ b.Value);
        }

        public static CatInt operator |(CatInt a, CatInt b)
        {
            return new CatInt(a.Value | b.Value);
        }

        public static CatInt operator &(CatInt a, CatInt b)
        {
            return new CatInt(a.Value & b.Value);
        }

        public static bool operator >(CatInt a, CatInt b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(CatInt a, CatInt b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >=(CatInt a, CatInt b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(CatInt a, CatInt b)
        {
            return a.Value <= b.Value;
        }
    }
}