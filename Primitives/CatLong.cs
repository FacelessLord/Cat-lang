using Cat.Structure;

namespace Cat.Primitives
{
    public class CatLong : CatPrimitiveObject
    {
        public long _value;
        public CatLong(object value) : base("long")
        {
            switch (value)
            {
                case string s:
                    _value = long.Parse(s);
                    break;
                case CatLong b:
                    _value = b._value;
                    break;
                default:
                    _value = (long) value;
                    break;
            }
        }

        public override string ToString()
        {
            return _value+"";
        }
    }
}