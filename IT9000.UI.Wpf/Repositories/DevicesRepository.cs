using IT9000.Shared.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace IT9000.UI.Wpf.Repositories
{
    public class DevicesRepository
    {
        private readonly PluginsRepository _pluginsRepository;
        public DevicesRepository(PluginsRepository pluginsRepository)
        { _pluginsRepository = pluginsRepository; }
        private Dispatcher _Dispatcher => Application.Current.Dispatcher;
        public ObservableCollection<Device> OnlineDevices { get; } = new();
        public ObservableCollection<Device> OfflineDevices { get; } = new();
        private void SortDevices(ObservableCollection<Device> devices)
        {
            List<Device> sortedDevices = devices.OrderBy(item => item.Info.Index).ToList();
            _Dispatcher.Invoke(() =>
            {
                for(int i=0;i<sortedDevices.Count();++i)
                { devices.Move(devices.IndexOf(sortedDevices[i]), i); }
            });
        }
        public Device SetDeviceOnline(Device device)
        {
            if(OfflineDevices.Contains(device))
            { _Dispatcher.BeginInvoke(() => OfflineDevices.Remove(device)); }
            if(!OnlineDevices.Contains(device))
            { _Dispatcher.BeginInvoke(() => OnlineDevices.Add(device)); }
            SortDevices(OfflineDevices);
            SortDevices(OnlineDevices);

            IDevicePanel devicePanel = _pluginsRepository
                .GetDevicePanelFactory(device.Info.Model).CreateDevicePanel(device.Info);
            device.Panel = devicePanel;
            return device;
            
        }
        public Device SetDeviceOffline(Device device)
        {
            if(OnlineDevices.Contains(device))
            { _Dispatcher.BeginInvoke(() => OnlineDevices.Remove(device)); }
            if(!OfflineDevices.Contains(device))
            { _Dispatcher.BeginInvoke(() => OfflineDevices.Add(device)); }
            SortDevices(OnlineDevices);
            SortDevices(OfflineDevices);
            device.Panel?.Disconnect();
            return device;
        }

    }
}
