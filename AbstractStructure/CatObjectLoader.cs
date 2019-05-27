using System;
using Cat.Primitives;
using Cat.Primitives.Precise;
using Cat.Structure;
using Cat.Utilities;

namespace Cat.AbstractStructure
{
	public class CatObjectLoader
	{
		public static void FinalizeObject(int obj)
		{
			CatCore.Heap[obj] = null;
			CatCore.RemoveCount += 1;
			CatCore.DoesHeapContainSpaces = true;
		}

		/// <summary>
		/// Function that creates new instance of type "type"
		/// </summary>
		/// <param name="type">Primitive type: "int", "long", "byte", "double", "angle", "string", "bool", "float", "precise"</param>
		/// <param name="args">Value of an object. Can contain</param>
		/// <returns></returns>
		public static int CreatePrimitiveObject(string type, params CatStructureObject[] args)
		{
			CatPrimitiveObject objToLoad = null;
			switch (type)
			{
				case "byte":
					objToLoad = new CatByte(args[0]);
					break;
				case "int":
					objToLoad = new CatInt(args[0]);
					break;
				case "long":
					objToLoad = new CatLong(args[0]);
					break;
				case "angle":
					objToLoad = new CatAngle(args[0]);
					break;
				case "string":
					objToLoad = new CatString(args[0]);
					break;
				case "bool":
					objToLoad = new CatBool(args[0]);
					break;
				case "float":
					objToLoad = new CatFloat(args[0]);
					break;
				case "double":
					objToLoad = new CatDouble(args[0]);
					break;
				case "precise":
					objToLoad = new CatPrecise(args[0]);
					break;
				case "array":
					objToLoad = new CatArray(args);
					break;
			}

			if (objToLoad != null)
				return HeapHandler.LoadObjectToHeap(objToLoad);
			throw new ArgumentException();
		}

		public static int CreateEmptyObject()
		{
			var obj = new CatStructureObject("object");
			return HeapHandler.LoadObjectToHeap(obj);
		}

		public static int CreateObjectByType(string type, params CatStructureObject[] args)
		{
			if (CatCore.Classes[type] != null)
			{
				var clazz = CatCore.Classes[type];
				var obj = clazz.CreateObjectFromClass();
				foreach (var property in obj.Properties)
				{
					if (property is CatConstructor constr)
					{
						if (constr.Signature == CreateSignatureFromArray(args))
						{
							MethodCaller.CallMethod(constr);
							break;
						}
					}
				}

				int ret = HeapHandler.LoadObjectToHeap(obj);
				return ret;
			}

			ExceptionHandler.ThrowException("TypeExistenceException",
				"tried to create an instance of not loaded Class");
			return CatCore.L0;
		}

		public static string[] CreateSignatureFromArray(params CatStructureObject[] args)
		{
			var sign = new string[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				var arg = args[i];
				sign[i] = arg.Type;
			}

			return sign;
		}
	}
}