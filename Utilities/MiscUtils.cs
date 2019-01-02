namespace Cat.Utilities
{
    public class MiscUtils
    {
        public static string ArrayToString<TH>(TH[] arr, string delimiter = ", ", string begin = "", string end = "")
        {
            var ar = begin;
            for (var i = 0; i < arr.Length; i++)
            {
                ar += arr[i];
                if (i < arr.Length - 1) ar += delimiter;
            }

            ar += end;
            return ar;
        }
    }
}