using System;
using static Cat.CatCore;
using System.Collections.Generic;
using Cat.AbstractStructure;
using Cat.Primitives;
using Cat.Primitives.Precise;
using Cat.Structure;

namespace Cat
{
	public class CatObjectLoader
	{
		public static void FinalizeObject(int obj)
		{
			Heap[obj] = null;
			RemoveCount += 1;
			DoesHeapContainSpaces = true;
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
			var obj = new CatObject("object");
			return HeapHandler.LoadObjectToHeap(obj);
		}

		public static int CreateObjectByType(string type, params CatObject[] args)
		{
			if (Classes[type] != null)
			{
				var clazz = Classes[type];
				var obj = clazz.CreateObjectFromClass();
				foreach (var property in obj._properties)
				{
					if (property is CatConstructor constr)
					{
						if (constr._signature == CreateSignatureFromArray(args))
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
			return L0;
		}

		public static string[] CreateSignatureFromArray(params CatObject[] args)
		{
			var sign = new string[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				var arg = args[i];
				sign[i] = arg._type;
			}

			return sign;
		}
	}
}