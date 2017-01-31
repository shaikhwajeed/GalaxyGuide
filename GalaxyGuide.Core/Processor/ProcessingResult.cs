using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyGuide.Core.Processor
{
    /// <summary>
    /// Represents processing result model, which is used to return combined result after processing.
    /// </summary>
    public class ProcessingResult
    {
        /// <summary>
        /// Get/Set result type which defines type of output.
        /// </summary>
        public Parser.ResultType ResultType { get; set; }

        /// <summary>
        /// Get/Set the final value after conversion.
        /// </summary>
        public decimal ConvertedValue { get; set; }

        /// <summary>
        /// Get/Set all the Units used fro conversion.
        /// </summary>
        public List<string> Units { get; set; }

        /// <summary>
        /// Get/Set the name of metal.
        /// </summary>
        public string   MetalName { get; set; }
    }
}
