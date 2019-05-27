using System;
using Cat.AbstractStructure;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatBool : CatPrimitiveObject
    {
        public bool Value;
        public CatBool(object value) : base("bool")
        {
            switch (value)
            {
                case bool s:
                    Value = s;
                    break;
                case CatAngle b:
                    value = b.Radians;
                    break;
                default:
                    Value = !(value is null);
                    break;
            }
        }

        public override string ToString()
        {
            return Value+"";
        }
        
        public override CatStructureObject GetFieldValue(string field)
        {
            switch (field)
            {
                case "not": return new CatBool(!Value);
                case "hashCode": return new CatInt(GetHashCode());
            }

            return base.GetFieldValue(field);
        }

        public override bool HasField(string field)
        {
            switch (field)
            {
                case "not":
                case "hashCode": return true;
            }

            return base.HasField(field);
        }
        public override CatInt ToInt()
        {
            return new CatInt(Value);
        }
        public override CatDouble ToDouble()
        {
            return new CatDouble(Value);
        }
    }
}