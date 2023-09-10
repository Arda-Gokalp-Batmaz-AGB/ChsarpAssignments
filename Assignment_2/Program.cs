namespace Assignment_2 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static List<T> Copy<T>(List<T> list, int N)
        {
            List<T> newList = new List<T>();
            if (N >= list.Count)
            {
                newList.AddRange(list);
            }
            else
            {
                newList.AddRange(list.Take(N));
            }
            return newList;
        }
        static void Main(string[] args)
        {
            var seperator = '|';
            var inputList = new List<int>{ 1, 2, 3, 4, 5 };
            var newList = Copy<int>(inputList, 3);
            newList.PrintToConsole(seperator);
        }
    }

    public static class ListExtension
    {
        public static void PrintToConsole<T>(this IEnumerable<T> list,char seperator)
        {
            var result = "";
            foreach (var item in list)
            {
                result = result + item + seperator;
            }
            result = result.Substring(0, result.Length - 1);
            Console.WriteLine(result);
        }
    }
}