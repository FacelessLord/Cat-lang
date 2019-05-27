using System;
using System.Linq;
using Cat.AbstractStructure;
using Cat.Structure;
using Cat.Utilities;

namespace Cat.Primitives
{
    public class CatArray : CatPrimitiveObject
    {
        public CatStructureObject[] Value;
        public CatArray(params CatStructureObject[] value) : base("array")
        {
            Value = value;
        }
        
        public override string ToString()
        {
            return MiscUtils.ArrayToString(Value,begin:"[",end:"]");
        }

        public CatStructureObject this[int i]
        {
            get => Value[i];
            set => Value[i] = value;
        }
        
        public CatStructureObject this[long i]
        {
            get => Value[i];
            set => Value[i] = value;
        }
        public CatStructureObject this[byte i]
        {
            get => Value[i];
            set => Value[i] = value;
        }
        
        Random _random=new Random();
        
        public override CatStructureObject GetFieldValue(string field)
        {
            var index = -1;
            if (int.TryParse(field, out index))
            {
                return Value[index];
            }
            var baseRet = base.GetFieldValue(field);
            switch (field)
            {
                case "length": return new CatInt(Value.Length);
                case "hashCode": return new CatInt(GetHashCode());
                case "shuffled": return new CatArray(Value.OrderBy(x => _random.Next()).ToArray());
            }

            return baseRet;
        }
        public override bool HasField(string field)
        {
            switch (field)
            {
                case "length":
                case "shuffled":
                case "hashCode": return true;
            }

            return base.HasField(field);
        }

        public int Length => Value.Length;
    }
}