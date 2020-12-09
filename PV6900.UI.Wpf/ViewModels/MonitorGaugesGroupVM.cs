using PV6900.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Threading;
using Prism.Commands;

namespace PV6900.UI.Wpf.ViewModels
{
    public class MonitorGaugesGroupVM:BindableBase
    {
        private readonly PV6900VirtualMachine _machine;
        public MonitorGaugesGroupVM(
            PV6900VirtualMachine machine)
        { 
            _machine = machine;
            StartMonitorCommand = new(() => StartMonitorAsync().ConfigureAwait(false));
            StopMonitorCommand = new(StopMonitor);
        }
        public DelegateCommand StartMonitorCommand { get; }
        public DelegateCommand StopMonitorCommand { get; }
        private int _interval = 100;
        private async Task StartMonitorAsync()
        {
            CancellationTokenSource localTokenSource = _monitorCancellationTokeSource;
            InMonitor = true;
            while (!localTokenSource.IsCancellationRequested)
            {
                Volta = _machine.Volta;
                Ampere = _machine.Ampere;
                SettingVolta = _machine.SettingVolta;
                SettingAmpere = _machine.SettingAmpere;
                await Task.Delay(_interval, localTokenSource.Token);
            }
        }
        private void StopMonitor()
        {
            _monitorCancellationTokeSource.Cancel();
            _monitorCancellationTokeSource.Token.WaitHandle.WaitOne();
            _monitorCancellationTokeSource = new();
            InMonitor = false;
        }
        private CancellationTokenSource _monitorCancellationTokeSource = new();

        public double Volta { get => _volta; set => SetProperty(ref _volta, value); }
        private double _volta;
        public double Ampere { get => _ampere; set => SetProperty(ref _ampere, value); }
        private double _ampere;
        public double SettingVolta { get => _settingVolta; set => SetProperty(ref _settingVolta, value); }
        private double _settingVolta;
        public double SettingAmpere { get => _settingAmpere; set => SetProperty(ref _settingAmpere, value); }
        private double _settingAmpere;
        public bool InMonitor { get => _inMonitor; set => SetProperty(ref _inMonitor, value); }
        private bool _inMonitor = false;
    }
}
