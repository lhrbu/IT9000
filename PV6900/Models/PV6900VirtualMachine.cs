using IT9000.Shared.Models;
using IT9000.Shared.Services;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PV6900.Models
{
    public class PV6900VirtualMachine
    {
        private readonly IIteInteropService _iteInteropService;
        private byte[] _buffer = new byte[255];
        private byte[] RentBuffer()
        {
            Array.Clear(_buffer, 0, 255);
            return _buffer;
        }
        public DeviceInfo DeviceInfo { get; }
        public PV6900VirtualMachine(
            IIteInteropService iteInteropService,
            DeviceInfo deviceInfo)
        {
            _iteInteropService = iteInteropService;
            DeviceInfo = deviceInfo;
        }
        public double SettingVolta
        {
            get
            {
                _iteInteropService.WaitHandle.WaitOne();
                byte[] settingVoltaBuffer = RentBuffer();
                _iteInteropService.IteDC_ReadCmd(DeviceInfo.Address, "VOLT?", settingVoltaBuffer);
                double settingVolta = double.Parse(Encoding.Default.GetString(settingVoltaBuffer).Trim(char.MinValue));
                _iteInteropService.WaitHandle.Set();
                return settingVolta;
            }
        }
        public double SettingAmpere
        {
            get
            {
                _iteInteropService.WaitHandle.WaitOne();
                byte[] settingAmpereBuffer = RentBuffer();
                _iteInteropService.IteDC_ReadCmd(DeviceInfo.Address, "CURR?", settingAmpereBuffer);
                double settingAmpere = double.Parse(Encoding.Default.GetString(settingAmpereBuffer).Trim(char.MinValue));
                _iteInteropService.WaitHandle.Set();
                return settingAmpere;
            }
        }
        public double Volta
        {
            get
            {
                _iteInteropService.WaitHandle.WaitOne();
                byte[] voltaBuffer = RentBuffer();
                _iteInteropService.IteDMM_GetMeasureVoltage(DeviceInfo.Address, string.Empty, voltaBuffer);
                double volta = double.Parse(Encoding.Default.GetString(voltaBuffer).Trim(char.MinValue));
                _iteInteropService.WaitHandle.Set();
                return volta;
            }
        }
        public double Ampere
        {
            get
            {
                _iteInteropService.WaitHandle.WaitOne();
                byte[] ampereBuffer = RentBuffer();
                _iteInteropService.IteDMM_GetMeasureCurrent(DeviceInfo.Address, string.Empty, ampereBuffer);
                double ampere = double.Parse(Encoding.Default.GetString(ampereBuffer).Trim(char.MinValue));
                _iteInteropService.WaitHandle.Set();
                return ampere;
            }
        }
        public TimeSpan CurrentStepRunningTime => _stepRunningTimeStopwatch.Elapsed;
        


        public async ValueTask ExecuteAsync(ProgramStep instruct)
        {
            _stepRunningTimeStopwatch.Restart();
            ExecuteInternal(instruct);
            long durationMilliseconds = (long)instruct.Duration * 1000;
            long timeEnd = _stepRunningTimeStopwatch.ElapsedMilliseconds;
            int waitTime = (int)(durationMilliseconds - timeEnd);
            if (waitTime > 0)
            { await Task.Delay(waitTime, _cancellationTokenSource.Token);}
        }
        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Token.WaitHandle.WaitOne();
            _stepRunningTimeStopwatch.Stop();
            ExecuteInternal(new ProgramStep{ Ampere = 0, Volta = 0, Duration = 0 });
            _cancellationTokenSource = new();
        }

        private void ExecuteInternal(ProgramStep instruct)
        {
            _iteInteropService.WaitHandle.WaitOne();
            int errNo = _iteInteropService.IteDC_WriteCmd(DeviceInfo.Address, $"VOLT {instruct.Volta}");
            errNo = _iteInteropService.IteDC_WriteCmd(DeviceInfo.Address, $"CURR {instruct.Ampere}");
            _iteInteropService.WaitHandle.Set();
        }
        private Stopwatch _stepRunningTimeStopwatch = Stopwatch.StartNew();
        private CancellationTokenSource _cancellationTokenSource=null!;
    }
}
