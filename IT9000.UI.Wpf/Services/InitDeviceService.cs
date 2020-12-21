using IT9000.Shared.Models;
using IT9000.Shared.Services;
using IT9000.UI.Wpf.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT9000.UI.Wpf.Services
{
    public class InitDeviceService
    {
        private readonly IIteInteropService _iteInteropService;
        private readonly DevicesRepository _devicesRepository;
        private readonly PluginsRepository _pluginsRepository;
        public InitDeviceService(
            IIteInteropService iteInteropService,
            DevicesRepository devicesRepository,
            PluginsRepository pluginsRepository)
        { 
            _iteInteropService = iteInteropService;
            _devicesRepository = devicesRepository;
            _pluginsRepository = pluginsRepository;
        }

        public void LoadDevices()
        {
            int deviceInfosCount = 0;
            int[] deviceAddressesBuffer = new int[255];
            int errNo = _iteInteropService.SystemTest(deviceAddressesBuffer, ref deviceInfosCount);

            List<int> deviceAddresses = deviceAddressesBuffer.Take(deviceInfosCount).ToList();
            List<string> deviceNames = Enumerable.Range(0, deviceInfosCount)
                .Select(index =>
                {
                    byte[] deviceNameBuffer = new byte[255];
                    _iteInteropService.GetDeviceName(deviceAddresses[index], deviceNameBuffer);
                    return Encoding.Default.GetString(deviceNameBuffer).Trim(char.MinValue);
                }).ToList();
            int usbDevicesCount = 0;
            byte[] usbAddressesBuffer = new byte[1024 * 100];
            _iteInteropService.IteDC_GetUsb(usbAddressesBuffer, ref usbDevicesCount);
            List<string> usbAddresses = Enumerable.Range(0, usbDevicesCount)
                .Select(index => Encoding.Default.GetString(usbAddressesBuffer, index * 100, 100)
                .Trim(char.MinValue)).ToList();


            IEnumerable<DeviceInfo> ZipDeviceInfos(List<string> deviceNames,
                List<int> deviceAddresses, List<string> usbAddresses)
            {
                IEnumerator<string> deviceNameItr = deviceNames.GetEnumerator();
                IEnumerator<int> deviceAddressItr = deviceAddresses.GetEnumerator();
                IEnumerator<string> usbAddressItr = usbAddresses.GetEnumerator();
                while (deviceNameItr.MoveNext() &&
                   deviceAddressItr.MoveNext() &&
                   usbAddressItr.MoveNext())
                {
                    yield return new DeviceInfo
                    {
                        Name = deviceNameItr.Current,
                        Address = deviceAddressItr.Current,
                        InterfaceType = "USB",
                        InterfaceParameter = usbAddressItr.Current
                    };
                }
            }

            IEnumerable<DeviceInfo> deviceInfos = ZipDeviceInfos(deviceNames, deviceAddresses, usbAddresses);

            List<ConfiguredTaskAwaitable> taskAwaitables = new(deviceInfosCount);
            foreach (DeviceInfo deviceInfo in deviceInfos.GroupBy(item => item.Model).Select(pair => pair.AsEnumerable().First()))
            {
                ConfiguredTaskAwaitable taskAwaitable = Task.Run(() =>
                {
                    try
                    {
                        _pluginsRepository.LoadDependency(deviceInfo.Model);
                        _pluginsRepository.Load(deviceInfo.Model);
                    }
                    catch { MessageBox.Show($"{deviceInfo.Model}' plugin is not loaded yet!"); }
                }).ConfigureAwait(false);
                taskAwaitables.Add(taskAwaitable);
            }
            foreach(DeviceInfo deviceInfo in deviceInfos)
            {
                _devicesRepository.OfflineDevices.Add(new Device { Info = deviceInfo });
            }

            foreach (ConfiguredTaskAwaitable awaitable in taskAwaitables)
            { awaitable.GetAwaiter().GetResult(); }
        }

        
    }
}
