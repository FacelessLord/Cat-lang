using System;
using static Cat.CatCore;
using System.Collections.Generic;

namespace Cat
{
	public class CatObjectLoader
	{
		public static void FinalizeObject(int obj)
		{
			var typeId = obj + 1;
			if (!((string) Heap[obj]).StartsWith("|p"))
			{
				var fieldNumId = obj + 2;
				var fieldNum = (int) Heap[fieldNumId];
				for (var i = 0; i < fieldNum; i++)
				{
					FinalizeObject(obj + 3 + 3 * i);
					//Removing all meta information about field
					Heap[obj + 4 + 3 * i] = H0; //Name
					Heap[obj + 5 + 3 * i] = H0; //Type
					Heap[obj + 6 + 3 * i] = H0; //Value(link)
					RemoveCount += 3;
				}

				Heap[obj] = H0;
				Heap[typeId] = H0;
				Heap[fieldNumId] = H0;
				RemoveCount += 3;
				DoesHeapContainSpaces = true;
			}
			else
			{
				Heap[obj] = H0;
				Heap[typeId] = H0;
				RemoveCount += 2;
				DoesHeapContainSpaces = true;
			}
		}

		/// <summary>
		/// Function that creates new instance of type "type"
		/// </summary>
		/// <param name="type">Primitive type: "int", "long", "byte", "double", "angle", "string", "bool", "float", "precise"</param>
		/// <param name="args">Value of an object. Can contain</param>
		/// <returns></returns>
		public static int CreatePrimitiveObject(string type, params object[] args)
		{
			var l = new List<object>() {"|p" + type, args[0]};
			return HeapHandler.LoadListToHeap(l);
		}

		public static int CreateEmptyObject()
		{
			var l = new List<object>() {"|cobject",};
			return HeapHandler.LoadListToHeap(l);
		}

		public static int CreateObjectByType(string type, params object[] args)
		{
			var l = new List<object>() {"|c" + type};
			var j = 0;
			var stype = "|C|" + type;
			while (j < Heap.Count && (!(Heap[j] is string s) || s != stype)) j++;
			Console.WriteLine(Heap[j]);
			if (j < Heap.Count && Heap[j] is string st && st == stype)
			{
				var nfc = (int) Heap[j + 1];
				var sfc = (int) Heap[j + 2];
				var nmc = (int) Heap[j + 3];
				var smc = (int) Heap[j + 4];
				l.Add(nfc);
				l.Add(sfc);
				l.Add(nmc);
				l.Add(smc);
				var nfcStart = j + 5;
				for (var k = 0; k < nfc; k++)
				{
					l.Add(Heap[nfcStart + 3 * k]);
					l.Add(Heap[nfcStart + 3 * k + 1]);
					l.Add(Heap[nfcStart + 3 * k + 2]);
				}

				var sfcStart = nfcStart + 3 * nfc;
				for (var k = 0; k < sfc; k++)
				{
					l.Add(Heap[sfcStart + 3 * k]);
					l.Add(Heap[sfcStart + 3 * k + 1]);
					l.Add(Heap[sfcStart + 3 * k + 2]);
				}

				var nmcStart = sfcStart + 3 * sfc;
				for (var k = 0; k < nmc; k++)
				{
					l.Add(Heap[nmcStart + 2 * k]);
					l.Add(Heap[nmcStart + 2 * k + 1]);
				}

				var smcStart = nmcStart + 2 * nmc;
				for (var k = 0; k < smc; k++)
				{
					l.Add(Heap[smcStart + 2 * k]);
					l.Add(Heap[smcStart + 2 * k + 1]);
				}

				int ret = HeapHandler.LoadListToHeap(l);
				//todo call Constructor
				return ret;
			}

			ExceptionHandler.ThrowException("TypeExistenceException",
				"tried to create an instance of not loaded Class");
			return L0;
		}
	}
}