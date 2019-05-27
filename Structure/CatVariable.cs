using Cat.AbstractStructure;
using Cat.Handlers;
using Cat.Utilities;

namespace Cat.Structure
{
    public class CatVariable : CatStructureObject
    {
        public CatVariable(string type, params Modifier[] modifiers) : base(type, modifiers)
        {
        }

        public string VariableType = "null"; 
        public CatStructureObject Value = TypeHandler.Null;

        public CatVariable SetValue(CatStructureObject value)
        {
            if (value.Type == VariableType)
            {
                Value = value;
            }
            return this;
        }
        
        public CatStructureObject GetValue()
        {
            return Value;
        }
    }
}