using Cat.AbstractStructure;
using Cat.Structure;
using Cat.Utilities;

namespace Cat.Primitives
{
    public class CatArray : CatPrimitiveObject
    {
        public CatStructureObject[] _value;
        public CatArray(params CatStructureObject[] value) : base("array")
        {
            _value = value;
        }
        
        public override string ToString()
        {
            return MiscUtils.ArrayToString(_value,begin:"[",end:"]");
        }

        public CatStructureObject this[int i]
        {
            get => _value[i];
            set => _value[i] = value;
        }

        public int Length => _value.Length;
    
    }
}