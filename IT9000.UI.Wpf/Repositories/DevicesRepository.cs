using IT9000.Shared.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace IT9000.UI.Wpf.Repositories
{
    public class DevicesRepository
    {
        private Dispatcher _Dispatcher => Application.Current.Dispatcher;
        private ConcurrentDictionary<string, IDevicePanel> OnlineDeviceNamePanels { get; } =  new();

        public ObservableCollection<DeviceInfo> OnlineDeviceInfos { get; } = new();
        public ObservableCollection<DeviceInfo> OfflineDeviceInfos { get; } = new();
        private void SortDeviceInfos(ObservableCollection<DeviceInfo> deviceInfos)
        {
            List<DeviceInfo> sortedDevices = deviceInfos.OrderBy(item => item.Index).ToList();
            Application.Current.Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < sortedDevices.Count(); ++i)
                { deviceInfos.Move(deviceInfos.IndexOf(sortedDevices[i]), i); }
            });
        }

        public void DeviceOnline(DeviceInfo deviceInfo, IDevicePanel devicePanel)
        {
            if (OfflineDeviceInfos.Contains(deviceInfo))
            { _Dispatcher.Invoke(() => OfflineDeviceInfos.Remove(deviceInfo)); }
            if (!OnlineDeviceInfos.Contains(deviceInfo))
            { _Dispatcher.Invoke(() => OnlineDeviceInfos.Add(deviceInfo)); }

            SortDeviceInfos(OfflineDeviceInfos);
            SortDeviceInfos(OnlineDeviceInfos);

            OnlineDeviceNamePanels.TryAdd(deviceInfo.Name, devicePanel);
        }

        public void DeviceOffline(DeviceInfo deviceInfo)
        {
            
            if (OnlineDeviceInfos.Contains(deviceInfo))
            { _Dispatcher.Invoke(() => OnlineDeviceInfos.Remove(deviceInfo)); }
            if (!OfflineDeviceInfos.Contains(deviceInfo))
            { _Dispatcher.Invoke(() => OfflineDeviceInfos.Add(deviceInfo)); }
            SortDeviceInfos(OnlineDeviceInfos);
            SortDeviceInfos(OfflineDeviceInfos);
            if(OnlineDeviceNamePanels.TryRemove(deviceInfo.Name,out IDevicePanel? devicePanel))
            {
                devicePanel?.Disconnect();
            }
        }
    }
}
