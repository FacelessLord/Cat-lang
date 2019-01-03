using Cat.Structure;

namespace Cat.Primitives
{
    public class CatFloat : CatPrimitiveObject
    {
        public float _value;
        public CatFloat(object value) : base("float")
        {
            switch (value)
            {
                case string s:
                    _value = float.Parse(s);
                    break;
                case CatFloat b:
                    _value = b._value;
                    break;
                default:
                    _value = (float) value;
                    break;
            }
        }

        public override string ToString()
        {
            return _value+"";
        }
    }
}