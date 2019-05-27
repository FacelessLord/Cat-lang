using Cat.AbstractStructure;

namespace Cat.Primitives
{
    public class CatLink : CatInt
    {
        public CatLink(object value) : base(value)
        {
            Type = "link";
        }

        public override string ToString()
        {
            return "#" + base.ToString();
        }
    }
}