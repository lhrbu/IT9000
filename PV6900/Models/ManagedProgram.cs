using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV6900.Models
{
    public class ManagedProgram
    {
        public int OuterLoopCount { get; init; } = 1;
        public List<ManagedProgramStep> ManagedProgramSteps { get; init; } = new();
    }
}
