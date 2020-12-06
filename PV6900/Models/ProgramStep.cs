using IT9000.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV6900.Models
{
    public class ProgramStep
    {
        public double Volta { get; init; }
        public double Ampere { get; init; }
        public double Duration { get; init; }
    }
}
