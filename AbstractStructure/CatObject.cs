namespace Cat.AbstractStructure
{
	/// <summary>
	/// Wrapper class for Cat objects
	/// </summary>
	public abstract class CatObject : CatStructureObject
	{
		/// <summary>
		/// Index of the type in heap
		/// </summary>
		public int _type;
		
		public CatObject(string type)
		{
			_type = CatCore.GetTypeIndex(type);
		}
	}
}