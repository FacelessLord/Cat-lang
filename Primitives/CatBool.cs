using Cat.Structure;

namespace Cat.Primitives
{
    public class CatBool : CatPrimitiveObject
    {
        public bool _value;
        public CatBool(object value) : base("bool")
        {
            switch (value)
            {
                case bool s:
                    _value = s;
                    break;
                case CatAngle b:
                    value = b._radians;
                    break;
                default:
                    _value = !(value is null);
                    break;
            }
        }

        public override string ToString()
        {
            return _value+"";
        }
    }
}