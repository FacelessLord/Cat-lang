using System;
using Cat.AbstractStructure;
using Cat.Primitives.Precise;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatFloat : CatNumber
    {
        public float Value;
        public CatFloat(object value) : base("float")
        {
            switch (value)
            {
                case string s:
                    Value = float.Parse(s);
                    break;
                case CatFloat b:
                    Value = b.Value;
                    break;
                default:
                    Value = (float) value;
                    break;
            }
        }

        public override string ToString()
        {
            return Value + "";
        }

        public override CatStructureObject GetFieldValue(string field)
        {
            var baseRet = base.GetFieldValue(field);
            switch (field)
            {
                case "abs": return new CatFloat(Math.Abs(Value));
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
            return b == typeof(CatByte) || b == typeof(CatInt) || b == typeof(CatLong);
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
                    return this; 
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
            return new CatLong(Value);
        }
        public override CatFloat ToFloat()
        {
            return this;
        }
        public override CatDouble ToDouble()
        {
            return new CatDouble(Value);
        }
        public override CatPrecise ToPrecise()
        {
            return new CatPrecise(Value+"");
        }
        
        public static CatFloat operator +(CatFloat a, CatFloat b)
        {
            return new CatFloat(a.Value + b.Value);
        }

        public static CatFloat operator -(CatFloat a, CatFloat b)
        {
            return new CatFloat(a.Value -b.Value);
        }

        public static CatFloat operator *(CatFloat a, CatFloat b)
        {
            return new CatFloat(a.Value * b.Value);
        }

        public static CatFloat operator /(CatFloat a, CatFloat b)
        {
            return new CatFloat(a.Value / b.Value);
        }

        public static CatFloat operator %(CatFloat a, CatFloat b)
        {
            return new CatFloat(a.Value % b.Value);
        }

        public static bool operator >(CatFloat a, CatFloat b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(CatFloat a, CatFloat b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >=(CatFloat a, CatFloat b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(CatFloat a, CatFloat b)
        {
            return a.Value <= b.Value;
        }
    }
}