using System;
using System.Collections.Generic;
using System.Numerics;

namespace Cat.Primitives.Precise
{
    public static class CatAlu
    {
        public static CatPrecise Sum(CatPrecise a, CatPrecise b)
        {
            while (a.Order < b.Order)
            {
                a.Digits.Insert(0, '0');
                a.Order++;
            }

            while (a.Order > b.Order)
            {
                b.Digits.Insert(0, '0');
                b.Order++;
            }
            while (a.Digits.Count < b.Digits.Count)
            {
                a.Digits.Add( '0');
            }

            while (a.Digits.Count > b.Digits.Count)
            {
                b.Digits.Add( '0');
            }
            int asign = a.LessThanZero ? -1 : 1;
            int bsign = b.LessThanZero ? -1 : 1;
            var (lastOverflowSign,lastOverflow,result) = SumCharLists(asign, a.Digits, bsign, b.Digits);

            int ord = a.Order;

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
            if (a.HavePeriod || b.HavePeriod)
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
                Digits = result, LessThanZero = lastOverflowSign < 0, Order = ord
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
            
            if (a.HavePeriod)
            {
                if (b.HavePeriod)
                {
                    for (var ai = 0; ai < b.Period.Count; ai++)
                    {
                        aPeriod.AddRange(a.Period);
                    }
                    for (var bi = 0; bi < a.Period.Count; bi++)
                    {
                        bPeriod.AddRange(b.Period);
                    }
                }
                else
                {
                    aPeriod = a.Period;
                        
                    for (var bi = 0; bi < a.Period.Count; bi++)
                    {
                        bPeriod.Add('0');
                    }
                }
            }
            else
            {
                bPeriod = b.Period;

                for (var bi = 0; bi < b.Period.Count; bi++)
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

        public static int PeriodLength = 50;

        public static CatPrecise Multiply(CatPrecise a, CatPrecise b)
        {
            var dIndex = a.Digits.Count - a.Order + b.Digits.Count - b.Order+(a.HavePeriod ? PeriodLength : 0)+(b.HavePeriod ? PeriodLength : 0);
            int aSign = a.LessThanZero ? -1 : 1;
            int bSign = b.LessThanZero ? -1 : 1;
            var sign = aSign * bSign;
            var resInts = new int[a.Digits.Count +(a.HavePeriod ? PeriodLength : 0)+ b.Digits.Count + 1+(b.HavePeriod ? PeriodLength : 0)];
            for (var i = 0; i < a.Digits.Count + (a.HavePeriod ? PeriodLength : 0); i++)
            for (var j = 0; j < b.Digits.Count + (b.HavePeriod ? PeriodLength : 0); j++)
            {
                var (overflow, res) = DigitIntMult(a[a.Digits.Count - 1 - i+(a.HavePeriod ? PeriodLength : 0)], b[b.Digits.Count - 1 - j+(b.HavePeriod ? PeriodLength : 0)]);
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
            if (a.HavePeriod)
            {
                cutLength -= PeriodLength / 2;
            }
            if (b.HavePeriod)
            {
                cutLength -= PeriodLength / 2;
            }

            var oldLength = result.Count;
            result = result.GetRange(0, cutLength);

            var resultPrecise = new CatPrecise(0)
                {Digits = result, LessThanZero = sign < 0, Order = oldLength - dIndex};
            resultPrecise.Validate();
            return resultPrecise;
        }

        public static CatPrecise Divide(CatPrecise a, CatPrecise b)
        {
            var bia = BigInteger.Parse(a.ToString().Replace(".", "")) * BigInteger.Parse("100000000000000000000");
            var bib = BigInteger.Parse(b.ToString().Replace(".", ""));
            var aord = a.Order;
            var bord = b.Order;
            var dia = bia / bib;
            var pra = new CatPrecise(dia.ToString()) {Order = Math.Max(0, aord-bord + (aord == bord ? 1 : 0))};

            return pra;
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