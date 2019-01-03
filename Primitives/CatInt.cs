using Cat.Structure;

namespace Cat.Primitives
{
    public class CatInt : CatPrimitiveObject
    {
        public int _value;
        public CatInt(object value) : base("int")
        {
            switch (value)
            {
                case string s:
                    _value = int.Parse(s);
                    break;
                case CatInt b:
                    _value = b._value;
                    break;
                default:
                    _value = (int) value;
                    break;
            }
        }

        public override string ToString()
        {
            return _value+"";
        }
    }
}