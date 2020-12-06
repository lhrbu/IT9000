using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT9000.Shared.Models
{
    public record DeviceInfo
    {
        public string Name { get; init; } = null!;
        public int Address { get; init; }
        public string InterfaceType { get; init; } = null!;
        public string InterfaceParameter { get; init; } = null!;

        public string Model => Name.Split('_')[0];
        public string Index => Name.Split('_')[1].Split('@')[0];
        public string ChannelNumber => Name.Split('@')[1];
    }
}
