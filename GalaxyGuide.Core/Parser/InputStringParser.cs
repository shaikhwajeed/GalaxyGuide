using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GalaxyGuide.Core.Parser
{
    /// <summary>
    /// Represents the Parser which identifies the type of input.
    /// </summary>
    public static class InputStringParser
    {
        private static List<string> StopWords = new List<string>() { "is", "credits", "how", "much", "?", "many" };

        public static ParserResult Parse(string userInput)
        {
            if (string.IsNullOrEmpty(userInput))
                return null;

            ParserResult parserResult = new ParserResult();
            // Split the input with space as separator. Clean the empty results and trim it.
            var inputs = userInput.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToList();

            // Remove the stop words.
            parserResult.Tokens = inputs.Where(s => !StopWords.Contains(s.ToLower())).ToList();

            parserResult.ResultType = ResultType.Unknown;

            // This is to add unit information. Basic input for processing : ex: glob is I
            if (userInput.Contains("?")) // User input contains question mark.
            {
                parserResult.ResultType = ResultType.Question;
            }
            else if (Regex.IsMatch(userInput, @"\d")) 
            {
                // Metal information where credits are mentioned. glob glob Silver is 34 Credits
                parserResult.ResultType = ResultType.MetalInfo;
            }
            else if (parserResult.Tokens.Count == 2)  
            {
                // This is to add unit information. Basic input for processing : ex: glob is I
                parserResult.ResultType = ResultType.UnitInfo;
            }
            
            return parserResult;
        }
    }
}
