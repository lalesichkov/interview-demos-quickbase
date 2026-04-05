using Backend;
using Backend.Backend;
using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

Console.WriteLine("Started");
Console.WriteLine("Getting DB Connection...");

IDbService dbService = new DbService();
var taskDbService = dbService.GetPopulationAsync();

IStatService statService = new ConcreteStatService();
var taskStatService = statService.GetCountryPopulationsAsync();

await Task.WhenAll(taskDbService, taskStatService);

var resDb = await taskDbService;
var resTask = await taskStatService;


var result = resDb.ToDictionary(x => CountryNormalizer.Normalize(x.Name), x => (int)x.Population);
resTask.Select(x =>
{
    Match match = Regex.Match(x.Item1, "(.+)\\s\\(([A-Za-z]+)\\)");
    if (match.Success)
    {
        return new Tuple<string, int>(CountryNormalizer.Normalize(match.Groups[1].ToString()), x.Item2);
    }
    else return new Tuple<string,int>(CountryNormalizer.Normalize(x.Item1),x.Item2);
}).Where(x => !result.ContainsKey(x.Item1)).ToList().ForEach(x => result.Add(x.Item1, x.Item2));

foreach (var dictEntry in result)
{
    Console.WriteLine($"{dictEntry.Key}: {dictEntry.Value}");
}
