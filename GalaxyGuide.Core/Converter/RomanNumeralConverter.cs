using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GalaxyGuide.Core.Converter
{
    /// <summary>
    /// Represents Converter class which can be used to converts roman numeral string to equivalent number.
    /// </summary>
    public static class RomanNumeralConverter
    {

        #region Private Members
             
        /// <summary>
        /// Represents the mapping record for Roman Symbol to it's value. Ex. I => 1 , V => 5
        /// </summary>
        private static Dictionary<char, int> RomanNumberMapping;

        /// <summary>
        /// Represents the mapping record for Roman Symbol to it's allowed subtractions.
        /// </summary>
        private static Dictionary<char, string> SubtractionMapping;

        /// <summary>
        /// Represent the rules for Roman Numeral Validation. Ex. "D", "L", and "V" can never be repeated.
        /// </summary>
        private static Dictionary<string, bool> RomanNumeralValidationRules;

        #endregion

        #region Initialization

        static RomanNumeralConverter()
        {
            InitializeRomanNumberMapping();
            InitializeSubtractionMapping();
            InitializeRomanNumberValidationRules();
        }
       
        private static void InitializeRomanNumberMapping()
        {
            RomanNumberMapping = new Dictionary<char, int>();
            RomanNumberMapping.Add('I', 1);
            RomanNumberMapping.Add('V', 5);
            RomanNumberMapping.Add('X', 10);
            RomanNumberMapping.Add('L', 50);
            RomanNumberMapping.Add('C', 100);
            RomanNumberMapping.Add('D', 500);
            RomanNumberMapping.Add('M', 1000);
        }

        private static void InitializeSubtractionMapping()
        {
            SubtractionMapping = new Dictionary<char, string>();

            SubtractionMapping.Add('I', "VX"); // "I" can be subtracted from "V" and "X" only
            SubtractionMapping.Add('X', "LC"); // "X" can be subtracted from "L" and "C" only
            SubtractionMapping.Add('C', "DM"); // "C" can be subtracted from "D" and "M" only
        }

        private static void InitializeRomanNumberValidationRules()
        {
            RomanNumeralValidationRules = new Dictionary<string, bool>();

            // Make sure it contains only valid roman symbols.
            var romanSymbols = new string(RomanNumberMapping.Keys.ToArray());
            RomanNumeralValidationRules.Add(string.Format("^[{0}]+$", romanSymbols), true);

            // Rule : The symbols "I", "X", "C", and "M" can be repeated three times in succession, but no more. 
            // (They may appear four times if the third and fourth are separated by a smaller value, such as XXXIX.)
            RomanNumeralValidationRules.Add(@"(.)\1{3,}", false);

            // Rule: "D", "L", and "V" can never be repeated.
            RomanNumeralValidationRules.Add(@"(D|L|V)\1{1,}", false);
        }

        #endregion


        /// <summary>
        /// Converts Roman Numeral String to Equivalent number.
        /// </summary>
        /// <param name="romanNumeralString">The roman numeral string</param>
        /// <returns>The converted value.</returns>
        public static long ConverToNumber(string romanNumeralString)
        {
            if (string.IsNullOrWhiteSpace(romanNumeralString)) // Invalid string.
                return -1;

            // All the symbols are in upper casing.
            romanNumeralString = romanNumeralString.ToUpper();

            // Make sure roman numeral string is valid string.
            if (RomanNumeralValidationRules.Any(rule => Regex.IsMatch(romanNumeralString, rule.Key) != rule.Value))
                return -1;

            long result = 0;
            char lastNumeral = char.MinValue;

            // Process numerals in reverse order so that it would be easy for calculations.
            foreach (var romanNumeral in romanNumeralString.Reverse())
            {
                // Rule: Only one small-value symbol may be subtracted from any large-value symbol.
                if (RomanNumberMapping.ContainsKey(lastNumeral) &&
                        RomanNumberMapping[romanNumeral] < RomanNumberMapping[lastNumeral])
                {
                    // Check if we need to do subtraction.
                    if (SubtractionMapping.ContainsKey(romanNumeral)
                        && SubtractionMapping[romanNumeral].Contains(lastNumeral.ToString()))
                    {
                        // Subtract the value as prefix is smaller and valid symbol.
                        result -= RomanNumberMapping[romanNumeral];
                        lastNumeral = romanNumeral;
                        continue;
                    }
                }

                result += RomanNumberMapping[romanNumeral];
                lastNumeral = romanNumeral;
            }
            return result;
        }

        /// <summary>
        /// Gets valid symbols for Roman Number System.
        /// </summary>
        /// <returns></returns>
        public static char[] GetValidSymbols()
        {
            return RomanNumberMapping.Keys.ToArray();
        }
    }
}
