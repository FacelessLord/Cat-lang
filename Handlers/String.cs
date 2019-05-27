using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Primitives;

namespace Cat.Handlers
{
    public class StringParser : IParser
    {
        public Regex Regex { get; set; } = new Regex("^\\s*(\"[^\"]*\"|'[^']*')");
        public CatStructureObject Process(string expr, List<string> code)
        {
            return null;
        }
    }
}