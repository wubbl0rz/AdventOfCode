var input = File.ReadAllLines("input.txt");

var result = input.Select(line =>
{
  var first = line.First(char.IsDigit);
  var last = line.Last(char.IsDigit);

  return int.Parse($"{first}{last}");
}).Sum();

Console.WriteLine(result);