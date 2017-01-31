using GalaxyGuide.Core.Parser;
using GalaxyGuide.Core.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace GalaxyGuide.ConsoleClient.Tests
{
    [TestClass]
    public class IntergalacticUnitProcessorTest
    {
        [TestMethod]
        public void TestNullAndEmptyScenarios()
        {
            IntergalacticUnitProcessor manager = new IntergalacticUnitProcessor();

            var result = manager.ProcessString(null);

        }

        [TestMethod]
        public void TestEntireFlow()
        {
            // Run for the default input.
            var inputs =
                 new[] {
                    "glob is I",
                    "prok is V",
                    "pish is X",
                    "tegj is L",
                    "glob glob Silver is 34 Credits",
                    "glob prok Gold is 57800 Credits",
                    "pish pish Iron is 3910 Credits",
                  };

            IntergalacticUnitProcessor manager = new IntergalacticUnitProcessor();

            foreach (var input in inputs)
            {
                manager.ProcessString(input);
            }

            var result = manager.ProcessString("how much is pish tegj glob glob ?");
            Debug.Assert(result.ResultType == ResultType.Question && result.ConvertedValue == 42);

            result = manager.ProcessString("how many Credits is glob prok Silver ?");
            Debug.Assert(result.ResultType == ResultType.Question && result.ConvertedValue == 68 && result.MetalName == "Silver");

            result = manager.ProcessString("how many Credits is glob prok Diamond ?");
            Debug.Assert(result.ResultType == ResultType.Unknown);

            result = manager.ProcessString("how many Credits is pish pish pish pish ?");
            Debug.Assert(result.ResultType == ResultType.Unknown);

        }

        [TestMethod]
        public void TestEntireFlow2()
        {
            // Run for the default input.
            var inputs =
                 new[] {
                    "ek is I",
                    "pach is V",
                    "daha is X",
                    "pannas is L",
                    "ek ek Silver is 2700 Credits",
                    "daha daha Gold is 24000 Credits",
                    "pach pannas Iron is 200 Credits",
                  };

            IntergalacticUnitProcessor manager = new IntergalacticUnitProcessor();

            foreach (var input in inputs)
            {
                manager.ProcessString(input);
            }

            var result = manager.ProcessString("how much is pannas ek ek ?");
            Debug.Assert(result.ResultType == ResultType.Question && result.ConvertedValue == 52);

            result = manager.ProcessString("how many Credits is pannas daha Silver ?");
            Debug.Assert(result.ResultType == ResultType.Question && result.ConvertedValue == 81000 && result.MetalName == "Silver");

            result = manager.ProcessString("how many Credits is daha pannas Gold ?");
            Debug.Assert(result.ResultType == ResultType.Question && result.ConvertedValue == 48000 && result.MetalName == "Gold");

            result = manager.ProcessString("how many Credits is pannas prok Diamond ?");
            Debug.Assert(result.ResultType == ResultType.Unknown);

            result = manager.ProcessString("how many Credits is pish pish pish pish ?");
            Debug.Assert(result.ResultType == ResultType.Unknown);

        }

        [TestMethod]
        public void TestUnitsProcessing()
        {
            IntergalacticUnitProcessor manager = new IntergalacticUnitProcessor();

            var result = manager.ProcessString("glob is I");
            Debug.Assert(result.ResultType == ResultType.UnitInfo);

            result = manager.ProcessString("prok is V");
            result = manager.ProcessString("pish is X");
            result = manager.ProcessString("tegj is L");
            Debug.Assert(result.ResultType == ResultType.UnitInfo);

        }

        [TestMethod]
        public void TestMetalInfoProcessing()
        {
            IntergalacticUnitProcessor manager = new IntergalacticUnitProcessor();

            // Add basic units.
            var result = manager.ProcessString("glob is I");
            result = manager.ProcessString("prok is V");
            result = manager.ProcessString("pish is X");
            result = manager.ProcessString("tegj is L");

            result = manager.ProcessString("glob glob Silver is 34 Credits");
            Debug.Assert(result.ResultType == ResultType.MetalInfo);

            result = manager.ProcessString("glob prok Gold is 57800 Credits");
            Debug.Assert(result.ResultType == ResultType.MetalInfo);

            result = manager.ProcessString("glob prok is 57800");
            Debug.Assert(result.ResultType == ResultType.Unknown);

            result = manager.ProcessString("pish pish Iron is 3910 Credits");
            Debug.Assert(result.ResultType == ResultType.MetalInfo);

        }

       

        [TestMethod]
        public void TestEntireFlowWithWrongInput()
        {
            // Run for the default input.
            var inputs =
                 new[] {
                    "L is hello",
                    "how much is silver",
                    "what is x2 ?",
                    "how much is gold?",
                    "how are you doing?",
                    "X is I",
                    "Y is Z",
                    "glob is I",
                    "prok is V",
                    "pish is X",
                    "how to delete file ?"
                  };

            IntergalacticUnitProcessor manager = new IntergalacticUnitProcessor();

            foreach (var input in inputs)
            {
                manager.ProcessString(input);
            }

            var result = manager.ProcessString("how much is glob glob glob ?");
            Debug.Assert(result.ResultType == ResultType.Question && result.ConvertedValue == 3);

        }
    }



}
