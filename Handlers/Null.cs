using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Primitives;

namespace Cat.Handlers
{
    public class NullParser : IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s*(null)");

        private static readonly CatStructureObject Null = new CatNull();
        public CatStructureObject Process(string expr, List<string> code)
        {
            return TypeHandler.Null;
        }
    }
}