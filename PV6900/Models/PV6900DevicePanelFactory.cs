using IT9000.Shared.Models;
using IT9000.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using PV6900.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PV6900.Models
{
    public class PV6900DevicePanelFactory
    {
        private readonly IServiceCollection _serivces = new ServiceCollection();
        public IServiceProvider ServiceProvider {get;private set;} = null!;
        private void ConfigureServices(IServiceCollection services)
        {

        }

        public void Initialize()
        {
            ConfigureServices(_serivces);
            ServiceProvider = _serivces.BuildServiceProvider();
        }

        IDevicePanel CreateDevicePanel(DeviceInfo deviceInfo)
        {
            IServiceScope serviceScope = ServiceProvider.CreateScope();
            DeviceInfoWrapService deviceInfoWrapService = 
                serviceScope.ServiceProvider.GetRequiredService<DeviceInfoWrapService>();
            deviceInfoWrapService.Set(deviceInfo);
            PV6900DevicePanel devicePanel = serviceScope.ServiceProvider
                .GetRequiredService<PV6900DevicePanel>();
            devicePanel.ServiceScope = serviceScope;
            return devicePanel;
        }
    }
}