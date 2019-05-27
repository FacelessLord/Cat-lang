using System.Collections.Generic;
using System.Threading;
using Cat.AbstractStructure;
using Cat.Handlers.Parsers;

namespace Cat.Handlers
{
    public static class SuperKeyWordHandler
    {
        public static bool Process(string lexem, Dictionary<string, CatStructureObject> variables)
        {
            if (lexem.StartsWith("|"))
            {
                LinearExpressionHandler.Labels.Add(lexem.Substring(1), LinearExpressionHandler.CurrentAdress);
                return true;
            }

            if (lexem.StartsWith("goto|"))
            {
                LinearExpressionHandler.CurrentAdress = LinearExpressionHandler.Labels[lexem.Substring(5)];
                return true;
            }

            if (lexem.StartsWith("sleep|"))
            {
                var strTime = lexem.Substring("sleep|".Length);
                var time = int.Parse(strTime);
                Thread.Sleep(time);
                return true;
            }

            return false;
        }
    }
}