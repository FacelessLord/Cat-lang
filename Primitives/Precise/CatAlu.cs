using System;
using System.Collections.Generic;

namespace Cat.Primitives.Precise
{
    public static class CatAlu
    {
        public static CatPrecise Sum(CatPrecise a, CatPrecise b)
        {
            while (a._order < b._order)
            {
                a._digits.Insert(0, '0');
                a._order++;
            }

            while (a._order > b._order)
            {
                b._digits.Insert(0, '0');
                b._order++;
            }
            int asign = a._lessThanZero ? -1 : 1;
            int bsign = b._lessThanZero ? -1 : 1;
            var (lastOverflowSign,lastOverflow,result) = SumCharLists(asign, a._digits, bsign, b._digits);

            int ord = a._order;

            if (lastOverflowSign < 0)
            {
                List<char> overflownNumber = new List<char>();
                overflownNumber.Add(lastOverflow);
                for (var j = 0; j < result.Count-1; j++)
                {
                    overflownNumber.Add('0');
                }

                var overflowSum = SumCharLists(-1, overflownNumber, 1, result);
                result = overflowSum.result;
            }
            else
            {
                if (lastOverflow != '0')
                {
                    result.Insert(0, lastOverflow);
                    ord++;
                }
            }

            List<char> period = new List<char>();
            if (a._havePeriod || b._havePeriod)
            {
                var (aPeriodList,bPeriodList) = CreateTwoPeriods(a, b);

                var periodSum = SumCharLists(asign, aPeriodList, bsign, bPeriodList);
                List<char> overflownNumber = new List<char>();
                overflownNumber.Add(periodSum.overflow);
                for (var j = 0; j < result.Count-1; j++)
                {
                    overflownNumber.Add('0');
                }
                period = periodSum.result;
                var overflowSum = SumCharLists(periodSum.sign, overflownNumber, 1, result);
                lastOverflowSign = overflowSum.sign;
                result = overflowSum.result;
            }
            
            
            a.Validate();
            b.Validate();
            var resultPrecise = new CatPrecise(0)
            {
                _digits = result, _lessThanZero = lastOverflowSign < 0, _order = ord
            };
            if (period.Count > 0)
            {
                resultPrecise.Period = period;
            }
            resultPrecise.Validate();
            return resultPrecise;
        }

        public static (List<char> a, List<char> b) CreateTwoPeriods(CatPrecise a, CatPrecise b)
        {
            var aPeriod = new List<char>();
            var bPeriod = new List<char>();
            
            if (a._havePeriod)
            {
                if (b._havePeriod)
                {
                    for (var ai = 0; ai < b._period.Count; ai++)
                    {
                        aPeriod.AddRange(a._period);
                    }
                    for (var bi = 0; bi < a._period.Count; bi++)
                    {
                        bPeriod.AddRange(b._period);
                    }
                }
                else
                {
                    aPeriod = a._period;
                        
                    for (var bi = 0; bi < a._period.Count; bi++)
                    {
                        bPeriod.Add('0');
                    }
                }
            }
            else
            {
                bPeriod = b._period;

                for (var bi = 0; bi < b._period.Count; bi++)
                {
                    aPeriod.Add('0');
                }
            }
            aPeriod.Reverse();
            bPeriod.Reverse();

            return (aPeriod, bPeriod);
        }

        public static (int sign, char overflow, List<char> result) SumCharLists(int asign, List<char> a, int bsign,
            List<char> b)
        {
            var result = new List<char>();
            var lastOverflow = '0';
            var lastOverflowSign = 1;
            var i = a.Count - 1;
            a.Reverse();
            b.Reverse();
            while (i >= 0)
            {
                var (osign, overflow, rsign, res) = DigitSum(asign, a[i], bsign, b[i]);
                var overSum = DigitSum(rsign, res, lastOverflowSign, lastOverflow); //overSum.overflow == '0'
                lastOverflow = overflow;
                lastOverflowSign = osign;
                result.Add(overSum.result);
                i--;
            }

            a.Reverse();
            b.Reverse();
            result.Reverse();
            return (lastOverflowSign, lastOverflow, result);
        }

        public static int periodLength = 50;

        public static CatPrecise Multiply(CatPrecise a, CatPrecise b)
        {
            var dIndex = a._digits.Count - a._order + b._digits.Count - b._order+(a._havePeriod ? periodLength : 0)+(b._havePeriod ? periodLength : 0);
            int aSign = a._lessThanZero ? -1 : 1;
            int bSign = b._lessThanZero ? -1 : 1;
            var sign = aSign * bSign;
            var resInts = new int[a._digits.Count +(a._havePeriod ? periodLength : 0)+ b._digits.Count + 1+(b._havePeriod ? periodLength : 0)];
            for (var i = 0; i < a._digits.Count + (a._havePeriod ? periodLength : 0); i++)
            for (var j = 0; j < b._digits.Count + (b._havePeriod ? periodLength : 0); j++)
            {
                var (overflow, res) = DigitIntMult(a[a._digits.Count - 1 - i+(a._havePeriod ? periodLength : 0)], b[b._digits.Count - 1 - j+(b._havePeriod ? periodLength : 0)]);
                resInts[i + j] += res;
                resInts[i + j + 1] += overflow;
            }

            for (int i = 0; i < resInts.Length; i++)
            {
                var resInt = resInts[i];
                var res = resInt % 10;
                var overflow = resInt / 10;
                resInts[i] = res;
                if (overflow > 0)
                    resInts[i + 1] += overflow;
            }

            var result = new List<char>();
            for (int i = resInts.Length - 1; i >= 0; i--)
            {
                var dig = GetDigit(resInts[i]);
                result.Add(dig);
            }

            var cutLength = result.Count;
            if (a._havePeriod)
            {
                cutLength -= periodLength / 2;
            }
            if (b._havePeriod)
            {
                cutLength -= periodLength / 2;
            }

            var oldLength = result.Count;
            result = result.GetRange(0, cutLength);

            var resultPrecise = new CatPrecise(0)
                {_digits = result, _lessThanZero = sign < 0, _order = oldLength - dIndex};
            resultPrecise.Validate();
            return resultPrecise;
        }

        public static CatPrecise Divide(CatPrecise a, CatPrecise b)
        {
            return null;
        }


        public static int Abs(int a)
        {
            return a >= 0 ? a : -a;
        }
        public static int Value(char a)
        {
            return a-'0';
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


        public static (int osign,char overflow,int rsign, char result) DigitSum(int asign, char a,int bsign, char b)
        {
            int res;
            char resChr;
            int rsign;
            int aVal = Value(a);
            int bVal = Value(b);
            
            if (Abs(asign*aVal + bsign*bVal) > 9)
            {
                int osign = 1;
                rsign = 1;
                int ov = Abs(asign * aVal + bsign * bVal) / 10;
                res = Abs(asign*aVal + bsign*bVal) % 10;
                if (asign * aVal + bsign * bVal < 0)
                {
                    osign = -1;
                }
                if (asign*aVal + bsign*bVal < 0)
                {
                    rsign = -1;
                    res = -res;
                }
                char ovChr = GetDigit(ov);
                resChr = GetDigit(res);
                return (osign,ovChr,rsign,resChr);
            }

            res = Abs(asign * aVal + bsign * bVal);
            rsign = 1;
            if (res < 0)
            {
                rsign = -1;
                res = -res;
            }
            resChr = GetDigit(res);
            return (1,'0',rsign,resChr);
        }
        
        public static (int overflow, int result) DigitIntMult( char a, char b)
        {
            int aVal = Value(a);
            int bVal = Value(b);

            var prod = aVal * bVal ;
            var res = prod % 10;
            var overf = prod / 10;
            return (overf,res);
        }
    }
}