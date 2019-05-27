using System;
using Cat.AbstractStructure;
using Cat.Primitives.Precise;
using Cat.Structure;
using Cat.Utilities;

namespace Cat.Primitives
{
    public class CatAngle : CatPrimitiveObject
    {
        public double Radians;

        public CatAngle(object o) : base("angle")
        {
            var value = 0d;
            switch (o)
            {
                case CatAngle b:
                    value = b.Radians;
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
                    else if (s.EndsWith("grad"))
                    {
                        value = (Math.PI / 200) * double.Parse(s.Substring(0, s.Length - 4));
                    }
                    else if (s.EndsWith("r"))
                    {
                        value = double.Parse(s.Substring(0, s.Length - 1));
                    }
                    else if (s.EndsWith("rad"))
                    {
                        value = double.Parse(s.Substring(0, s.Length - 3));
                    }
                    else if (s.EndsWith("deg"))
                    {
                        value = (Math.PI / 180) * double.Parse(s.Substring(0, s.Length - 3));
                    }
                    else
                    {
                        throw new ArgumentException("Didn't specified angle measurement units");
                    }

                    break;
                }
                default:
                    value = 0;
                    break;
            }

            Radians = value;
        }


        public override CatStructureObject GetFieldValue(string field)
        {
            var baseRet = base.GetFieldValue(field);
            switch (field)
            {
                case "radians": return new CatDouble(Radians);
                case "degrees": return new CatDouble(Radians*180/Math.PI);
                case "grads": return new CatDouble(Radians*200/Math.PI);
                case "radiansD": return new CatDouble(Radians);
                case "radiansF": return new CatFloat(Radians);
                case "radiansP": return new CatPrecise(Radians+"");
                case "degreesD": return new CatDouble(Radians*180/Math.PI);
                case "degreesF": return new CatFloat(Radians*180/Math.PI);
                case "degreesP": return new CatPrecise(Radians*180/Math.PI+"");
                case "gradsD": return new CatDouble(Radians*200/Math.PI);
                case "gradsF": return new CatFloat(Radians*200/Math.PI);
                case "gradsP": return new CatPrecise(Radians*200/Math.PI+"");
                case "hashCode": return new CatInt(GetHashCode());
            }

            return baseRet;
        }
        public override bool HasField(string field)
        {
            switch (field)
            {
                case "radians":
                case "degrees":
                case "grads":
                case "radiansD":
                case "radiansF":
                case "radiansP":
                case "degreesD":
                case "degreesF":
                case "degreesP":
                case "gradsD":
                case "gradsF":
                case "gradsP":
                case "hashCode": return true;
            }

            return base.HasField(field);
        }


        public override string ToString()
        {
            return Radians+"rad";
        }
        public override CatInt ToInt()
        {
            return new CatInt(Radians);
        }
        public override CatDouble ToDouble()
        {
            return new CatDouble(Radians);
        }
    }
}