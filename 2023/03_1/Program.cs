using System.Data;
using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var table = new Table(input);
var selections = table.GetAllNumbers();
var symbolRegex = new Regex(@"[^.\d]");

var cnt = 0;

foreach (var selection in selections)
{
  var text = table.GetSurroundingText(selection);

  if (text.Any(line => symbolRegex.IsMatch(line)))
  {
    cnt += int.Parse(selection.Value);
  }
}

Console.WriteLine(cnt);

record Selection(string Value, int Row, int Index)
{
  public int Length => this.Value.Length;
}

class Table
{
  private readonly string[] _rows;
  private readonly Regex _numberRegex = new(@"\d+");

  public Table(string[] rows)
  {
    _rows = rows;
  }

  public IEnumerable<string> GetSurroundingText(Selection selection)
  {
    var startRow = Math.Max(selection.Row - 1, 0);
    var endRow = Math.Min(selection.Row + 1, _rows.Length - 1);

    var list = new List<string>();

    for (var rowNum = startRow; rowNum <= endRow; rowNum++)
    {
      var row = _rows[rowNum];

      var startIndex = Math.Max(selection.Index - 1, 0);
      var endIndex = Math.Min(selection.Index + selection.Length + 1, row.Length);
      
      list.Add(row[startIndex..endIndex]);
    }

    return list;
  }

  public IEnumerable<Selection> GetAllNumbers()
  {
    var result = new List<Selection>();
    
    for (var rowNum = 0; rowNum < _rows.Length; rowNum++)
    {
      var row = _rows[rowNum];

      foreach (Match match in _numberRegex.Matches(row))
      {
        result.Add(new Selection(match.Value, rowNum, match.Index));
      }
    }

    return result;
  }
}