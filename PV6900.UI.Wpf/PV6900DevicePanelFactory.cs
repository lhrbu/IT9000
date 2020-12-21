using IT9000.Shared.Models;
using IT9000.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using PV6900.Models;
using PV6900.Services;
using PV6900.UI.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PV6900.UI.Wpf
{
    public class PV6900DevicePanelFactory : IDevicePanelFactory
    {
        private readonly IServiceCollection _serivces = new ServiceCollection();
        private IServiceProvider _serviceProvider  = null!;
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<PV6900UIElement>();

            services.AddScoped<MonitorControllerVM>();
            services.AddScoped<PV6900VirtualMachine>();
            services.AddScoped<ManagedProgramInterpreter>();
            services.AddScoped<PV6900DevicePanel>();
            services.AddScoped<DeviceInfoBoxService>();
            services.AddScoped<TimeSpanAmpereChartVM>();
            services.AddScoped<TimeSpanVoltaChartVM>();

            services.AddSingleton<IIteInteropService, MockIterInteropService>();
        }

        public void Initialize()
        {
            ConfigureServices(_serivces);
            _serviceProvider = _serivces.BuildServiceProvider();

        }

        public IDevicePanel CreateDevicePanel(DeviceInfo deviceInfo)
        {
            IServiceScope serviceScope = _serviceProvider.CreateScope();
            DeviceInfoBoxService deviceInfoWrapService =
                serviceScope.ServiceProvider.GetRequiredService<DeviceInfoBoxService>();
            deviceInfoWrapService.Box(deviceInfo);
            PV6900DevicePanel devicePanel = serviceScope.ServiceProvider
                .GetRequiredService<PV6900DevicePanel>();
            devicePanel.ServiceScope = serviceScope;
            return devicePanel;
        }
    }
}