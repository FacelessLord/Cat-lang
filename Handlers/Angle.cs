using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Primitives;

namespace Cat.Handlers
{
    public class AngleParser : IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s*([\+\-]{0,1}\d*\.{0,1}\d*(g|grad|r|rad|deg))");

        public CatStructureObject Process(string expr, List<string> code)
        {
            return null;
        }
    }
}