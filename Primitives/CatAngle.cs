using System;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatAngle : CatPrimitiveObject
    {
        public double _radians;

        public CatAngle(object o) : base("angle")
        {
            var value = 0d;
            switch (o)
            {
                case CatAngle b:
                    value = b._radians;
                    break;
                case double d:
                    value = d;
                    break;
                case decimal dec:
                    value = (double) dec;
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
                    value = (int) o;
                    break;
                case uint ui:
                    value = ui;
                    break;
                case string s:
                {
                    if (s.EndsWith("g"))
                    {
                        value = (Math.PI / 200) * double.Parse(s.Substring(0, s.Length - 1));
                    }

                    if (s.EndsWith("grad"))
                    {
                        value = (Math.PI / 200) * double.Parse(s.Substring(0, s.Length - 4));
                    }

                    if (s.EndsWith("r"))
                    {
                        value = double.Parse(s.Substring(0, s.Length - 1));
                    }

                    if (s.EndsWith("rad"))
                    {
                        value = double.Parse(s.Substring(0, s.Length - 3));
                    }

                    if (s.EndsWith("deg"))
                    {
                        value = (Math.PI / 180) * double.Parse(s.Substring(0, s.Length - 3));
                    }

                    break;
                }
                default:
                    value = 0;
                    break;
            }

            _radians = value;
        }

        public override string ToString()
        {
            return _radians+"rad";
        }
    }
}