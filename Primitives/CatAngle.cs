using Cat.Structure;

namespace Cat.Primitives
{
    public class CatAngle : CatPrimitiveObject
    {
        public double radians;
        
        public CatAngle(object o) : base("angle")
        {
            var value = 0d;
            switch (o)
            {
                case double d:
                    value = d;
                    break;
                case decimal dec:
                    value = (double)dec;
                    break;
                case float f:
                    value = f;
                    break;
                case long l:
                    value = l;
                    break;
                case ulong ul:
                    value = ul;
                    break;
                case int _:
                case byte _:
                    value = (int)o;
                    break;
                case uint ui:
                    value = ui;
                    break;
                default:
                    value = 0;
                    break;
            }
            this.radians = value;
        }
    }
}