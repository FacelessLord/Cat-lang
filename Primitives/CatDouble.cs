using System;
using Cat.AbstractStructure;
using Cat.Primitives.Precise;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatDouble : CatNumber
    {
        public double Value;
        public CatDouble(object value) : base("double")
        {
            switch (value)
            {
                case string s:
                    Value = double.Parse(s);
                    return;
                case CatDouble b:
                    Value = b.Value;
                    return;
            }
            Value = double.Parse(""+value);
        }

        public override string ToString()
        {
            return Value+"";
        }

        public override CatStructureObject GetFieldValue(string field)
        {
            switch (field)
            {
                case "abs": return new CatDouble(Math.Abs(Value));
                case "hashCode": return new CatInt(GetHashCode());
            }

            return base.GetFieldValue(field);
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
            return b == typeof(CatByte) || b == typeof(CatInt)|| b == typeof(CatLong)|| b == typeof(CatPrecise);
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
                    return ToLong(); 
                case "CatFloat":
                    return ToFloat(); 
                case "CatDouble":
                    return this; 
                case "CatPrecise":
                    return ToPrecise(); 
            }

            throw new InvalidCastException();
        }
        public override CatByte ToByte()
        {
            return new CatByte((byte)Value);
        }
        public override CatInt ToInt()
        {
            return new CatInt((int)Value);
        }
        public override CatLong ToLong()
        {
            return new CatLong((long)Value);
        }
        public override CatFloat ToFloat()
        {
            return new CatFloat(Value);
        }
        public override CatDouble ToDouble()
        {
            return this;
        }
        public override CatPrecise ToPrecise()
        {
            return new CatPrecise(Value+"");
        }
        
        public static CatDouble operator +(CatDouble a, CatDouble b)
        {
            return new CatDouble(a.Value + b.Value);
        }

        public static CatDouble operator -(CatDouble a, CatDouble b)
        {
            return new CatDouble(a.Value -b.Value);
        }

        public static CatDouble operator *(CatDouble a, CatDouble b)
        {
            return new CatDouble(a.Value * b.Value);
        }

        public static CatDouble operator /(CatDouble a, CatDouble b)
        {
            return new CatDouble(a.Value / b.Value);
        }

        public static CatDouble operator %(CatDouble a, CatDouble b)
        {
            return new CatDouble(a.Value % b.Value);
        }

        public static bool operator >(CatDouble a, CatDouble b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(CatDouble a, CatDouble b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >=(CatDouble a, CatDouble b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(CatDouble a, CatDouble b)
        {
            return a.Value <= b.Value;
        }
    }
}