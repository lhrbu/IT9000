using IT9000.Shared.Models;
using IT9000.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT9000.UI.Wpf.Services
{
    public class DeviceInfoDetectService
    {
        private IIteInteropService _iteInteropService;
        public DeviceInfoDetectService(
            IIteInteropService iteInteropService)
        { _iteInteropService = iteInteropService; }

        public IEnumerable<DeviceInfo> GetDeviceInfos()
        {
            int devicesCount = 0;
            int[] deviceAddressesBuffer = new int[255];
            int errNo = _iteInteropService.SystemTest(deviceAddressesBuffer, ref devicesCount);

            List<int> deviceAddresses = deviceAddressesBuffer.Take(devicesCount).ToList();
            List<string> deviceNames = Enumerable.Range(0, devicesCount)
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
    }
}
