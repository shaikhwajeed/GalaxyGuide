using GalaxyGuide.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyGuide.Core.Processor
{
    /// <summary>
    /// Represents the Processor class which accepts user input and returns processed result accordingly.
    /// It handles, units info, metal credit info also responds to the questions.
    /// </summary>
    public class IntergalacticUnitProcessor
    {
        private Dictionary<string, char> UnitsMapping { get; set; }

        private Dictionary<string, decimal> MetalPriceMapping { get; set; }

        public IntergalacticUnitProcessor()
        {
            UnitsMapping = new Dictionary<string, char>();
            MetalPriceMapping = new Dictionary<string, decimal>();
        }

        /// <summary>
        /// Processes input string for conversion. It is responsible for identifying input string if it's a unit value or metal detail or question.
        /// </summary>
        /// <param name="userInput">The input string.</param>
        /// <returns>The Processing Result which contains information which client can use to understand the processing.</returns>
        public ProcessingResult ProcessString(string userInput)
        {
            ProcessingResult conversionResult = new ProcessingResult() { ResultType = Parser.ResultType.Unknown };

            if (string.IsNullOrWhiteSpace(userInput))
                return conversionResult;

            // Send string to parser which processing input and returns tokens.
            var result = Parser.InputStringParser.Parse(userInput);

            conversionResult.ResultType = result.ResultType;

            switch (result.ResultType)
            {
                case Parser.ResultType.UnitInfo:
                    AddUnitInformationInDictionary(result, conversionResult);
                    break;
                case Parser.ResultType.MetalInfo:
                    AddMetalInformationInDictionary(result, conversionResult);
                    break;
                case Parser.ResultType.Question:
                    ProcessQuestion(result, conversionResult);
                    break;
                case Parser.ResultType.Unknown:
                    // Do nothing.
                    break;
                default:
                    break;
            }

            return conversionResult;
        }

        private void ProcessQuestion(Parser.ParserResult result, ProcessingResult conversionResult)
        {
            conversionResult.Units = result.Tokens.Where(s => UnitsMapping.ContainsKey(s)).ToList();
            conversionResult.MetalName = result.Tokens.Except(conversionResult.Units).FirstOrDefault();

            if (conversionResult.Units.Count == 0)
            {
                conversionResult.ResultType = Parser.ResultType.Unknown;
                return;
            }

            conversionResult.ConvertedValue = GetEquivalentNumber(conversionResult.Units);
            if (conversionResult.ConvertedValue == -1)
            {
                conversionResult.ResultType = Parser.ResultType.Unknown;
                return;
            }
            if (conversionResult.MetalName != null)
            {
                if (MetalPriceMapping.ContainsKey(conversionResult.MetalName))
                    conversionResult.ConvertedValue *= MetalPriceMapping[conversionResult.MetalName];
                else
                {
                    conversionResult.ResultType = Parser.ResultType.Unknown;
                }
            }
        }

        private void AddMetalInformationInDictionary(Parser.ParserResult result, ProcessingResult conversionResult)
        {
            var units = result.Tokens.Where(s => UnitsMapping.ContainsKey(s)).ToList();
            var metalInfo = result.Tokens.Except(units).ToList(); // Get remaining info.
            if (metalInfo.Count != 2)
            {
                conversionResult.ResultType = Parser.ResultType.Unknown;
                return;
            }

            long romanEquivalentNumber = GetEquivalentNumber(units);
            if (romanEquivalentNumber == -1)
            {
                conversionResult.ResultType = Parser.ResultType.Unknown;
                return;
            }

            var metalName = metalInfo.First();
            var metalCredits = Decimal.Parse(metalInfo.Last());

            // Get the credit for each unit.
            var creditsPerUnit = metalCredits / romanEquivalentNumber;

            if (!MetalPriceMapping.ContainsKey(metalName))
                MetalPriceMapping.Add(metalName, creditsPerUnit);
            else
                MetalPriceMapping[metalName] = creditsPerUnit;

        }

        private void AddUnitInformationInDictionary(Parser.ParserResult result, ProcessingResult conversionResult)
        {
            // Add token information.
            var intergalacticUnit = result.Tokens.First();
            var romanEquivalentSymbol = result.Tokens.Last().First(); // get the first character.

            var validRomanSymbols = RomanNumeralConverter.GetValidSymbols();
            if (!validRomanSymbols.Contains(romanEquivalentSymbol))
            {
                conversionResult.ResultType = Parser.ResultType.Unknown;
                return;
            }
            if (!UnitsMapping.ContainsKey(intergalacticUnit))
                UnitsMapping.Add(intergalacticUnit, romanEquivalentSymbol); // Add units mapping.
            else
                UnitsMapping[intergalacticUnit] = romanEquivalentSymbol;
        }

        private long GetEquivalentNumber(List<string> units)
        {
            // Build string for units from UnitsMapping dictionary.
            StringBuilder romanString = new StringBuilder();
            units.ForEach(unit => romanString.Append(UnitsMapping[unit]));

            // Ge the equivalent number for Roman numeral string.
            return Converter.RomanNumeralConverter.ConverToNumber(romanString.ToString());
        }
    }
}

