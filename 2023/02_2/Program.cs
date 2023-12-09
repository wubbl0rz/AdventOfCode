using System.Text.RegularExpressions;
using System.Threading.Channels;

var input = File.ReadAllLines("input.txt");

var result = input
  .Select(Game.Parse)
  .Select(game => game.MaxRed * game.MaxBlue * game.MaxGreen)
  .Sum();

Console.WriteLine(result);

public class Game
{
  public int Id { get; set; }
  
  public int MaxBlue { get; set; }
  public int MaxRed { get; set; }
  public int MaxGreen { get; set; }

  public static Game Parse(string line)
  {
    var regex = new Regex(@"(^Game (\d+): |(\d+)\s(red|green|blue))");
    
    var matches = regex.Matches(line).ToArray();

    var id = int.Parse(matches[0].Groups[2].ToString());

    var result = matches[1..].Select(m => new
      {
        color = m.Groups[4].ToString(),
        count = int.Parse(m.Groups[3].ToString())
      })
      .GroupBy(c => c.color)
      .ToDictionary(g => g.Key, g => g.Max(c => c.count));

    return new Game()
    {
      Id = id,
      MaxBlue = result["blue"],
      MaxRed = result["red"],
      MaxGreen = result["green"],
    };
  }
}
