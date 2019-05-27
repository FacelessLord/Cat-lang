using Cat.AbstractStructure;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatNull : CatStructureObject
    {
        public CatNull() : base("null", 0)
        {
        }
        
        public static readonly CatNull Null = new CatNull();

        public override string ToString()
        {
            return "null";
        }

        public override CatStructureObject GetFieldValue(string field)
        {
            return this;
        }

        public override bool HasField(string field)
        {
            return true;
        }

        public override CatInt ToInt()
        {
            return new CatInt(0);
        }
        public override CatDouble ToDouble()
        {
            return new CatDouble(0);
        }
    }
}