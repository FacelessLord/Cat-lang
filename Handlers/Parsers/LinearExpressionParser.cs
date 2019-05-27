using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;

namespace Cat.Handlers.Parsers
{
    public class LinearExpressionParser
    {
        public static void Run(string expression, Dictionary<string, CatStructureObject> variables)
        {
            List<Token>
        }
    }

    public class Token
    {
        public string _lexem;
        public int _type;
        
        public Token(string lexem, int type)
        {
            _lexem = lexem;
            _type = type;
        }
    }

    public class LexemParser
    {
        public int _state;
        public static Regex re_variable;
        
        public List<Token> ParseTokens(string expression)
        {
            
        }
    }
}