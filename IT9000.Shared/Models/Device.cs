using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT9000.Shared.Models
{
    public class Device
    {
        public DeviceInfo Info { get; set; } = null!;
        public IDevicePanel? Panel { get; set; }
    }
}
