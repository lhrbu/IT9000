using IT9000.Shared.Models;
using IT9000.Shared.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PV6900.Services;
using Microsoft.Extensions.DependencyInjection;
using PV6900.Models;

namespace PV6900.UI.Wpf
{
    public class PV6900DevicePanel : IDevicePanel, IDisposable
    {
        private readonly IIteInteropService _iteInteropService;
        private readonly DeviceInfo _deviceInfo;
        private readonly ManagedProgramInterpreter _interpreter;
        public PV6900DevicePanel(
            IIteInteropService iteInteropService,
            DeviceInfoBoxService deviceInfoBoxService,
            ManagedProgramInterpreter interpreter)
        {
            _iteInteropService = iteInteropService;
            _deviceInfo = deviceInfoBoxService.Unbox()!;
            _interpreter = interpreter;
        }

        public ManagedProgram ManagedProgram { get; } = new();
        public void Connect()
        {
            _iteInteropService.WaitHandle.WaitOne();
            _iteInteropService.IteDC_WriteCmd(_deviceInfo.Address, "SYST:REM");
            _iteInteropService.ItePow_SetOutputState(_deviceInfo.Address, "1");
            _iteInteropService.WaitHandle.Set();
        }
        public void Disconnect()
        {
            _interpreter.Stop();
            _iteInteropService.WaitHandle.WaitOne();
            _iteInteropService.ItePow_RemoteMode(_deviceInfo.Address);
            _iteInteropService.ItePow_SetOutputState(_deviceInfo.Address, "0");
            _iteInteropService.WaitHandle.Set();
        }
        public void Start() => _interpreter.ExecuteManagedProgramAsync(ManagedProgram).ConfigureAwait(false);
        public void Stop() => _interpreter.Stop();
        public bool CanStart() => !_interpreter.InRunning;
        public bool CanStop() => _interpreter.InRunning;

        public IServiceScope ServiceScope { get; set; } = null!;
        public void Dispose()
        {
            ServiceScope.Dispose();
            GC.SuppressFinalize(this);
        }

        public object CreateUI() => ServiceScope.ServiceProvider
            .GetRequiredService<PV6900UIElement>();
    }
}
