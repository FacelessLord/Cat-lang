using Cat.AbstractStructure;
using Cat.Structure;

namespace Cat.Primitives
{
    public class CatRange : CatPrimitiveObject
    {
        public CatNumber From;
        public CatNumber To;

        public int IterationStep = 0;
        public int IterationCount => From is CatInt fi && To is CatInt ti ? (ti - fi).Value + 1 : 200;

        public CatRange(CatNumber from, CatNumber to) : base("range")
        {
            (From, To) = (from, to);
        }

        public override bool HasField(string field)
        {
            if (field == "left" || field == "right")
                return true;
            return base.HasField(field);
        }

        public override CatStructureObject GetFieldValue(string field)
        {
            if (field == "left")
                return From;
            if (field == "right")
                return To;
            return base.GetFieldValue(field);
        }

        public CatNumber GetCurrentIterationValue()
        {
            if (To is CatInt ti && From is CatInt fi)
            {
                CatInt dInt = new CatInt((ti - fi).Value + 1) / new CatInt(IterationCount);
                return From + dInt * new CatInt(IterationStep);
            }

            CatDouble dVal = new CatDouble(To - From) / new CatDouble(200);
            return From + dVal * new CatDouble(IterationStep);
        }

        public override string ToString()
        {
            return $"[{From}, {To}]";
        }
    }
}