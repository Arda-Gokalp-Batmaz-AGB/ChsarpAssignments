using System.Text.Json;
using static Assignment_3.Program;

namespace Assignment_3 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public class District
        {
            public string Region { get; set; }
            public string CityName { get; set; }
            public string DistrictName { get; set; }
            public int NumberPlate { get; set; }
            public int Population { get; set; }

            public override string ToString()
            {
                return $"{Region} - {CityName} - {DistrictName}";
            }
        }
        static void Main(string[] args)
        {
            string fileName = "Turkiye-Districts.json";
            string jsonString = File.ReadAllText(fileName);
            List<District> districtList = JsonSerializer.Deserialize<List<District>>(jsonString)!;

            var districtsEndWithSomeChar = from district in districtList
                                           where district.DistrictName.EndsWith("H")
                                           || district.DistrictName.EndsWith("F")
                                           || district.DistrictName.EndsWith("O")
                                           || district.DistrictName.EndsWith("V")
                                           orderby district.Region, district.CityName, district.DistrictName
                                           select district;
            foreach (var item in districtsEndWithSomeChar)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----------------------------");

            var citiesWith15OrMoreDistricts = from district in districtList
                                              group district.DistrictName by district.CityName into gr
                                              let count = gr.Count()
                                              where count > 15
                                              orderby gr.Key
                                              select new { city = gr.Key, districtCount = count };
            foreach (var item in citiesWith15OrMoreDistricts)
            {
                Console.WriteLine($"{item.city} : {item.districtCount}");
            }

            Console.WriteLine("----------------------------");

            var groupSize = 10;

            var secondMinimumSum = districtList.Where(x => x.NumberPlate <= 80)
                .OrderBy(x => x.NumberPlate)
                .GroupBy(x => (x.NumberPlate - 1) / (groupSize), x => x.Population)
                .Select(k => new
                {
                    range = $"{(k.Key * groupSize) + 1}-{(k.Key + 1) * groupSize}",
                    population = k.Sum()
                })
                .OrderBy(x => x.population).Skip(1).First();
            Console.WriteLine($"{secondMinimumSum.range}: {secondMinimumSum.population.ToString("#,##0")}");
            
        }
    }
}