using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CalculatorConsole;
using Cat.Structure;
using Cat.Utilities;

namespace Cat.Primitives.Precise
{
    /// <inheritdoc />
    /// <summary>
    /// Representation of a number that have valuable decimal digits 
    /// </summary>
    public class CatPrecise : CatPrimitiveObject
    {
        public static CatPrecise Pi = new CatPrecise("3.1415926535 8979323846 2643383279 5028841971 6939937510 5820974944 5923078164 0628620899 8628034825 3421170679 8214808651 3282306647 0938446095 5058223172 5359408128 4811174502 8410270193 8521105559");
        public static CatPrecise Tau = Pi * 2;
        public static CatPrecise E = new CatPrecise("2.7182818284 5904523536 0287471352 6624977572 4709369995 9574966967 6277240766 3035354759 4571382178 5251664274 2746639193 2003059921 8174135966 2904357290 0334295260 5956307381 3232862794 3490763233");
        
        public List<char> _digits = new List<char>();

        public bool _havePeriod;
        public List<char> _period = new List<char>();

        public List<char> Period
        {
            get => _period;
            set
            {
                _period = value;
                _havePeriod = !(value is null);
            }
        }

        public char this[int i]
        {
            get
            {
                if(i<_digits.Count)
                    return _digits[i];
                if (_havePeriod)
                    return _period[(i-_digits.Count) % _period.Count];
                return '0';
            }
            set => _digits[i] = value;
        }

        /// <summary>
        /// first {order} digits are for integer part
        /// </summary>
        public int _order = 1;

        public bool _lessThanZero = false;

        public CatPrecise(object value) : base("precise")
        {
            switch (value)
            {
                case 0:
                {
                    _digits.Add('0');
                    _order = 1;
                    break;
                }
                case double d:
                    FromDouble(d);
                    break;
                case decimal dec:
                    FromDouble((double) dec);
                    break;
                case float f:
                    FromDouble(f);
                    break;
                case long l:
                    FromDouble(l);
                    break;
                case ulong ul:
                    FromDouble(ul);
                    break;
                case int _:
                case byte _:
                    FromDouble((int) value);
                    break;
                case uint ui:
                    FromDouble(ui);
                    break;
                case string s:
                    FromString(s);
                    break;
            }
        }

        public static CatPrecise operator +(CatPrecise a, CatPrecise b)
        {
            return CatAlu.Sum(a, b);
        }

        public static CatPrecise operator -(CatPrecise a, CatPrecise b)
        {
            return CatAlu.Sum(a, -b);
        }

        public static CatPrecise operator +(double a, CatPrecise b)
        {
            return CatAlu.Sum(new CatPrecise(a + ""), b);
        }

        public static CatPrecise operator +(CatPrecise a, double b)
        {
            return CatAlu.Sum(a, new CatPrecise(b + ""));
        }

        public static CatPrecise operator -(double a, CatPrecise b)
        {
            return CatAlu.Sum(new CatPrecise(a + ""), -b);
        }

        public static CatPrecise operator -(CatPrecise a, double b)
        {
            return CatAlu.Sum(a, new CatPrecise("" + -b));
        }

        public static CatPrecise operator -(CatPrecise a)
        {
            var ta = (CatPrecise) a.MemberwiseClone();
            ta._lessThanZero = !ta._lessThanZero;
            return ta;
        }

        public static CatPrecise operator *(CatPrecise a, CatPrecise b)
        {
            return CatAlu.Multiply(a, b);
        }

        public static CatPrecise operator *(double a, CatPrecise b)
        {
            return CatAlu.Multiply(new CatPrecise(a + ""), b);
        }

        public static CatPrecise operator *(CatPrecise a, double b)
        {
            return CatAlu.Multiply(a, new CatPrecise(b + ""));
        }

        public static CatPrecise operator !(CatPrecise a)
        {
            var ta = (CatPrecise) a.MemberwiseClone();
            ta._lessThanZero = false;
            return ta;
        }

        public static bool operator <=(CatPrecise a, CatPrecise b)
        {
            return (a < b) || (a == b);
        }

        public static bool operator >=(CatPrecise a, CatPrecise b)
        {
            return (a > b) || (a == b);
        }

        public static bool operator <(CatPrecise a, CatPrecise b)
        {
            if (a._order > b._order || a._digits.Count > b._digits.Count)
            {
                return false;
            }

            if (a._order < b._order || a._digits.Count < b._digits.Count)
            {
                return true;
            }

            int i = 0;
            while (i < a._digits.Count+1 && a[i] == b[i])
            {
                i++;
            }

            if (i < a._digits.Count) //didn't went to the end
            {
                return a[i] < b[i];
            }

            return a[i - 1] < b[i - 1];
        }

        public static bool operator >(CatPrecise a, CatPrecise b)
        {
            if (a._order < b._order || a._digits.Count < b._digits.Count)
            {
                return false;
            }

            if (a._order > b._order || a._digits.Count > b._digits.Count)
            {
                return true;
            }

            int i = 0;
            while (i < a._digits.Count+1 && a[i] == b[i])
            {
                i++;
            }

            if (i < a._digits.Count) //didn't went to the end
            {
                return a[i] > b[i];
            }

            return a[i - 1] > b[i - 1];
        }

        public static bool operator ==(CatPrecise a, CatPrecise b)
        {
            if (a is null)
            {
                return b is null;
            }

            if (b is null)
            {
                return true;
            }

            if (a._order != b._order || a._digits.Count != b._digits.Count)
            {
                return false;
            }

            var i = 0;
            while (i < a._digits.Count && a._digits[i] == b._digits[i])
            {
                i++;
            }

            return i == a._digits.Count;
        }

        public static bool operator !=(CatPrecise a, CatPrecise b)
        {
            return !(a == b);
        }


        public void Validate()
        {
            while (_digits[0] == '0' && _order > 1)
            {
                _digits.RemoveAt(0);
                _order--;
            }

            while (_digits.Count > 0 && _digits[_digits.Count - 1] == '0' && _digits.Count > _order)
            {
                _digits.RemoveAt(_digits.Count - 1);
            }
        }

        public void FromString(string s)
        {
            s = s.Replace(" ", "");
            _lessThanZero = s.StartsWith("-");

            if (s.StartsWith("-") || s.StartsWith("+"))
            {
                s = s.Substring(1);
            }

            var pointIndex = s.IndexOf(".", StringComparison.Ordinal);
            if (s.EndsWith("p"))
            {
                s = s.Substring(0, s.Length - 1);
            }

            if (pointIndex == -1)
            {
                pointIndex = s.Length;
            }

            var intPtStr = s.Substring(0, pointIndex);
            var decPtStr = s.Substring(Math.Min(pointIndex + 1, s.Length));

            for (var i = 0; i < intPtStr.Length; i++)
            {
                _digits.Add(intPtStr[i]);
                CheckDigit(intPtStr[i], s);
            }

            _order = intPtStr.Length;
            if (_order == 0)
            {
                _order = 1;
                _digits.Add('0');
            }

            for (var i = 0; i < decPtStr.Length; i++)
            {
                _digits.Add(decPtStr[i]);
                CheckDigit(decPtStr[i], s);
            }
        }

        public void FromDouble(double d)
        {
            var ad = d;
            if (ad < 0)
            {
                ad = -ad;
            }

            int ord = 0;
            while (ad > 10)
            {
                ad /= 10;
                ord++;
            }

            while (ad < 1)
            {
                ad *= 10;
                ord--;
            }

            while (ord < 0)
            {
                _digits.Add('0');
                ord++;
            }

            while (ad > 0)
            {
                _digits.Add(GetDigit(ad % 10));
                ad -= (int) ad % 10;
                ad *= 10;
            }

            _lessThanZero = d < 0;
        }

        public override string ToString()
        {
            var sgn = (_lessThanZero ? "-" : "");
            var intPt = new StringBuilder();
            var decPt = new StringBuilder();
            for (var i = 0; i < _order; i++)
            {
                intPt.Append(this[i]);
            }

            for (var i = _order; i < _digits.Count; i++)
            {
                decPt.Append(this[i]);
            }
            var periodStr = new StringBuilder();
            if (_havePeriod)
            {
                periodStr.Append($"({MiscUtils.ArrayToString(_period.ToArray(),delimiter:"")})");
            }

            return sgn + intPt.Append((decPt.Length > 0 || periodStr.Length > 0 ? (".") : "") + decPt + periodStr);
        }
        
        public string WithDigits(int num)
        {
            var sgn = (_lessThanZero ? "-" : "");
            var intPt = new StringBuilder();
            var decPt = new StringBuilder();
            for (var i = 0; i < _order; i++)
            {
                intPt.Append(this[i]);
            }

            for (var i = _order; i < _order+num; i++)
            {
                decPt.Append(this[i]);
            }
            var periodStr = new StringBuilder();

            return sgn + intPt.Append(decPt.Length > 0 ? ("." + decPt) : "")+periodStr;
        }

        public string ToExponentialForm(int precision = 8)
        {
            int mantStart = _order;
            while (this[_order - mantStart] == '0')
            {
                mantStart--;
            }

            var res = new StringBuilder();
            res.Append(this[_order - mantStart]);
            res.Append('.');

            for (var i = 1; i < precision; i++)
            {
                res.Append(this[_order - mantStart + i]);
            }

            while (res[res.Length - 1] == '0')
            {
                res.Remove(res.Length - 1, 1);
            }

            var order = mantStart - 1;
            return res.Append("E" + order).ToString();
        }

        public static char GetDigit(double num)
        {
            switch ((int) num)
            {
                case 0: return '0';
                case 1: return '1';
                case 2: return '2';
                case 3: return '3';
                case 4: return '4';
                case 5: return '5';
                case 6: return '6';
                case 7: return '7';
                case 8: return '8';
                case 9: return '9';
                default: throw new ArgumentException();
            }
        }

        public static void CheckDigit(char num, string line)
        {
            switch (num)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9': return;
                default:
                    throw new ArgumentException(
                        $"Invalid character '{num}' met when tried to parse Precise from '{line}'");
            }
        }

        protected bool Equals(CatPrecise other)
        {
            return Equals(_digits, other._digits) && _havePeriod == other._havePeriod &&
                    Equals(_period, other._period) && _order == other._order &&
                   _lessThanZero == other._lessThanZero;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((CatPrecise) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_digits != null ? _digits.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _havePeriod.GetHashCode();
                hashCode = (hashCode * 397) ^ (_period != null ? _period.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _order;
                hashCode = (hashCode * 397) ^ _lessThanZero.GetHashCode();
                return hashCode;
            }
        }
    }
}