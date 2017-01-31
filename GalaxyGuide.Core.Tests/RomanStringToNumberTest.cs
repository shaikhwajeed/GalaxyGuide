using GalaxyGuide.Core.Converter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace GalaxyGuide.ConsoleClient.Tests
{
    [TestClass]
    public class RomanStringToNumberTest
    {
        [TestMethod]
        public void TestValidRomanNumerals()
        {
            // Validate basic numbers.
            var tests = File.ReadAllLines(@"Resources\RomenNumeralWithValue.txt");

            foreach (var test in tests)
            {
                var inputs = test.Split(' ');
                int number = int.Parse(inputs[0]);
                System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber(inputs[1]) == number);
            }
        }

        [TestMethod]
        public void TestInvalidRomanNumerals()
        {
            // Null and Empty inputs
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber(null) == -1);
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("") == -1);
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("   ") == -1);

            // The symbols "I", "X", "C", and "M" can be repeated three times in succession, but no more. 
            // (They may appear four times if the third and fourth are separated by a smaller value, such as XXXIX.) 
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("XXXX") == -1);
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("IIII") == -1);
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("CCCC") == -1);
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("MMMM") == -1);

            // "D", "L", and "V" can never be repeated.
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("DDI") == -1);
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("LL") == -1);
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("VV") == -1);
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("DDILLIVV") == -1);

        }

        [TestMethod]
        public void TestLowerCaseRomanNumerals()
        {
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("mcmiii") == 1903);
            System.Diagnostics.Debug.Assert(RomanNumeralConverter.ConverToNumber("cXcViIi") == 198);
        }
    }
}
