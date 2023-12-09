using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var digits = new Dictionary<string, string>()
{
  { "1", "1" },
  { "2", "2" },
  { "3", "3" },
  { "4", "4" },
  { "5", "5" },
  { "6", "6" },
  { "7", "7" },
  { "8", "8" },
  { "9", "9" },
  { "one", "1" },
  { "two", "2" },
  { "three", "3" },
  { "four", "4" },
  { "five", "5" },
  { "six", "6" },
  { "seven", "7" },
  { "eight", "8" },
  { "nine", "9" },
};

var regex = new Regex($"(?=({string.Join("|", digits.Keys)})){{1}}");

var result = input.Select(line =>
{
  var matches = regex.Matches(line);

  var first = matches.First().Groups[1].Value;
  var last = matches.Last().Groups[1].Value;

  return int.Parse($"{digits[first]}{digits[last]}");
}).Sum();

Console.WriteLine(result);
