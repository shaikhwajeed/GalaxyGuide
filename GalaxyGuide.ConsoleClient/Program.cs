using GalaxyGuide.Core.Parser;
using GalaxyGuide.Core.Processor;
using System;
using System.IO;

namespace GalaxyGuide.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IntergalacticUnitProcessor manager = new IntergalacticUnitProcessor();

            if (args.Length != 0) // Command line input
            {
                ProcessInputFromCommandLineArguments(args, manager);
                return;
            }

            Console.WriteLine("Would you like to enter input manually?");
            var userResponse = Console.ReadLine().ToLower();
            if (userResponse == "yes" || userResponse == "y") // Manual input
            {
                // User will type in the input
                Console.WriteLine("Please enter your input. enter exit to close the program.");
                var input = Console.ReadLine();
                while (!input.ToLower().Contains("exit"))
                {
                    Process(manager, input);
                    input = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Output for default test case:");
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
                    "how much is pish tegj glob glob ?",
                    "how many Credits is glob prok Silver ?",
                    "how many Credits is glob prok Gold ?",
                    "how many Credits is glob prok Iron ?",
                    "how much wood could a woodchuck chuck if a woodchuck could chuck wood ?"};

                foreach (var input in inputs)
                {
                    Process(manager, input);
                }
            }
            Console.ReadLine();
        }
        
        private static void Process(IntergalacticUnitProcessor manager, string input)
        {
            var result = manager.ProcessString(input);
            if (result.ResultType == ResultType.Question)
            {
                if (!string.IsNullOrWhiteSpace(result.MetalName))
                    Console.WriteLine("{0} {1} is {2} Credits", string.Join(" ", result.Units)
                        , result.MetalName, result.ConvertedValue.ToString());
                else
                    Console.WriteLine("{0} is {1}", string.Join(" ", result.Units), result.ConvertedValue);
            }
            else if (result.ResultType == ResultType.Unknown)
                Console.WriteLine("I have no idea what you are talking about");
        }

        private static void ProcessInputFromCommandLineArguments(string[] args, IntergalacticUnitProcessor manager)
        {
            string[] inputs;
            // Check if input is provided from File.
            if (File.Exists(args[0]))
            {
                inputs = File.ReadAllLines(args[0]);

            }
            else
            {
                // Seems like input is provided as command line argument.
                inputs = args;
            }

            foreach (var input in inputs)
            {
                Process(manager, input);
            }
            Console.ReadLine();
        }
    }
}
