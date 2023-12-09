using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var limit = new GameLimit(12, 13, 14);

var result = input
  .Select(Game.Parse)
  .Where(game => game.IsValidGame(limit))
  .Sum(game => game.Id);

Console.WriteLine(result);

public record GameLimit(int LimitRed, int LimitGreen, int LimitBlue);

public class Game
{
  public int Id { get; set; }
  
  public int MaxBlue { get; set; }
  public int MaxRed { get; set; }
  public int MaxGreen { get; set; }

  public bool IsValidGame(GameLimit limit) => this.MaxRed <= limit.LimitRed && 
                                              this.MaxGreen <= limit.LimitGreen && 
                                              this.MaxBlue <= limit.LimitBlue;

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