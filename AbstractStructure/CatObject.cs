namespace Cat.AbstractStructure
{
	/// <summary>
	/// Wrapper class for Cat objects
	/// </summary>
	public class CatObject : CatStructureObject
	{
		/// <summary>
		/// Index of the type in heap
		/// </summary>
		public string _type;
		
		public CatObject(string type)
		{
			_type = type;
		}
	}
}