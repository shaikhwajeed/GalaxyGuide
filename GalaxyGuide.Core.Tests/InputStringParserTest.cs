using GalaxyGuide.Core.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace GalaxyGuide.ConsoleClient.Tests
{
    [TestClass]
    public class InputStringParserTest
    {
        [TestMethod]
        public void TestValidUnitInformation()
        {
            // Validate basic numbers.
            var result = InputStringParser.Parse("glob is I");
            Debug.Assert(result.ResultType == ResultType.UnitInfo && result.Tokens.Count == 2);
        }

        [TestMethod]
        public void TestMetalInformation()
        {
            // Validate basic numbers.

            var result = InputStringParser.Parse("glob glob Silver is 34 Credits");
            Debug.Assert(result.ResultType == ResultType.MetalInfo);

        }

        [TestMethod]
        public void TestQuestionInformation()
        {
            // Validate basic numbers.
            var result = InputStringParser.Parse("how many Credits is glob prok Silver ?");
            Debug.Assert(result.ResultType == ResultType.Question);

            result = InputStringParser.Parse("glob Silver ?");
            Debug.Assert(result.ResultType == ResultType.Question);
        }

        [TestMethod]
        public void TestRandomText()
        {
            // Validate basic numbers.
            var result = InputStringParser.Parse("how much wood could a woodchuck chuck if a woodchuck could chuck wood?");
            Debug.Assert(result.ResultType == ResultType.Question);
        }

        [TestMethod]
        public void TestInValidInput()
        {
            var result = InputStringParser.Parse("glob is");
            Debug.Assert(result.ResultType == ResultType.Unknown);

            result = InputStringParser.Parse("glob");
            Debug.Assert(result.ResultType == ResultType.Unknown);

        }

    }
}
