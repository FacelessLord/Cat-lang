using Cat.Structure;

namespace Cat.Primitives
{
    public class CatString : CatPrimitiveObject
    {
        public string _value;
        public CatString(object value) : base("string")
        {
            switch (value)
            {
                case string s:
                    _value = s;
                    break;
                case CatString b:
                    _value = b._value;
                    break;
                default:
                    _value = "" + value;
                    break;
            }
        }

        public override string ToString()
        {
            return "\""+_value + "\"";
        }
    }
}