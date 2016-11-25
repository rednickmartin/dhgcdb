using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace DHGCDB.Controllers
{

  public class Queries
  {
    private enum ParseState { OutOfBracket, InBracket }

    public static Dictionary<string, string> GetQueries(string queriesfile)
    {
      var result = new Dictionary<string, string>();

      var currentState = ParseState.OutOfBracket;
      var openPattern = new Regex(@"(.*)\{(.*)");
      var closePattern = new Regex(@"(.*)\}");
      var whiteSpace = new Regex(@"^\s*$");

      var currentVarName = "";
      var currentQueryText = "";

      foreach(var line in System.IO.File.ReadAllLines(queriesfile)) {
        if(whiteSpace.IsMatch(line))
          continue;

        if(currentState == ParseState.OutOfBracket) {
          if(openPattern.IsMatch(line)) {
            currentState = ParseState.InBracket;
          }
          else {
            currentVarName = line;
          }
        }
        else if(currentState == ParseState.InBracket) {
          if(closePattern.IsMatch(line)) {
            currentState = ParseState.OutOfBracket;
            result[currentVarName] = currentQueryText;
            currentQueryText = "";
          }
          else {
            currentQueryText += line + "\n";
          }
        }
      }

      return result;
    }

  }
}