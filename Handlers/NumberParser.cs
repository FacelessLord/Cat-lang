using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Primitives;

namespace Cat.Handlers
{
    public class NumberParser : IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s*(0[xX][0-9A-Fa-f]+|0b[01]+|[\+\-]{0,1}\d*\.{0,1}\d*[dflp])");
        public CatStructureObject Process(string expr, List<string> code)
        {
            return null;
        }
    }
}