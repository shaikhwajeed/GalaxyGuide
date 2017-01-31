using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyGuide.Core.Parser
{
    public class ParserResult
    {
        public List<string> Tokens { get; set; }

        public ResultType ResultType { get; set; }
    }
}
