using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV6900.Models
{
    public class ManagedProgramStep
    {
        public double Volta { get; init; }
        public double Ampere { get; init; }
        public double Duration { get; init; }
        public InnerLoopFlag InnerLoopFlag { get; init; } = InnerLoopFlag.None;
        public int InnerLoopCount { get; init; }
    }
}
