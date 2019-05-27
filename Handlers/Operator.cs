using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Handlers.Parsers;
using Cat.Primitives;

namespace Cat.Handlers
{
    public class OperatorParser :IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s(\+|\-|\*|\/\/|\/|\%|\^|\=\=|\!\=|\<\=|\>\=|\>|\<|\|)");
        
        public static List<string> Operations = new List<string>(){"+","-","*","//","/","%","^","==","!=","<=",">=",">","<","|"};
        public CatStructureObject Process(string expr, List<string> code)
        {
            return null;
        }
        
        public static CatStructureObject CallOperator(string op, CatStructureObject a, CatStructureObject b)
        {
            switch (op)
            {
                case "+": return a + b;
                case "-": return a - b;
                case "*": return a * b;
                case "//": return a -( a % b);
                case "/": return a / b;
                case "%": return a % b;
                case "^": return a ^ b;
                case "==": return new CatBool(a == b);
                case "!=": return new CatBool(a != b);
                case "<=": return new CatBool(a <= b);
                case ">=": return new CatBool(a >= b);
                case "<": return new CatBool(a < b);
                case ">": return new CatBool(a > b);
                case "|": return a | b;
                default: return a + b;
            }
        }
    }
}