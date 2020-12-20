using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT9000.Shared.Models
{
    public interface IDevicePanelFactory
    {
        void Initialize();
        IDevicePanel CreateDevicePanel(DeviceInfo deviceInfo);
    }
}
