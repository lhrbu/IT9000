using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT9000.Shared.Models
{
    public interface IDevicePanel
    {
        void Connect();
        void Disconnect();
        void Start();
        void Stop();
        bool CanStart();
        bool CanStop();
        object CreateUI();
    }
}
