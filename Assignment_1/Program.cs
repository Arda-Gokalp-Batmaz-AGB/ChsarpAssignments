namespace Assignment_1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> numbOccurCounts = new Dictionary<int, int>();
            int[] array = new int[50000];
            Random rnd = new Random();
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = rnd.Next(10000) + 1;
                if (numbOccurCounts.ContainsKey(array[i]))
                {
                    numbOccurCounts[array[i]] = numbOccurCounts[array[i]] + 1;
                }
                else
                {
                    numbOccurCounts.Add(array[i], 1);
                }
            }

            while(true)
            {
                Console.Write("Enter a number between 1-10000 :");
                var inpNumber = Convert.ToInt32(Console.ReadLine());
                if (inpNumber == -1) { break; };
                Console.WriteLine($"{inpNumber} exists {numbOccurCounts[inpNumber]} times.");
            }

        }
    }
}