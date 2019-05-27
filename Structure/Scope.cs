using System.Collections.Generic;
using Cat.AbstractStructure;

namespace Cat.Structure
{
    public class Scope
    {
        public Dictionary<string, CatVariable> Variables = new Dictionary<string, CatVariable>();

        public void SetVariable(string name, CatStructureObject value)
        {
            if (name != "_" && name != "null")
            {
                if (Variables.ContainsKey(name))
                {
                    Variables.Remove(name);
                }

                Variables.Add(name, new CatVariable(value.Type).SetValue(value)); //todo enable strict typization
            }
        }

        public void AddVariable(string name, CatStructureObject value, params Modifier[] modifiers)
        {
            Variables.Add(name, new CatVariable(value.Type, modifiers).SetValue(value));
        }
    }
}