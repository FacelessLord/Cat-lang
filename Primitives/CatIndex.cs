using System;
using Cat.AbstractStructure;

namespace Cat.Primitives
{
    public class CatIndex : CatNumber
    {
        public CatRange Range;

        public CatIndex(CatRange range) : base("index")
        {
            Range = range;
        }

        public override bool HasField(string field)
        {
            if (field == "range")
                return true;
            return base.HasField(field);
        }

        public override CatStructureObject GetFieldValue(string field)
        {
            if (field == "range")
                return Range;
            return base.GetFieldValue(field);
        }

        public override bool IsBiggerThan(Type b)
        {
            return Evaluate().IsBiggerThan(b);
        }

        public override CatNumber CastTo(Type b)
        {
            switch (b.Name)
            {
                case "CatByte":
                    return Evaluate().ToByte();
                case "CatInt":
                    return Evaluate().ToInt();
                case "CatLong":
                    return Evaluate().ToLong();
                case "CatFloat":
                    return Evaluate().ToFloat();
                case "CatDouble":
                    return Evaluate().ToDouble();
                case "CatPrecise":
                    return Evaluate().ToPrecise();
            }

            throw new InvalidCastException();
        }

        public CatNumber Evaluate()
        {
            return Range.GetCurrentIterationValue();
        }

        public override string ToString()
        {
            return "i in "+Range;
        }
    }
}