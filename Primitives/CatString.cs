using System;
using Cat.AbstractStructure;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatString : CatPrimitiveObject
    {
        public string Value;

        public CatString(object value) : base("string")
        {
            switch (value)
            {
                case string s:
                    Value = s;
                    break;
                case CatString b:
                    Value = b.Value;
                    break;
                default:
                    Value = "" + value;
                    break;
            }
        }
        
        public CatStructureObject this[int i]
        {
            get => new CatString(Value[i]);
        }
        
        public CatStructureObject this[byte i]
        {
            get => new CatString(Value[i]);
        }

        public override string ToString()
        {
            return "\"" + Value + "\"";
        }

        public override CatStructureObject GetFieldValue(string field)
        {
            //Console.WriteLine("StrField");
            switch (field)
            {
                case "length": return new CatInt(Value.Length);
                case "hashCode": return new CatInt(GetHashCode());
            }

            var baseRet = base.GetFieldValue(field);

            return baseRet;
        }

        public override bool HasField(string field)
        {
            switch (field)
            {
                case "length":
                case "hashCode": return true;
            }

            return base.HasField(field);
        }
    }
}