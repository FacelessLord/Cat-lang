using Cat.Structure;

namespace Cat.Primitives
{
    public class CatDouble : CatPrimitiveObject
    {
        public double _value;
        public CatDouble(object value) : base("double")
        {
            switch (value)
            {
                case string s:
                    _value = double.Parse(s);
                    break;
                case CatDouble b:
                    _value = b._value;
                    break;
                default:
                    _value = (double) value;
                    break;
            }
        }

        public override string ToString()
        {
            return _value+"";
        }
    }
}