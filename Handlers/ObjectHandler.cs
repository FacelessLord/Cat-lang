using Cat.AbstractStructure;
using Cat.Structure;
using static Cat.CatCore;

namespace Cat
{
	public class ObjectHandler
	{
		/// <summary>
		/// Method will return link to the value that field has
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="field"></param>
		/// <returns></returns>
		public static CatField GetObjectField(CatCompoundObject obj, string field)
		{
			foreach (var prop in obj._properties)
			{
				if(prop.IsField())
				if (prop._name == field)
					return (CatField)prop;
			}

			return null;
		}
	}
}